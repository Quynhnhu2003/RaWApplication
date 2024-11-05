namespace RaWMVC.Data.Entities
{
    public class Notification
    {
        public Guid NotificationId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; } = false;

        public string? Link { get; set; }
    }
}
