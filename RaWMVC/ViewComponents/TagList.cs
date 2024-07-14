using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaW.MVC.Data;
using RaWMVC.Data.Entities;

namespace RaWMVC.ViewComponents
{
    public class TagList : ViewComponent
    {
        private readonly RaWDbContext _context;
        public TagList(RaWDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var item = await GetItemsAsync();
                //var item = await _context.Tags
                //    .OrderBy(t => t.Position)
                //    .ToListAsync();

                return View(item);
            }
            catch (Exception ex)
            {

                throw;
            }
            return null;
        }

        private Task<List<Tag>> GetItemsAsync()
        {
            return _context.Tags.ToListAsync();
        }
    }
}
