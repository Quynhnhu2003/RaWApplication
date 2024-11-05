namespace RaWMVC.ViewModels
{
    public class PostViewModel
    {
        public Guid PostId { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }
        public string PostContent { get; set; }
        public DateTime CreateOn { get; set; }
        public List<ReplyViewModel> Replies { get; set; }
    }
}
