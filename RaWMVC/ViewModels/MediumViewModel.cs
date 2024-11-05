namespace RaWMVC.ViewModels
{
    public class MediumViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string Extension{ get; set; }
        //public string Extension { get; set; } //mp3, mp4
        public int Position { get; set; }
    }
}
