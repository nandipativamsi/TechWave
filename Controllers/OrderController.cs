using Microsoft.AspNetCore.Mvc;
using TechWave.Models.DomainModel;


namespace TechWave.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult CompleteCheckout(Order model)
        {
            if (ModelState.IsValid)
            {

                TempData["Message"] = $"Thank you, {model.User.UserName}, for ordering!";
                return RedirectToAction("ThankYou");
            }

            return View("Checkout", model);
        }

        public IActionResult ThankYou()
        {
            ViewBag.Message = TempData["Message"] ?? "Thank you for your order!";
            return View();
        }
    }
}