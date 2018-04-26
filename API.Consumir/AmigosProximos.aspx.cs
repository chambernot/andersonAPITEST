using API.Domain.Entidades;
using API.Dominio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace API.Consumir
{
    public partial class AmigosProximos : System.Web.UI.Page
    {
        List<Entrada> ListaEntradaLocalizacao = new List<Entrada>();

        Guid _identificacao = Guid.NewGuid();
        List<RetornoAPI> ListaRetornoAPI = new List<RetornoAPI>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.rptBand.ItemDataBound += new RepeaterItemEventHandler(rptBand_ItemDataBound);
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            
            LoginTokenResult token = ConsultaToken();
            List<List<EnderecosCalculados>> retorno = PesquisaLocalizacao1(token.AccessToken);
            foreach (List<EnderecosCalculados> item in retorno)
            {
                RetornoAPI retornoapi = new RetornoAPI();
                for (int i = 0; i < item.Count; i++)
                {
                    
                    if (i == 0)
                    {
                        retornoapi.Nome = item[i].Nome;
                        retornoapi.Itens = new List<RetornoAPIItens>();
                    }
                    
                    RetornoAPIItens retornoAPIItens = new RetornoAPIItens();
                    retornoAPIItens.NomeProximo = item[i].NomeProximo;
                    retornoAPIItens.LocalizacaoProximo = item[i].LocalizacaoProximo;
                    retornoapi.Itens.Add(retornoAPIItens);
                    
                }
                ListaRetornoAPI.Add(retornoapi);
                
            }
            
            rptBand.DataSource = ListaRetornoAPI;
            rptBand.DataBind();

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


        public class RetornoAPI
        {
            public string Nome { get; set; }
            public List<RetornoAPIItens> Itens { get; set; }

        }

        public class RetornoAPIItens
        {
            public string NomeProximo { get; set; }
            public string LocalizacaoProximo { get; set; }
        }

        protected void rptBand_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

            if (e.Item.Controls.Contains((Repeater)e.Item.FindControl("rptBandItens")))

            {

                // cria um objeto Repeater e define-o como o objeto AlunoRepeater do item

                Repeater rptBandItens = (Repeater)e.Item.FindControl("rptBandItens");

                // converte o item (linha do repeater) para um professor e define a propriedade Alunos como DataSource do repeater

                rptBandItens.DataSource = ((RetornoAPI)e.Item.DataItem).Itens;

                rptBandItens.DataBind();

            }




        }
    }
}