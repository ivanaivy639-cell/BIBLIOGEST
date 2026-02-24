using System;
using System.ComponentModel.DataAnnotations;

namespace GestionProduits.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        public int ProduitsId { get; set; }

        public int Quantite { get; set; }

        public int SeuilAlerte { get; set; }

        public DateTime DerniereMiseAJour { get; set; }

        public Product? Product { get; set; }
    }
}
