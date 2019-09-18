
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sampekey.Interface;
using Sampekey.Model.Identity;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;
        private readonly ILogger logger;
        public UserController(IUser _user, ILogger<UserController> _logger)
        {
            user = _user;
            logger = _logger;
        }

        [HttpGet]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public ActionResult<Task<IEnumerable<User>>> GetAllUsers()
        {
            Task<IEnumerable<User>> data = user.GetAllUsers();
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpGet]
        [Route("V1/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public ActionResult<User> FindUserById(string id)
        {
            Task<User> data = user.FindUserById(id);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpPost]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<User> AddUser([FromBody] User value)
        {
            Task<User> data = user.AddUser(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }

        [HttpPut]
        [Route("V1")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<User> UpdateUser([FromBody] User value)
        {
            Task<User> data = user.UpdateUser(value);
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
        public ActionResult<bool> DeleteUser([FromBody] User value)
        {
            Task<bool> data = user.DeleteUser(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else return Ok(data.Result);
        }

    }
}
