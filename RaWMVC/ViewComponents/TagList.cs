using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Commons;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using RaWMVC.ViewModels;

namespace RaWMVC.ViewComponents
{
    public class TagList : ViewComponent
    {
        private readonly RaWDbContext _context;
        public TagList(RaWDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int currentPage = 1)
        {
			//để lấy dữ liệu ra từ Be đến Fe thì dùng ViewBag
			ViewBag.CurrentPage = currentPage;

			var take = Constants.TAKE;
			var skip = (currentPage - 1) * take;

			var musicList = await _context.Tags
				 .ToListAsync();

			if (musicList != null && musicList.Count > 0)
			{
				var tempNumber = musicList.Count / take;
				ViewBag.MaxPage = (musicList.Count % take == 0) ? tempNumber : tempNumber + 1;
			};

			var item = musicList
				.OrderBy(m => m.Position)
				.Select(m => new TagViewModel
				{
					TagId = m.TagId,
					TagName = m.TagName,
					TagDescription = m.TagDescription,
					Position = m.Position,
				})
				.Skip(skip) //hàm Skip của Linq dùng để tính phần tử bỏ qua
				.Take(take) // hàm Take của Linq dùng để tính số phần tử cần lấy cho mỗi trang
				.ToList();

			return View(item);
		}

        private Task<List<Tag>> GetItemAsync()
        {
            return _context.Tags.ToListAsync();
        }
    }
}
