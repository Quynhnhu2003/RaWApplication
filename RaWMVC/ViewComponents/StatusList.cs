using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data;
using RaWMVC.Data.Entities;

namespace RaWMVC.ViewComponents
{
	public class StatusList : ViewComponent
	{
		private readonly RaWDbContext _context;
        public StatusList(RaWDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var item = await GetItemAsync();

			return View(item);
		}

        private Task<List<Status>> GetItemAsync()
        {
            return _context.Status
                .OrderBy(s => s.Position)
                .ToListAsync();
        }
    }
}
