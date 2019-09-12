
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sampekey.Interface;
using Sampekey.Model.Identity;
using Sampekey.Contex;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount accountRepo;
        private readonly IUser userRepo;
        private readonly IRole roleRepo;
        private readonly ISystemAlert systemAlertRepo;
        public AccountController(
            IAccount _accountRepo,
            IUser _userRepo,
            IRole _roleRepo,
            ISystemAlert _systemAlertRepo
        )
        {
            accountRepo = _accountRepo;
            userRepo = _userRepo;
            roleRepo = _roleRepo;
            systemAlertRepo = _systemAlertRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> Get()
        {
            return Ok(userRepo.GetAllUsers());
        }

        [HttpGet]
        [Route("V1/GetUsersWithActiveDirectory")]
        public HashSet<string> GetUsersWithActiveDirectory([FromBody] SampekeyUserAccountRequest userAccountRequest)
        {
            return accountRepo.GetUsersWithActiveDirectory(userAccountRequest);
        }

        [HttpPost]
        [Route("V1/LoginWithActiveDirectory")]
        public async Task<ActionResult<User>> LoginWithActiveDirectory([FromBody] SampekeyUserAccountRequest userAccountRequest)
        {
            if (ModelState.IsValid && accountRepo.LoginWithActiveDirectory(userAccountRequest))
            {
                var user_found = await userRepo.FindUserByUserName(userAccountRequest);
                if (user_found != null)
                {
                    userAccountRequest.Email = user_found.Email;
                    await accountRepo.UpdateForcePaswordAsync(userAccountRequest);
                    return Ok(new
                    {
                        User = user_found,
                        Token = SampekeyParams.CreateToken(userAccountRequest)
                    });
                }
                else
                {
                    userAccountRequest.Email = $"{userAccountRequest.UserName}@{Environment.GetEnvironmentVariable("AD_DDOMAIN")}";
                    if ((await userRepo.CreateUser(userAccountRequest)).Succeeded)
                    {
                        var new_user = await userRepo.FindUserByUserName(userAccountRequest);
                        if ((await userRepo.AddDefaultRoleToUser(new_user)).Succeeded)
                        {
                            return Ok(new
                            {
                                User = new_user,
                                Roles = userRepo.GetRolesFromUser(user_found),
                                Token = SampekeyParams.CreateToken(userAccountRequest)
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
                return Unauthorized(systemAlertRepo.GetUnauthorizedMenssageFromActiveDirectory());
            }
        }

        [HttpPost]
        [Route("V1/LoginWithSampeKey")]
        public async Task<ActionResult<User>> LoginWithSampeKey([FromBody] SampekeyUserAccountRequest userAccountRequest)
        {
            var user_found = await userRepo.FindUserByUserName(userAccountRequest);

            if (ModelState.IsValid && user_found != null)
            {
                if ((await accountRepo.LoginWithSampeKey(userAccountRequest)).Succeeded)
                {
                    return Ok(new
                    {
                        User = user_found,
                        Token = SampekeyParams.CreateToken(userAccountRequest)
                    });
                }
                else
                {
                    return Unauthorized(systemAlertRepo.GetUnauthorizedMenssage());
                }
            }
            else
            {
                return Unauthorized(systemAlertRepo.GetUnauthorizedMenssage());
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

        [HttpPost]
        [Route("V1/GeneratePermanentToken")]
        public ActionResult<User> GeneratePermanentToken([FromBody] SampekeyUserAccountRequest userAccountRequest)
        {
            if (ModelState.IsValid)
            {
                return Ok(new
                {
                    Token = SampekeyParams.CreateToken(userAccountRequest)
                });
            }
            else
            {
                return Unauthorized(systemAlertRepo.GetUnauthorizedMenssage());
            }
        }

    }
}
