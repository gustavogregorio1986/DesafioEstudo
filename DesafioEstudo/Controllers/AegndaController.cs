using AutoMapper;
using DesafioEstudo.Data.DTO;
using DesafioEstudo.Dominio.Dominio;
using DesafioEstudo.Service.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEstudo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AegndaController : ControllerBase
    {
        private readonly IAgendaService _agendaService;
        private readonly IMapper _mapper;

        public AegndaController(IAgendaService agendaService, IMapper mapper)
        {
            _agendaService = agendaService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AdicionarAgenda")]
        public async  Task<IActionResult> AdicionarAgenda(AgendaDTO dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agenda = _mapper.Map<Agenda>(dto);
            await _agendaService.AdicionarAgenda(agenda);


            return CreatedAtAction(nameof(AdicionarAgenda), new { id = agenda.Id }, agenda);
        }


    }
}

