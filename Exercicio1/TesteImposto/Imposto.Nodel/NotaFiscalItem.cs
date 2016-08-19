using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Domain
{
    public class NotaFiscalItem
    {
        public int Id { get; set; }
        public int IdNotaFiscal { get; set; }
        public string Cfop { get; set; }
        public string TipoIcms { get; set; }
        public double BaseIcms { get; set; }
        public double AliquotaIcms { get; set; }
        public double ValorIcms { get; set; }
        public string NomeProduto { get; set; }
        public string CodigoProduto { get; set; }
        public double BaseIP { get; set; }
        public double AliquotaIPI { get; set; }
        public double ValorIPI { get; set; }
        public double ValorDesconto { get; set; }

        //Propriedade Acrescentada 
        //Como sugestão e ja implementadas no código
        //Sei que não estava no pedido, mas como 
        //estou fazendo a solução em casa e tenho tempo 
        //resolvi fazer as alterações
        //pois pra consulta ou validação da nota fica complicado 
        // você validar valores do objetos sem ter está informação 
        public double ValorItem { get; set; }
        public int Quantidade { get; set; }
        public bool Brinde { get; set; }


    }

}
