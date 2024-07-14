using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data;
using RaWMVC.Data.Entities;

namespace RaWMVC.ViewComponents
{
    public class GenreList : ViewComponent
    {
        private readonly RaWDbContext _context;
        public GenreList(RaWDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var item = await GetItemAsync();

            return View(item);
        }
        private Task<List<Genre>> GetItemAsync()
        {
            return _context.Genres
                .OrderBy(g => g.Position)
                .ToListAsync();
        }
    }
}
