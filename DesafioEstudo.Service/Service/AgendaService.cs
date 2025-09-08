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
        private readonly IAgendaRepository _agendaRepository;

        public AgendaService(IAgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }

        public async Task<Agenda> AdicionarAgenda(Agenda agenda)
        {
            return await _agendaRepository.AdicionarAgenda(agenda);
        }

        public async Task<Agenda> AtualizarAgenda(Guid id, Agenda novaAgenda)
        {
            var agendaExistente = await _agendaRepository.ObterPorId(id);

            if(agendaExistente == null)
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
    }
}
