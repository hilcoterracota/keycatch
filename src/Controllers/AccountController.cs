using Microsoft.AspNetCore.Mvc;
using keycatch.Contex;

namespace keycatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SampekeyDbContex contex;
        public AccountController(SampekeyDbContex _contex) 
        {
          contex = _contex;
        }

        [HttpGet]
        public object Get() => contex.User;

    }
}
