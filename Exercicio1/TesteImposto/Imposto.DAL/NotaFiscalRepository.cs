using Imposto.DAL;
using Imposto.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.DAl
{
    public class NotaFiscalRepositoryEntity : INotaFiscalRepository
    {
        private NotaFiscalContext _notaFiscal;
        public NotaFiscalRepositoryEntity()
        {
            _notaFiscal = new NotaFiscalContext();
        }
        

        public void SalvarAtualizarNF( NotaFiscal objNotaFiscal )
        {
            
            
                using (var dbContextTransaction = _notaFiscal.Database.BeginTransaction())
                {
                    try
                    {
                       ObjectParameter pidNota = new ObjectParameter("pId", objNotaFiscal.Id);
                       
                       _notaFiscal.P_NOTA_FISCAL(pidNota, objNotaFiscal.NumeroNotaFiscal, objNotaFiscal.Serie, objNotaFiscal.NomeCliente, 
                                                    objNotaFiscal.EstadoDestino, objNotaFiscal.EstadoOrigem);

                    if (pidNota.Value.ToString() == "0")
                    {
                        throw new Exception("Erro ao iserir a notafiscal - Cabeçalho");
                    }
                    objNotaFiscal.Id = Convert.ToInt32(pidNota.Value);

                    for (int i = 0; i < objNotaFiscal.ItensDaNotaFiscal.Count; i++)
                    {

                        ObjectParameter pidNotaItem = new ObjectParameter("pId", objNotaFiscal.Id);

                        _notaFiscal.P_NOTA_FISCAL_ITEM(pidNotaItem, objNotaFiscal.ItensDaNotaFiscal[i].Id, objNotaFiscal.ItensDaNotaFiscal[i].Cfop, objNotaFiscal.ItensDaNotaFiscal[i].TipoIcms, Convert.ToDecimal(objNotaFiscal.ItensDaNotaFiscal[i].BaseIcms),
                                                       Convert.ToDecimal(objNotaFiscal.ItensDaNotaFiscal[i].AliquotaIcms), Convert.ToDecimal(objNotaFiscal.ItensDaNotaFiscal[i].ValorIcms), objNotaFiscal.ItensDaNotaFiscal[i].NomeProduto, objNotaFiscal.ItensDaNotaFiscal[i].CodigoProduto);

                     
                    }

                    _notaFiscal.SaveChanges();
                    dbContextTransaction.Commit();
                    

                    }
                    catch ( Exception ex)
                    {
                       dbContextTransaction.Rollback();
                       throw (ex);
                    }
                 }
            }
             
        }
        
   }

