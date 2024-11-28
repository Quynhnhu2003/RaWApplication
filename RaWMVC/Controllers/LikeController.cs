﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RaWMVC.Areas.Identity.Data;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace RaWMVC.Controllers
{
    public class LikeController : Controller
    {
        private readonly RaWDbContext _context;
        private readonly UserManager<RaWMVCUser> _userManager;
        public LikeController(RaWDbContext context, UserManager<RaWMVCUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> ToggleLike(Guid chapterId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "User not authenticated." });
            }

            try
            {
                // Check if the user has liked this chapter
                var existingLike = await _context.Like
                    .FirstOrDefaultAsync(l => l.ChapterId == chapterId && l.UserId == user.Id);

                if (existingLike != null)
                {
                    // If liked, then remove the like
                    _context.Like.Remove(existingLike);
                }
                else
                {
                    // Add a new like
                    var like = new Like
                    {
                        LikeId = Guid.NewGuid(),
                        UserId = user.Id,
                        ChapterId = chapterId,
                    };

                    var chapter = await _context.Chapters
                        .Include(c => c.Story) // Include the Story entity
                        .FirstOrDefaultAsync(c => c.ChapterId == chapterId);

                    if (chapter != null)
                    {
                        // Create a notification for the story author
                        var notification = new Notification
                        {
                            NotificationId = Guid.NewGuid(),
                            UserId = chapter.Story.UserId,
                            Username = user.UserName,
                            Message = $"{user.UserName} liked your chapter '{chapter.ChapterTitle}' in the story '{chapter.Story.StoryTitle}'",
                            CreatedDate = DateTime.Now
                        };
                        _context.Notifications.Add(notification);
                    }

                    _context.Like.Add(like);
                }

                await _context.SaveChangesAsync();

                // Recount the number of likes for the chapter
                var likeCount = await _context.Like.CountAsync(l => l.ChapterId == chapterId);

                return Json(new { success = true, likeCount, isLiked = existingLike == null });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes if needed
                return Json(new { success = false, message = "An error occurred while toggling the like." });
            }
        }
    }
}