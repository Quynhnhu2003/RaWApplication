using Microsoft.AspNetCore.Mvc;

namespace RaWMVC.ViewModels
{
    [Bind("Id, UserId, Username, ProfilePicture, FollowerId, FolloweeId, FollowedOn")]
    public class FollowViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }
        public Guid Id { get; set; }
        public Guid FollowerId { get; set; }
        public Guid FolloweeId { get; set; }
        public DateTime FollowedOn { get; set; }
    }
}
