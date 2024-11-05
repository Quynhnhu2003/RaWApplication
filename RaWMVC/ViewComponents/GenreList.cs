using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Commons;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using RaWMVC.ViewModels;

namespace RaWMVC.ViewComponents
{
    public class GenreList : ViewComponent
    {
        private readonly RaWDbContext _context;
        public GenreList(RaWDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int currentPage = 1)
        {
			//để lấy dữ liệu ra từ Be đến Fe thì dùng ViewBag
			ViewBag.CurrentPage = currentPage;

			var take = Constants.TAKE;
			var skip = (currentPage - 1) * take;

			var genreList = await _context.Genres
				 .ToListAsync();

			if (genreList != null && genreList.Count > 0)
			{
				var tempNumber = genreList.Count / take;
				ViewBag.MaxPage = (genreList.Count % take == 0) ? tempNumber : tempNumber + 1;
			};

			var item = genreList
				.OrderBy(m => m.Position)
				.Select(m => new GenreViewModel
				{
					GenreId = m.GenreId,
					GenreName = m.GenreName,
					GenreDescription = m.GenreDescription,
					Position = m.Position,
				})
				.Skip(skip) //hàm Skip của Linq dùng để tính phần tử bỏ qua
				.Take(take) // hàm Take của Linq dùng để tính số phần tử cần lấy cho mỗi trang
				.ToList();

			return View(item);
		}
        private Task<List<Genre>> GetItemAsync()
        {
            return _context.Genres.ToListAsync();
        }
    }
}
