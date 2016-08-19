using Imposto.BLL.Interfaces;
using Imposto.Core.DAl;
using Imposto.DAL;
using Imposto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.BLL
{
    public class NotaFiscalBLL : INotaFiscalBLL
    {
        #region Metodos
        #region Public
        public NotaFiscal EmitirNotaFiscal(Pedido pedido)
        {
            NotaFiscal objNotaFiscal = new NotaFiscal();
            objNotaFiscal.NumeroNotaFiscal = 99999;
            objNotaFiscal.Serie = new Random().Next(Int32.MaxValue);
            objNotaFiscal.NomeCliente = pedido.NomeCliente;

            objNotaFiscal.EstadoDestino = pedido.EstadoDestino;
            objNotaFiscal.EstadoOrigem = pedido.EstadoOrigem;

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();
                notaFiscalItem.ValorItem = itemPedido.ValorItemPedido;
                if (objNotaFiscal.EstadoDestino.Equals("MG") || objNotaFiscal.EstadoDestino.Equals("SP") ||
                      objNotaFiscal.EstadoDestino.Equals("RJ") || objNotaFiscal.EstadoDestino.Equals("ES"))
                {
                    notaFiscalItem.ValorDesconto = notaFiscalItem.ValorItem * 0.10;
                }
                
                notaFiscalItem.Brinde = itemPedido.Brinde;
                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                objNotaFiscal.ItensDaNotaFiscal.Add(notaFiscalItem);


            }
            GerarImpostoCFOPItensNotaFiscal(objNotaFiscal);
            //Acrescentando o código para adcionar na lista de itens da nota fiscal 
            return objNotaFiscal;

        }

        /// <summary>
        /// Metodo responsável pela geração do CFOP e impostos dos itens
        /// </summary>
        /// <param name="objNotaFiscal"></param>
        public void GerarImpostoCFOPItensNotaFiscal(NotaFiscal objNotaFiscal)
        {

            GerarCFOPItensNotaFiscal(objNotaFiscal);
            GerarICMSItensNotaFiscal(objNotaFiscal);
            CalcularIPI(objNotaFiscal);
        }


        public void SalvarAtuaizarNotaFiscal(NotaFiscal objNotaFiscal)
        {
            INotaFiscalRepository objNotafiscalRepository = new NotaFiscalRepositoryDAO();
            if (objNotaFiscal.GerouXML == true)
                objNotafiscalRepository.SalvarAtualizarNF(objNotaFiscal);
            else
                throw new Exception("É necessário persistir primeiro o arquivo xml");
        }

        #endregion
        #region Private

        /// <summary>
        /// Gera ICMS dos itens da  Nota fiscal
        /// </summary>
        /// <param name="objNotaFiscal"></param>
        private void GerarICMSItensNotaFiscal(NotaFiscal objNotaFiscal)
        {
            for( int i=0;i< objNotaFiscal.ItensDaNotaFiscal.Count;i++ )
            {
                if (objNotaFiscal.EstadoDestino == objNotaFiscal.EstadoOrigem)
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].TipoIcms = "60";
                    objNotaFiscal.ItensDaNotaFiscal[i].AliquotaIcms = 0.18;
                }
                else
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].TipoIcms = "10";
                    objNotaFiscal.ItensDaNotaFiscal[i].AliquotaIcms = 0.17;
                }
                if (objNotaFiscal.ItensDaNotaFiscal[i].Cfop == "6.009")
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].BaseIcms = objNotaFiscal.ItensDaNotaFiscal[i].ValorItem * 0.90; //redução de base
                }
                else
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].BaseIcms = objNotaFiscal.ItensDaNotaFiscal[i].ValorItem;
                }
                objNotaFiscal.ItensDaNotaFiscal[i].ValorIcms = objNotaFiscal.ItensDaNotaFiscal[i].BaseIcms * objNotaFiscal.ItensDaNotaFiscal[i].AliquotaIcms;

                if (objNotaFiscal.ItensDaNotaFiscal[i].Brinde)
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].TipoIcms = "60";
                    objNotaFiscal.ItensDaNotaFiscal[i].AliquotaIcms = 0.18;
                    objNotaFiscal.ItensDaNotaFiscal[i].ValorIcms = objNotaFiscal.ItensDaNotaFiscal[i].BaseIcms * objNotaFiscal.ItensDaNotaFiscal[i].AliquotaIcms;
                }
            }
                
            
        }

        /// <summary>
        /// Gerar CFP dos itens da Nota Fiscal
        /// </summary>
        /// <param name="notaFiscalItem"></param>
        private void GerarCFOPItensNotaFiscal(NotaFiscal objNotaFiscal)
        {
            for (int i = 0; i < objNotaFiscal.ItensDaNotaFiscal.Count; i++)
            {
                if ((objNotaFiscal.EstadoOrigem == "SP") && (objNotaFiscal.EstadoDestino == "RJ"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.000";
                }
                else if ((objNotaFiscal.EstadoOrigem == "SP") && (objNotaFiscal.EstadoDestino == "PE"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.001";
                }
                else if ((objNotaFiscal.EstadoOrigem == "SP") && (objNotaFiscal.EstadoDestino == "MG"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.002";
                }
                else if ((objNotaFiscal.EstadoOrigem == "SP") && (objNotaFiscal.EstadoDestino == "PB"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.003";
                }
                else if ((objNotaFiscal.EstadoOrigem == "SP") && (objNotaFiscal.EstadoDestino == "PR"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.004";
                }
                else if ((objNotaFiscal.EstadoOrigem == "SP") && (objNotaFiscal.EstadoDestino == "PI"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.005";
                }
                else if ((objNotaFiscal.EstadoOrigem == "SP") && (objNotaFiscal.EstadoDestino == "RO"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.006";
                }
                else if ((objNotaFiscal.EstadoOrigem == "SP") && (objNotaFiscal.EstadoDestino == "SE"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.007";
                }
                else if ((objNotaFiscal.EstadoOrigem == "SP") && (objNotaFiscal.EstadoDestino == "TO"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.008";
                }
                else if ((objNotaFiscal.EstadoOrigem == "SP") && (objNotaFiscal.EstadoDestino == "SE"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.009";
                }
                else if ((objNotaFiscal.EstadoOrigem == "SP") && (objNotaFiscal.EstadoDestino == "PA"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.010";
                }
                else if ((objNotaFiscal.EstadoOrigem == "MG") && (objNotaFiscal.EstadoDestino == "RJ"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.000";
                }
                else if ((objNotaFiscal.EstadoOrigem == "MG") && (objNotaFiscal.EstadoDestino == "PE"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.001";
                }
                else if ((objNotaFiscal.EstadoOrigem == "MG") && (objNotaFiscal.EstadoDestino == "MG"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.002";
                }
                else if ((objNotaFiscal.EstadoOrigem == "MG") && (objNotaFiscal.EstadoDestino == "PB"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.003";
                }
                else if ((objNotaFiscal.EstadoOrigem == "MG") && (objNotaFiscal.EstadoDestino == "PR"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.004";
                }
                else if ((objNotaFiscal.EstadoOrigem == "MG") && (objNotaFiscal.EstadoDestino == "PI"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.005";
                }
                else if ((objNotaFiscal.EstadoOrigem == "MG") && (objNotaFiscal.EstadoDestino == "RO"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.006";
                }
                else if ((objNotaFiscal.EstadoOrigem == "MG") && (objNotaFiscal.EstadoDestino == "SE"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.007";
                }
                else if ((objNotaFiscal.EstadoOrigem == "MG") && (objNotaFiscal.EstadoDestino == "TO"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.008";
                }
                else if ((objNotaFiscal.EstadoOrigem == "MG") && (objNotaFiscal.EstadoDestino == "SE"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.009";
                }
                else if ((objNotaFiscal.EstadoOrigem == "MG") && (objNotaFiscal.EstadoDestino == "PA"))
                {
                    objNotaFiscal.ItensDaNotaFiscal[i].Cfop = "6.010";
                }
            }
        }

        /// <summary>
        /// Gera Claculo IP dos intens da nota
        /// </summary>
        /// <param name="objNotaFiscal"></param>
        private void CalcularIPI(NotaFiscal objNotaFiscal)
        {

            for (int i = 0; i < objNotaFiscal.ItensDaNotaFiscal.Count; i++)
            {

                objNotaFiscal.ItensDaNotaFiscal[i].AliquotaIPI = objNotaFiscal.ItensDaNotaFiscal[i].Brinde ? 0 : 0.10;
                objNotaFiscal.ItensDaNotaFiscal[i].ValorIPI = objNotaFiscal.ItensDaNotaFiscal[i].AliquotaIPI * objNotaFiscal.ItensDaNotaFiscal[i].ValorItem;
            }
        }
        #endregion
        #endregion
    }
}
