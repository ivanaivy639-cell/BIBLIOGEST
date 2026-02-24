using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BIBLIOGEST.Models;
using GestionProduits.Data;
using Microsoft.EntityFrameworkCore;

namespace BIBLIOGEST.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.ToListAsync();
        var categories = products
            .Select(p => p.Categorie)
            .Where(c => !string.IsNullOrWhiteSpace(c))
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        var dashboard = new DashboardViewModel
        {
            TotalProduits = products.Count,
            StockTotal = products.Sum(p => p.QuantiteStock),
            ProduitsStockFaible = products.Count(p => p.QuantiteStock > 0 && p.QuantiteStock <= 10),
            ProduitsRupture = products.Count(p => p.QuantiteStock == 0),
            NombreCategories = categories.Count,
            ValeurStock = products.Sum(p => p.Prix * p.QuantiteStock),
            DerniersProduits = products
                .OrderByDescending(p => p.DateCreation)
                .Take(8)
                .ToList(),
            TopCategories = products
                .GroupBy(p => p.Categorie)
                .Select(g => new CategoryStatsViewModel
                {
                    Categorie = g.Key,
                    NombreProduits = g.Count()
                })
                .OrderByDescending(c => c.NombreProduits)
                .Take(5)
                .ToList(),
            Categories = categories
        };

        return View(dashboard);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
