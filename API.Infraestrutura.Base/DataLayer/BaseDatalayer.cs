using API.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infraestrutura.Base.DataLayer
{
    public abstract class BaseDatalayer<T> : IBase<T>
    {
        public BaseDatalayer(string conexao)
        {

        }
        public abstract Task Salvar(T dados);
       
    }
}
