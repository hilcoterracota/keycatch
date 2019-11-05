using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Novell.Directory.Ldap;
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
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> LoginAsync([FromBody] SampekeyUserAccountRequest value)
        {
            var data = await account.Login(value);
            if (data) return Ok(new { Token = SampekeyParams.CreateToken(value) });
            else return Unauthorized();
        }

        [HttpPost]
        [Route("V1/GetUsersWithActiveDirectory")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<HashSet<object>> GetUsersWithActiveDirectory([FromBody] SampekeyUserAccountRequest value)
        {
            var users = new HashSet<object>();
            try
            {
                using (var connection = new LdapConnection { SecureSocketLayer = false })
                {
                    var _domain = Environment.GetEnvironmentVariable("AD_DDOMAIN");
                    var _domainServer = Environment.GetEnvironmentVariable("AD_DDOMAIN_SSERVER");
                    var _port = Environment.GetEnvironmentVariable("AD_PORT");

                    connection.Connect(_domainServer, int.Parse(_port));
                    connection.Bind($"{value.UserName}@{_domain}", value.Password);
                    
                    LdapSearchResults searchResults = connection.Search(
                        Environment.GetEnvironmentVariable("BIND_DN"),
                        LdapConnection.SCOPE_SUB,
                        Environment.GetEnvironmentVariable("LDAP_FILTER"),
                        null,
                        false
                    );

                    while (searchResults.hasMore())
                    {
                        LdapEntry nextEntry = null;
                        nextEntry = searchResults.next();
                        nextEntry.getAttributeSet();
                        var attr = nextEntry.getAttribute("mail");
                        if (attr == null)
                        {
                            users.Add(nextEntry.getAttribute("distinguishedName").StringValue);
                        }
                        else
                        {
                            users.Add(new{
                                Email = nextEntry.getAttribute("mail").StringValue,
                                UserName = nextEntry.getAttribute("sAMAccountName").StringValue
                            });
                        }
                    }
                    return users;
                }
            }
            catch
            {
                return users;
            }
            /*
            HashSet<string> data = account.GetUsersWithActiveDirectory(value);
            return Ok(data);
            */
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

        [HttpGet]
        [Route("V1/GetStatusSession")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult GetStatusSession()
        {
            return Ok();
        }
    }
}
