using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.FrameWork
{
    

    /// <summary>
    /// Classe responsável por funções comuns dentro do projeto
    /// </summary>
    public static class Utils
    {


        /// <summary>
        /// Gerar um arquivo xml apartir de um objeto 
        /// </summary>
        /// <typeparam name="ObjetoGeracaoXML"> Objeto para geração do XML  </typeparam>
        /// <param name="Caminho"> Caminho para ser gerado o XML </param>
        /// <param name="Nome"> Nome do arquivo xml com extenção </param>
        public static void GerarXML <T>( T ObjetoGeracaoXML, String Caminho, string Nome )
        {
            try {  
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(T));

            System.IO.StreamWriter file = new System.IO.StreamWriter(
                Caminho +  Nome );
            writer.Serialize(file, ObjetoGeracaoXML);
            file.Close();
            } 
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
