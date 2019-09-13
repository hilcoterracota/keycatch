
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
        private readonly IEsrp kingdomCastleRolePermission;

        public EsrController(IEsrp _kingdomCastleRolePermission)
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
        [Route("V1/{id}:string")]
        public ActionResult<KingdomCastleRolePermission> FindKingdomCastleRolePermissionById(string id)
        {
            return Ok(kingdomCastleRolePermission.FindKingdomCastleRolePermissionById(id));
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
