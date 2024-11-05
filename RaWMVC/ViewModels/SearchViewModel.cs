using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RaWMVC.Areas.Identity.Data;
using RaWMVC.Data.Entities;

namespace RaWMVC.ViewModels
{
    public class SearchViewModel
    {
        public string StoryTitle { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public List<StoryViewModel> Stories { get; set; }
        public List<AccountViewModel> Users { get; set; }

    }
}
