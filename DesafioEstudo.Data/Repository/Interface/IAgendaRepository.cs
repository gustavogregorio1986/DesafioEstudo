using DesafioEstudo.Dominio.Dominio;
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
    }
}
