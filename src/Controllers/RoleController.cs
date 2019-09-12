
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sampekey.Interface;
using Sampekey.Model.Identity;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ISystemAlert systemAlertRepo;
        private readonly IRole roleRepo;
        public RoleController(
            ISystemAlert _systemAlertRepo,
            IRole _roleRepo
        )
        {
            systemAlertRepo = _systemAlertRepo;
            roleRepo = _roleRepo;
        }

        [HttpGet]
        [Route("V1")]
        public async Task<ActionResult<Role>> GetRoles()
        {
            return Ok(await roleRepo.GetRoles());
        }


        [HttpPost]
        [Route("V1/CreateRole")]
        public async Task<ActionResult<Role>> CreateRole([FromBody] Role role)
        {
            if (ModelState.IsValid)
            {
                if ((await roleRepo.CreateRole(role)).Succeeded)
                {
                    return Ok((await roleRepo.FindRoleByName(role.Name)));
                }
                else
                {
                    return ValidationProblem();
                }
            }
            else
            {
                return Unauthorized(systemAlertRepo.GetUnauthorizedMenssageFromActiveDirectory());
            }
        }

        [HttpPost]
        [Route("V1/AddClaimToRole")]
        public async Task<ActionResult<Role>> AddClaimToRole([FromBody] RoleClaim roleClaim)
        {
            if (ModelState.IsValid)
            {
                if ((await roleRepo.AddClaimAsyncToRole(await roleRepo.FindRoleById(roleClaim.RoleId), roleClaim.ToClaim())).Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return ValidationProblem();
            }
        }
    }
}
