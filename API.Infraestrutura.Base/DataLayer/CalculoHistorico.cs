using API.Domain.Entidades;
using System;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace API.Infraestrutura.Base.DataLayer
{
    public class CalculoHistorico : BaseDatalayer<EnderecosCalculados>
    {
        string _conexao;
        public CalculoHistorico(string conexao) : base(conexao)
        {
            _conexao = conexao;
        }

        public override async Task Salvar(EnderecosCalculados dados)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_conexao))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CriaCalculoHistorico", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nome", dados.Nome);
                        cmd.Parameters.AddWithValue("@Localizacao", dados.Localizacao);
                        cmd.Parameters.AddWithValue("@NomeProximo", dados.NomeProximo);
                        cmd.Parameters.AddWithValue("@LocalizacaoProximo", dados.LocalizacaoProximo);
                        cmd.Parameters.AddWithValue("@distancia", dados.distancia);
                        cmd.Parameters.AddWithValue("@identificacaoexterna", dados.idIdentificadorExterno);
                        con.Open();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {

                
            }
            

            
        }
    }
}
