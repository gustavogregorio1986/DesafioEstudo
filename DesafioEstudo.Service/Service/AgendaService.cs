using AutoMapper;
using ClosedXML.Excel;
using DesafioEstudo.Data.DTO;
using DesafioEstudo.Data.Repository.Interface;
using DesafioEstudo.Dominio.Dominio;
using DesafioEstudo.Service.Service.Interface;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;




namespace DesafioEstudo.Service.Service
{
    public class AgendaService : IAgendaService
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IMapper _mapper;


        public AgendaService(IAgendaRepository agendaRepository, IMapper mapper)
        {
            _agendaRepository = agendaRepository;
            _mapper = mapper;
        }

        public async Task<byte[]> GerarExcelPorAnoAsync()
        {
            var compromissos = await _agendaRepository.ListarAgenda();
            var agrupadosPorAno = compromissos.GroupBy(c => c.DataInicio.Year);

            using var workbook = new XLWorkbook();

            foreach (var grupo in agrupadosPorAno)
            {
                var worksheet = workbook.Worksheets.Add(grupo.Key.ToString());

                worksheet.Cell(1, 1).Value = "Título";
                worksheet.Cell(1, 2).Value = "Início";
                worksheet.Cell(1, 3).Value = "Fim";
                worksheet.Cell(1, 4).Value = "Situação";
                worksheet.Cell(1, 5).Value = "Descrição";

                int linha = 2;
                foreach (var item in grupo.OrderBy(c => c.DataInicio))
                {
                    worksheet.Cell(linha, 1).Value = item.Titulo;
                    worksheet.Cell(linha, 2).Value = item.DataInicio.ToString("dd/MM/yyyy HH:mm");
                    worksheet.Cell(linha, 3).Value = item.DataFim.ToString("dd/MM/yyyy HH:mm") ?? "—";
                    worksheet.Cell(linha, 4).Value = item.enumSituacao.ToString();
                    worksheet.Cell(linha, 5).Value = item.Descricao;
                    linha++;
                }

                worksheet.Columns().AdjustToContents();
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public async Task<Agenda> AdicionarAgenda(Agenda agenda)
        {
            return await _agendaRepository.AdicionarAgenda(agenda);
        }

        public async Task<Agenda> AtualizarAgenda(Guid id, Agenda novaAgenda)
        {
            var agendaExistente = await _agendaRepository.ObterPorId(id);

            if (agendaExistente == null)
                throw new Exception("Agenda não encontrada");

            agendaExistente.Titulo = novaAgenda.Titulo;
            agendaExistente.DataInicio = novaAgenda.DataInicio;
            agendaExistente.DataFim = novaAgenda.DataFim;
            agendaExistente.Descricao = novaAgenda.Descricao;

            return await _agendaRepository.AtualizarAneda(agendaExistente);

        }

        public async Task<bool> Deletar(Guid id)
        {
            return await _agendaRepository.Deletar(id);
        }

        public async Task<List<Agenda>> ListarAgenda()
        {
            return await _agendaRepository.ListarAgenda();
        }

        public async Task<List<Agenda>> ListarAgendasAtivas()
        {
            return await _agendaRepository.ListarPorSituacao(Dominio.Enum.EnumSituacao.Ativo);
        }

        public async Task<List<Agenda>> ListarAgendasInativas()
        {
            return await _agendaRepository.ListarPorSituacao(Dominio.Enum.EnumSituacao.Inativo);
        }

        public async Task<List<Agenda>> ListarAgendasPendentes()
        {
            return await _agendaRepository.ListarPorSituacao(Dominio.Enum.EnumSituacao.Pendente);
        }

        public async Task<Agenda> ObterPorId(Guid id)
        {
            return await _agendaRepository.ObterPorId(id);
        }

        public async Task<byte[]> GerarPdfPorAnoAsync()
        {
            var compromissos = await _agendaRepository.ListarAgenda();
            var agrupadosPorAno = compromissos
                .GroupBy(c => c.DataInicio.Year)
                .OrderBy(g => g.Key);

            var pdf = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Text("Relatório de Compromissos por Ano")
                        .FontSize(18)
                        .Bold()
                        .AlignCenter();

                    page.Content().Column(col =>
                    {
                        foreach (var grupo in agrupadosPorAno)
                        {
                            // ✅ Corrigido: PaddingBottom aplicado ao Item, não ao Text
                            col.Item().PaddingBottom(5).Text($"Ano: {grupo.Key}")
                                .FontSize(14)
                                .Bold()
                                .Underline();

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(); // Título
                                    columns.RelativeColumn(); // Início
                                    columns.RelativeColumn(); // Fim
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

                                foreach (var item in grupo.OrderBy(c => c.DataInicio))
                                {
                                    table.Cell().Text(item.Titulo);
                                    table.Cell().Text(item.DataInicio.ToString("dd/MM/yyyy HH:mm"));
                                    table.Cell().Text(item.DataFim.ToString("dd/MM/yyyy HH:mm") ?? "—");
                                    table.Cell().Text(item.enumSituacao.ToString());
                                    table.Cell().Text(item.Descricao);
                                }
                            });

                            col.Item().PaddingBottom(10); // espaçamento entre grupos
                        }
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                    });
                });
            }).GeneratePdf();

            return pdf;
        }

        public async Task AtualizarSituacaoAsync(Guid id, SituacaoDto dto)
        {
            var agenda = await _agendaRepository.ObterPorId(id);
            if (agenda == null)
                throw new Exception("Agenda não encontrada");

            _mapper.Map(dto, agenda); // aplica EnumSituacao
            await _agendaRepository.AtualizarAneda(agenda);
        }


    }

}
