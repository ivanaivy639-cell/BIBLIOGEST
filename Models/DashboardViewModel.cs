using GestionProduits.Models;

namespace BIBLIOGEST.Models;

public class DashboardViewModel
{
    public int TotalProduits { get; set; }
    public int StockTotal { get; set; }
    public int ProduitsStockFaible { get; set; }
    public int ProduitsRupture { get; set; }
    public int NombreCategories { get; set; }
    public decimal ValeurStock { get; set; }
    public List<Product> DerniersProduits { get; set; } = new();
    public List<CategoryStatsViewModel> TopCategories { get; set; } = new();
    public List<string> Categories { get; set; } = new();
}

public class CategoryStatsViewModel
{
    public string Categorie { get; set; } = string.Empty;
    public int NombreProduits { get; set; }
}
