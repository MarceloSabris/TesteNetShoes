using Imposto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Interfaces
{
    public interface INotaFiscalServices
    {
        NotaFiscal GerarNotaFiscal(Domain.Pedido pedido);
        void GerarXML(NotaFiscal objNotaFiscal, string Caminho);
        void PersistirNotaBanco(NotaFiscal objNotaFiscal);
        void GerarImpostoDesCFOP(NotaFiscal notafiscal);
    }
}
