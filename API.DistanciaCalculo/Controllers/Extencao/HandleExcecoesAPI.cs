using API.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace API.DistanciaCalculo.Controllers.Extensao
{
    public class HandleExcecoesAPI : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Exception ex = actionExecutedContext.Exception;

            HttpResponseMessage response = actionExecutedContext.ActionContext.Request.CreateResponse(
                HttpStatusCode.BadRequest, ResultadoDaOperacao<Exception>.Criar(ex, ex.ObterMensagensDasExcecoes().ToList(), true));

            actionExecutedContext.Response = response;
        }
    }
}