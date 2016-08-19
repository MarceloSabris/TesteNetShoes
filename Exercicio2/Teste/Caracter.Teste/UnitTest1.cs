using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Teste;

namespace Caracter.Teste
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TesteCenario1()
        {
            IStream stream = new LeituraString("CAAABECI");
            Assert.AreEqual("E",Util.FistChar(stream));

        }
        [TestMethod]
        public void TesteCenario2()
        {
            IStream stream = new LeituraString("CAAABEECI");
            Assert.AreEqual("I", Util.FistChar(stream));


        }

        [TestMethod]
        public void TesteCenario3()
        {
            IStream stream = new LeituraString("A ABEECI");
            Assert.AreEqual("I", Util.FistChar(stream));


        }
        [TestMethod]
        public void TesteCenario4()
        {
            IStream stream = new LeituraString("aAbABacfe");
            Assert.AreEqual("e", Util.FistChar(stream));


        }
    }
}
