
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sampekey.Contex;
using Sampekey.Interface;
using Sampekey.Model.Configuration.Module;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemModuleController : ControllerBase
    {
        private readonly ISystemModule systemModule;

        public SystemModuleController(ISystemModule _systemModule)
        {
            systemModule = _systemModule;
        }

        [HttpGet]
        [Route("V1")]
        public ActionResult<IEnumerable<CastleLand>> GetAllCastleLands()
        {
            return Ok(systemModule.GetAllCastleLands());
        }

        [HttpGet]
        [Route("V1/search")]
        public ActionResult<CastleLand> FindCastleLandById([FromBody] CastleLand value)
        {
            return Ok(systemModule.FindCastleLandById(value));
        }

        [HttpPost]
        [Route("V1")]
        public ActionResult<CastleLand> AddCastleLand([FromBody] CastleLand value)
        {
            return Ok(systemModule.AddCastleLand(value));
        }

        [HttpPut]
        [Route("V1")]
        public ActionResult<CastleLand> UpdateCastleLand([FromBody] CastleLand value)
        {
            return Ok(systemModule.UpdateCastleLand(value));
        }

        [HttpDelete]
        [Route("V1")]
        public ActionResult<bool> DeleteCastleLand([FromBody] CastleLand value)
        {
            return Ok(systemModule.DeleteCastleLand(value));
        }

    }
}
