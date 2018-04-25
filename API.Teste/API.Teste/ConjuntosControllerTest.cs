using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using FluentAssertions;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web.Http;

namespace API.Teste
{
    [TestClass]
    public class ConjuntosControllerTest //: ControllerApiTesteBase
    {
        private static HttpConfiguration configuracao;
        private static HttpServer servidor;
        private static HttpClient proxy;

        [ClassInitialize]
        public static void Inicializar(TestContext context)
        {
            configuracao = new HttpConfiguration();
            configuracao.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}"
            );

            servidor = new HttpServer(configuracao);
            proxy = new HttpClient(servidor);
        }

        [TestMethod]
        public void TestCalculoDistancia()
        {
            //using (var cliente = new HttpClient(servidor))
            //{
                var resultadoDaCriacao =
            proxy.PostAsync(
                "http://localhost:61350/token",
                new StringContent(
                    "{\"Nome\":\"Israel\", \"Cidade\":\"Valinhos\"}",
                    Encoding.Default, "application/json"))
            .Result;
                Assert.AreEqual(HttpStatusCode.Created, resultadoDaCriacao.StatusCode);
                Assert.IsNotNull(resultadoDaCriacao.Headers.Location);
                //var conteudo = new[] { 1 };
                //using (var request = CriarRequest("api/Distancia/CalculoProximidadePessoasGet", HttpMethod.Post, conteudo))
                //{
                //    using (var response = cliente.SendAsync(request, new CancellationTokenSource().Token).Result)
                //    {
                //        response.Content.ReadAsStringAsync().Result.Should().Be(JsonConvert.SerializeObject(new int { } ));
                //    }
                //}
            //}
        }
    }
}
