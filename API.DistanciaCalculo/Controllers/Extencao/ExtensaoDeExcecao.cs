using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.DistanciaCalculo.Controllers.Extensao
{
    public static class ExtensaoDeExcecao
    {
        public static IEnumerable<Exception> ObterTodasExcecoes(this Exception ex)
        {
            Exception excecao = ex;
            yield return excecao;
            while (excecao.InnerException != null)
            {
                excecao = excecao.InnerException;
                yield return excecao;
            }
        }


        public static IEnumerable<string> ObterMensagensDasExcecoes(this Exception ex)
        {
            Exception excecao = ex;
            yield return excecao.Message;
            while (excecao.InnerException != null)
            {
                excecao = excecao.InnerException;
                yield return excecao.Message;
            }
        }
    }
}