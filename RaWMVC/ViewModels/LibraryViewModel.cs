using Microsoft.AspNetCore.Mvc;
using RaWMVC.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace RaWMVC.ViewModels
{
    [Bind("Id, LibraryId, StoryId, StoryTitle, IsInMyLibrary, IsInReadingList")]
    public class LibraryViewModel
    {
        public Guid LibraryId { get; set; }
        [Required]
        public Guid StoryId { get; set; }
        public List<StoryViewModel> Stories { get; set; }
        public string Id { get; set; }
        public string StoryTitle { get; set; }
        public string? Description { get; set; }
        public bool IsInMyLibrary { get; set; } = true;
        public bool IsInReadingList { get; set; } = true;

        public Medium Media { get; set; }
    }
}
