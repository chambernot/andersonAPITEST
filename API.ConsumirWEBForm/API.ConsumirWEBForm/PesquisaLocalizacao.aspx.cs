using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace API.ConsumirWEBForm
{
    public partial class PesquisaLocalizacao : System.Web.UI.Page
    {

        List<Entrada> ListaEntradaLocalizacao = new List<Entrada>();
        
        Guid _identificacao = Guid.NewGuid();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            LoginTokenResult token = ConsultaToken();
            PesquisaLocalizacao1(token.AccessToken);
        }

        public RetornoLocalizacao PesquisaLocalizacao1(string token)
        {

            using (var client = new HttpClient())
            {
                PessoasLocalizacao();
                var serializedProduto = JsonConvert.SerializeObject(ListaEntradaLocalizacao);
                HttpResponseMessage response =
                client.PostAsync("http://localhost:61350/api/Distancia/CalculoProximidadePessoasGet",
                    new StringContent(serializedProduto, Encoding.UTF8,
                         "application/json")).Result;
                string resultJSON = response.Content.ReadAsStringAsync().Result;
                RetornoLocalizacao result = JsonConvert.DeserializeObject<RetornoLocalizacao>(resultJSON);

                return result;

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
       
    }
    public class RetornoLocalizacao
    {
        

        [JsonProperty(PropertyName = "Valor")]
        public string Valor { get; set; }

        [JsonProperty(PropertyName = "Mensagens")]
        public string[] Mensagens { get; set; }

        [JsonProperty(PropertyName = "HouveErrosDuranteProcessamento")]
        public bool HouveErrosDuranteProcessamento { get; set; }

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