using API.Domain.Entidades;
using API.Dominio;
using API.Infraestrutura.Base;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;


namespace API.DistanciaCalculo.Controllers
{

    
    public class CalcularDistanciaController : ApiController
    {
        [Route("api/Distancia/CalculoProximidadePessoasGet")]
        //[Authorize]
        
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
            return Request.CreateResponse<List<List<EnderecosCalculados>>>(HttpStatusCode.Created, RetornoLista);
        }
       
    }
}