using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            IStream stream = new LeituraString("CAAABECI");
             Console.WriteLine(Util.FistChar(stream));

        }


       
    }


    

    
}
