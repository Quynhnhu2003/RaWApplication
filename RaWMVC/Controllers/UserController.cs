using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RaWMVC.Areas.Identity.Data;
using RaWMVC.Data;
using RaWMVC.ViewModels;
using Microsoft.EntityFrameworkCore;
using RaWMVC.ViewComponents;
using RaWMVC.Data.Entities;

namespace RaWMVC.Controllers
{
    //[Authorize(Roles = "Admintrator")]
    public class UserController : Controller
    {
        private readonly RaWDbContext _context;
        private readonly SignInManager<RaWMVCUser> _signInManager;
        private readonly UserManager<RaWMVCUser> _userManager;
        private readonly RoleManager<RaWMVCRole> _roleManager;
        private readonly ILogger<UserController> _logger;

        public UserController(RaWDbContext context, UserManager<RaWMVCUser> userManager,
            SignInManager<RaWMVCUser> signInManager, RoleManager<RaWMVCRole> roleManager,
            ILogger<UserController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = roles;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountViewModel accountVM)
        {
            var returnURL = "~/Identity/Account/Login";

            // Create a new user with the provided username
            var newUser = new RaWMVCUser
            {
                UserName = accountVM.Username // Use Username instead of Email
            };

            // Create the user with the provided password
            var result = await _userManager.CreateAsync(newUser, accountVM.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account.");

                // Check and assign the role
                if (!string.IsNullOrEmpty(accountVM.Role))
                {
                    var role = await _roleManager.FindByNameAsync(accountVM.Role);
                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(newUser, role.Name);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Role does not exist.");
                        return View(accountVM);
                    }
                }

                // Sign in the user directly
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                return LocalRedirect(returnURL);
            }

            // Handle errors and return the view with the validation messages
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(accountVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.Users.Where(r => r.Id == id).SingleOrDefaultAsync();
            if (user == null) return BadRequest();

            var currentRoles = await _userManager.GetRolesAsync(user);
            var currentRole = currentRoles.FirstOrDefault();

            ViewBag.Roles = await _roleManager.Roles.ToListAsync();

            var model = new AccountViewModel
            {
                Id = user.Id,
                Username = user.UserName, // Change Email to Username
                Role = currentRole
            };

            return View(nameof(Index), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccountViewModel accountVM)
        {
            try
            {
                var account = await _userManager.FindByIdAsync(accountVM.Id);
                if (account == null) return BadRequest("User not found");

                // Update user's username
                account.UserName = accountVM.Username; // Change Email to Username

                var updateResult = await _userManager.UpdateAsync(account);
                if (!updateResult.Succeeded)
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(accountVM);
                }

                // Get the user's current roles
                var currentRoles = await _userManager.GetRolesAsync(account);

                ViewBag.Roles = await _roleManager.Roles.ToListAsync();

                // Remove the current roles if any
                if (currentRoles.Count > 0)
                {
                    await _userManager.RemoveFromRolesAsync(account, currentRoles);
                }

                // Assign the new role (if a valid one was selected)
                if (!string.IsNullOrEmpty(accountVM.Role))
                {
                    var newRole = await _roleManager.FindByNameAsync(accountVM.Role);
                    if (newRole != null)
                    {
                        await _userManager.AddToRoleAsync(account, newRole.Name);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Selected role does not exist.");
                        return View(accountVM);
                    }
                }

                // Display a success message and redirect
                TempData["Message"] = "Account updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Message"] = "Failed to edit account.";

                return View(nameof(Index), accountVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var status = false;
            var message = "Not yet implemented!";
            try
            {
                var account = await _userManager.FindByIdAsync(id);
                if (account == null)
                {
                    message = "Account not found!";
                    return Json(new { status, message });
                }

                var result = await _userManager.DeleteAsync(account);
                if (result.Succeeded)
                {
                    status = true;
                    message = "Account deleted successfully.";
                }
                else
                {
                    message = "Error deleting account.";
                }
            }
            catch (Exception ex)
            {
                message = "Execution error: " + ex.Message;
            }
            return Json(new { status, message });
        }

        public IActionResult ReloadAccountList()
        {
            return ViewComponent(nameof(AccountList));
        }
    }
}
