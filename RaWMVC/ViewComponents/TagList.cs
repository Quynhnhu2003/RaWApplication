using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Commons;
using RaWMVC.Data;
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
        public async Task<IViewComponentResult> InvokeAsync(int page = 1)
        {
            //var item = await GetItemsAsync(page);
			var skip = (page - 1) * Constants.TAKE;

			ViewBag.Page = page;
            var listTag = await _context.Tags
                .ToListAsync();

            if (listTag != null && listTag.Count > 0) {
				//=== 101 item (10 item/page) ===//
				//var total = listTag.Count;
				//var maxPage = 0;
				//if (total % Constants.TAKE == 0)
				//{
				//	maxPage = total / Constants.TAKE;
				//}
				//else
				//{
				//	maxPage = (total / Constants.TAKE) + 1;
				//}
				//ViewBag.MaxPage = maxPage;

				ViewBag.MaxPage = (listTag.Count % Constants.TAKE == 0) 
                    ? listTag.Count / Constants.TAKE
					: listTag.Count / Constants.TAKE + 1;
				var itemsPaged = listTag
					.OrderBy(t => t.Position)
					.Skip(skip)
					.Take(Constants.TAKE)
					.ToList();
				return View(itemsPaged);
			}
            return View();
        }

        private Task<List<Tag>> GetItemsAsync(int page = 1)
        {
            var skip = (page - 1) * Constants.TAKE;

            return _context.Tags
                .OrderBy(t => t.Position)
                .Skip(skip)
                .Take(Constants.TAKE)
                .ToListAsync();
        }
    }
}
