namespace RaWMVC.Data.Entities
{
    public class Tag
    {
        public Tag()
        {
            TagId = Guid.NewGuid();
        }
        public Guid TagId { get; set; }
        public string TagName { get; set; }
        public string TagDescription { get; set; }
        public int Position { get; set; }

    }
}
