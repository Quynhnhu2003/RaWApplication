using RaWMVC.Enums;

namespace RaWMVC.Data.Entities
{
    public class StoryImage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public string Description { get; set; }
        // tên lưu trữ đã thay 
        public string FileName { get; set; }
        public string Extension { get; set; } //mp3, mp4
        public MediaTypeEnum Type { get; set; } //image, video clip,... 
        public int Position { get; set; }

    }
}
