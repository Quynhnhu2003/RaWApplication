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
    public class ReadingListController : Controller
    {
        private readonly RaWDbContext _context;
        private readonly UserManager<RaWMVCUser> _userManager;

        public ReadingListController(RaWDbContext context, UserManager<RaWMVCUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var readingLists = await _context.ReadingLists
                .Where(rl => rl.UserId == userId)
                .Include(rl => rl.ReadingListStories)
                .ThenInclude(rls => rls.Story)
                .ThenInclude(rls => rls.Medium)
                .ToListAsync();

            var viewModel = new ProfileViewModel
            {
                ReadingLists = readingLists
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            return PartialView(nameof(Create));
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var user = await _userManager.GetUserAsync(User);

            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(new { success = false, message = "Reading list name cannot be empty." });
            }

            //=== Check if reading list name already exists ===//
            var exists = await _context.ReadingLists
                .AnyAsync(rl => rl.Name.ToLower() == name.ToLower() && rl.UserId == user.Id);

            if (exists)
            {
                return Json(new { success = false, message = "Reading list name already exists." });
            }

            var readingList = new ReadingList
            {
                ReadingListsId = Guid.NewGuid(),
                UserId = user.Id,
                Name = name
            };

            await _context.ReadingLists.AddAsync(readingList);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Reading list created successfully!" });
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var readingList = await _context.ReadingLists.FindAsync(id);
            if (readingList == null)
            {
                return NotFound();
            }

            return PartialView(readingList);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, string name)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "You must be logged in to edit a reading list." });
            }

            var readingList = await _context.ReadingLists.FindAsync(id);
            if (readingList == null || readingList.UserId != user.Id)
            {
                return Json(new { success = false, message = "Reading list not found or you do not have permission to edit it." });
            }

            readingList.Name = name;
            _context.ReadingLists.Update(readingList);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Reading list updated successfully!" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "You must be logged in to delete a reading list." });
            }

            var readingList = await _context.ReadingLists.FindAsync(id);
            if (readingList == null || readingList.UserId != user.Id)
            {
                return Json(new { success = false, message = "Reading list not found or you do not have permission to delete it." });
            }

            _context.ReadingLists.Remove(readingList);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Reading list deleted successfully!" });
        }

        public async Task<IActionResult> ListStory(Guid idReadingList)
        {
            var user = await _userManager.GetUserAsync(User);

            //=== Query to get a list of stories in the current user's reading list ===//
            var listStory = await _context.ReadingListStories
                .Where(ls => ls.ReadingListId == idReadingList
                              && ls.ReadingLists.UserId == user.Id)
                .Include(ls => ls.Story)
                .ThenInclude(ls => ls.Medium)
                .Select(ls => new ReadingListStoryViewModel
                {
                    StoryId = ls.StoryId,
                    StoryTitle = ls.Story.StoryTitle,
                    StoryDescription = ls.Story.StoryDescription,
                    Medium = new Medium {
                        FileName = ls.Story.Medium.FileName,
                        Extension = ls.Story.Medium.Extension
                    }
                })
                .ToListAsync();

            // Kiểm tra nếu không có story nào được tìm thấy trong ReadingList
            if (!listStory.Any())
            {
                return NotFound("No stories found in this reading list.");
            }

            // Trả danh sách story về view
            return View(listStory);
        }


        [HttpPost]
        public async Task<IActionResult> AddStoryToList(Guid readingListId, Guid storyId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "You must be logged in to add a story to a reading list." });
            }

            var readingList = await _context.ReadingLists.FindAsync(readingListId);
            if (readingList == null || readingList.UserId != user.Id)
            {
                return Json(new { success = false, message = "Reading list not found or you do not have permission to add stories to it." });
            }

            // Kiểm tra nếu câu chuyện đã tồn tại trong danh sách đọc
            var exists = await _context.ReadingListStories.AnyAsync(rls => rls.ReadingListId == readingListId && rls.StoryId == storyId);
            if (exists)
            {
                return Json(new { success = false, message = "Story is already in the reading list." });
            }

            var readingListStory = new ReadingListStory
            {
                ReadingListId = readingListId,
                StoryId = storyId
            };

            await _context.ReadingListStories.AddAsync(readingListStory);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Story added to reading list successfully!" });
        }
    }
}
