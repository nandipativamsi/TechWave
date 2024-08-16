using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechWave.Models.DomainModel;
using TechWave.Models.ViewModel;

namespace TechWave.Controllers
{
    public class ManageUserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ManageUserController(UserManager<User> userMngr, RoleManager<IdentityRole> roleMngr)
        {
            userManager = userMngr;
            roleManager = roleMngr;
        }
        // Get Method
        public async Task<IActionResult> Index()
        {
            var users = userManager.Users.ToList();
            var nonAdminUsers = new List<User>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                if (!roles.Contains("Admin"))
                {
                    nonAdminUsers.Add(user);
                }
            }

            var model = new UserViewModel
            {
                Users = nonAdminUsers
            };

            return View(model);
        }

        // Post Method
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                    TempData["message"] = errorMessage;
                }
            }
            return RedirectToAction("Index");
        }

       
        [HttpGet]
        public IActionResult Add()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
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

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View("404");
            }

            var userViewModel = new EditUserViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(userViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return View("404");
            }

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to update user.");
                return View(model);
            }

            return RedirectToAction("Index");
        }



    }
}
