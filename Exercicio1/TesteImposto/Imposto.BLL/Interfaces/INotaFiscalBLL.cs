using Imposto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.BLL.Interfaces
{
    public interface INotaFiscalBLL
    {
        NotaFiscal EmitirNotaFiscal(Pedido pedido);
        void GerarImpostoCFOPItensNotaFiscal(NotaFiscal objNotaFiscal);
        void SalvarAtuaizarNotaFiscal(NotaFiscal objNotaFiscal);


    }
}
