namespace RaWMVC.Data.Entities
{
    public class ReadingListStory
    {
        public Guid ReadingListId { get; set; }
        public ReadingList ReadingLists { get; set; }

        public Guid StoryId { get; set; }
        public Story Story { get; set; }
    }
}
