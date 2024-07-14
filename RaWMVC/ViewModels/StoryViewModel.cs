using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RaWMVC.ViewModels
{
    [Bind("storyId, storyTitle, storyDescription, coverImage, publishDate")]
    public class StoryViewModel
    {
        public Guid storyId { get; set; }
        [Required(ErrorMessage = "Do not leave blank!!!")]
        [MaxLength(200, ErrorMessage = "Only allowed to contain 200 characters!!!")]
        public string storyTitle { get; set; }
        [MaxLength(500, ErrorMessage = "Only allowed to contain 500 characters!!!")]
        public string storyDescription { get; set; }
        public IFormFile coverImage { get; set; }
        public DateTime publishDate { get; set; }
    }
}
