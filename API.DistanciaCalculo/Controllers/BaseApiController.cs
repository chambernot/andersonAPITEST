using API.DistanciaCalculo.Controllers.Extensao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.DistanciaCalculo.Controllers
{
    [HandleExcecoesAPI]
    public class BaseApiController : ApiController
    {
        public HttpResponseMessage CreateResponse<T>(T resultadoDaOperacao, List<string> mensagens = null,
           bool houveErros = false, HttpStatusCode status = HttpStatusCode.OK)
        {
            return Request.CreateResponse(status
                , resultadoDaOperacao 
                , new JsonMediaTypeFormatter());
        }

        public HttpResponseMessage CreateErrorResponse<T>(T resultadoDaOperacao, List<string> mensagens = null,
            bool houveErros = true, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return CreateResponse(resultadoDaOperacao, mensagens, houveErros, status);
        }

        public async Task<HttpResponseMessage> CreateResponse<T>(Task<T> resultadoDaOperacao,
            List<string> mensagens = null, bool houveErros = false, HttpStatusCode status = HttpStatusCode.OK)
        {
            return CreateResponse(await resultadoDaOperacao, mensagens, houveErros, status);
        }

        public async Task<HttpResponseMessage> CreateErrorResponse<T>(Task<T> resultadoDaOperacao,
            List<string> mensagens = null, bool houveErros = true, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return CreateErrorResponse(await resultadoDaOperacao, mensagens, houveErros, status);
        }
    }
}