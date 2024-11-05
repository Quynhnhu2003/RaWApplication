using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using RaWMVC.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace RaWMVC.ViewComponents
{
    public class ChapterList : ViewComponent
    {
        private readonly RaWDbContext _context;

        public ChapterList(RaWDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid idStory)
        {
            var story = await GetStoryWithChaptersAsync(idStory);

            var storyVM = new StoryViewModel
            {
                StoryId = story.StoryId,
                StoryTitle = story.StoryTitle,
                Chapters = story.Chapters.Select(c => new ChapterViewModel
                {
                    ChapterId = c.ChapterId,
                    //Position = c.Position,
                    ChapterTitle = c.ChapterTitle,
                    PublishDate = c.PublishDate
                }).ToList()
            };

            return View(storyVM);
        }

        private async Task<Story> GetStoryWithChaptersAsync(Guid idStory)
        {
            return await _context.Stories
                .Include(s => s.Chapters)
                .Where(s => s.StoryId.Equals(idStory))
                .OrderBy(s => s.Position)
                .FirstOrDefaultAsync();
        }
    }
}
