﻿namespace RaWMVC.Data.Entities
{
    public class ScheduledDelete
    {
        public Guid ScheduledDeleteId { get; set; }
        public Guid StoryId { get; set; }
        public DateTime ScheduledTime { get; set; }
    }
}