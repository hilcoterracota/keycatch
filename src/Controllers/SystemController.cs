
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
    public class SystemController : ControllerBase
    {
        private readonly ISystem system;
        private readonly ILogger logger;
        public SystemController(ISystem _system, ILogger<SystemController> _logger)
        {
            system = _system;
            logger = _logger;
        }

        [HttpGet]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<Castle>), (int)HttpStatusCode.OK)]
        public ActionResult<Task<IEnumerable<Castle>>> GetAllCastles()
        {
            Task<IEnumerable<Castle>> data = system.GetAllCastles();
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpGet]
        [Route("V1/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(Castle), (int)HttpStatusCode.OK)]
        public ActionResult<Castle> FindCastleById(string id)
        {
            Task<Castle> data = system.FindCastleById(id);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpPost]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Castle), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Castle> AddCastle([FromBody] Castle value)
        {
            Task<Castle> data = system.AddCastle(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpPut]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Castle), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Castle> UpdateCastle([FromBody] Castle value)
        {
            Task<Castle> data = system.UpdateCastle(value);
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
        public ActionResult<bool> DeleteCastle([FromBody] Castle value)
        {
            Task<bool> data = system.DeleteCastle(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else return Ok(data.Result);
        }

    }
}
