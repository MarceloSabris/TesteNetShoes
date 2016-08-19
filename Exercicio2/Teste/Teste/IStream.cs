using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste
{
    public interface IStream
    {
        char getNext();
        Boolean hasNext();
    }
}
