
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
        private readonly IRoleRepo roleRepo;
        private readonly ISystemRepo systemRepo;
        private readonly ISampeKeyAccount sampeKeyAccount;
        public AccountController(
            IAccountRepo _accountRepo,
            IUserRepo _userRepo,
            IRoleRepo _roleRepo,
            ISystemRepo _systemRepo,
            ISampeKeyAccount _sampeKeyAccount
        )
        {
            accountRepo = _accountRepo;
            userRepo = _userRepo;
            roleRepo = _roleRepo;
            systemRepo = _systemRepo;
            sampeKeyAccount = _sampeKeyAccount;
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
                    userAccountRequest.Email = user_found.Email;
                    await accountRepo.UpdateForcePaswordAsync(userAccountRequest);
                    return Ok(new
                    {
                        User = user_found,
                        Roles = await userRepo.GetRolesFromUser(user_found),
                        Claims = await userRepo.GetClaimsFromUser(user_found),
                        Token = sampeKeyAccount.CreateToken(userAccountRequest)
                    });
                }
                else
                {
                    userAccountRequest.Email = userAccountRequest.UserName+"@cnsf.gob.mx";
                    if ((await userRepo.CreateUser(userAccountRequest)).Succeeded)
                    {
                        var new_user = await userRepo.FindUserByUserName(userAccountRequest);
                        if ((await userRepo.AddDefaultRoleToUser(new_user)).Succeeded)
                        {
                            return Ok(new
                            {
                                User = new_user,
                                Roles = await userRepo.GetRolesFromUser(new_user),
                                Claims = await userRepo.GetClaimsFromUser(new_user),
                                Token = sampeKeyAccount.CreateToken(userAccountRequest)
                            });
                        }
                        else
                        {
                            return ValidationProblem();
                        }
                    }
                    else
                    {
                        return ValidationProblem();
                    }
                }
            }
            else
            {
                return Unauthorized(systemRepo.GetUnauthorizedMenssageFromCnsfActiveDirectory());
            }
        }

        [HttpPost]
        [Route("V1/LoginWithSampeKey")]
        public async Task<ActionResult<User>> LoginWithSampeKey([FromBody] SampekeyUserAccountRequest userAccountRequest)
        {
            var user_found = await userRepo.FindUserByUserName(userAccountRequest);

            if (ModelState.IsValid && user_found != null)
            {
                if ((await accountRepo.LoginCnsfWithSampeKey(userAccountRequest)).Succeeded)
                {
                    userAccountRequest.Email = user_found.Email;
                    return Ok(new
                    {
                        User = user_found,
                        Roles = await userRepo.GetRolesFromUser(user_found),
                        Claims = await userRepo.GetClaimsFromUser(user_found),
                        Token = sampeKeyAccount.CreateToken(userAccountRequest)
                    });
                }
                else
                {
                    return Unauthorized(systemRepo.GetUnauthorizedMenssage());
                }
            }
            else
            {
                return Unauthorized(systemRepo.GetUnauthorizedMenssage());
            }
        }

        [HttpPost]
        [Route("V1/CreateUser")]
        public async Task<ActionResult<User>> CreateUser([FromBody] SampekeyUserAccountRequest userAccountRequest)
        {
            if (ModelState.IsValid)
            {
                if ((await userRepo.CreateUser(userAccountRequest)).Succeeded)
                {
                    var new_user = await userRepo.FindUserByUserName(userAccountRequest);
                    if ((await userRepo.AddDefaultRoleToUser(new_user)).Succeeded)
                    {
                        return Ok(new_user);
                    }
                    else
                    {
                        return ValidationProblem();
                    }
                }
                else
                {
                    return ValidationProblem();
                }
            }
            else
            {
                return ValidationProblem();
            }
        }

    }
}
