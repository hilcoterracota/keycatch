
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sampekey.Contex;
using Sampekey.Interface;
using Sampekey.Model.Configuration.Module;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISystem system;

        public SystemController(ISystem _system)
        {
            system = _system;
        }

        [HttpGet]
        [Route("V1")]
        public ActionResult<IEnumerable<Castle>> GetAllCastles()
        {
            return Ok(system.GetAllCastles());
        }

        [HttpGet]
        [Route("V1/search")]
        public ActionResult<Castle> FindCastleById([FromBody] Castle value)
        {
            return Ok(system.FindCastleById(value));
        }

        [HttpPost]
        [Route("V1")]
        public ActionResult<Castle> AddCastle([FromBody] Castle value)
        {
            return Ok(system.AddCastle(value));
        }

        [HttpPut]
        [Route("V1")]
        public ActionResult<Castle> UpdateCastle([FromBody] Castle value)
        {
            return Ok(system.UpdateCastle(value));
        }

        [HttpDelete]
        [Route("V1")]
        public ActionResult<bool> DeleteCastle([FromBody] Castle value)
        {
            return Ok(system.DeleteCastle(value));
        }

    }
}
