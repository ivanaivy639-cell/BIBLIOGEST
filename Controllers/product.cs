using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionProduits.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(200, ErrorMessage = "Le nom ne peut pas dépasser 200 caractères")]
        [Display(Name = "Nom du produit")]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'auteur est obligatoire")]
        [StringLength(150, ErrorMessage = "Le nom de l'auteur ne peut pas dépasser 150 caractères")]
        [Display(Name = "Auteur")]
        public string Auteur { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'ISBN est obligatoire")]
        [StringLength(20, ErrorMessage = "L'ISBN ne peut pas dépasser 20 caractères")]
        [Display(Name = "ISBN")]
        [RegularExpression(@"^(?:ISBN(?:-1[03])?:? )?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9X]$", 
            ErrorMessage = "Format ISBN invalide")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prix est obligatoire")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0 , 9999.99, ErrorMessage = "Le prix doit être entre 0 et 9999.99")]
        [Display(Name = "Prix (FCFA)")]
        [DataType(DataType.Currency)]
        public decimal Prix { get; set; }

        [Display(Name = "Description")]
        [StringLength(2000, ErrorMessage = "La description ne peut pas dépasser 2000 caractères")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "La catégorie est obligatoire")]
        [Display(Name = "Catégorie")]
        public string Categorie { get; set; } = string.Empty;

        [Required(ErrorMessage = "La quantité est obligatoire")]
        [Range(0, 99999, ErrorMessage = "La quantité doit être entre 0 et 99999")]
        [Display(Name = "Quantité en stock")]
        public int QuantiteStock { get; set; }

        [Required(ErrorMessage = "Le fournisseur est obligatoire")]
        [StringLength(150, ErrorMessage = "Le nom du fournisseur ne peut pas dépasser 150 caractères")]
        [Display(Name = "Fournisseur")]
        public string Fournisseur { get; set; } = string.Empty;

        [Display(Name = "Date de création")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        [Display(Name = "Date de modification")]
        [DataType(DataType.DateTime)]
        public DateTime? DateModification { get; set; }

        
        [NotMapped]
        public string StatutStock
        {
            get
            {
                if (QuantiteStock == 0) return "Rupture";
                if (QuantiteStock <= 10) return "Faible";
                return "Disponible";
            }
        }
    }
}
