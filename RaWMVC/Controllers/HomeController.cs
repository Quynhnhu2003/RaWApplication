using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Areas.Identity.Data;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using RaWMVC.Models;
using RaWMVC.ViewModels;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RaWMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly RaWDbContext _context;
        private readonly UserManager<RaWMVCUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(RaWDbContext context, ILogger<HomeController> logger, UserManager<RaWMVCUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var suggestStories = await _context.Stories
                .OrderBy(c => Guid.NewGuid())
                .Take(5)
                .Select(c => new StoryViewModel
                {
                    StoryId = c.StoryId,
                    StoryTitle = c.StoryTitle,
                    StoryDescription = c.StoryDescription,
                    TagName = c.Tag.TagName,
                    GenreName = c.Genre.GenreName,
                    StatusName = c.Status.StatusName,
                    Medium = new Medium
                    {
                        FileName = c.Medium.FileName,
                        Extension = c.Medium.Extension
                    }
                })
                .ToListAsync();

            var hottestStory = await _context.Stories
                .Take(5)
                .Select(c => new StoryViewModel
                {
                    StoryId = c.StoryId,
                    StoryTitle = c.StoryTitle,
                    StoryDescription = c.StoryDescription,
                    TagName = c.Tag.TagName,
                    GenreName = c.Genre.GenreName,
                    StatusName = c.Status.StatusName,
                    Medium = new Medium
                    {
                        FileName = c.Medium.FileName,
                        Extension = c.Medium.Extension
                    }
                })
                .ToListAsync();

            var newestStory = await _context.Stories
                .OrderByDescending(c => c.PublishDate)
                .Take(5)
                .Select(c => new StoryViewModel
                {
                    StoryId = c.StoryId,
                    StoryTitle = c.StoryTitle,
                    StoryDescription = c.StoryDescription,
                    TagName = c.Tag.TagName,
                    GenreName = c.Genre.GenreName,
                    StatusName = c.Status.StatusName,
                    Medium = new Medium
                    {
                        FileName = c.Medium.FileName,
                        Extension = c.Medium.Extension
                    }
                })
                .ToListAsync();

            var completedStory = await _context.Stories
                .Where(s => s.Status.StatusName == "Completed")
                .Take(5)
                .Select(c => new StoryViewModel
                {
                    StoryId = c.StoryId,
                    StoryTitle = c.StoryTitle,
                    StoryDescription = c.StoryDescription,
                    TagName = c.Tag.TagName,
                    GenreName = c.Genre.GenreName,
                    StatusName = c.Status.StatusName,
                    Medium = new Medium
                    {
                        FileName = c.Medium.FileName,
                        Extension = c.Medium.Extension
                    }
                })
                .ToListAsync();

            var storyVM = new StoryViewModel
            {
                SuggestedStories = suggestStories,
                NewestStories = newestStory,
                HotestStories = hottestStory,
                CompletedStories = completedStory,
            };

            return View(storyVM);
        }

        public async Task<IActionResult> Search(string textSearch)
        {
            //=== Queryable ===//
            var storiesList = _context.Stories
            .Include(s => s.Tag)
            .Include(s => s.Genre)
            .AsQueryable();

            var userlist = _userManager.Users.AsQueryable();

            if (!String.IsNullOrEmpty(textSearch))
            {
                storiesList = storiesList.Where(m => m.StoryTitle.Contains(textSearch)
                                            || m.StoryDescription.Contains(textSearch)
                                            || m.Genre.GenreName.Contains(textSearch)
                                            || m.Tag.TagName.Contains(textSearch));
            }

            var stories = await storiesList.Select(s => new
            {
                storyTitle = s.StoryTitle,
            }).ToListAsync();

            // Return JSON data for JavaScript popover
            return Json(stories);
        }

        public async Task<IActionResult> FullSearch(string query, DateOnly? dateUpdated = null, 
            int chapterCountIndex = 0)
        {
            ViewData["SearchString"] = query;

            var searchResultViewModel = await GetDataStory(query, chapterCountIndex, dateUpdated);

            return View(searchResultViewModel);
        }

        private async Task<IActionResult> SearchResult(string query, int chapterCountIndex = 0,
           DateOnly? dateUpdated = null)
        {
            ViewData["SearchString"] = query;

            var searchResultViewModel = await GetDataStory(query, chapterCountIndex, dateUpdated);

            return PartialView(searchResultViewModel);
        }

        private async Task<SearchViewModel> GetDataStory(string query, int chapterCountIndex = 0,
           DateOnly? dateUpdated = null)
        {
            var arrayChapterCount = new[] { 0, 20, 40, 100 };
            //var user = await _userManager.GetUserAsync(User);

            //var storiesList = _context.Stories
            //    .Include(s => s.Status)
            //    .Include(s => s.Medium)
            //    .Include(s => s.Chapters)
            //    .Where(s => s.StoryTitle.Contains(query)
            //           || s.StoryDescription.Contains(query)
            //           || s.Genre.GenreName.Contains(query)
            //           || s.Tag.TagName.Contains(query)
            //           || s.Status.StatusName.Contains(query)) // Chỉ lọc các truyện có chứa query
            //    .AsQueryable();

            var storiesList = _context.Stories
                .Include(s => s.Status)
                .Include(s => s.Medium)
                .Include(s => s.Chapters)
                .AsQueryable();

            var userList = _userManager.Users
                .AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                storiesList = storiesList.Where(m => m.StoryTitle.Contains(query)
                                           || m.StoryDescription.Contains(query)
                                           || m.Genre.GenreName.Contains(query)
                                           || m.Tag.TagName.Contains(query)
                                           || m.Status.StatusName.Contains(query));

                userList = userList.Where(u => u.UserName.Contains(query) || u.Email.Contains(query));
            }

            if (chapterCountIndex > 0)
            {
                var index = chapterCountIndex;
                storiesList = storiesList.Where(s => s.Chapters.Count() <= arrayChapterCount[index]
                        && s.Chapters.Count() > arrayChapterCount[index - 1]);
            }

            if (dateUpdated.HasValue)
            {
                storiesList = storiesList.Where(s => DateOnly.FromDateTime(s.PublishDate) == dateUpdated);
            }

            var stories = await storiesList.Select(s => new StoryViewModel
            {
                StoryId = s.StoryId,
                StoryTitle = s.StoryTitle,
                StoryDescription = s.StoryDescription,
                PublishDate = s.PublishDate,
                GenreName = s.Genre.GenreName,
                TagName = s.Tag.TagName,
                StatusName = s.Status.StatusName,
                Username = s.Username,
                UserId = s.UserId,
                Medium = new Medium
                {
                    FileName = s.Medium.FileName,
                    Extension = s.Medium.Extension
                }
            }).ToListAsync();

            var users = await userList.Select(u => new AccountViewModel
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                ProfilePicture = u.ProfilePicture,
            }).ToListAsync();

            var searchResultViewModel = new SearchViewModel
            {
                Stories = stories,
                Users = users
            };

            return searchResultViewModel;
        }

        public IActionResult Profile()
        {
            return View();
        }

    }
}
