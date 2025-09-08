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
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaService _agendaService;
        private readonly IMapper _mapper;

        public AgendaController(IAgendaService agendaService, IMapper mapper)
        {
            _agendaService = agendaService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AdicionarAgenda")]
        public async  Task<IActionResult> AdicionarAgenda(AgendaDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var agenda = _mapper.Map<Agenda>(dto);
                await _agendaService.AdicionarAgenda(agenda);


                return CreatedAtAction(nameof(AdicionarAgenda), new { id = agenda.Id }, agenda);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("ListarAgenda")]
        public async Task<List<Agenda>> ListarAgenda()
        {
            return await _agendaService.ListarAgenda();
        }

        [HttpGet]
        [Route("ListarAgendasAtivas")]
        public async Task<List<Agenda>> ListarAgendasAtivas()
        {
            return await _agendaService.ListarAgendasAtivas();
        }

        [HttpGet]
        [Route("ListarAgendasInativas")]
        public async Task<List<Agenda>> ListarAgendasInativas()
        {
            return await _agendaService.ListarAgendasInativas();
        }

        [HttpGet]
        [Route("ListarAgendasPendentes")]
        public async Task<List<Agenda>> ListarAgendasPendentes()
        {
            return await _agendaService.ListarAgendasPendentes();
        }

        [HttpGet]
        [Route("ObterPorId/{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var agenda = await _agendaService.ObterPorId(id);
            if(agenda == null)
            {
                return NotFound();
            }
            return Ok(agenda);
        }

        [HttpPut]
        [Route("AtualizarAgenda/{id}")]
        public async Task<IActionResult> AtualizarAgenda(Guid id, AgendaDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var novaAgenda = _mapper.Map<Agenda>(dto);
                var agendaAtualizada = await _agendaService.AtualizarAgenda(id, novaAgenda);
                return Ok(agendaAtualizada);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

