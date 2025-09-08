using DesafioEstudo.Data.Repository.Interface;
using DesafioEstudo.Dominio.Context;
using DesafioEstudo.Dominio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstudo.Data.Repository
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly DesafioEstudoContext _context;

        public AgendaRepository(DesafioEstudoContext context)
        {
            _context = context;
        }

        public async Task<Agenda> AdicionarAgenda(Agenda agenda)
        {
            await _context.Agendas.AddAsync(agenda);
            await _context.SaveChangesAsync();
            return agenda;
        }
    }
}
