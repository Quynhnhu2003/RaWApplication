using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RaWMVC.Areas.Identity.Data;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using System.Composition;

namespace RaWMVC.Controllers
{
    public class PostController : Controller
    {
        private readonly RaWDbContext _context;
        private readonly UserManager<RaWMVCUser> _userManager;

        public PostController(RaWDbContext context, UserManager<RaWMVCUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
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

        [HttpPost]
        public async Task<IActionResult> Delete(Guid idPost)
        {
            if (idPost == Guid.Empty)
            {
                return Json(new { success = false, message = "Post ID cannot be empty." });
            }

            var user = await _userManager.GetUserAsync(User);
            var post = await _context.Posts.FindAsync(idPost);

            if (post == null)
            {
                return Json(new { success = false, message = "Post not found." });
            }

            //=== Optionally check if the user is authorized to delete this post ===//
            if (post.UserId != user.Id)
            {
                return Json(new { success = false, message = "You do not have permission to delete this post." });
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "The post has been deleted successfully." });
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(Guid postId, string content)
        {
            var user = await _userManager.GetUserAsync(User);

            if (string.IsNullOrWhiteSpace(content))
            {
                ModelState.AddModelError("", "Comment content cannot be left blank.");
                return RedirectToAction("Index", new { userId = user.Id }); // Redirect lại về trang profile
            }

            var postExists = await _context.Posts
                .Where(p => p.PostId == postId)
                .Select(p => new { p.UserId }) // Get the userId of the post owner
                .FirstOrDefaultAsync();

            if (postExists == null)
            {
                ModelState.AddModelError("", "Post does not exist.");
                return RedirectToAction("Index", "Profile", new { userId = postExists.UserId });
            }

            var reply = new Reply
            {
                ReplyId = Guid.NewGuid(),
                PostId = postId,
                ReplyContent = content,
                CreateAt = DateTime.Now,
                UserId = user.Id,
                ProfilePicture = user.ProfilePicture,
                Username = user.UserName
            };

            _context.Replies.Add(reply);

            // Create a notification for the post owner
            var notification = new Notification
            {
                UserId = postExists.UserId, // UserId of the post owner
                Username = user.UserName, // UserName of the reply owner
                Message = $"{user.UserName} replied to your post.",
                CreatedDate = DateTime.Now
            };

            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Profile", new { userId = postExists.UserId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(Guid idPost, Guid idReply)
        {
            bool isDeleted = false;
            string message = "Comment not found.";

            try
            {
                var reply = await _context.Replies
                    .Where(c => c.PostId == idPost && c.ReplyId == idReply)
                    .SingleOrDefaultAsync();

                if (reply != null)
                {
                    var currentUser = await _userManager.GetUserAsync(User);

                    // Check comment deletion permissions (only allow creator or admin to delete)
                    if (reply.UserId == currentUser.Id || User.IsInRole("Admin"))
                    {
                        _context.Replies.Remove(reply);
                        await _context.SaveChangesAsync();

                        isDeleted = true;
                        message = "Comment deleted successfully.";
                    }
                    else
                    {
                        message = "You do not have permission to delete this comment.";
                    }
                }
            }
            catch (Exception ex)
            {
                message = "An error occurred while deleting the comment: " + ex.Message;
            }

            return Json(new { isDeleted, message });
        }

    }
}
