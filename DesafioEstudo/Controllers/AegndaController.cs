using DesafioEstudo.Dominio.Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEstudo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AegndaController : ControllerBase
    {
        [HttpPost]
        [Route("AdicionarAgenda")]
        public IActionResult AdicionarAgenda(Agenda agenda)
        {
            return Ok(agenda);
        }
    }
}
