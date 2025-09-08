using DesafioEstudo.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstudo.Data.DTO
{
    public class AgendaDTO
    {
        public Guid Id { get; set; }

        public string? Titulo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public string? Descricao { get; set; }

        public EnumSituacao? enumSituacao { get; set; }
    }
}
