using API.Domain.Entidades;
using API.Dominio;
using API.Infraestrutura.Base.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infraestrutura.Base
{
    public class CalcularDistancia
    {
        string _conexao;
        public CalcularDistancia(string conexao)
        {
            _conexao = conexao;
        }
        public async Task<List<List<EnderecosCalculados>>> RetornaCalculoDistancita(List<Entrada> ListaEntrada)
        {
            CalculoHistorico _calculohistorico = new CalculoHistorico(_conexao);
            double duracao;
            double distancia;
            string enderecoretorno = string.Empty;
            distancia = duracao = 0;
            List<Entrada> ListaEntradaCalc = new List<Entrada>();
            List<EnderecosCalculados> ListaEnderecosCalculada = new List<EnderecosCalculados>();
            List<List<EnderecosCalculados>> ListaFinalResult = new List<List<EnderecosCalculados>>();
            try
            {
                for (int i = 0; i < ListaEntrada.Count; i++)
                {
                    EnderecosCalculados retorno = ListaEnderecosCalculada.Find(x => x.Indice == i);


                    if (retorno == null)
                    {
                        string origem = ListaEntrada[i].Localizacao;
                        string nomeorigem = ListaEntrada[i].Nome;


                        for (int j = 0; j < ListaEntrada.Count; j++)
                        {

                            if (i != j)
                            {
                                string url2 = string.Format(
                                "http://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&sensor=false",
                                origem, ListaEntrada[j].Localizacao);

                                System.Net.WebRequest request = System.Net.HttpWebRequest.Create(url2);
                                System.Net.WebResponse response = request.GetResponse();
                                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                                {
                                    System.Web.Script.Serialization.JavaScriptSerializer parser = new System.Web.Script.Serialization.JavaScriptSerializer();
                                    string responseString = reader.ReadToEnd();
                                    RootObject responseData = parser.Deserialize<RootObject>(responseString);
                                    if (responseData != null)
                                    {

                                        double distanciaRetornada = responseData.routes.Sum(r => r.legs.Sum(l => l.distance.value));
                                        double duracaoRetornada = responseData.routes.Sum(r => r.legs.Sum(l => l.duration.value));

                                        if (distanciaRetornada != 0)
                                        {
                                            enderecoretorno = responseData.routes[0].legs[0].end_address;
                                            distancia = distanciaRetornada;
                                            duracao = duracaoRetornada;
                                        }
                                    }
                                }

                                EnderecosCalculados calc = new EnderecosCalculados();
                                calc.Indice = i;
                                calc.Nome = nomeorigem;
                                calc.Localizacao = origem;
                                calc.distancia = distancia;
                                calc.NomeProximo = ListaEntrada[j].Nome;
                                calc.LocalizacaoProximo = enderecoretorno;
                                calc.idIdentificadorExterno = ListaEntrada[j].Identificacao;

                                await _calculohistorico.Salvar(calc);
                                ListaEnderecosCalculada.Add(calc);
                            }

                        }




                    }

                }


                var agrupados = ListaEnderecosCalculada.GroupBy(x => x.Nome).ToList();

                foreach (var item in agrupados)
                {


                    ListaFinalResult.Add(item.Take(3).OrderBy(x => x.distancia).ToList());


                }



            }

            catch { }
            return ListaFinalResult;
        }
    }
}
