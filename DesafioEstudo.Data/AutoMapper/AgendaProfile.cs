using AutoMapper;
using DesafioEstudo.Data.DTO;
using DesafioEstudo.Dominio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstudo.Data.AutoMapper
{
    public class AgendaProfile : Profile
    {
        public AgendaProfile()
        {
            CreateMap<Agenda, AgendaDTO>();
            CreateMap<AgendaDTO, Agenda>();
        }
    }
}
