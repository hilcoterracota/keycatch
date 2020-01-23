
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sampekey.Interface;
using Sampekey.Model.Configuration.Breakers;
using Sampekey.Model.Configuration.Quid;

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
        [ProducesResponseType(typeof(IEnumerable<EnviromentProjectRolePermission>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<EnviromentProjectRolePermission>> GetAllEnviromentProjectRolePermissionsAsync()
        {
            Task<IEnumerable<EnviromentProjectRolePermission>> data = esrp.GetAllEnviromentProjectRolePermissions();
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpGet]
        [Route("V1/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(Enviroment), (int)HttpStatusCode.OK)]
        public ActionResult<EnviromentProjectRolePermission> FindEnviromentProjectRolePermissionById(string id)
        {
            Task<EnviromentProjectRolePermission> data = esrp.FindEnviromentProjectRolePermissionById(id);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpPost]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Enviroment), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<EnviromentProjectRolePermission> AddEnviromentProjectRolePermission([FromBody] EnviromentProjectRolePermission value)
        {
            Task<EnviromentProjectRolePermission> data = esrp.AddEnviromentProjectRolePermission(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpPut]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Enviroment), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<EnviromentProjectRolePermission> UpdateEnviromentProjectRolePermission([FromBody] EnviromentProjectRolePermission value)
        {
            Task<EnviromentProjectRolePermission> data = esrp.UpdateEnviromentProjectRolePermission(value);
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
        public ActionResult<bool> DeleteEnviromentProjectRolePermission([FromBody] EnviromentProjectRolePermission value)
        {
            Task<bool> data = esrp.DeleteEnviromentProjectRolePermission(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else return Ok(data.Result);
        }

    }
}
