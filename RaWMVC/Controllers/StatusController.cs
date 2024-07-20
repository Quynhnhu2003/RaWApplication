using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using RaWMVC.ViewComponents;
using RaWMVC.ViewModels;

namespace RaWMVC.Controllers
{
    public class StatusController : Controller
    {
        private readonly RaWDbContext _context;
        public StatusController(RaWDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create(StatusViewModel statusVM)
        {
            try
            {
                var countStatus = await _context.Status.CountAsync();
                var newSttaus = new Status
                {
                    statusName = statusVM.statusName.Trim(),
                    statusDescription = statusVM.statusDescription?.Trim(),
                    Position = countStatus + 1,
                };
                //=== The status has just been added to the database ===//
                _context.Status.Add(newSttaus);
                await _context.SaveChangesAsync();

                //=== Display successfully saved message ===//
                TempData["Message"] = "Added tag successfully.";

                //=== Return to continue creating ===//
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //=== Display faily saved message ===//
                TempData["Message"] = "Failed to add status.";

                //=== If not successful, check the data again ===//
                return View(nameof(Index));
            }
        }
		// GET: StatusController/Edit/5
        public async Task<IActionResult> Edit (Guid idStatus)
        {
            var statusVM = await _context.Status
                .Where(s => s.statusId.Equals(idStatus))
                .Select(a => new StatusViewModel
                {
                    statusId = a.statusId,
                    statusName = a.statusName,
                    statusDescription = a.statusDescription,
                })
                .SingleOrDefaultAsync();

            if (statusVM == null) return BadRequest();

            return View(nameof(Index), statusVM);
        }
		// POST: StatusController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Status statusVM)
        {
            try
            {
                var status = await _context.Status.FindAsync(statusVM.statusId);
                if (status == null) return BadRequest();

                status.statusName = statusVM.statusName.Trim();
                status.statusDescription = statusVM.statusDescription?.Trim();

                await _context.SaveChangesAsync();

                TempData["Message"] = "Edited status successfully.";

				return RedirectToAction(nameof(Index));
			}
			catch
            {
                TempData["Message"] = "Failed to edit status";
                
                return View(nameof(Index), statusVM); 
            }
        }

        // POST: ArtistController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid idStatus)
        {
            var status = false;
            var message = "Not yet implemented!!!";
            try
            {
                //=== Predicate/delgate ===//
                var statusVM = await _context.Status
                    .Where(t => t.statusId.Equals(idStatus))
                    .SingleOrDefaultAsync();

                if (statusVM != null)
                {
                    //=== Decreasement Position ===//
                    var currentPosition = statusVM.Position;
                    var listStatus = await _context.Status
                        .Where(x => x.Position > currentPosition)
                        .ToListAsync();
                    if (listStatus != null && listStatus.Count > 0)
                    {
                        foreach (var item in listStatus)
                        {
                            item.Position -= 1;
                        }
                    }
                    //=== Remove Status ====//
                    _context.Status.Remove(statusVM);
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
        public IActionResult ReloadStatusList()
        {

            return ViewComponent(nameof(StatusList));
        }

    }
}
