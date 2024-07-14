namespace RaWMVC.Data.Entities
{
    public class Tag
    {
        public Tag()
        {
            tagId = Guid.NewGuid();
        }
        public Guid tagId { get; set; }
        public string tagName { get; set; }
        public string tagDescription { get; set; }
        public int Position { get; set; }
    }
}
