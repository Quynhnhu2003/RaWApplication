using Microsoft.AspNetCore.Mvc;
using RaWMVC.Data.Entities;

namespace RaWMVC.ViewModels
{
    public class CommentViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public Guid CommentId { get; set; }
        public string? CommentContent { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid ChapterId { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}


