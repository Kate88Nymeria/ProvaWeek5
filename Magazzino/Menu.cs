using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazzino
{
    public class Menu
    {
        public static void Start()
        {
            bool continuare = true;
            int scelta = 0;

            do
            {
                Console.WriteLine("Scegli l'operazione eseguire");
                Console.WriteLine();
                Console.WriteLine("1 - Visualizza Prodotti");
                Console.WriteLine("2 - Inserisci Prodotto");
                Console.WriteLine("3 - Modifica Prodotto");
                Console.WriteLine("4 - Cancella Prodotto");
                Console.WriteLine("5 - Interroga Database");
                Console.WriteLine("6 - Aggiorna Database");
                Console.WriteLine("0 - Esci dal Programma");

                scelta = Utilities.Helpers.CheckInt();

                switch (scelta)
                {
                    case 1:
                        Console.Clear();
                        Program.ProdottiDs.StampaProdotti();
                        Utilities.Helpers.ProseguiEsecuzione();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        Utilities.Forms.NuovoProdotto();
                        Utilities.Helpers.ProseguiEsecuzione();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        Utilities.Forms.AggiornaProdotto();
                        Utilities.Helpers.ProseguiEsecuzione();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        Utilities.Forms.EliminaProdotto();
                        Utilities.Helpers.ProseguiEsecuzione();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        Reports();
                        Console.Clear();
                        break;
                    case 6:
                        Program.ProdottiDs.AggiornaDatabase();
                        Utilities.Helpers.ProseguiEsecuzione();
                        Console.Clear();
                        break;
                    case 0:
                        continuare = false;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Errore: Scelta non ammessa");
                        Utilities.Helpers.ProseguiEsecuzione();
                        break;
                }
            } while (continuare);
        }

        public static void Reports()
        {
            bool continuare = true;
            int scelta = 0;

            do
            {
                Console.WriteLine("Scegli l'operazione eseguire");
                Console.WriteLine();
                Console.WriteLine("1 - Prodotti in Giacenza Limitata");
                Console.WriteLine("2 - Numero di Prodotti per Categoria");
                Console.WriteLine("0 - Torna al Menù Principale");

                scelta = Utilities.Helpers.CheckInt();

                switch (scelta)
                {
                    case 1:
                        Console.Clear();
                        Queries.MostraProdottiGiacenzaLimitata();
                        Utilities.Helpers.ProseguiEsecuzione();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        Queries.MostraProdottiPerCategoria();
                        Utilities.Helpers.ProseguiEsecuzione();
                        Console.Clear();
                        break;
                    case 0:
                        continuare = false;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Errore: Scelta non ammessa");
                        Utilities.Helpers.ProseguiEsecuzione();
                        break;
                }
            } while (continuare);
        }
    }
}
