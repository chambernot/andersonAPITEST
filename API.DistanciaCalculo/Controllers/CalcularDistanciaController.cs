using API.Domain.Entidades;
using API.Dominio;
using API.Infraestrutura.Base;
using API.Infraestrutura.Base.Execucao;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;


namespace API.DistanciaCalculo.Controllers
{

    
    public class CalcularDistanciaController: BaseApiController
    {
        [Route("api/Distancia/CalculoProximidadePessoasGet")]
        [Authorize]
        
        public async Task<HttpResponseMessage> CalculoProximidadePessoas(List<Entrada> ListaLocalizacao)
        {

            string strConexao;
            try
            {
                strConexao = ConfigurationManager.ConnectionStrings["SQLServerConnection"].ConnectionString;

            }
            catch (System.Exception)
            {

                strConexao = @"Data Source = (localdb)\MSSQLLocalDB;Integrated Security=true;Database=APICALCULO";
            }
                 CalcularDistancia _calcDistancia = new CalcularDistancia(strConexao);
             List<List<EnderecosCalculados>> RetornoLista = await _calcDistancia.RetornaCalculoDistancita(ListaLocalizacao);
            var resultado = new Resultado<List<List<EnderecosCalculados>>>()
                            .Executar(() => ResultadoDaOperacao <List<List<EnderecosCalculados>>>.ComValor(RetornoLista));

            if (Request == null)
                return new HttpResponseMessage { Content = new ObjectContent<ResultadoDaOperacao<List<List<EnderecosCalculados>>>>(resultado, new JsonMediaTypeFormatter()) };

            return CreateResponse(resultado);
        }
       
    }
}