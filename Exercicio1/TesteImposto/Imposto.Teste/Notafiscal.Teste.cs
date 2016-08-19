
using Imposto.Core.Service;
using Imposto.Domain;
using Imposto.FrameWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imposto.BLL.Interfaces;
using Imposto.BLL;

namespace Imposto.Teste
{
    [TestClass]
    public class TstNotaFiscal
    {



        private NotaFiscalService service;
        //metodo contrutor para geração da classe de nota fiscal 
        public TstNotaFiscal()
        {
            service = new NotaFiscalService();
        }

        //metodo para verificação da geração da nota fiscal 
        [TestMethod]
        public void TesteGerarNotaFiscalPedido()
        {
            try
            {
                GerarNotaFiscal();
                Assert.AreEqual(1, 1);
            }
            catch (Exception ex)
            {
                Assert.Fail("Erro na geração da nota " + ex.ToString());

            }

        }

        //metodo privado para auxilio da geração de massa de dados 
        // podemos dizer que um mock bem simples
        private NotaFiscal GerarNotaFiscal()
        {
            Pedido pedido = new Pedido();
            
            pedido.EstadoOrigem = "SP";
            pedido.EstadoDestino = "RJ";
            pedido.NomeCliente = "teste Clinete";
            for (int i = 0; i <= 4; i++)
            {
                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = false,
                        CodigoProduto = i.ToString(),
                        NomeProduto = "Teste item com Brinde",
                        ValorItemPedido = 10.15
                    });
            }
            for (int i = 5; i <= 9; i++)
            {
                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = true,
                        CodigoProduto =  i.ToString(),
                        NomeProduto = "Teste",
                        ValorItemPedido = 10.15
                    });
            }
            return service.GerarNotaFiscal(pedido);
        }

        //O caso de teste está pobre pois o mesmo não cobre todas as situações 
        // apenas demonstrei meus conhecimentos de analise  
        // e validação tenstando aluguns cenários, 
        // em caso real, utilizando o TDD como conceito para desenvolvimento 
        // deveríamos testar todos os cenários possíveis 
        [TestMethod]
        public void TestImpostoCFOP( )
        {

            NotaFiscal objnota = GerarNotaFiscal();
            INotaFiscalBLL objNotaFiscalBLL = new NotaFiscalBLL();
            objNotaFiscalBLL.GerarImpostoCFOPItensNotaFiscal(objnota);
            Assert.AreEqual(objnota.ItensDaNotaFiscal[0].Cfop , "6.000");
            Assert.AreEqual(objnota.ItensDaNotaFiscal[0].AliquotaIcms, 0.17);
            Assert.AreEqual(objnota.ItensDaNotaFiscal[0].AliquotaIPI, 0.10);
            //testanto especificamente o bug
            Assert.AreEqual(objnota.ItensDaNotaFiscal[0].ValorDesconto, objnota.ItensDaNotaFiscal[0].ValorItem * 0.10);




        }


        //teste responsável para verificação da geração do arquivo XML
        [TestMethod]
        public void TesteNotaFiscalXML()
        {
            try {  

            NotaFiscal notafiscal = GerarNotaFiscal();
                service.GerarXML(notafiscal, @"c:\");
                
            
            Assert.AreEqual(notafiscal.GerouXML, true);
            }
            catch (Exception ex)
            {
                Assert.Fail("Erro na geração da nota " + ex.ToString());

            }

        }

        //teste responsável para verificação da geração do arquivo XML
        [TestMethod]
        public void TestePersistenciaBanco()
        {
            try
            {
                Pedido pedido = new Pedido();

                pedido.EstadoOrigem = "SP";
                pedido.EstadoDestino = "RJ";
                pedido.NomeCliente = "teste Clinete";
                
                    pedido.ItensDoPedido.Add(
                        new PedidoItem()
                        {
                            Brinde = false,
                            CodigoProduto = "10",
                            NomeProduto = "Teste item com Brinde",
                            ValorItemPedido = 10.15
                        });
                NotaFiscal notafiscal =  service.GerarNotaFiscal(pedido);
                notafiscal.GerouXML = true;
                service.PersistirNotaBanco(notafiscal);
                 Assert.AreEqual(true, true);
            }
            catch (Exception ex)
            {
                Assert.Fail("Erro na geração da nota " + ex.ToString());

            }

        }

        [TestMethod]
        public void TestePersistenciaBancoSemXML()
        {
            try
            {
                Pedido pedido = new Pedido();

                pedido.EstadoOrigem = "SP";
                pedido.EstadoDestino = "RJ";
                pedido.NomeCliente = "teste Clinete";

                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = false,
                        CodigoProduto = "10",
                        NomeProduto = "Teste item com Brinde",
                        ValorItemPedido = 10.15
                    });
                NotaFiscal notafiscal = service.GerarNotaFiscal(pedido);
                notafiscal.GerouXML = false;
                service.PersistirNotaBanco(notafiscal);
                Assert.AreEqual(true, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);

            }

        }




    }
}
