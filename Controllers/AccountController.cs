using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechWave.Models.DomainModel;
using TechWave.Models.ViewModel;

namespace TechWave.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        public AccountController(UserManager<User> userMngr,
        SignInManager<User> signInMngr)
        {
            userManager = userMngr;
            signInManager = signInMngr;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LogIn(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fetchUser = await userManager.FindByEmailAsync(model.Email);
                if (fetchUser != null)
                {
                    var result = await signInManager.PasswordSignInAsync(
                        fetchUser.UserName, model.Password, isPersistent: false,
                        lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        // Check if the user is an Admin
                        var isAdmin = await userManager.IsInRoleAsync(fetchUser, "Admin");

                        if (isAdmin)
                        {
                            // Redirect Admin user to ManageUser index page
                            return RedirectToAction("Index", "ManageUser");
                        }

                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username/password.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username, PhoneNumber = model.PhoneNumber, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                   // await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("LogIn", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
