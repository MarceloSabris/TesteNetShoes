using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste
{
    /// <summary>
    /// Cria a leitura de caracteres apartir de uma string 
    /// </summary>
    public class LeituraString : IStream
    {
        string _caracter;
        int _posAtual;



        public LeituraString(string Caracteres)
        {
            _caracter = Caracteres;


        }

        public char getNext()
        {
            if (hasNext())
            {
                return (char)_caracter[_posAtual++];
            }
            return ' ';


        }

        public bool hasNext()
        {
            return _caracter.Length > _posAtual;
        }
    }
}
