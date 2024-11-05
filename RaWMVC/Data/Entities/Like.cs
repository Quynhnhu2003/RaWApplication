namespace RaWMVC.Data.Entities
{
    public class Like
    {
        public Guid LikeId{ get; set; }
        public Guid ChapterId{ get; set; }
        public string UserId{ get; set; }
        public int LikeCount{ get; set; }
    }
}
