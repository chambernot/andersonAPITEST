using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using API.Dominio;
using API.DistanciaCalculo.Controllers;
using System.Threading.Tasks;
using System.Configuration;
using API.Domain.Entidades;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Linq;
using System.Web;

namespace API.Teste
{
    [TestClass]
    public class ConjuntosControllerTest
    {
        
        List<Entrada> ListaEntradaLocalizacao = new List<Entrada>();
        Guid _identificacao = Guid.NewGuid();
  

        [TestMethod]
        public void TestCalculoDistancia()
        {
           
            try
            {
                LoginTokenResult token = ConsultaToken();
                List<List<EnderecosCalculados>> Resultado = PesquisaLocalizacao1(token.AccessToken);
               
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message.ToString());
            }
            
            
        }

        private LoginTokenResult ConsultaToken()

        {



            using (var client = new HttpClient())
            {

                HttpResponseMessage response =
                client.PostAsync("http://localhost:61350/token",
                    new StringContent(string.Format("grant_type=password&username={0}&password={1}",
                        HttpUtility.UrlEncode("Fulano"),
                         HttpUtility.UrlEncode("1234")), Encoding.UTF8,
                         "application/x-www-form-urlencoded")).Result;
                string resultJSON = response.Content.ReadAsStringAsync().Result;
                LoginTokenResult result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);

                return result;

            }

        }

        public List<List<EnderecosCalculados>> PesquisaLocalizacao1(string token)
        {

            using (var client = new HttpClient())
            {
                PessoasLocalizacao();
                var serializedProduto = JsonConvert.SerializeObject(ListaEntradaLocalizacao);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage response =
                client.PostAsync("http://localhost:61350/api/Distancia/CalculoProximidadePessoasGet",
                    new StringContent(serializedProduto, Encoding.UTF8,
                         "application/json")).Result;
                string resultJSON = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<List<EnderecosCalculados>>>(resultJSON).ToList();


            }
        }

        private void PessoasLocalizacao()
        {
            Entrada _entrada;
            _entrada = new Entrada();
            _entrada.Identificacao = _identificacao;
            _entrada.Nome = "Anderson";
            _entrada.Localizacao = "Rua Cedral, Santo Andre";
            ListaEntradaLocalizacao.Add(_entrada);

            _entrada = new Entrada();
            _entrada.Identificacao = _identificacao;
            _entrada.Nome = "Camila";
            _entrada.Localizacao = "Rua Maria gastaldo do catelan";
            ListaEntradaLocalizacao.Add(_entrada);

            _entrada = new Entrada();
            _entrada.Identificacao = _identificacao;
            _entrada.Nome = "Joao";
            _entrada.Localizacao = "RUA 13 DE MAIO, SAO PAULO";
            ListaEntradaLocalizacao.Add(_entrada);

            _entrada = new Entrada();
            _entrada.Identificacao = _identificacao;
            _entrada.Nome = "Edinho";
            _entrada.Localizacao = "Rua figueiras, santo andre";
            ListaEntradaLocalizacao.Add(_entrada);
        }
    }

    public class LoginTokenResult
    {
        public override string ToString()
        {
            return AccessToken;
        }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }

    }
}
