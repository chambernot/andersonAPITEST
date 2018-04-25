using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Dominio
{
    public class Entrada
    {
        public string Nome { get; set; }
        public string Localizacao { get; set; }

        public Guid Identificacao { get; set; }
    }
}
