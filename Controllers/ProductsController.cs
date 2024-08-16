using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TechWave.Models.DomainModel;
using TechWave.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace TechWave.Controllers
{
    public class ProductsController : Controller
    {
        private readonly TechWaveDBContext _context;
        private readonly UserManager<User> _userManager;

        public ProductsController(TechWaveDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Products(int? categoryId)
        {
            ViewBag.Categories = _context.Categories.ToList();

            var productsQuery = _context.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                productsQuery = productsQuery.Where(p => p.CategoryID == categoryId.Value);
            }

            var products = productsQuery.ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            if (id <= 0) // Handle invalid ID
            {
                return NotFound();
            }
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            // Check if the user is logged in
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to login page with the return URL
                return RedirectToAction("LogIn", "Account", new { returnUrl = Url.Action("AddToCartAfterLogin", "Products", new { productId, quantity }) });
            }

            // Retrieve product
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            // Get the logged-in user
            var user = await _userManager.GetUserAsync(User);

            // Check if cart item already exists for the user
            var existingCartItem = _context.Carts
                .FirstOrDefault(c => c.UserID == user.Id && c.ProductID == productId);

            if (existingCartItem != null)
            {
                // Update existing cart item quantity
                existingCartItem.Quantity += quantity;
                _context.Carts.Update(existingCartItem);
            }
            else
            {
                // Add new cart item
                var cartItem = new Cart
                {
                    UserID = user.Id,
                    ProductID = productId,
                    Quantity = quantity
                };
                _context.Carts.Add(cartItem);
            }

            await _context.SaveChangesAsync();

            // Redirect to the cart page
            return RedirectToAction("Cart", "Cart");
        }

        public async Task<IActionResult> AddToCartAfterLogin(int productId, int quantity)
        {
            // Retrieve product
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            // Get the logged-in user
            var user = await _userManager.GetUserAsync(User);

            // Check if cart item already exists for the user
            var existingCartItem = _context.Carts
                .FirstOrDefault(c => c.UserID == user.Id && c.ProductID == productId);

            if (existingCartItem != null)
            {
                // Update existing cart item quantity
                existingCartItem.Quantity += quantity;
                _context.Carts.Update(existingCartItem);
            }
            else
            {
                // Add new cart item
                var cartItem = new Cart
                {
                    UserID = user.Id,
                    ProductID = productId,
                    Quantity = quantity
                };
                _context.Carts.Add(cartItem);
            }

            await _context.SaveChangesAsync();

            // Redirect to the cart page
            return RedirectToAction("Cart", "Cart");
        }
    }
}
