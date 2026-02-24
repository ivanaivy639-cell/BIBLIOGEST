using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionProduits.Models 
{
    public class ProductListViewModels
    {
        public  required IEnumerable<Product> Products { get; set; }
        public required string SearchTerm { get; set; }
        public required string SearchAuteur { get; set; }
        public required string SearchISBN { get; set; }
        public required string SelectedCategory { get; set; }
        public required string SortOrder { get; set; }
        public decimal? PrixMin { get; set; }
        public decimal? PrixMax { get; set; }
        public required string Disponibilite { get; set; }
        public SelectList? Categories { get; set; }
        

        public int TotalProduits { get; set; }
        public int StockTotal { get; set; }
        public int ProduitsStockFaible { get; set; }
        public int NombreCategories { get; set; }
        
   
        public int PageActuelle { get; set; } = 1;
        public int TotalPages { get; set; }
        public int ElementsParPage { get; set; } = 10;
    }

    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(200)]
        [Display(Name = "Nom du produit")]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'auteur est obligatoire")]
        [StringLength(150)]
        [Display(Name = "Auteur")]
        public string Auteur { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'ISBN est obligatoire")]
        [StringLength(20)]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prix est obligatoire")]
        [Range(0.01, 9999.99)]
                [Display(Name = "Prix (FCFA)")]
        [DataType(DataType.Currency)]
        public decimal Prix { get; set; }

        [Display(Name = "Description")]
        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "La catégorie est obligatoire")]
        [Display(Name = "Catégorie")]
        public string Categorie { get; set; } = string.Empty;
      
   

        [Required(ErrorMessage = "La quantité est obligatoire")]
        [Range(0, 99999)]
        [Display(Name = "Quantité en stock")]
        public int QuantiteStock { get; set; }

        [Required(ErrorMessage = "Le fournisseur est obligatoire")]
        [StringLength(150)]
        [Display(Name = "Fournisseur")]
        public string Fournisseur { get; set; } = string.Empty;

        public SelectList? Categories { get; set; }
        
    }

    public class ProductEditViewModel : ProductCreateViewModel
    {
        public int Id { get; set; }
    }
}
