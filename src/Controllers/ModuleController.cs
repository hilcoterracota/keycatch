
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
    public class ModuleController : ControllerBase
    {
        private readonly IModule module;
        private readonly ILogger logger;
        public ModuleController(IModule _module, ILogger<ModuleController> _logger)
        {
            module = _module;
            logger = _logger;
        }

        [HttpGet]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<Land>), (int)HttpStatusCode.OK)]
        public ActionResult<Task<IEnumerable<Land>>> GetAllLands()
        {
            Task<IEnumerable<Land>> data = module.GetAllLands();
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpGet]
        [Route("V1/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(Land), (int)HttpStatusCode.OK)]
        public ActionResult<Land> FindLandById(string id)
        {
            Task<Land> data = module.FindLandById(id);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpPost]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Land), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Land> AddLand([FromBody] Land value)
        {
            Task<Land> data = module.AddLand(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpPut]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Land), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Land> UpdateLand([FromBody] Land value)
        {
            Task<Land> data = module.UpdateLand(value);
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
        public ActionResult<bool> DeleteLand([FromBody] Land value)
        {
            Task<bool> data = module.DeleteLand(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else return Ok(data.Result);
        }

    }
}
