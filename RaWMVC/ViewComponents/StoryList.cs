using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using RaWMVC.ViewModels;

namespace RaWMVC.ViewComponents
{
    public class StoryList : ViewComponent
    {
        private readonly RaWDbContext _context;
        public StoryList(RaWDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var story = await GetStoryAsync();

            return View(story);
        }

        private async Task<List<StoryViewModel>> GetStoryAsync()
        {
            var stories = await _context.Stories
                .Include(s => s.Medium)
                .ToListAsync();

            var chapterCounts = await _context.Chapters
                .GroupBy(c => c.StoryId)
                .Select(g => new
                {
                    StoryId = g.Key,
                    PostedCount = g.Count(c => !c.IsDraft),
                    DraftCount = g.Count(c => c.IsDraft)
                })
                .ToListAsync();

            var result = stories.Select(story =>
            {
                var counts = chapterCounts.FirstOrDefault(c => c.StoryId == story.StoryId);
                return new StoryViewModel
                {
                    StoryId = story.StoryId,
                    StoryTitle = story.StoryTitle,
                    Medium = story.Medium,
                    PublishedChapter = counts?.PostedCount ?? 0,
                    DraftChapter = counts?.DraftCount ?? 0,
                    PublishDate = story.PublishDate
                };
            }).ToList();

            return result;
        }


    }
}
