using DesafioEstudo.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstudo.Data.DTO
{
    public class AgendaDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O titulo é Obrigatorio")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "A data de inicio é Obrigatorio")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de fim é Obrigatorio")]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "A descrição é Obrigatorio")]
        public string? Descricao { get; set; }

        [EnumDataType(typeof(EnumSituacao), ErrorMessage = "Situação invalida")]
        public EnumSituacao? enumSituacao { get; set; }
    }
}
