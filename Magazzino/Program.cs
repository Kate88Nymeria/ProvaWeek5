using System;

namespace Magazzino
{
    class Program
    {
        public static readonly string ConnectionString = "Server = (localdb)\\mssqllocaldb; Database = Magazzino; Trusted_Connection = True;";

        public static readonly DataService ProdottiDs = new(ConnectionString); 

        static void Main(string[] args)
        {
            Console.WriteLine("====== BENVENUTO ======");
            Console.WriteLine();

            ProdottiDs.Init();

            Menu.Start();

            Console.WriteLine();
            Console.WriteLine("====== ARRIVEDERCI ======");
        }
    }
}
