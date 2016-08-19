

using Imposto.BLL;
using Imposto.BLL.Interfaces;
using Imposto.Core.Interfaces;
using Imposto.Domain;
using Imposto.FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Service
{
    public class NotaFiscalService : INotaFiscalServices
    {

        private INotaFiscalBLL notaficalBLL; 


        public NotaFiscalService()
        {
            notaficalBLL = new NotaFiscalBLL();
        }

        /// <summary>
        /// Gera Nota fiscal apartir de um pedido
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public NotaFiscal GerarNotaFiscal(Domain.Pedido pedido)
        {

            return notaficalBLL.EmitirNotaFiscal(pedido);
       
        }


        /// <summary>
        /// Gera Nota fiscal apartir de um pedido
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public void GerarImpostoDesCFOP(NotaFiscal notafiscal)
        {

             notaficalBLL.GerarImpostoCFOPItensNotaFiscal(notafiscal);

        }
        /// <summary>
        /// Gera o XML de uma nota fiscal
        /// </summary>
        /// <param name="objNotaFiscal"> Nota fiscal </param>
        /// <param name="Caminho"> Caminho para geração </param>
        public void GerarXML ( NotaFiscal objNotaFiscal, string Caminho )
        {

            string nomeArquivo = "NotaFiscal_" +  objNotaFiscal.NumeroNotaFiscal + "_Serie_" + objNotaFiscal.Serie + ".xml";
            Utils.GerarXML<NotaFiscal>(objNotaFiscal, Caminho, nomeArquivo);
            objNotaFiscal.GerouXML = true;

        }

        
        public void PersistirNotaBanco(NotaFiscal objNotaFiscal)
        {

            notaficalBLL.SalvarAtuaizarNotaFiscal(objNotaFiscal);
        }


    }


}
