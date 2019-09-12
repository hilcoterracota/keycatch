
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sampekey.Interface;
using Sampekey.Model.Configuration.Module;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EsrController : ControllerBase
    {
        private readonly IKingdomCastleRolePermission kingdomCastleRolePermission;

        public EsrController(IKingdomCastleRolePermission _kingdomCastleRolePermission)
        {
            kingdomCastleRolePermission = _kingdomCastleRolePermission;
        }

        [HttpGet]
        [Route("V1")]
        public ActionResult<IEnumerable<KingdomCastleRolePermission>> GetAllKingdomCastleRolePermissions()
        {
            return Ok(kingdomCastleRolePermission.GetAllKingdomCastleRolePermissions());
        }

        [HttpGet]
        [Route("V1/search")]
        public ActionResult<KingdomCastleRolePermission> FindKingdomCastleRolePermissionById([FromBody] KingdomCastleRolePermission value)
        {
            return Ok(kingdomCastleRolePermission.FindKingdomCastleRolePermissionById(value));
        }

        [HttpPost]
        [Route("V1")]
        public ActionResult<KingdomCastleRolePermission> AddKingdomCastleRolePermission([FromBody] KingdomCastleRolePermission value)
        {
            return Ok(kingdomCastleRolePermission.AddKingdomCastleRolePermission(value));
        }

        [HttpPut]
        [Route("V1")]
        public ActionResult<KingdomCastleRolePermission> UpdateKingdomCastleRolePermission([FromBody] KingdomCastleRolePermission value)
        {
            return Ok(kingdomCastleRolePermission.UpdateKingdomCastleRolePermission(value));
        }

        [HttpDelete]
        [Route("V1")]
        public ActionResult<bool> DeleteKingdomCastleRolePermission([FromBody] KingdomCastleRolePermission value)
        {
            return Ok(kingdomCastleRolePermission.DeleteKingdomCastleRolePermission(value));
        }

    }
}
