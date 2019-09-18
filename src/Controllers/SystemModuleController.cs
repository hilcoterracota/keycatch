
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
    public class SystemModuleController : ControllerBase
    {
        private readonly ISystemModule systemModule;
        private readonly ILogger logger;
        public SystemModuleController(ISystemModule _systemModule, ILogger<SystemModuleController> _logger)
        {
            systemModule = _systemModule;
            logger = _logger;
        }

        [HttpGet]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<CastleLand>), (int)HttpStatusCode.OK)]
        public ActionResult<Task<IEnumerable<CastleLand>>> GetAllCastleLands()
        {
            Task<IEnumerable<CastleLand>> data = systemModule.GetAllCastleLands();
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpGet]
        [Route("V1/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(CastleLand), (int)HttpStatusCode.OK)]
        public ActionResult<CastleLand> FindCastleLandById(string id)
        {
            Task<CastleLand> data = systemModule.FindCastleLandById(id);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpPost]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(CastleLand), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<CastleLand> AddCastleLand([FromBody] CastleLand value)
        {
            Task<CastleLand> data = systemModule.AddCastleLand(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpPut]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(CastleLand), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<CastleLand> UpdateCastleLand([FromBody] CastleLand value)
        {
            Task<CastleLand> data = systemModule.UpdateCastleLand(value);
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
        public ActionResult<bool> DeleteCastleLand([FromBody] CastleLand value)
        {
            Task<bool> data = systemModule.DeleteCastleLand(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else return Ok(data.Result);
        }

    }
}
