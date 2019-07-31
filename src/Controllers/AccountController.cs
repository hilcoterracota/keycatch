
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using keycatch.Interfaces;
using Sampekey.Contex;
using Sampekey.Model;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo accountRepo;
        private readonly IUserRepo userRepo;
        private readonly ISystemRepo systemRepo;
        public AccountController(
            IAccountRepo _accountRepo,
            IUserRepo _userRepo,
            ISystemRepo _systemRepo
        )
        {
            accountRepo = _accountRepo;
            userRepo = _userRepo;
            systemRepo = _systemRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> Get()
        {
            return Ok(userRepo.GetAllUsers());
        }

        [HttpPost]
        [Route("V1/LoginWithCnsfActiveDirectory")]
        public async Task<ActionResult<User>> LoginWithCnsfActiveDirectory([FromBody] SampekeyUserAccountRequest userAccountRequest)
        {
            if (ModelState.IsValid && accountRepo.LoginCnsfWithActiveDirectory(userAccountRequest))
            {
                var user_found = await userRepo.FindUserByUserName(userAccountRequest);
                if (user_found != null)
                {
                    return Ok(user_found);
                }else
                {
                    return Unauthorized(systemRepo.GetUnauthorizedMenssageFromSampeKey());
                }
            }
            else
            {
                return Unauthorized(systemRepo.GetUnauthorizedMenssageFromCnsfActiveDirectory());
            }
        }

    }
}
