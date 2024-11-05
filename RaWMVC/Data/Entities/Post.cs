namespace RaWMVC.Data.Entities
{
    public class Post
    {
        public Guid PostId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }
        public string PostContent { get; set; }
        public DateTime CreateOn { get; set; }
        public ICollection<Reply> Replies{ get; set; }
    }
}
