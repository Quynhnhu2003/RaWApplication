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
                return View(nameof(Index), tagVM);
            }
            catch
            {
                //=== Display faily saved message ===//
                TempData["Message"] = "Failed to add tag.";

                //=== If not successful, check the data again ===//
                return View(nameof(Index), tagVM);
            }
        }

        // GET: TagController/Edit/5
        public async Task<IActionResult> Edit(Guid idTag)
        {
            var tagVm = await _context.Tags
                .Where(t => t.tagId.Equals(idTag))
                .Select(t => new TagViewModel
                {
                    tagId = t.tagId,
                    tagName = t.tagName,
                    tagDescription = t.tagDescription.Trim(),
                })
                .SingleOrDefaultAsync();

            if (tagVm == null) return BadRequest();

            return View(nameof(Index));
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

                //=== The tag has just been edited to the database ===//
                await _context.SaveChangesAsync();

                //=== Display successfully saved message ===//
                TempData["Message"] = "Tag edited successfully.";

                return View(nameof(Index), tagVM);
            }
            catch 
            {
                //=== Display faily saved message ===//
                TempData["Message"] = "Failed to edit tag.";

                //=== If not successful, check the data again ===//
                return View(nameof(Index), tagVM);
            }
        }

        // GET: ArtistController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArtistController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Delete(Guid idTag)
        //{
        //    var status = false;
        //    var message = "Not yet implemented!!!";
        //    try
        //    {
        //        //=== Predicate/delgate ===//
        //        var tag = await _context.Tags
        //            .Where(t => t.tagId.Equals(idTag))
        //            .SingleOrDefaultAsync();

        //        if(tag != null)
        //        {
        //            //=== Decreasement Position ===//
        //            var currentPosition = tag.Position;
        //            var listTag= await _context.Tags
        //                .Where(x => x.Position > currentPosition)
        //                .ToListAsync();
        //            if (listTag != null && listTag.Count > 0)
        //            {
        //                foreach (var item in listTag)
        //                {
        //                    item.Position -= 1;
        //                }
        //            }
        //            //=== Remove Music ====//
        //            _context.Tags.Remove(tag);
        //        }
        //        await _context.SaveChangesAsync();
        //        status = true;
        //    }
        //    catch
        //    {
        //        message = "Execution error!!!";
        //    }
        //    return Json(new { status, message });
        //}
        //public IActionResult ReloadTagList()
        //{

        //    return ViewComponent(nameof(TagList));
        //}
    }
}
