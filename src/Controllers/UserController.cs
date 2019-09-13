
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sampekey.Interface;
using Sampekey.Model.Identity;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;

        public UserController(IUser _user)
        {
            user = _user;
        }

        [HttpGet]
        [Route("V1")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(user.GetAllUsers());
        }

        [HttpGet]
        [Route("V1/{id}:string")]
        public ActionResult<User> FindUserById(string id)
        {
            return Ok(user.FindUserById(id));
        }

        [HttpPost]
        [Route("V1")]
        public ActionResult<User> AddUser([FromBody] User value)
        {
            return Ok(user.AddUser(value));
        }

        [HttpPut]
        [Route("V1")]
        public ActionResult<User> UpdateUser([FromBody] User value)
        {
            return Ok(user.UpdateUser(value));
        }

        [HttpDelete]
        [Route("V1")]
        public ActionResult<bool> DeleteUser([FromBody] User value)
        {
            return Ok(user.DeleteUser(value));
        }

    }
}
