using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazzino.Utilities
{
    public class Helpers
    {
        public static void ProseguiEsecuzione()
        {
            Console.WriteLine();
            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadKey();
        }

        public static int CheckInt()
        {
            bool isInt = false;
            int numero = 0;

            do
            {
                isInt = int.TryParse(Console.ReadLine(), out numero);

                if (!isInt)
                {
                    Console.WriteLine("Errore: non hai inserito un numero intero. Riprova:");
                }
            } while (!isInt);

            return numero;
        }

        public static decimal CheckDecimal()
        {
            bool isDecimal = false;
            decimal numero = 0;

            do
            {
                isDecimal = Decimal.TryParse(Console.ReadLine(), out numero);

                if (!isDecimal)
                {
                    Console.WriteLine("Errore: non hai inserito un numero decimale. Riprova:");
                }
            } while (!isDecimal);

            return numero;
        }
    }
}
