using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWave.Models;
using TechWave.Models.DomainModel;

namespace TechWave.Controllers
{
    public class CartController : Controller
    {
        private readonly TechWaveDBContext _context;
        private readonly UserManager<User> _userManager;

        public CartController(TechWaveDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Cart
        public async Task<IActionResult> Cart()
        {
            var userId = await GetUserIdAsync();
            var cartItems = await _context.Carts
                                          .Include(c => c.Product)
                                          .Where(c => c.UserID == userId)
                                          .ToListAsync();

            var cartSummary = new CartSummary
            {
                Items = cartItems,
                Subtotal = cartItems.Sum(item => item.Product.Price * item.Quantity),
                Tax = cartItems.Sum(item => item.Product.Price * item.Quantity) * 0.13m,
                Total = cartItems.Sum(item => item.Product.Price * item.Quantity) * 1.13m
            };

            return View(cartSummary);
        }

        // POST: /Cart/UpdateQuantity
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int productId, int quantity)
        {
            // Enforce quantity limits
            quantity = Math.Clamp(quantity, 1, 5);

            var userId = await GetUserIdAsync();
            var cartItem = await _context.Carts
                                          .FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID == productId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                await _context.SaveChangesAsync();

                // Recalculate cart summary
                var cartItems = await _context.Carts
                                              .Include(c => c.Product)
                                              .Where(c => c.UserID == userId)
                                              .ToListAsync();

                var cartSummary = new CartSummary
                {
                    Items = cartItems,
                    Subtotal = cartItems.Sum(item => item.Product.Price * item.Quantity),
                    Tax = cartItems.Sum(item => item.Product.Price * item.Quantity) * 0.13m,
                    Total = cartItems.Sum(item => item.Product.Price * item.Quantity) * 1.13m
                };

                return Json(new { success = true, summary = cartSummary });
            }
            return Json(new { success = false, message = "Item not found" });
        }

        // POST: /Cart/Remove
        [HttpPost]
        public async Task<IActionResult> Remove(int productId)
        {
            var userId = await GetUserIdAsync();
            var cartItem = await _context.Carts
                                          .FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID == productId);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            return Json(new { success = true });
        }

        // POST: /Cart/Checkout
        [HttpPost]
        public IActionResult Checkout()
        {
            return RedirectToAction("Checkout", "Order");
        }

        private async Task<string> GetUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Id;
        }
    }
}
