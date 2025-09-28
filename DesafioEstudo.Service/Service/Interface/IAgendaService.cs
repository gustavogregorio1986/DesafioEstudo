using DesafioEstudo.Dominio.Dominio;
using DesafioEstudo.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstudo.Service.Service.Interface
{
    public interface IAgendaService
    {
        Task<Agenda> AdicionarAgenda(Agenda agenda);

        Task<List<Agenda>> ListarAgenda();

        Task<List<Agenda>> ListarAgendasPendentes();

        Task<List<Agenda>> ListarAgendasAtivas();

        Task<List<Agenda>> ListarAgendasInativas();

        Task<Agenda> ObterPorId(Guid id);

        Task<Agenda> AtualizarAgenda(Guid id, Agenda novaAgenda);

        Task<bool> Deletar(Guid id);

        Task<byte[]> GerarExcelPorAnoAsync();

        Task<byte[]> GerarPdfPorAnoAsync();
    }
}
