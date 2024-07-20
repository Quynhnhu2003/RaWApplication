using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using RaWMVC.ViewComponents;
using RaWMVC.ViewModels;

namespace RaWMVC.Controllers
{
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
					genreName = genreVM.genreName.Trim(),
					genreDescription = genreVM.genreDescription?.Trim(),
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
                .Where(g => g.genreId.Equals(idGenre))
                .Select(a => new GenreViewModel
                {
                    genreId = a.genreId,
                    genreName = a.genreName,
                    genreDescription = a.genreDescription,
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
				var genre = await _context.Genres.FindAsync(genreVM.genreId);
				if (genre == null) return BadRequest();

				genre.genreName = genreVM.genreName.Trim();
				genre.genreDescription = genreVM.genreDescription?.Trim();

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
                    .Where(t => t.genreId.Equals(idGenre))
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
        public IActionResult ReloadGenreList()
        {

            return ViewComponent(nameof(GenreList));
        }
    }
}
