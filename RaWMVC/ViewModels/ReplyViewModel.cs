﻿using RaWMVC.Data.Entities;

namespace RaWMVC.ViewModels
{
    public class ReplyViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public Guid ReplyId { get; set; }
        public string ReplyContent { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid PostId { get; set; }

        public List<Reply> Replies { get; set; } = new List<Reply>();
    }
}