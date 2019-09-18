
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sampekey.Interface;
using Sampekey.Model.Administration;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermission permission;
        private readonly ILogger logger;
        public PermissionController(IPermission _permission, ILogger<PermissionController> _logger)
        {
            permission = _permission;
            logger = _logger;
        }

        [HttpGet]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<Permission>), (int)HttpStatusCode.OK)]
        public ActionResult<Task<IEnumerable<Permission>>> GetAllPermissions()
        {
            Task<IEnumerable<Permission>> data = permission.GetAllPermissions();
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpGet]
        [Route("V1/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(Permission), (int)HttpStatusCode.OK)]
        public ActionResult<Permission> FindPermissionById(string id)
        {
            Task<Permission> data = permission.FindPermissionById(id);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpPost]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Permission), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Permission> AddPermission([FromBody] Permission value)
        {
            Task<Permission> data = permission.AddPermission(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpPut]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Permission), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Permission> UpdatePermission([FromBody] Permission value)
        {
            Task<Permission> data = permission.UpdatePermission(value);
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
        public ActionResult<bool> DeletePermission([FromBody] Permission value)
        {
            Task<bool> data = permission.DeletePermission(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else return Ok(data.Result);
        }

    }
}
