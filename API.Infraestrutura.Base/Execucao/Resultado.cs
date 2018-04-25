using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Domain.Entidades;

namespace API.Infraestrutura.Base.Execucao
{
    public class Resultado<T> : IResultadoExecucao<T>
    {
        
        public ResultadoDaOperacao<T> Executar(Func<ResultadoDaOperacao<T>> daOperacao)
        {
            return new ResultadoDaOperacao<T>();
        }


    }
}
