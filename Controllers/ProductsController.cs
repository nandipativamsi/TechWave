using TechWave.Models.DomainModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TechWave.Models;
using Microsoft.EntityFrameworkCore;

namespace TechWave.Controllers
{
    public class ProductsController : Controller
    {
        private readonly TechWaveDBContext _context;

        public ProductsController(TechWaveDBContext context)
        {
            _context = context;
        }

        public IActionResult Products(int? categoryId)
        {
            // Fetch all categories for the filter
            ViewBag.Categories = _context.Categories.ToList();

            // Fetch products, optionally filtered by category
            var productsQuery = _context.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                productsQuery = productsQuery.Where(p => p.CategoryID == categoryId.Value);
            }

            var products = productsQuery.ToList();
            return View(products);
        }

        // New action for showing product details
        public IActionResult Details(int id)
        {
            var product = _context.Products
                .Include(p => p.Category) // Include related data if needed
                .FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
