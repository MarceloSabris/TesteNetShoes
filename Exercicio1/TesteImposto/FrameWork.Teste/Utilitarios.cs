using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Imposto.FrameWork;


namespace Imposto.Teste
{
    [TestClass]
    public  class Utilitarios
    {
        [TestMethod]
        public void TesteGerarXML()
        {
            List<TesteXML> lstTestXML = new List<TesteXML>(); 

            for (int i =0;i<=10;i++ )
            {
                lstTestXML.Add(new TesteXML()
                {
                    id = i,
                    Nome = "teste"+i    
                });
            }
            
            try
            {

               Utils.GerarXML(lstTestXML, @"c:\", "Teste.xml");
                Assert.IsTrue(true);
            } catch ( Exception ex )
            {
                Assert.Fail("Erro na geração do XML" + ex.ToString());
            }
        }
        

        }
            
    
    public class TesteXML
    {
        public int id {get;set;} 
        public string Nome { get; set; }

    }
}
