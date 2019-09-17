
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sampekey.Interface;
using Sampekey.Model.Configuration.Module;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EsrController : ControllerBase
    {
        private readonly IEsrp esrp;
        private readonly ILogger logger;

        public EsrController(IEsrp _esrp,ILogger<EsrController> _logger)
        {
            esrp = _esrp;
            logger = _logger;
        }

        [HttpGet]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<KingdomCastleRolePermission>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<KingdomCastleRolePermission>> GetAllKingdomCastleRolePermissionsAsync()
        {
            Task<IEnumerable<KingdomCastleRolePermission>> data = esrp.GetAllKingdomCastleRolePermissions();
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpGet]
        [Route("V1/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(Kingdom), (int)HttpStatusCode.OK)]
        public ActionResult<KingdomCastleRolePermission> FindKingdomCastleRolePermissionById(string id)
        {
            Task<KingdomCastleRolePermission> data = esrp.FindKingdomCastleRolePermissionById(id);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpPost]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Kingdom), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<KingdomCastleRolePermission> AddKingdomCastleRolePermission([FromBody] KingdomCastleRolePermission value)
        {
            Task<KingdomCastleRolePermission> data = esrp.AddKingdomCastleRolePermission(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpPut]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Kingdom), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<KingdomCastleRolePermission> UpdateKingdomCastleRolePermission([FromBody] KingdomCastleRolePermission value)
        {
            Task<KingdomCastleRolePermission> data = esrp.UpdateKingdomCastleRolePermission(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpDelete]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<bool> DeleteKingdomCastleRolePermission([FromBody] KingdomCastleRolePermission value)
        {
            Task<bool> data = esrp.DeleteKingdomCastleRolePermission(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else return Ok(data.Result);
        }

    }
}
