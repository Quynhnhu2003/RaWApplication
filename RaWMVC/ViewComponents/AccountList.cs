using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Areas.Identity.Data;
using RaWMVC.ViewModels;

namespace RaWMVC.ViewComponents
{
    public class AccountList : ViewComponent
    {
        private readonly UserManager<RaWMVCUser> _userManager;
        private readonly RoleManager<RaWMVCRole> _roleManager;

        public AccountList(UserManager<RaWMVCUser> userManager, 
            RoleManager<RaWMVCRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var users = await GetItemsAsync();
            return View(users);
        }
		

		private async Task<List<AccountServiceVM>> GetItemsAsync()
		{
			var users = await _userManager.Users.OrderBy(u => u.UserName).ToListAsync();
			var usersWithRoles = new List<AccountServiceVM>();

			foreach (var user in users)
			{
				// Assuming that each user has only one role
				var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

				usersWithRoles.Add(new AccountServiceVM
				{
					User = user,
					Role = role // Use only one role
				});
			}

			return usersWithRoles;
		}
	}
}
