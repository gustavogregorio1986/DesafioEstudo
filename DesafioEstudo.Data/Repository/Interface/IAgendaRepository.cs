using DesafioEstudo.Dominio.Dominio;
using DesafioEstudo.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstudo.Data.Repository.Interface
{
    public interface IAgendaRepository
    {
        Task<Agenda>  AdicionarAgenda(Agenda agenda);

        Task<List<Agenda>> ListarAgenda();

        Task<List<Agenda>> ListarPorSituacao(EnumSituacao situacao);

        Task<Agenda> ObterPorId(Guid id);

        Task<Agenda> AtualizarAneda(Agenda agenda);

        Task<bool> Deletar(Guid id);

        Task AtualizarSituacaoAsync(int id, string novaSituacao);
    }
}
