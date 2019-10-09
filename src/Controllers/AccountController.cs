using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sampekey.Contex;
using Sampekey.Interface;
using Sampekey.Model.Identity;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount account;
        private readonly ILogger logger;
        public AccountController(IAccount _account, ILogger<AccountController> _logger)
        {
            account = _account;
            logger = _logger;
        }

        [HttpPost]
        [Route("V1/login")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> LoginAsync([FromBody] SampekeyUserAccountRequest value)
        {
            try
            {
                return Ok(new{
                    data = await account.Login(value)
                });
                /**
                if (account.LoginWithActiveDirectory(value).IsCompleted || account.LoginWithSampeKey(value).IsCompleted){
                    return Ok(SampekeyParams.CreateToken(value));
                } else return NoContent();
                */
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("V1/GetUsersWithActiveDirectory")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<HashSet<string>> GetUsersWithActiveDirectory([FromBody] SampekeyUserAccountRequest value)
        {
            HashSet<string> data = account.GetUsersWithActiveDirectory(value);
            return Ok(data);
        }

        [HttpPost]
        [Route("V1/CreateUser")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IdentityResult> CreateUser([FromBody] SampekeyUserAccountRequest value)
        {
            Task<IdentityResult> data = account.CreateUser(value);
            if (data.IsCanceled) return BadRequest(data.Exception);
            else if (data.Result == null) return NoContent();
            else return Ok(data.Result);
        }
    }
}
