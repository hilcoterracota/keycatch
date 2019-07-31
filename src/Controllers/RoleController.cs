
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using keycatch.Interfaces;
using Sampekey.Model;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ISystemRepo systemRepo;
        private readonly IRoleRepo roleRepo;
        public RoleController(
            ISystemRepo _systemRepo,
            IRoleRepo _roleRepo
        )
        {
            systemRepo = _systemRepo;
            roleRepo = _roleRepo;
        }

        [HttpGet]
        [Route("V1/GetRoles")]
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
                return Unauthorized(systemRepo.GetUnauthorizedMenssageFromActiveDirectory());
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
