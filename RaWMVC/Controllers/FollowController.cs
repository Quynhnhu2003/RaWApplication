using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Areas.Identity.Data;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using RaWMVC.ViewModels;

namespace RaWMVC.Controllers
{
    public class FollowController : Controller
    {
        private readonly RaWDbContext _context;
        private readonly UserManager<RaWMVCUser> _userManager;

        public FollowController(RaWDbContext context, UserManager<RaWMVCUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFollow(Guid followeeId)
        {
            var followerId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(followerId))
            {
                return Unauthorized();
            }

            var existingFollow = await _context.Follows
                .FirstOrDefaultAsync(f => f.FollowerId == Guid.Parse(followerId) && f.FolloweeId == followeeId);

            if (existingFollow == null)
            {
                var follow = new Follow
                {
                    FollowerId = Guid.Parse(followerId),
                    FolloweeId = followeeId,
                    FollowedOn = DateTime.Now
                };

                _context.Follows.Add(follow);

                var notification = new Notification
                {
                    UserId = followeeId.ToString(), // User being followed
                    Username = User.Identity.Name,
                    Message = $"{User.Identity.Name} is now following you.",
                    CreatedDate = DateTime.Now
                };

                _context.Notifications.Add(notification);

                TempData["Message"] = "You are now following this user.";
            }
            else
            {
                _context.Follows.Remove(existingFollow);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Profile", new { userId = followeeId });
        }
    }
}
