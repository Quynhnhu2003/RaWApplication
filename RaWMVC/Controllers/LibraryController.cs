using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Areas.Identity.Data;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using RaWMVC.ViewModels;
using System.Security.Claims;

namespace RaWMVC.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private readonly RaWDbContext _context;
        private readonly ILogger _logger;
        private readonly UserManager<RaWMVCUser> _userManager;

        public LibraryController(RaWDbContext context, UserManager<RaWMVCUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Check if Libraries DbSet is initialized
            if (_context.Libraries == null)
            {
                throw new Exception("Libraries table is not initialized in the DbContext");
            }

            // Eagerly load related entities like Story and Medium
            var libraries = await _context.Libraries
                .Where(l => l.UserId == user.Id)
                .Include(l => l.Story)
                .ThenInclude(s => s.Medium)
                .ToListAsync();

            return View(libraries);
        }

        [HttpPost]
        public async Task<IActionResult> AddStory(Guid storyId, bool addToLibrary = true, Guid? readingListId = null)
        {
            var user = await _userManager.GetUserAsync(User);

            var story = await _context.Stories.FindAsync(storyId);
            if (story == null)
            {
                Console.WriteLine("Story not found.");
                return NotFound("Story not found.");
            }

            // Check if the story is already in the user's library
            var libraryEntry = await _context.Libraries
                .FirstOrDefaultAsync(l => l.StoryId == storyId && l.UserId == user.Id && l.InMyLibrary == true);

            var currentListId = await _context.ReadingLists
                .Where(rl => rl.UserId == user.Id && rl.Name == "Current List")
                .Select(rl => rl.ReadingListsId)
                .FirstOrDefaultAsync();

            if (libraryEntry == null)
            {
                // Add story to the library if not already there
                var newLibraryEntry = new Library
                {
                    Id = Guid.NewGuid().ToString(),
                    ReadingListsId = readingListId ?? currentListId,
                    StoryId = storyId,
                    UserId = user.Id,
                    InMyLibrary = true,
                    IsInReadingList = false
                };
                await _context.Libraries.AddAsync(newLibraryEntry);
            }

            // Add the story to the selected reading list
            if (readingListId != null)
            {
                var readingListEntry = await _context.ReadingListStories
                    .FirstOrDefaultAsync(rls => rls.StoryId == storyId && rls.ReadingListId == readingListId);

                if (readingListEntry == null)
                {
                    var newReadingListEntry = new ReadingListStory
                    {
                        ReadingListId = readingListId.Value,
                        StoryId = storyId
                    };
                    await _context.ReadingListStories.AddAsync(newReadingListEntry);
                }
            }

            await _context.SaveChangesAsync();
            return Ok("Story added to your library and selected reading list.");
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromLibrary(Guid storyId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var libraryEntry = await _context.Libraries
                .FirstOrDefaultAsync(l => l.StoryId == storyId && l.Id == user.Id);

            if (libraryEntry != null)
            {
                libraryEntry.InMyLibrary = false; // Mark as not in library
                                                  // You may also want to check if you need to remove it from the reading list
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
