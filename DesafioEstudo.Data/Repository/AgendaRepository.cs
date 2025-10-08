using DesafioEstudo.Data.DTO;
using DesafioEstudo.Data.Repository.Interface;
using DesafioEstudo.Dominio.Context;
using DesafioEstudo.Dominio.Dominio;
using DesafioEstudo.Dominio.Enum;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Agenda> AtualizarAneda(Agenda agenda)
        {
            _context.Agendas.Update(agenda);
            await _context.SaveChangesAsync();
            return agenda;
        }

        public async Task AtualizarSituacaoAsync(Guid id, EnumSituacao novaSituacao)
        {
            var agenda = await ObterPorId(id);
            if (agenda == null) throw new Exception("Agenda não encontrada");

            agenda.enumSituacao = novaSituacao;
            await _context.SaveChangesAsync();

        }

        public async Task<bool> Deletar(Guid id)
        {
            var agenda = await _context.Agendas.FindAsync(id);
            if(agenda == null) return false;

            _context.Agendas.Remove(agenda);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AgendaDTO>> ListarAgenda()
        {
            var agendas = await _context.Agendas.ToListAsync();

            var agendasDTO = agendas.Select(a => new AgendaDTO
            {
                Titulo = a.Titulo,
                DataInicio = a.DataInicio,
                DataFim = a.DataFim,
                Descricao = a.Descricao,
                enumSituacao = a.enumSituacao,
                Turno = string.IsNullOrEmpty(a.Turno)
                    ? DefinirTurno(a.DataInicio) // ← aqui você aplica a lógica
                    : a.Turno
            }).ToList();

            return agendasDTO;


        }

        //Logica paraa aparecer o turno na listagem
        private string DefinirTurno(DateTime dataInicio)
        {
            var hora = dataInicio.Hour;

            if (hora >= 5 && hora < 12)
                return "manha";
            else if (hora >= 12 && hora < 18)
                return "tarde";
            else
                return "noite";
        }

        public async Task<List<Agenda>> ListarPorSituacao(EnumSituacao situacao)
        {
            return await _context.Agendas
                .Where(a => a.enumSituacao == situacao)
                .ToListAsync();
        }

        public async Task<Agenda> ObterPorId(Guid id)
        {
            return await _context.Agendas.FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task AtualizarSituacaoAsync(int id, string novaSituacao)
        {
            throw new NotImplementedException();
        }
    }
}
