using API.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace API.Consumir
{
    public partial class AmigosProximos : System.Web.UI.Page
    {
        HttpClient client;
        Uri usuarioUri;

        public AmigosProximos()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:61350");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            getAll();
        }
        PostData data = new PostData
        {
            test = "something",
            lines = new SomeSubData
            {
                line1 = "a line",
                line2 = "a second line"
            }
        };

        private void getAll()
        {
            
            StringContent queryString = new StringContent(data.ToString());
            //chamando a api pela url
            System.Net.Http.HttpResponseMessage response = client.PostAsync("api/Distancia/CalculoProximidadePessoasGet", queryString).Result;

            //se retornar com sucesso busca os dados
            if (response.IsSuccessStatusCode)
            {
                //pegando o cabeçalho
                usuarioUri = response.Headers.Location;

                //Pegando os dados do Rest e armazenando na variável usuários
                var usuarios = response.Content.ReadAsAsync<IEnumerable<List<List<EnderecosCalculados>>>>().Result;

                //preenchendo a lista com os dados retornados da variável
                //GridView1.DataSource = usuarios;
                //GridView1.DataBind();
            }

            //Se der erro na chamada, mostra o status do código de erro.
            else
                Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase);
        }
    }

    class SomeSubData
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
    }

    class PostData
    {
        public string test { get; set; }
        public SomeSubData lines { get; set; }
    }
}