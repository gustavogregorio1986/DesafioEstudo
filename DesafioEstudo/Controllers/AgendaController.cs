using AutoMapper;
using ClosedXML.Excel;
using DesafioEstudo.Data.DTO;
using DesafioEstudo.Dominio.Dominio;
using DesafioEstudo.Dominio.Enum;
using DesafioEstudo.Service.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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
        public async Task<List<AgendaDTO>> ListarAgenda()
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

        [HttpDelete]
        [Route("Deletar/{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                var resultado = await _agendaService.Deletar(id);
                if (!resultado)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}/situacao")]
        public async Task<IActionResult> AtualizarSituacao(Guid id, [FromBody] SituacaoDto dto)
        {
            if (!Enum.IsDefined(typeof(EnumSituacao), dto.Situacao))
                return BadRequest("Situação inválida.");

            await _agendaService.AtualizarSituacaoAsync(id, dto);
            return NoContent();
        }


        [HttpGet("ExportarExcelPorAno")]
        public async Task<IActionResult> ExportarExcelPorAno()
        {
            var arquivo = await _agendaService.GerarExcelPorAnoAsync();
            return File(arquivo,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"agenda-por-ano-{DateTime.Now:yyyyMMdd}.xlsx");
        }


        [HttpGet("GerarRelatorio")]
        public async Task<IActionResult> GerarRelatorio()
        {
            var compromissos = await _agendaService.ListarAgenda();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Text("Relatório de Compromissos").FontSize(18).Bold().AlignCenter();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(); // Título
                            columns.RelativeColumn(); // Data Início
                            columns.RelativeColumn(); // Data Fim
                            columns.RelativeColumn(); // Situação
                            columns.RelativeColumn(); // Descrição
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Título").Bold();
                            header.Cell().Text("Início").Bold();
                            header.Cell().Text("Fim").Bold();
                            header.Cell().Text("Situação").Bold();
                            header.Cell().Text("Descrição").Bold();
                        });

                        foreach (var item in compromissos)
                        {
                            table.Cell().Text(item.Titulo);
                            table.Cell().Text(item.DataInicio.ToString("dd/MM/yyyy HH:mm"));
                            table.Cell().Text(item.DataFim.ToString("dd/MM/yyyy HH:mm"));
                            table.Cell().Text(item.enumSituacao); // ou item.enumSituacao se for enum
                            table.Cell().Text(item.Descricao);
                        }
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                    });
                });
            }).GeneratePdf();

            return File(pdf, "application/pdf", "agenda.pdf");
        }

        [HttpGet("ListarAgendasTurnos")]
        public async Task<IActionResult> ListarAgendasTurnos()
        {
            var agendas = await _agendaService.ListarAgendasTurnos();
            return Ok(agendas); // ← Turno já vem preenchido!
        }
    }
}

