using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechWave.Models;
using TechWave.Models.DomainModel;


namespace TechWave.Controllers
{
    public class OrderController : Controller
    {
        private readonly TechWaveDBContext _context;
        private readonly UserManager<User> _userManager;

        public OrderController(TechWaveDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
       

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            // Get the current user's ID asynchronously
            var userId = await GetUserIdAsync();

            // Fetching cart items for the current user by UserID
            var cartItems = _context.Carts
                .Where(c => c.UserID == userId)
                .Include(c => c.Product)
                .ToList();

            // Calculate the total amount and tax
            decimal totalAmount = 0;
            decimal totalTax = 0;
            decimal taxRate = 0.13m; // Example tax rate (13%)

            foreach (var item in cartItems)
            {
                totalAmount += item.Quantity * item.Product.Price; // Assuming Product has a Price property
            }

            totalTax = totalAmount * taxRate;

            // Storing the total amount and tax in ViewBag to pass to the view
            ViewBag.TotalAmount = totalAmount.ToString("C");
            ViewBag.TotalTax = totalTax.ToString("C");

            return View(new Order { UserID = userId });
        }

        [HttpPost]
        public async Task<IActionResult> CompleteCheckout(Order model)
        {
            // Get the current user's ID asynchronously
            var userId = await GetUserIdAsync();
            model.UserID = userId;
            model.OrderDate = DateTime.Now;

            // Remove validation for User and UserID since they are being set manually
            ModelState.Remove("User");
            ModelState.Remove("UserID");

            if (ModelState.IsValid)
            {
                // Calculate the total amount and tax
                var cartItems = _context.Carts
                    .Where(c => c.UserID == userId)
                    .Include(c => c.Product)
                    .ToList();

                decimal totalAmount = 0;
                decimal taxRate = 0.13m; // Example tax rate (13%)

                foreach (var item in cartItems)
                {
                    totalAmount += item.Quantity * item.Product.Price;
                }

                model.TotalAmount = totalAmount;
                model.TotalTax = totalAmount * taxRate;

                // Save the Order to the database
                _context.Orders.Add(model);
                await _context.SaveChangesAsync();  // Ensure the OrderID is generated

                // Save each item in the cart as an OrderDetail
                foreach (var item in cartItems)
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderID = model.OrderID,  // Link to the newly created Order
                        ProductID = item.ProductID,
                        Quantity = item.Quantity
                    };
                    _context.OrderDetails.Add(orderDetail);
                }

                // Clear the cart
                _context.Carts.RemoveRange(cartItems);
                await _context.SaveChangesAsync();

                //ViewBag.OrderId = model.OrderID;
                return View("ThankYou");
            }

            // If validation fails, return the view with the model to display errors
            return View("Checkout", model);
        }


       

        private async Task<string> GetUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Id;
        }

    }
}