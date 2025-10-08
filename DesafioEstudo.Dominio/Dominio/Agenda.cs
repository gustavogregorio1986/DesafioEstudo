using DesafioEstudo.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstudo.Dominio.Dominio
{
    public class Agenda
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Titulo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public string? Descricao { get; set; }

        public EnumSituacao? enumSituacao { get; set; }

        public string? Turno { get; set; }
    }
}
