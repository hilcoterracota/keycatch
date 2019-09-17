
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sampekey.Interface;
using Sampekey.Model.Administration;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermission permission;

        public PermissionController(IPermission _permission)
        {
            permission = _permission;
        }

        [HttpGet]
        [Route("V1")]
        public ActionResult<IEnumerable<Permission>> GetAllPermissions()
        {
            return Ok(permission.GetAllPermissions());
        }

        [HttpGet]
        [Route("V1/{id}")]
        public ActionResult<Permission> FindPermissionById(string id)
        {
            return Ok(permission.FindPermissionById(id));
        }

        [HttpPost]
        [Route("V1")]
        public ActionResult<Permission> AddPermission([FromBody] Permission value)
        {
            return Ok(permission.AddPermission(value));
        }

        [HttpPut]
        [Route("V1")]
        public ActionResult<Permission> UpdatePermission([FromBody] Permission value)
        {
            return Ok(permission.UpdatePermission(value));
        }

        [HttpDelete]
        [Route("V1")]
        public ActionResult<bool> DeletePermission([FromBody] Permission value)
        {
            return Ok(permission.DeletePermission(value));
        }

    }
}
