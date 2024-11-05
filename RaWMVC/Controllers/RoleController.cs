using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Areas.Identity.Data;
using RaWMVC.Data.Entities;
using RaWMVC.ViewComponents;
using RaWMVC.ViewModels;

namespace RaWMVC.Controllers
{
    [Authorize(Roles = "Admintrator")]
    public class RoleController : Controller
    {
        private readonly RoleManager<RaWMVCRole> _roleManager;

        public RoleController(RoleManager<RaWMVCRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleVM)
        {
            if (ModelState.IsValid)
            {
                var newRole = new RaWMVCRole
                {
                    Name = roleVM.Name,
                    NormalizedName = roleVM.Name.ToUpper(),
                    Description = roleVM.Description,
                };

                var result = await _roleManager.CreateAsync(newRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                // Add errors if creation failed
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(nameof(Index), roleVM);
        }

        // GET: Edit a role (fetch data by role id)
        public async Task<IActionResult> Edit(string id)
        {
            //var role = await _roleManager.FindByIdAsync(id);
            var role = await _roleManager.Roles.Where(r => r.Id == id).SingleOrDefaultAsync();
			if (role == null) return BadRequest();

            var model = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
            };

            return View(nameof(Index), model);
        }

        // POST: Edit a role (submit changes)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleViewModel roleVM)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleVM.Id);
                if (role == null) return BadRequest();

                role.Name = roleVM.Name;
                role.NormalizedName = roleVM.Name?.ToUpper();
                role.Description = roleVM.Description;

                await _roleManager.UpdateAsync(role);

                TempData["Message"] = "Edited role successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Failed to edit role";

                return View(nameof(Index), roleVM);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var status = false;
            var message = "Not yet implemented!!!";
            try
            {
                //=== Predicate/delgate ===//
                var role = await _roleManager.FindByIdAsync(id);
                //=== Remove Role ====//
                var result = await _roleManager.DeleteAsync(role);
                status = true;
            }
            catch
            {
                message = "Execution error!!!";
            }
            return Json(new { status, message });
        }
        public IActionResult ReloadRoleList()
        {
            return ViewComponent(nameof(RoleList));
        }
    }
}
