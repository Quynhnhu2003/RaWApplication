using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Areas.Identity.Data;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using RaWMVC.ViewComponents;
using RaWMVC.ViewModels;
using System.Composition;

namespace RaWMVC.Controllers
{
    public class CommentController : Controller
    {
        private RaWDbContext _context;
        private UserManager<RaWMVCUser> _userManager;

        public CommentController(RaWDbContext context, UserManager<RaWMVCUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return Json(new { success = false, message = "Post content cannot be empty." });
            }

            var user = await _userManager.GetUserAsync(User);
            var post = new Post
            {
                PostId = Guid.NewGuid(),
                UserId = user.Id,
                Username = user.UserName,
                ProfilePicture = user.ProfilePicture,
                PostContent = content,
                CreateOn = DateTime.Now
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            // Get all followers of the user
            var followers = await _context.Follows
               .Where(f => f.FolloweeId.ToString() == user.Id)
               .Select(f => f.FollowerId)
               .ToListAsync();

            // Create the profile link
            var profileLink = Url.Action("Index", "Profile", new { userId = post.UserId }, Request.Scheme);

            // Prepare notifications for followers
            foreach (var followerId in followers)
            {
                var notification = new Notification
                {
                    UserId = followerId.ToString(),
                    Username = user.UserName,
                    Message = $"{user.UserName} has posted a new update.",
                    Link = profileLink,
                    CreatedDate = DateTime.Now
                };

                _context.Notifications.Add(notification); // Add notification to the context
            }

            await _context.SaveChangesAsync(); // Save all changes, including notifications

            return Json(new { success = true, message = "The post has been posted on your profile." });
        }

        public async Task<IActionResult> Delete(Guid idComment, Guid chapterId)
        {
            bool isDeleted = false;
            string message = "Comment not found.";

            try
            {
                //=== Retrieve Comment ===//
                var comment = await _context.Comments
                    .Where(c => c.ChapterId.Equals(chapterId))
                    .SingleOrDefaultAsync(c => c.CommentId == idComment);

                if (comment != null)
                {
                    //=== Remove Comment ===//
                    _context.Comments.Remove(comment);
                    await _context.SaveChangesAsync();

                    isDeleted = true;
                    message = "Comment deleted successfully.";
                }
            }
            catch
            {
                message = "An error occurred while deleting the comment.";
            }

            return Json(new { isDeleted, message, chapterId });
        }
    }
}
