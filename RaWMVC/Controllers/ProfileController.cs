using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Areas.Identity.Data;
using RaWMVC.Data;
using RaWMVC.Data.Entities;
using RaWMVC.ViewModels;

namespace RaWMVC.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly RaWDbContext _context;
        private readonly UserManager<RaWMVCUser> _userManager;

        public ProfileController(RaWDbContext context, UserManager<RaWMVCUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            //=== Get user information of profile ===//
            var profileUser = await _userManager.FindByIdAsync(userId);
            if (profileUser == null) return NotFound();

            //=== Check the user's following status ===//
            var isFollowing = await _context.Follows.AnyAsync(f =>
                f.FollowerId == Guid.Parse(currentUser.Id) && f.FolloweeId == Guid.Parse(userId));

            //=== Count the number of followers and the number of reading lists ===//
            var followersCount = await _context.Follows.CountAsync(f => f.FolloweeId == Guid.Parse(userId));
            var readingListCount = await _context.ReadingLists.CountAsync(rl => rl.UserId == userId);

            //=== Get ReadingList ===//
            var currentReadingStories = await _context.ReadingLists
                .Where(rl => rl.UserId == userId && rl.Name == "Current List")
                .SelectMany(rl => rl.ReadingListStories)
                .Select(s => new LibraryViewModel
                {
                    StoryId = s.StoryId,
                    StoryTitle = s.Story.StoryTitle,
                    Media = s.Story.Medium
                }).ToListAsync();

            //=== Get Follower List ==//
            var followers = await _context.Follows
                .Where(f => f.FolloweeId == Guid.Parse(userId))
                .Select(f => f.FollowerId)
                .ToListAsync();

            var followViewModels = await Task.WhenAll(followers.Select(async followerId =>
            {
                var followerUser = await _userManager.FindByIdAsync(followerId.ToString());
                return followerUser == null ? null : new FollowViewModel
                {
                    UserId = followerUser.Id,
                    Username = followerUser.UserName,
                    ProfilePicture = followerUser.ProfilePicture
                };
            }));

            var model = new ProfileViewModel
            {
                UserId = profileUser.Id,
                UserName = profileUser.UserName,
                AvatarUrl = profileUser.ProfilePicture,
                JoinedDate = profileUser.JoinedDate,
                IsFollowing = isFollowing,
                FollowersCount = followersCount,
                ReadingListCount = readingListCount,
                CurrentReadingStories = currentReadingStories,
                Introduction = profileUser.Introduction,
                ReadingLists = await _context.ReadingLists
                    .Where(rl => rl.UserId == userId)
                    .Select(rl => new ReadingList
                    {
                        ReadingListsId = rl.ReadingListsId,
                        Name = rl.Name,
                    }).ToListAsync(),
                Follows = followViewModels.Where(f => f != null).ToList(),
                Posts = await _context.Posts
                    .Where(p => p.UserId == userId)
                    .Select(p => new PostViewModel
                    {
                        PostId = p.PostId,
                        Username = currentUser.UserName,
                        ProfilePicture = profileUser.ProfilePicture,
                        PostContent = p.PostContent,
                        CreateOn = p.CreateOn,
                        Replies = _context.Replies
                            .Where(r => r.PostId == p.PostId)
                            .Select(r => new ReplyViewModel
                            {
                                ReplyId = r.ReplyId,
                                Username = r.Username,
                                ReplyContent = r.ReplyContent,
                                CreateAt = r.CreateAt,
                                PostId = p.PostId,
                                ProfilePicture = r.ProfilePicture
                            }).ToList()
                    }).ToListAsync(),
            };

            return View(model);
        }

        public async Task<IActionResult> FollowList(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            //=== Get a list of followers from the follow table ===// 
            var followers = await _context.Follows
                .Where(f => f.FolloweeId == Guid.Parse(userId))
                .Select(f => f.FollowerId)
                .ToListAsync();

            //=== Get information from RaWMVCUser based on each FollowerId ===//
            var followUsers = await Task.WhenAll(followers.Select(async followerId =>
            {
                var followerUser = await _userManager.FindByIdAsync(followerId.ToString());
                return followerUser == null ? null : new FollowViewModel
                {
                    UserId = followerUser.Id,
                    Username = followerUser.UserName,
                    ProfilePicture = followerUser.ProfilePicture
                };
            }));

            return PartialView("_FollowList", followUsers.Where(f => f != null).ToList());
        }
    }
}
