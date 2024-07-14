namespace RaWMVC.Data.Entities
{
    public class Status
    {
        public Status()
        {
            statusId = Guid.NewGuid();
        }
        public Guid statusId { get; set; }
        public string statusName { get; set; }
        public string statusDescription { get; set; }
        public int Position { get; set; }
    }
}
