using Microsoft.AspNetCore.Mvc;
using RaWMVC.Commons;
using System.ComponentModel.DataAnnotations;

namespace RaWMVC.ViewModels
{
    [Bind("genreId, genreName, genreDescription")]
    public class GenreViewModel
    {
        public Guid genreId { get; set; }
        [MaxLength(Constants.MAXLENGTH_EntitiesName)]
        public string genreName { get; set; }
        [MaxLength(Constants.MAXLENGTH_EntitiesDescription)]
        public string? genreDescription { get; set; }
        public int Position { get; set; }
    }
}
