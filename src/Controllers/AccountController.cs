
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using keycatch.Interfaces;
using Sampekey.Contex;
using Sampekey.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        public AccountController(
            IAccountRepo _accountRepo,
            IUserRepo _userRepo,
            IRoleRepo _roleRepo,
            ISystemRepo _systemRepo
        )
        {
            accountRepo = _accountRepo;
            userRepo = _userRepo;
            roleRepo = _roleRepo;
            systemRepo = _systemRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> Get()
        {
            return Ok(userRepo.GetAllUsers());
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

                    var roles_user = await userRepo.GetRolesFromUser(user_found);
                    
                    IList<object> claims_roles =  new List<object>();
                    foreach (var roleName in roles_user)
                    {
                        claims_roles.Add(await roleRepo.GetClaimsFromRole(await roleRepo.FindRoleByName(roleName)));
                    }

                    return Ok(new
                    {
                        User = user_found,
                        Roles = roles_user,
                        Claims = claims_roles,
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
                                Roles = await userRepo.GetRolesFromUser(new_user),
                                Claims = await userRepo.GetClaimsFromUser(new_user),
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
                return Unauthorized(systemRepo.GetUnauthorizedMenssageFromActiveDirectory());
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
                    userAccountRequest.Email = user_found.Email;
                    var roles_user = await userRepo.GetRolesFromUser(user_found);
                    
                    IList<object> claims_roles =  new List<object>();
                    foreach (var roleName in roles_user)
                    {
                        claims_roles.Add(await roleRepo.GetClaimsFromRole(await roleRepo.FindRoleByName(roleName)));
                    }

                    return Ok(new
                    {
                        User = user_found,
                        Roles = roles_user,
                        Claims = claims_roles,
                        Token = SampekeyParams.CreateToken(userAccountRequest)
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                return Unauthorized(systemRepo.GetUnauthorizedMenssage());
            }
        }

    }
}
