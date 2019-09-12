
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sampekey.Contex;
using Sampekey.Interface;
using Sampekey.Model.Configuration.Module;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviromentController : ControllerBase
    {
        private readonly IEnviroment enviroment;

        public EnviromentController(IEnviroment _enviroment)
        {
            enviroment = _enviroment;
        }

        [HttpGet]
        [Route("V1")]
        public ActionResult<IEnumerable<Kingdom>> GetAllKingdoms()
        {
            return Ok(enviroment.GetAllKingdoms());
        }

        [HttpGet]
        [Route("V1/search")]
        public ActionResult<Kingdom> FindKingdomById([FromBody] Kingdom value)
        {
            return Ok(enviroment.FindKingdomById(value));
        }

        [HttpPost]
        [Route("V1")]
        public ActionResult<Kingdom> AddKingdom([FromBody] Kingdom value)
        {
            return Ok(enviroment.AddKingdom(value));
        }

        [HttpPut]
        [Route("V1")]
        public ActionResult<Kingdom> UpdateKingdom([FromBody] Kingdom value)
        {
            return Ok(enviroment.UpdateKingdom(value));
        }

        [HttpDelete]
        [Route("V1")]
        public ActionResult<bool> DeleteKingdom([FromBody] Kingdom value)
        {
            return Ok(enviroment.DeleteKingdom(value));
        }

    }
}
