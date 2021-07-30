using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazzino
{
    public class DataService
    {
        private static string ConnectionString;

        public DataService(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private static SqlConnection conn;
        public static DataSet ds = new();
        private SqlDataAdapter prodottiAdapter;

        public void Init()
        {
            Console.WriteLine("Attendere Prego...");

            using (conn = new(ConnectionString))
            {
                conn.Open();

                if(conn.State != ConnectionState.Open)
                {
                    Console.WriteLine("Errore: connessione al database fallita");
                }

                Console.Clear();

                this.prodottiAdapter = new();
                prodottiAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                SqlCommand selectProdotti = new("SELECT * FROM Prodotti", conn);

                prodottiAdapter.SelectCommand = selectProdotti;
                prodottiAdapter.InsertCommand = ComandoInsertProdotto(conn);
                prodottiAdapter.UpdateCommand = ComandoUpdateProdotto(conn);
                prodottiAdapter.DeleteCommand = ComandoDeleteProdotto(conn);

                prodottiAdapter.Fill(ds, "Prodotti");
            }
        }

        public void AggiornaDatabase()
        {
            Console.WriteLine("Attendere Prego...");

            using (conn = new(ConnectionString))
            {
                conn.Open();

                if (conn.State != ConnectionState.Open)
                {
                    Console.WriteLine("Errore: connessione al database fallita");
                }

                prodottiAdapter.SelectCommand.Connection = conn;
                prodottiAdapter.InsertCommand.Connection = conn;
                prodottiAdapter.UpdateCommand.Connection = conn;
                prodottiAdapter.DeleteCommand.Connection = conn;

                prodottiAdapter.Update(ds, "Prodotti");
                prodottiAdapter.Fill(ds, "Prodotti");
            }
            Console.WriteLine();
            Console.WriteLine("Database Aggiornato Correttamente");
        }

        #region Operazioni CRUD

        public void StampaProdotti()
        {
            Console.WriteLine("====== ELENCO PRODOTTI ======");
            Console.WriteLine();

            Console.WriteLine("{0,-10}{1,-17}{2,-15}{3,-31}{4,17}{5,25}", 
                "Id", "Codice Prodotto", "Categoria", "Descrizione", "Prezzo Unitario", "Quantità Disponibile");
            Console.WriteLine(new string('-',120));

            foreach(DataRow riga in ds.Tables["Prodotti"].Rows)
            {
                Console.WriteLine("{0,-10}{1,-17}{2,-15}{3,-31}{4,17}{5,25}",
                        riga["Id"],
                        riga["CodiceProdotto"],
                        riga["Categoria"],
                        riga["Descrizione"],
                        riga["PrezzoUnitario"],
                        riga["QuantitaDisponibile"]
                );
            }
        }

        public void AggiungiProdotto(Models.Prodotto p)
        {
            DataRow nuovaRiga = ds.Tables["Prodotti"].NewRow();

            nuovaRiga["CodiceProdotto"] = p.CodiceProdotto;
            nuovaRiga["Categoria"] = p.Categoria;
            nuovaRiga["Descrizione"] = p.Descrizione;
            nuovaRiga["PrezzoUnitario"] = p.PrezzoUnitario;
            nuovaRiga["QuantitaDisponibile"] = p.QuantitaDisponibile;

            ds.Tables["Prodotti"].Rows.Add(nuovaRiga);
        }

        public void ModificaProdotto(Models.Prodotto p)
        {
            DataRow rigaDaModificare = ds.Tables["Prodotti"].Rows.Find(p.Id);

            if(rigaDaModificare != null)
            {
                rigaDaModificare["CodiceProdotto"] = p.CodiceProdotto;
                rigaDaModificare["Categoria"] = p.Categoria;
                rigaDaModificare["Descrizione"] = p.Descrizione;
                rigaDaModificare["PrezzoUnitario"] = p.PrezzoUnitario;
                rigaDaModificare["QuantitaDisponibile"] = p.QuantitaDisponibile;
            }
        }

        public void RimuoviProdotto(int id)
        {
            DataRow rigaDaEliminare = ds.Tables["Prodotti"].Rows.Find(id);

            rigaDaEliminare?.Delete();

            AggiornaDatabase();
        }

        #endregion

        #region Metodi di servizio per Comandi

        SqlCommand ComandoInsertProdotto(SqlConnection connessione)
        {
            string comando = "INSERT INTO Prodotti " +
                             "VALUES (@CodiceProdotto, @Categoria, @Descrizione," +
                                     "@PrezzoUnitario, @QuantitaDisponibile)";

            SqlCommand insertCommand = new(comando, connessione);
            insertCommand.CommandType = CommandType.Text;

            insertCommand.Parameters.Add(new SqlParameter("@CodiceProdotto", SqlDbType.NVarChar, 20, "CodiceProdotto"));
            insertCommand.Parameters.Add(new SqlParameter("@Categoria", SqlDbType.NVarChar, 20, "Categoria"));
            insertCommand.Parameters.Add(new SqlParameter("@Descrizione", SqlDbType.NVarChar, 500, "Descrizione"));
            insertCommand.Parameters.Add(new SqlParameter("@PrezzoUnitario", SqlDbType.Decimal, 12, "PrezzoUnitario"));
            insertCommand.Parameters.Add(new SqlParameter("@QuantitaDisponibile", SqlDbType.Int, 50, "QuantitaDisponibile"));

            return insertCommand;
        }

        SqlCommand ComandoUpdateProdotto(SqlConnection connessione) //errore chiave unique
        {
            string comando = "UPDATE Prodotti " +
                             "SET CodiceProdotto = @CodiceProdotto, Categoria = @Categoria, " +
                             "Descrizione = @Descrizione, PrezzoUnitario = @PrezzoUnitario, " +
                             "QuantitaDisponibile = @QuantitaDisponibile";

            SqlCommand updateCommand = new(comando, connessione);
            updateCommand.CommandType = CommandType.Text;

            updateCommand.Parameters.Add(new SqlParameter("@CodiceProdotto", SqlDbType.NVarChar, 20, "CodiceProdotto"));
            updateCommand.Parameters.Add(new SqlParameter("@Categoria", SqlDbType.NVarChar, 20, "Categoria"));
            updateCommand.Parameters.Add(new SqlParameter("@Descrizione", SqlDbType.NVarChar, 500, "Descrizione"));
            updateCommand.Parameters.Add(new SqlParameter("@PrezzoUnitario", SqlDbType.Real, 12, "PrezzoUnitario"));
            updateCommand.Parameters.Add(new SqlParameter("@QuantitaDisponibile", SqlDbType.Int, 50, "QuantitaDisponibile"));

            return updateCommand;
        }

        SqlCommand ComandoDeleteProdotto(SqlConnection connessione)
        {
            string comando = "DELETE FROM Prodotti WHERE Id = @Id";

            SqlCommand deleteCommand = new(comando, connessione);
            deleteCommand.CommandType = CommandType.Text;

            deleteCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 50, "Id"));

            return deleteCommand;
        }

        public Models.Prodotto DatiProdotto(int id)
        {
            DataRow riga = ds.Tables["Prodotti"].Rows.Find(id);

            Models.Prodotto p = null;

            if(riga != null)
            {
                p = new()
                {
                    Id = riga.Field<int>("Id"),
                    CodiceProdotto = riga.Field<string>("CodiceProdotto"),
                    Categoria = riga.Field<string>("Categoria"),
                    Descrizione = riga.Field<string>("Descrizione"),
                    PrezzoUnitario = riga.Field<decimal>("PrezzoUnitario"),
                    QuantitaDisponibile = riga.Field<int>("QuantitaDisponibile")
                };
            }

            return p;
        }


        //public static bool TrovaCodiceProdotto(string codiceProd)
        //{
        //    bool giaPresente = true;

        //    Console.WriteLine("Attendere Prego...");

        //    using (conn = new(ConnectionString))
        //    {
        //        conn.Open();

        //        if (conn.State != ConnectionState.Open)
        //        {
        //            Console.WriteLine("Errore: connessione al database fallita");
        //        }

        //        SqlCommand leggi = new("SELECT * FROM Prodotti", conn);
        //        leggi.CommandType = CommandType.Text;

        //        SqlDataReader reader = leggi.ExecuteReader();

        //        List<string> codici = new();
        //        int conta = 0;

        //        while (reader.Read())
        //        {
        //            codici.Add((string)reader["CodiceProdotto"]);
        //        }

        //        foreach (string c in codici)
        //        {
        //            if (c == codiceProd)
        //            {
        //                Console.WriteLine("Errore: il codice è già inserito. Riprova:");
        //            }
        //            else
        //            {
        //                conta++;
        //            }
        //        }

        //        if (conta == codici.Count)
        //        {
        //            giaPresente = true;
        //        }
        //    }
        //    return giaPresente;
        //}

        //public static List<Models.Prodotto> OttieniListaProdotti()
        //{
        //    List<Models.Prodotto> elencoProdotti = new();
        //    Models.Prodotto p = new();

        //    foreach (DataRow riga in ds.Tables["Prodotti"].Rows)
        //    {
        //        riga["CodiceProdotto"] = p.CodiceProdotto;
        //        riga["Categoria"] = p.Categoria;
        //        riga["Descrizione"] = p.Descrizione;
        //        riga["PrezzoUnitario"] = p.PrezzoUnitario;
        //        riga["QuantitaDisponibile"] = p.QuantitaDisponibile;

        //        elencoProdotti.Add(p);
        //    }

        //    return elencoProdotti;
        //}

        #endregion
    }
}
