using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using API.Dominio;
using API.DistanciaCalculo.Controllers;
using System.Threading.Tasks;
using System.Configuration;


namespace API.Teste
{
    [TestClass]
    public class ConjuntosControllerTest
    {
        
        List<Entrada> ListaEntradaLocalizacao = new List<Entrada>();
        CalcularDistanciaController _calcularDistanciaController;
        Guid _identificacao = Guid.NewGuid();
  

        [TestMethod]
        public async Task TestCalculoDistancia()
        {
            _calcularDistanciaController = new CalcularDistanciaController();
            PessoasLocalizacao();
            string strConexao = ConfigurationManager.ConnectionStrings["SQLServerConnection"].ConnectionString;
            await _calcularDistanciaController.CalculoProximidadePessoas(ListaEntradaLocalizacao);

            try
            {
                using (SqlConnection con = new SqlConnection(strConexao))
                {
                    string sql = "select Nome, IdidenticacaoExterno, COUNT(0) AS QTD from CalculoHistoricoLog where IdidenticacaoExterno = @IdidenticacaoExterno  group by Nome,IdidenticacaoExterno";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@IdidenticacaoExterno", _identificacao);
                        con.Open();
                        SqlDataReader dr = await cmd.ExecuteReaderAsync();

                        while (await dr.ReadAsync())
                        {
                            string nome = dr["Nome"].ToString();
                            Entrada entra = ListaEntradaLocalizacao.Find(x => x.Nome == nome);
                            Assert.IsNotNull(entra);
                            Assert.AreEqual((int)dr["QTD"], 3);
                                                               
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message.ToString());
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
}
