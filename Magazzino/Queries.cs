using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazzino
{
    public class Queries
    {
        public static void MostraProdottiGiacenzaLimitata()
        {
            Console.WriteLine("Attendere prego...");

            string query = "SELECT * " +
                            "FROM Prodotti " +
                            "WHERE QuantitaDisponibile< 10";

            using (SqlConnection conn = new(Program.ConnectionString))
            {
                conn.Open();

                if (conn.State != ConnectionState.Open)
                {
                    Console.WriteLine("Errore: connessione al database fallita");
                }


                SqlCommand leggi = new(query, conn);
                leggi.CommandType = CommandType.Text;

                SqlDataReader reader = leggi.ExecuteReader();

                Console.Clear();
                Console.WriteLine("============ PRODOTTI CON GIACENZA LIMITATA ============");
                Console.WriteLine();

                Console.WriteLine("{0,-10}{1,-17}{2,-15}{3,-31}{4,17}{5,25}",
                "Id", "Codice Prodotto", "Categoria", "Descrizione", "Prezzo Unitario", "Quantità Disponibile");
                Console.WriteLine(new string('-', 120));

                while (reader.Read())
                {
                    Console.WriteLine("{0,-10}{1,-17}{2,-15}{3,-31}{4,17}{5,25}",
                        reader["Id"],
                        reader["CodiceProdotto"],
                        reader["Categoria"],
                        reader["Descrizione"],
                        reader["PrezzoUnitario"],
                        reader["QuantitaDisponibile"]
                    );
                }
            }
        }

        public static void MostraProdottiPerCategoria()
        {
            Console.WriteLine("Attendere prego...");

            string query =  "SELECT Categoria, COUNT(*) AS [Numero Prodotti] " +
                            "FROM Prodotti " +
                            "GROUP BY Categoria";

            using (SqlConnection conn = new(Program.ConnectionString))
            {
                conn.Open();

                if (conn.State != ConnectionState.Open)
                {
                    Console.WriteLine("Errore: connessione al database fallita");
                }


                SqlCommand leggi = new(query, conn);
                leggi.CommandType = CommandType.Text;

                SqlDataReader reader = leggi.ExecuteReader();

                Console.Clear();
                Console.WriteLine("====== NUMERO PRODOTTI PER CATEGORIA ======");
                Console.WriteLine();

                Console.WriteLine("{0,-20}{1,-15}", "Categoria", "Numero Prodotti");
                Console.WriteLine(new string('-', 43));

                while (reader.Read())
                {
                    Console.WriteLine("{0,-20}{1,-15}",
                        reader["Categoria"],
                        reader["Numero Prodotti"]
                    );
                }
            }
        }
    }
}
