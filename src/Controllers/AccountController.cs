
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sampekey.Contex;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {


        private readonly SampekeyDbContex _contex;
        private readonly ISampeKeyAccount _accountRepository;
        public AccountController(
            SampekeyDbContex contex,
            ISampeKeyAccount accountRepository
        )
        {
            _contex = contex;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> Get()
        {
            return _contex.User.ToList();
        }


        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<object>> Login([FromBody] SampekeyUserAccountRequest userAccountRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.LoginAccount(userAccountRequest);
                if (result.Succeeded)
                {
					return Ok(result);
                }
                else
                {
                    return BadRequest(new{
                        ModelState = ModelState,
                        UserAccountRequest = userAccountRequest
                    });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
