using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Areas.Identity.Data;
using RaWMVC.Commons;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using RaWMVC.Enums;
using RaWMVC.ViewComponents;
using RaWMVC.ViewModels;
using System.Diagnostics.Eventing.Reader;

namespace RaWMVC.Controllers
{
    public class StoryController : Controller
    {
        private readonly UserManager<RaWMVCUser> _userManager;
        private readonly RaWDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public StoryController(RaWDbContext context, IWebHostEnvironment environment, UserManager<RaWMVCUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }
        [Authorize(Roles = "Author")]
        //=============== Index ===============//
        public async Task<IActionResult> Index()
        {
            var story = await _context.Stories
                .Include(s => s.Chapters)
                .Include(s => s.Medium)
                .OrderBy(s => s.Position)
                .ToListAsync();

            return View(story);
        }

        [Authorize(Roles = "Author")]
        //=============== Create ===============//
        public async Task<IActionResult> ReportStory()
        {
            //ViewBag.Tags = await GetTagsSelectList();
            //ViewBag.Status = await GetStatusSelectList();
            //ViewBag.Genres = await GetGenreSelectList();
            await PopulateViewBagsAsync();

            return PartialView(nameof(ReportStory));
        }

        [Authorize(Roles = "Author")]
        [HttpPost]
        public async Task<IActionResult> Create(StoryViewModel storyVM)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    TempData["Message"] = "User not logged in or content is empty.";
                    return Json(new { success = false, messageFail = "User not logged in." });
                }

                var fileImage = await SaveMedia(storyVM.CoverImage);

                if (fileImage == null)
                {
                    //ViewBag.Tags = await GetTagsSelectList();
                    //ViewBag.Status = await GetStatusSelectList();
                    //ViewBag.Genres = await GetGenreSelectList();
                    await PopulateViewBagsAsync();

                    return Json(new { success = false, messageFail = "You must add a file cover image." });
                }

                var countStory = await _context.Stories.CountAsync();
                var newStory = new Story
                {
                    StoryTitle = storyVM.StoryTitle.Trim(),
                    StoryDescription = storyVM.StoryDescription?.Trim(),
                    StatusId = storyVM.StatusId,
                    TagId = storyVM.TagId,
                    GenreId = storyVM.GenreId,
                    PublishDate = DateTime.Now,
                    UserId = user.Id,
                    Username = user.UserName,
                    MediumId = fileImage.Id,
                    Position = countStory + 1
                };
                _context.Stories.Add(newStory);
                await _context.SaveChangesAsync();

                TempData["MessageSuccess"] = "Added story successfully";

                return Json(new { success = true, messageSuccess = "Added story successfully" });
            }
            catch(Exception ex)
            {

                var innerException = ex.InnerException?.Message ?? ex.Message;
                TempData["MessageFail"] = $"Failed to add story: {innerException}";

                await PopulateViewBagsAsync();

                return Json(new { success = false, messageFail = "Failed to add story.{innerException}" });
            }
        }

        [Authorize(Roles = "Author")]
        //=============== Edit ===============//
        //GET: StoryController/Edit/27
        public async Task<IActionResult> Edit(Guid idStory)
        {
            var story = await _context.Stories
                .Include(s => s.Chapters)
                .Where(s => s.StoryId.Equals(idStory))
                .SingleOrDefaultAsync();

            if (story == null) return BadRequest();

            var storyVM = new StoryViewModel
            {
                StoryId = story.StoryId,
                StoryTitle = story.StoryTitle,
                StoryDescription = story.StoryDescription,
                TagId = story.TagId,
                GenreId = story.GenreId,
                StatusId = story.StatusId,
            };

            await PopulateViewBagsAsync();

            return PartialView(nameof(Edit), storyVM);
        }

        [Authorize(Roles = "Author")]
        //POST: StoryController/Edit/12
        [HttpPost]
        public async Task<IActionResult> Edit(StoryViewModel storyVM)
        {
            try
            {
                var story = await _context.Stories.FindAsync(storyVM.StoryId);

                if(story == null) return BadRequest();

                var fileImage = await SaveMedia(storyVM.CoverImage);

                if (fileImage == null)
                {
                    await PopulateViewBagsAsync();

                    return Json(new { success = false, messageFail = "You must add a file cover image." });
                }

                story.StoryTitle = storyVM.StoryTitle;
                story.StoryDescription = storyVM.StoryDescription;
                story.GenreId = storyVM.GenreId;
                story.TagId = storyVM.TagId;
                story.StatusId = storyVM.StatusId;
                //story.MediumId = fileImage.Id;

                if (fileImage != null)
                {
                    story.MediumId = fileImage.Id;
                }
                _context.Update(story);
                await _context.SaveChangesAsync();

                TempData["MessageSuccess"] = "Story edited successfully";

                return PartialView("Edit", storyVM);
            }
            catch
            {
                TempData["MessageFail"] = "Failed to edited story";

                return PartialView("Edit", storyVM);
            }

        }

        [Authorize(Roles = "Author")]
        //=============== Delete ===============//
        // POST: StoryController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid idStory)
        {
            var story = false;
            var message = "Not yet implemented!!!";
            try
            {
                //=== Predicate/delgate ===//
                var storyVM = await _context.Stories
                    .Where(t => t.StoryId.Equals(idStory))
                    .SingleOrDefaultAsync();

                if (storyVM != null)
                {
                    //=== Decreasement Position ===//
                    var currentPosition = storyVM.Position;
                    var listStory = await _context.Stories
                        .Where(x => x.Position > currentPosition)
                        .ToListAsync();

                    if (listStory != null && listStory.Count > 0)
                    {
                        foreach (var item in listStory)
                        {
                            item.Position -= 1;
                        }
                    }
                    //=== Remove Status ====//
                    _context.Stories.Remove(storyVM);
                }
                await _context.SaveChangesAsync();

                story = true;
            }
            catch
            {
                message = "Execution error!!!";
            }

            return Json(new { story, message });
        }

        [Authorize(Roles = "Author")]
        public IActionResult ReloadStoryList()
        {

            return ViewComponent(nameof(StoryList));
        }
        //=============== Detail ===============//
        public async Task<IActionResult> Detail(Guid idStory)
        {
            var suggestStories = await _context.Stories
                .Take(3)
                .Select(c => new StoryViewModel
                {
                    StoryId = c.StoryId,
                    StoryTitle = c.StoryTitle,
                    StoryDescription = c.StoryDescription,
                    Medium = new Medium
                    {
                        FileName = c.Medium.FileName,
                        Extension = c.Medium.Extension
                    }
                })
                .ToListAsync();

            var story = await _context.Stories
                .Include(s => s.Chapters)
                .Include(s => s.Medium)
                .Include(s => s.Status)
                .Include(s => s.Genre)
                .Include(s => s.Tag)
                .Where(s => s.StoryId.Equals(idStory))
                .FirstOrDefaultAsync();


            var chapterIds = story.Chapters.Select(c => c.ChapterId).ToList();

            var chapterReadCounts = await _context.ChapterReadCounts
                .Where(cr => chapterIds.Contains(cr.ChapterId))
                .GroupBy(cr => cr.ChapterId)
                .Select(g => new
                {
                    ChapterId = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var user = await _userManager.GetUserAsync(User);

            var readingLists = await _context.ReadingLists
                .Where(rl => rl.UserId == user.Id)
                .Select(rl => new ReadingList
                {
                    ReadingListsId = rl.ReadingListsId,
                    Name = rl.Name
                }).ToListAsync();

            var totalReadCount = chapterReadCounts.Sum(c => c.Count);

            var currentListId = await _context.ReadingLists
                .Where(rl => rl.UserId == user.Id && rl.Name == "Current List") 
                .Select(rl => rl.ReadingListsId)
                .FirstOrDefaultAsync();

            var storyVM = new StoryViewModel
            {
                StoryId = story.StoryId,
                StoryTitle = story.StoryTitle,
                StoryDescription = story.StoryDescription,
                SuggestedStories = suggestStories,
                TotalReadCount = totalReadCount,
                ReadingLists = readingLists,
                UserId = story.UserId,
                Username = story.Username,
                ProfilePicture = user.ProfilePicture,
                CurrentListId = currentListId,
                StatusName = story.Status.StatusName,
                GenreName = story.Genre.GenreName,
                TagName = story.Tag.TagName,
                Medium = new Medium
                {
                    FileName = story.Medium.FileName,
                    Extension = story.Medium.Extension
                },
                Chapters = story.Chapters
                    .Where(c => c.IsPublished)
                    .Select(c => new ChapterViewModel
                    {
                        ChapterId = c.ChapterId,
                        ChapterTitle = c.ChapterTitle,
                        Position = c.Position,
                        PublishDate = c.PublishDate,
                        IsPublished = c.IsPublished,
                    })
                    .OrderBy(c => c.PublishDate)
                    .ToList()
            };

            return View(storyVM);
        }

        [Authorize(Roles = "Author")]
        //=============== List Chapter ===============//
        public async Task<IActionResult> ListChapter(Guid idStory)
        {
            if (idStory == Guid.Empty)
            {
                return BadRequest("Invalid story ID.");
            }

            var story = await _context.Stories
                .Include(s => s.Chapters)
                .Where(s => s.StoryId.Equals(idStory))
                .Select(s => new StoryViewModel
                {
                    StoryId = s.StoryId,
                    StoryTitle = s.StoryTitle,
                    Chapters = s.Chapters.Select(c => new ChapterViewModel
                    {
                        ChapterId = c.ChapterId,
                        ChapterTitle = c.ChapterTitle,
                        Position = c.Position,
                        PublishDate = c.PublishDate,
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (story == null) return BadRequest();

            return View(story); 
        }

        //=============== Get Data List ===============//
        private async Task<SelectList> GetTagsSelectList()
        {
            var listTag = await _context.Tags
                .Select(t => new
                {
                    TagId = t.TagId,
                    TagName = t.TagName,
                })
                .ToListAsync();
            return new SelectList(listTag, "TagId", "TagName");
        }
        private async Task<SelectList> GetStatusSelectList()
        {
            var listStatus = await _context.Status
                .Select(s => new
                {
                    StatusId = s.StatusId,
                    StatusName = s.StatusName,
                })
                .ToListAsync();
            return new SelectList(listStatus, "StatusId", "StatusName");
        }
        private async Task<SelectList> GetGenreSelectList()
        {
            var listGenre = await _context.Genres
                .Select(t => new
                {
                    GenreId = t.GenreId,
                    GenreName = t.GenreName,
                })
                .ToListAsync();
            return new SelectList(listGenre, "GenreId", "GenreName");
        }
        private async Task PopulateViewBagsAsync()
        {
            ViewBag.Tags = await GetTagsSelectList();
            ViewBag.Status = await GetStatusSelectList();
            ViewBag.Genres = await GetGenreSelectList();
        }

        //=============== Save Media ===============//
        private async Task<Medium> SaveMedia(IFormFile? file)
        {
            bool isOk = false;
            if (file == null || file.Length == 0)
            {
                TempData["Message"] = "You must to add file cover image";
                return null;
            }

            var fileGuidName = Guid.NewGuid().ToString();
            var fileName = "";
            var fileExtension = "";
            var fileNameString = file.FileName;

            if (string.IsNullOrEmpty(fileNameString))
            {
                TempData["Message"] = "Invalid file name.";
                return null;
            }

            try
            {
                string[] arrayExtension = fileNameString.Split('.');
                var fullFileName = "";
                if (arrayExtension != null && arrayExtension.Length > 0)
                {
                    for (int i = 0; i < arrayExtension.Length; i++)
                    {
                        var ext = arrayExtension[i];
                        if (Constants.Invalid_Extenstion.Contains(ext))
                        {
                            return null;
                        }
                    }
                    fileName = arrayExtension[0];
                    fileExtension = arrayExtension[arrayExtension.Length - 1];
                    //=== Check if the file is valid ===//
                    if (!Constants.Valid_Extenstion.Contains(fileExtension))
                    {
                        return null;
                    }
                    fullFileName = fileGuidName + "." + fileExtension;
                }
                var webRoot = _environment.WebRootPath.Normalize();
                var physicalMusicPath = Path.Combine(webRoot, "storyImg/");

                if (!Directory.Exists(physicalMusicPath))
                {
                    Directory.CreateDirectory(physicalMusicPath);
                }
                var physicalPath = Path.Combine(physicalMusicPath, fullFileName);
                using (var stream = System.IO.File.Create(physicalPath))
                {
                    await file.CopyToAsync(stream);
                }
                // === Tạo media ===//
                var count = await _context.Media.CountAsync();
                var newMedium = new Medium
                {
                    Name = fileName,//Tên tìm kiếm
                    FileName = fileGuidName,// Tên lưu trữ
                    Extension = fileExtension,
                    Type = MediaTypeEnum.Audio,
                    Position = count + 1,
                };
                _context.Media.Add(newMedium);
                isOk = true;
                return newMedium;
            }
            catch
            {
            }
            return null;
        }
    }
}
