using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazzino.Utilities
{
    public class Forms
    {
        public static void NuovoProdotto()
        {
            Console.WriteLine("====== INSERIMENTO PRODOTTO ======");
            Models.Prodotto p = new();

            Console.WriteLine();
            Console.WriteLine("Inserisci Codice Prodotto");
            Console.WriteLine();

            //do
            //{
                p.CodiceProdotto = Console.ReadLine();
            //} while (DataService.TrovaCodiceProdotto(p.CodiceProdotto));
            
           
            Console.WriteLine("Inserisci Categoria");
            p.Categoria = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Inserisci Descrizione");
            p.Descrizione = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Inserisci Prezzo Unitario");
            p.PrezzoUnitario = Helpers.CheckDecimal();
            Console.WriteLine();

            Console.WriteLine("Inserisci Quantità Disponibile");
            p.QuantitaDisponibile = Helpers.CheckInt();
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Prodotto Aggiunto Correttamente");

            Program.ProdottiDs.AggiungiProdotto(p);
            Console.WriteLine();
        }

        public static void AggiornaProdotto()
        {
            Program.ProdottiDs.StampaProdotti();
            Console.WriteLine();
            Console.WriteLine("Inserisci ID del Prodotto da modificare");
            int idProdotto = Helpers.CheckInt();
            Console.WriteLine();

            Models.Prodotto p = Program.ProdottiDs.DatiProdotto(idProdotto);

            Console.WriteLine("====== AGGIORNAMENTO PRODOTTO ======");
            
            Console.WriteLine();

            Console.WriteLine($"Codice Attuale: {p.CodiceProdotto}");
            Console.WriteLine("Inserisci Codice Prodotto");
            p.CodiceProdotto = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine($"Categoria Attuale: {p.Categoria}");
            Console.WriteLine("Inserisci Categoria");
            p.Categoria = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine($"Descrizione Attuale: {p.Descrizione}");
            Console.WriteLine("Inserisci Descrizione");
            p.Descrizione = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine($"Prezzo Unitario Attuale: {p.PrezzoUnitario}");
            Console.WriteLine("Inserisci Prezzo Unitario");
            p.PrezzoUnitario = Helpers.CheckDecimal();
            Console.WriteLine();

            Console.WriteLine($"Quantità Disponibile Attuale: {p.QuantitaDisponibile}");
            Console.WriteLine("Inserisci Quantità Disponibile");
            p.QuantitaDisponibile = Helpers.CheckInt();
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Prodotto Aggiornato Correttamente");

            Program.ProdottiDs.ModificaProdotto(p);
        }

        public static void EliminaProdotto()
        {
            Program.ProdottiDs.StampaProdotti();
            Console.WriteLine();

            Console.WriteLine("====== ELIMINA PRODOTTO ======");
            Console.WriteLine();
            Console.WriteLine("Inserisci ID del Prodotto da Eliminare");
            int idProdotto = Helpers.CheckInt();
            
            Console.WriteLine();
            Console.WriteLine("Prodotto Eliminato Correttamente");

            Program.ProdottiDs.RimuoviProdotto(idProdotto);
        }
    }
}
