using DesafioEstudo.Data.Repository.Interface;
using DesafioEstudo.Dominio.Dominio;
using DesafioEstudo.Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstudo.Service.Service
{
    public class AgendaService : IAgendaService
    {
        private IAgendaRepository _agendaRepository;

        public async Task<Agenda> AdicionarAgenda(Agenda agenda)
        {
            return await _agendaRepository.AdicionarAgenda(agenda);
        }
    }
}
