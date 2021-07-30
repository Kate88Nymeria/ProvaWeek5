using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazzino.Models
{
    public class Prodotto
    {
        public int Id { get; set; }
        public string CodiceProdotto { get; set; }
        public string Categoria { get; set; }
        public string Descrizione { get; set; }
        public decimal PrezzoUnitario { get; set; }
        public int QuantitaDisponibile { get; set; }
    }
}
