
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sampekey.Contex;
using Sampekey.Interface;
using Sampekey.Model.Configuration.Module;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IModule module;

        public ModuleController(IModule _module)
        {
            module = _module;
        }

        [HttpGet]
        [Route("V1")]
        public ActionResult<IEnumerable<Land>> GetAllLands()
        {
            return Ok(module.GetAllLands());
        }

        [HttpGet]
        [Route("V1/search")]
        public ActionResult<Land> FindLandById([FromBody] Land value)
        {
            return Ok(module.FindLandById(value));
        }

        [HttpPost]
        [Route("V1")]
        public ActionResult<Land> AddLand([FromBody] Land value)
        {
            return Ok(module.AddLand(value));
        }

        [HttpPut]
        [Route("V1")]
        public ActionResult<Land> UpdateLand([FromBody] Land value)
        {
            return Ok(module.UpdateLand(value));
        }

        [HttpDelete]
        [Route("V1")]
        public ActionResult<bool> DeleteLand([FromBody] Land value)
        {
            return Ok(module.DeleteLand(value));
        }

    }
}
