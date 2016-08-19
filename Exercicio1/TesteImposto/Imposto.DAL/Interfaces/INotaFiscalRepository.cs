using Imposto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.DAL
{
    public interface INotaFiscalRepository
    {
        void SalvarAtualizarNF(NotaFiscal objNotaFiscal);
    }
}
