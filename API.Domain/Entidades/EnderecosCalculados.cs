using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.Entidades
{
    [DataContract]
    public class EnderecosCalculados
    {
       
        public int Indice { get; set; }
        [DataMember(Name = "Nome")]
        public string Nome { get; set; }
        [DataMember(Name = "Localizacao")]
        public string Localizacao { get; set; }
        [DataMember(Name = "NomeProximo")]
        public string NomeProximo { get; set; }
        [DataMember(Name = "LocalizacaoProximo")]
        public string LocalizacaoProximo { get; set; }
        [DataMember(Name = "distancia")]
        public double distancia { get; set; }

        public Guid idIdentificadorExterno { get; set; }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

   

}
