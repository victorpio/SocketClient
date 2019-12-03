using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrix_Socket_Server
{
    class SocketConsultas
    {

        #region Models&Data


        public OdbcConnection DbConnection;
        private string nomeConexao = "Driver={SQL Server}; Server=sistdist; Database=petrix_db; User Id=victor; Password=123;";
        #region abrir conexao
        public void abrirConexao()
        {
            this.DbConnection = new OdbcConnection(nomeConexao);
            try
            {
                this.DbConnection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao conectar via ODBC " + ex.Message);
            }

        }
        #endregion
        #region fechar conexão
        public void fecharConexao()
        {
            this.DbConnection.Close();
        }
        #endregion






        /*     public List<String> select(string nome)
             {
                 List<String> retorno = new List<String>();
                 try
                 {
                     this.abrirConexao();
                     OdbcCommand DbCommand = DbConnection.CreateCommand();
                     string sql = "SELECT count(*) FROM Anuncio ";

                     if (nome != "")
                     {
                         sql += " where nome like '%" + nome + "%'";
                     }
                     DbCommand.CommandText = sql;
                     OdbcDataReader DbReader = DbCommand.ExecuteReader();
                     while (DbReader.Read())
                     {
                         string m = DbReader.GetString(DbReader.GetOrdinal("nome"));
                         retorno.Add(m);
                     }

                     DbReader.Close();
                     DbCommand.Dispose();
                     this.fecharConexao();
                 }
                 catch (Exception e)
                 {

                     throw new Exception("Falha ao executar instrução select " + e.Message);
                 }
                 return retorno;
             }*/



        #endregion



        public string Contagem()
        {
            string statement = "";
            int qtd_anuncios = 0;
            int qtd_gatos = 0;
            int qtd_cachorros = 0;



            try
            {
                this.abrirConexao();
                OdbcCommand DbCommand = DbConnection.CreateCommand();
                string sql = "SELECT count(*) as qtd_anuncios FROM Anuncio  ";


                DbCommand.CommandText = sql;
                OdbcDataReader DbReader = DbCommand.ExecuteReader();
                while (DbReader.Read())
                {
                    qtd_anuncios = Convert.ToInt32(DbReader.GetString(DbReader.GetOrdinal("qtd_anuncios")));

                }

                sql = "SELECT count(*) FROM Anuncio as qtd_cachorros where conteudo_anun like '%cacho%'";
                DbCommand.CommandText = sql;
                DbReader = DbCommand.ExecuteReader();
                while (DbReader.Read())
                {
                    qtd_cachorros = Convert.ToInt32(DbReader.GetString(DbReader.GetOrdinal("qtd_cachorros")));

                }

                sql = "SELECT count(*) FROM Anuncio as qtd_gatos where conteudo_anun like '%gat%'";
                DbCommand.CommandText = sql;
                DbReader = DbCommand.ExecuteReader();
                while (DbReader.Read())
                {
                    qtd_cachorros = Convert.ToInt32(DbReader.GetString(DbReader.GetOrdinal("qtd_gatos")));

                }

                DbReader.Close();
                DbCommand.Dispose();
                this.fecharConexao();
            }
            catch (Exception e)
            {

                throw new Exception("Falha ao solicitar relatório " + e.Message);
            }

            statement = "Neste momento temos:\n- " + qtd_anuncios + " registros de anúncio;\n- " + qtd_cachorros + " cachorros e;\n- " + qtd_gatos + " gatos!";

            return statement;
        }
    }
}
