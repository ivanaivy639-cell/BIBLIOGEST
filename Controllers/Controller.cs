using GestionProduits.Data;
using GestionProduits.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BIBLIOGEST.Controllers;

public class ProduitsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProduitsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(
        string? searchTerm,
        string? selectedCategory,
        string? disponibilite,
        decimal? prixMin,
        decimal? prixMax)
    {
        var vm = await BuildProductListViewModelAsync(
            searchTerm,
            null,
            null,
            selectedCategory,
            disponibilite,
            prixMin,
            prixMax,
            "NomAsc");
        return View(vm);
    }

    public async Task<IActionResult> AdvancedSearch(
        string? searchTerm,
        string? searchAuteur,
        string? searchISBN,
        string? selectedCategory,
        string? disponibilite,
        decimal? prixMin,
        decimal? prixMax,
        string? sortOrder)
    {
        var vm = await BuildProductListViewModelAsync(
            searchTerm,
            searchAuteur,
            searchISBN,
            selectedCategory,
            disponibilite,
            prixMin,
            prixMax,
            string.IsNullOrWhiteSpace(sortOrder) ? "NomAsc" : sortOrder);

        return View(vm);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id.Value);
        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    public async Task<IActionResult> Create()
    {
        var vm = new ProductCreateViewModel
        {
            Categories = new SelectList(await GetCategoriesAsync())
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateViewModel vm)
    {
        if (await _context.Products.AnyAsync(p => p.ISBN == vm.ISBN))
        {
            ModelState.AddModelError(nameof(vm.ISBN), "Cet ISBN existe déjà.");
        }

        if (!ModelState.IsValid)
        {
            vm.Categories = new SelectList(await GetCategoriesAsync());
            return View(vm);
        }

        var product = new Product
        {
            Nom = vm.Nom,
            Auteur = vm.Auteur,
            ISBN = vm.ISBN,
            Prix = vm.Prix,
            Description = vm.Description,
            Categorie = vm.Categorie,
            QuantiteStock = vm.QuantiteStock,
            Fournisseur = vm.Fournisseur,
            DateCreation = DateTime.Now
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        await UpsertStockAsync(product.Id, product.QuantiteStock);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id.Value);
        if (product is null)
        {
            return NotFound();
        }

        var vm = new ProductEditViewModel
        {
            Id = product.Id,
            Nom = product.Nom,
            Auteur = product.Auteur,
            ISBN = product.ISBN,
            Prix = product.Prix,
            Description = product.Description,
            Categorie = product.Categorie,
            QuantiteStock = product.QuantiteStock,
            Fournisseur = product.Fournisseur,
            Categories = new SelectList(await GetCategoriesAsync(), product.Categorie)
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductEditViewModel vm)
    {
        if (id != vm.Id)
        {
            return NotFound();
        }

        if (await _context.Products.AnyAsync(p => p.ISBN == vm.ISBN && p.Id != vm.Id))
        {
            ModelState.AddModelError(nameof(vm.ISBN), "Cet ISBN existe déjà.");
        }

        if (!ModelState.IsValid)
        {
            vm.Categories = new SelectList(await GetCategoriesAsync(), vm.Categorie);
            return View(vm);
        }

        var product = await _context.Products.FindAsync(vm.Id);
        if (product is null)
        {
            return NotFound();
        }

        product.Nom = vm.Nom;
        product.Auteur = vm.Auteur;
        product.ISBN = vm.ISBN;
        product.Prix = vm.Prix;
        product.Description = vm.Description;
        product.Categorie = vm.Categorie;
        product.QuantiteStock = vm.QuantiteStock;
        product.Fournisseur = vm.Fournisseur;
        product.DateModification = DateTime.Now;

        await UpsertStockAsync(product.Id, product.QuantiteStock);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id.Value);
        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is not null)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ProduitsId == id);
            if (stock is not null)
            {
                _context.Stocks.Remove(stock);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<List<string>> GetCategoriesAsync()
    {
        return await _context.Products
            .Select(p => p.Categorie)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
    }

    private async Task<ProductListViewModels> BuildProductListViewModelAsync(
        string? searchTerm,
        string? searchAuteur,
        string? searchISBN,
        string? selectedCategory,
        string? disponibilite,
        decimal? prixMin,
        decimal? prixMax,
        string sortOrder)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p => p.Nom.Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(searchAuteur))
        {
            query = query.Where(p => p.Auteur.Contains(searchAuteur));
        }

        if (!string.IsNullOrWhiteSpace(searchISBN))
        {
            query = query.Where(p => p.ISBN.Contains(searchISBN));
        }

        if (!string.IsNullOrWhiteSpace(selectedCategory))
        {
            query = query.Where(p => p.Categorie == selectedCategory);
        }

        if (!string.IsNullOrWhiteSpace(disponibilite))
        {
            query = disponibilite switch
            {
                "Rupture" => query.Where(p => p.QuantiteStock == 0),
                "Faible" => query.Where(p => p.QuantiteStock > 0 && p.QuantiteStock <= 10),
                "Disponible" => query.Where(p => p.QuantiteStock > 10),
                _ => query
            };
        }

        if (prixMin.HasValue)
        {
            query = query.Where(p => p.Prix >= prixMin.Value);
        }

        if (prixMax.HasValue)
        {
            query = query.Where(p => p.Prix <= prixMax.Value);
        }

        query = sortOrder switch
        {
            "NomDesc" => query.OrderByDescending(p => p.Nom),
            "PrixAsc" => query.OrderBy(p => p.Prix),
            "PrixDesc" => query.OrderByDescending(p => p.Prix),
            "StockAsc" => query.OrderBy(p => p.QuantiteStock),
            "StockDesc" => query.OrderByDescending(p => p.QuantiteStock),
            _ => query.OrderBy(p => p.Nom)
        };

        var products = await query.ToListAsync();
        var allProducts = await _context.Products.ToListAsync();
        var categories = await _context.Products
            .Select(p => p.Categorie)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();

        return new ProductListViewModels
        {
            Products = products,
            SearchTerm = searchTerm ?? string.Empty,
            SearchAuteur = searchAuteur ?? string.Empty,
            SearchISBN = searchISBN ?? string.Empty,
            SelectedCategory = selectedCategory ?? string.Empty,
            SortOrder = sortOrder,
            Disponibilite = disponibilite ?? string.Empty,
            PrixMin = prixMin,
            PrixMax = prixMax,
            Categories = new SelectList(categories),
            TotalProduits = allProducts.Count,
            StockTotal = allProducts.Sum(p => p.QuantiteStock),
            ProduitsStockFaible = allProducts.Count(p => p.QuantiteStock > 0 && p.QuantiteStock <= 10),
            NombreCategories = categories.Count
        };
    }

    private async Task UpsertStockAsync(int productId, int quantite)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ProduitsId == productId);

        if (stock is null)
        {
            stock = new Stock
            {
                ProduitsId = productId,
                Quantite = quantite,
                SeuilAlerte = 10,
                DerniereMiseAJour = DateTime.Now
            };
            _context.Stocks.Add(stock);
            return;
        }

        stock.Quantite = quantite;
        stock.DerniereMiseAJour = DateTime.Now;
    }
}
