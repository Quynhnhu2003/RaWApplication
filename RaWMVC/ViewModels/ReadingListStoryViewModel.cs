using RaWMVC.Data.Entities;

namespace RaWMVC.ViewModels
{
    public class ReadingListStoryViewModel
    {
        public Guid ReadingListsId { get; set; }
        public Guid StoryId { get; set; }
        public string StoryTitle { get; set; }
        public string StoryDescription { get; set; }
        public string StatusName { get; set; }
        public Medium Medium { get; set; }
        public string UserId{ get; set; }
        public string Username { get; set; }

    }
}
