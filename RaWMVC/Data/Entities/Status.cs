namespace RaWMVC.Data.Entities
{
    public class Status
    {
        public Status()
        {
            StatusId = Guid.NewGuid();
        }
        public Guid StatusId { get; set; }
        public string StatusName { get; set; }
        public string? StatusDescription { get; set; }
        public int Position { get; set; }
    }
}
