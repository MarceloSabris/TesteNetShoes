using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste
{
    /// <summary>
    /// Cria um sctream a partir de um arquivo TXT
    /// </summary>
    public class LeituraArquivo : IStream
    {
        Stream arquivo;



        public LeituraArquivo(string NomeArquivo)
        {
            arquivo = File.OpenRead(NomeArquivo);


        }

        public char getNext()
        {
            return (char)arquivo.ReadByte();


        }

        public bool hasNext()
        {
            return arquivo.CanRead && arquivo.Position < arquivo.Length;
        }
    }
}
