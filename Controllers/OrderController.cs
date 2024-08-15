using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechWave.Models.DomainModel;

namespace TechWave.Controllers
{
    public class OrderController : Controller
    {
        [HttpPost]
        public IActionResult CompleteCheckout(Order order)
        {
            if (ModelState.IsValid)
            {
                // Save the order to the database
                order.OrderDate = DateTime.Now;
                _context.Orders.Add(order);
                _context.SaveChanges();

                // Redirect to the homepage after checkout
                return RedirectToAction("Index", "Home");
            }

            // If the model is not valid, re-display the checkout page with validation errors
            return View("Checkout", order);
        }

    }
}
