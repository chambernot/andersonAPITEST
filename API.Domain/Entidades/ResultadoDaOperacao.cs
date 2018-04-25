using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.Entidades
{
    [Serializable, DataContract(Name = "ResultadoDaOperacaoDe{0}")]
    public class ResultadoDaOperacao<T>
    {

        #region Construtor

        public ResultadoDaOperacao()
            : this(default(T))
        {
        }

        public ResultadoDaOperacao(T valor)
        {
            Valor = valor;
        }

        private ResultadoDaOperacao(T valor, List<string> mensagens = null, bool houveErros = false)
        {
            Valor = valor;
            Mensagens = mensagens ?? new List<string>();
            HouveErrosDuranteProcessamento = houveErros;
        }

        #endregion

        [DataMember]
        public T Valor { get; set; }


        private IList<string> _mensagens;

        [DataMember]
        public IList<string> Mensagens
        {
            get { return _mensagens ?? (_mensagens = new List<string>()); }

            set { _mensagens = value; }
        }

        public static ResultadoDaOperacao<T> Criar(T valor, List<string> mensagens = null, bool houveErros = false)
        {
            return new ResultadoDaOperacao<T>(valor, mensagens, houveErros);
        }

        [DataMember(Order = 1, IsRequired = false)]
        public bool HouveErrosDuranteProcessamento { get; set; }

        public ResultadoDaOperacao<TNovo> Copiar<TNovo>()
        {
            return new ResultadoDaOperacao<TNovo> { _mensagens = new List<string>(_mensagens) };
        }


        /*
		 * 
		 * Métodos Estáticos
		 * 
		 */

        public static ResultadoDaOperacao<T> ComValor(T valor)
        {
            return new ResultadoDaOperacao<T>(valor);
        }

        public static ResultadoDaOperacao<T> ComMensagem(string mensagem, params object[] parametros)
        {
            return ComMensagem(string.Format(mensagem, parametros));
        }

        public static ResultadoDaOperacao<T> ComMensagem(string mensagem)
        {
            var resultado = new ResultadoDaOperacao<T>();

            resultado.Mensagens.Add(mensagem);

            return resultado;
        }


        public static ResultadoDaOperacao<T> ComMensagemDeExcecao(Exception ex)
        {
            var resultado = new ResultadoDaOperacao<T> { HouveErrosDuranteProcessamento = true };

            resultado.Mensagens.Add(ObterTodasExcecoes(ex));

            return resultado;
        }

        public static string ObterTodasExcecoes(Exception ex, string separator = "\r\n ")
        {
            if (ex.InnerException == null)
                return ex.Message;

            return ex.Message + separator + ObterTodasExcecoes(ex.InnerException, separator);
        }


        public static ResultadoDaOperacao<T> ComMensagemDeExcecao(string mensagem)
        {
            var resultado = new ResultadoDaOperacao<T> { HouveErrosDuranteProcessamento = true };

            resultado.Mensagens.Add(mensagem);

            return resultado;
        }


        public static ResultadoDaOperacao<T> ComMensagens(params string[] notas)
        {
            return ComMensagens((IEnumerable<string>)notas);
        }

        public static ResultadoDaOperacao<T> ComMensagens(IEnumerable<string> notas)
        {
            var resultado = new ResultadoDaOperacao<T>();


            foreach (string nota in notas)
            {
                resultado.Mensagens.Add(nota);
            }

            return resultado;
        }
    }
}
