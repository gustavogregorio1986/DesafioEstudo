using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DesafioEstudo.Dominio.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumSituacao
    {
        Ativo,
        Pendente,
        Inativo
    }
}
