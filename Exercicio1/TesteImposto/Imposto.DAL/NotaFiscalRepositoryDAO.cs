using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imposto.Domain;

using System.Data;
using System.Data.SqlClient;

namespace Imposto.DAL
{
    public class NotaFiscalRepositoryDAO : INotaFiscalRepository
    {


        public NotaFiscalRepositoryDAO()
        {

        }

        public void SalvarAtualizarNF(NotaFiscal objNotaFiscal)
        {
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {

                conn = new
                    SqlConnection("Server=MARCELO-PC\\SQLEXPRESS;DataBase=Teste;Integrated Security=SSPI");
                conn.Open();
                tran = conn.BeginTransaction();
                InserirNotafiscal(conn, objNotaFiscal, tran);
                InserirItem(conn, objNotaFiscal, tran);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                tran = null;
                throw (ex);
            }

            finally
            {
                if (tran != null)
                {
                    tran.Commit();
                    tran = null;
                }
                if (conn != null)
                {
                    conn.Close();
                }


            }
        }

        private void InserirNotafiscal(SqlConnection con, NotaFiscal objNotaFiscal, SqlTransaction tran)
        {
            SqlCommand cmd = new SqlCommand(
                    "P_NOTA_FISCAL", con, tran);

            cmd.CommandType = CommandType.StoredProcedure;
            var pidNotaFiscal = new SqlParameter("@pId", objNotaFiscal.Id);
            pidNotaFiscal.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(pidNotaFiscal);

            cmd.Parameters.Add(
               new SqlParameter("@pNumeroNotaFiscal", objNotaFiscal.NumeroNotaFiscal));
            cmd.Parameters.Add(
                new SqlParameter("@pSerie", objNotaFiscal.Serie));
            cmd.Parameters.Add(
                new SqlParameter("@pNomeCliente", objNotaFiscal.NomeCliente));
            cmd.Parameters.Add(
               new SqlParameter("@pEstadoDestino", objNotaFiscal.EstadoDestino));
            cmd.Parameters.Add(
                new SqlParameter("@pEstadoOrigem", objNotaFiscal.EstadoDestino));

            cmd.ExecuteNonQuery();
            objNotaFiscal.Id = Convert.ToInt32(pidNotaFiscal.Value);

        }

        private void InserirItem(SqlConnection con, NotaFiscal objNotaFiscal, SqlTransaction tran)
        {
            for (int i = 0; i < objNotaFiscal.ItensDaNotaFiscal.Count; i++)
            {
                SqlCommand cmd = new SqlCommand(
                    "P_NOTA_FISCAL_ITEM", con, tran);

                cmd.CommandType = CommandType.StoredProcedure;
                var pidItem = new SqlParameter("@pId", objNotaFiscal.Id);
                pidItem.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(pidItem);

                cmd.Parameters.Add(
                    new SqlParameter("@pIdNotaFiscal", objNotaFiscal.NumeroNotaFiscal));
                cmd.Parameters.Add(
                    new SqlParameter("@pCfop", objNotaFiscal.ItensDaNotaFiscal[i].Cfop));
                cmd.Parameters.Add(
                    new SqlParameter("@pTipoIcms", objNotaFiscal.ItensDaNotaFiscal[i].TipoIcms));
                cmd.Parameters.Add(
                    new SqlParameter("@pBaseIcms", objNotaFiscal.ItensDaNotaFiscal[i].BaseIcms));
                cmd.Parameters.Add(
                    new SqlParameter("@pAliquotaIcms", objNotaFiscal.ItensDaNotaFiscal[i].AliquotaIcms));
                cmd.Parameters.Add(
                    new SqlParameter("@pValorIcms", objNotaFiscal.ItensDaNotaFiscal[i].ValorIcms));
                cmd.Parameters.Add(
                    new SqlParameter("@pNomeProduto", objNotaFiscal.ItensDaNotaFiscal[i].NomeProduto));
                cmd.Parameters.Add(
                    new SqlParameter("@pCodigoProduto", objNotaFiscal.ItensDaNotaFiscal[i].CodigoProduto));
                cmd.Parameters.Add(
                    new SqlParameter("@pValorDesconto", objNotaFiscal.ItensDaNotaFiscal[i].ValorDesconto));

                cmd.ExecuteNonQuery();
                objNotaFiscal.ItensDaNotaFiscal[i].Id = Convert.ToInt32(pidItem.Value);
            }
        }
    }
}
