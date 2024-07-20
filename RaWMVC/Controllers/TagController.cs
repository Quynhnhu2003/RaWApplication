using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using RaWMVC.ViewComponents;
using RaWMVC.ViewModels;

namespace RaWMVC.Controllers
{
    public class TagController : Controller
    {
        private readonly RaWDbContext _context;
        public TagController(RaWDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TempData.Keep();
            return View();
        }
        // POST: TagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagViewModel tagVM)
        {
            try
            {
                var countTag = await _context.Tags.CountAsync();
                var newTag = new Tag
                {
                    tagName = tagVM.tagName.Trim(),
                    tagDescription = tagVM.tagDescription?.Trim(),
                    Position = countTag + 1
                };
                //=== The tag has just been added to the database ===//
                _context.Tags.Add(newTag);
                await _context.SaveChangesAsync();

                //=== Display successfully saved message ===//
                TempData["Message"] = "Tag added successfully.";

                //=== Return to continue creating ===//
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //=== Display faily saved message ===//
                TempData["Message"] = "Failed to add tag.";

                //=== If not successful, check the data again ===//
                return View(nameof(Index));
            }
        }

		// GET: TagController/Edit/5
		public async Task<IActionResult> Edit(Guid idTag)
		{
			var tagVM = await _context.Tags
				.Where(s => s.tagId.Equals(idTag))
				.Select(a => new TagViewModel
				{
					tagId = a.tagId,
					tagName = a.tagName,
					tagDescription = a.tagDescription,
				})
				.SingleOrDefaultAsync();

			if (tagVM == null) return BadRequest();

			return View(nameof(Index), tagVM);
		}

		// POST: TagController/Edit/5
		[HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Tag tagVM)
		{
			try
			{
				var tag = await _context.Tags.FindAsync(tagVM.tagId);
				if (tag == null) return BadRequest();

				tag.tagName = tagVM.tagName.Trim();
				tag.tagDescription = tagVM.tagDescription?.Trim();

				await _context.SaveChangesAsync();

				TempData["Message"] = "Edited tag successfully.";

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				TempData["Message"] = "Failed to edit tag";

				return View(nameof(Index), tagVM);
			}
		}

		public IActionResult GetData(int page = 1) 
        { 
            return ViewComponent("TagList", page); 
        }

        // POST: ArtistController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid idTag)
        {
            var status = false;
            var message = "Not yet implemented!!!";
            try
            {
                //=== Predicate/delgate ===//
                var tag = await _context.Tags
                    .Where(t => t.tagId.Equals(idTag))
                    .SingleOrDefaultAsync();

                if (tag != null)
                {
                    //=== Decreasement Position ===//
                    var currentPosition = tag.Position;
                    var listTag = await _context.Tags
                        .Where(x => x.Position > currentPosition)
                        .ToListAsync();
                    if (listTag != null && listTag.Count > 0)
                    {
                        foreach (var item in listTag)
                        {
                            item.Position -= 1;
                        }
                    }
                    //=== Remove Tag ====//
                    _context.Tags.Remove(tag);
                }
                await _context.SaveChangesAsync();
                status = true;
            }
            catch
            {
                message = "Execution error!!!";
            }
            return Json(new { status, message });
        }
        public IActionResult ReloadTagList()
        {

            return ViewComponent(nameof(TagList));
        }
    }
}
