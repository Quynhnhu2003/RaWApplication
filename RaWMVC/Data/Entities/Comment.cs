namespace RaWMVC.Data.Entities
{
    public class Comment
    {
        public string UserId{ get; set; }
        public string Username { get; set; }
        public Guid CommentId{ get; set; }
        public string CommentContent{ get; set; }
        public DateTime CreateAt{ get; set; }
        public Guid ChapterId { get; set; }
        public Chapter Chapter { get; set; }
    }
}
