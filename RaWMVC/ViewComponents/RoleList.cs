using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Areas.Identity.Data;

namespace RaWMVC.ViewComponents
{
    public class RoleList:ViewComponent
    {
        private readonly RoleManager<RaWMVCRole> _roleManager;

        public RoleList(RoleManager<RaWMVCRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var item = await GetItemsAsync();
            return View(item);
        }

        private Task<List<RaWMVCRole>> GetItemsAsync()
        {
            return _roleManager.Roles
                .OrderBy(r => r.Name)
                .ToListAsync();
        }
    }
}
