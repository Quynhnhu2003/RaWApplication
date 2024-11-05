using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using RaWMVC.ViewComponents;
using RaWMVC.ViewModels;

namespace RaWMVC.Controllers
{
    [Authorize(Roles = "Admintrator")]
    public class GenreController : Controller
	{
		private readonly RaWDbContext _context;
        public GenreController(RaWDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> Create(GenreViewModel genreVM)
		{
			try
			{
				var countGenre = await _context.Genres.CountAsync();
				var newGenre = new Genre
				{
					GenreName = genreVM.GenreName.Trim(),
					GenreDescription = genreVM.GenreDescription?.Trim(),
					Position = countGenre + 1,
				};
				_context.Genres.Add(newGenre);
				await _context.SaveChangesAsync();

				TempData["Message"] = "Genre added successfully";

				return RedirectToAction(nameof(Index));
			}
			catch
			{

				TempData["Message"] = "Failed to add genre!!!!";

				return View(nameof(Index));
			}
		}

        //GET: GenreController/Edit/5
        public async Task<IActionResult> Edit(Guid idGenre)
        {
            var genreVM = await _context.Genres
                .Where(g => g.GenreId.Equals(idGenre))
                .Select(a => new GenreViewModel
                {
                    GenreId = a.GenreId,
                    GenreName = a.GenreName,
                    GenreDescription = a.GenreDescription,
                })
                .SingleOrDefaultAsync();

            if (genreVM == null) return BadRequest();

            return View(nameof(Index), genreVM);
        }

        //POST: GenreController/Edit/5
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Genre genreVM) 
		{
			try
			{
				var genre = await _context.Genres.FindAsync(genreVM.GenreId);
				if (genre == null) return BadRequest();

				genre.GenreName = genreVM.GenreName.Trim();
				genre.GenreDescription = genreVM.GenreDescription?.Trim();

				await _context.SaveChangesAsync();

				TempData["Message"] = "Genre edited successfully";

				return RedirectToAction(nameof(Index), genreVM);
			}
			catch
			{
				TempData["Message"] = "Failed to edited genre!!!";

				return View(nameof(Index), genreVM);
			}
		}
        // POST: ArtistController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid idGenre)
        {
            var status = false;
            var message = "Not yet implemented!!!";
            try
            {
                //=== Predicate/delgate ===//
                var genre = await _context.Genres
                    .Where(t => t.GenreId.Equals(idGenre))
                    .SingleOrDefaultAsync();

                if (genre != null)
                {
                    //=== Decreasement Position ===//
                    var currentPosition = genre.Position;
                    var listGenre = await _context.Genres
                        .Where(x => x.Position > currentPosition)
                        .ToListAsync();
                    if (listGenre != null && listGenre.Count > 0)
                    {
                        foreach (var item in listGenre)
                        {
                            item.Position -= 1;
                        }
                    }
                    //=== Remove Genre ====//
                    _context.Genres.Remove(genre);
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
        public IActionResult ReloadGenreList(int currentPage = 1)
        {

            return ViewComponent(nameof(GenreList), new {currentPage});
        }
    }
}
