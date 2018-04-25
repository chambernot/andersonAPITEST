using API.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infraestrutura.Base.Execucao
{
    public interface IResultadoExecucao<T>
    {
        
        ResultadoDaOperacao<T> Executar(Func<ResultadoDaOperacao<T>> daOperacao);

    }
}
