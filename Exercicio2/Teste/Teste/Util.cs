using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste
{
    public static class Util
    {
        /// <summary>
        /// Retorno a primeira vogal após o primeiro caracater sem repetição 
        /// caso não encontre ,retorna uma menssagem amigavel ao usuario 
        /// </summary>
        /// <param name="imput"></param>
        /// <returns></returns>
        public static string  FistChar(IStream imput)
        {
            //lista com as vogais possíveis 
            List<char> vogal = new List<char>() { 'A', 'E', 'O','I','U' };
            char letraAtual;
            List<char> VogalCandidata = new List<char>();
            Boolean blConsoante = false;
            while (imput.hasNext())
            {
                letraAtual = imput.getNext();
                
                //verifica se é uma letra 
                if (Char.IsLetter(letraAtual))
                {
                    //verifica se uma vogal 
                    if (vogal.FindAll(x =>  x == letraAtual.ToString().ToUpper().ToCharArray()[0]).Count() == 0)
                        blConsoante = true;
                    else
                    {
                     
                        //add a vogal candidata
                        if (blConsoante == true && VogalCandidata.FindAll(x => x.ToString().ToUpper() == letraAtual.ToString().ToUpper()).Count() == 0)
                            VogalCandidata.Add(letraAtual);
                        else
                        {
                            //percorre todas as vogais candidatas
                            for (int i = 0; i < VogalCandidata.Count; i++)
                            {
                                if (VogalCandidata[i].ToString().ToUpper() == letraAtual.ToString().ToUpper())
                                    VogalCandidata.RemoveAt(i);
                            }
                        }
                        //seta a consoante como false
                        blConsoante = false;
                    }
                }
            }
            if (VogalCandidata.Count > 0)
                return VogalCandidata[0].ToString();
            else 
                return "Nenuma vogal encontrada com os critérios especificados.";
        }
    }
}
