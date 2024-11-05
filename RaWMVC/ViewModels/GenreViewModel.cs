using Microsoft.AspNetCore.Mvc;
using RaWMVC.Commons;
using System.ComponentModel.DataAnnotations;

namespace RaWMVC.ViewModels
{
    [Bind("GenreId, GenreName, GenreDescription")]
    public class GenreViewModel
    {
        public Guid GenreId { get; set; }
        [MaxLength(Constants.MAXLENGTH_EntitiesName)]
        public string GenreName { get; set; }
        [MaxLength(Constants.MAXLENGTH_EntitiesDescription)]
        public string? GenreDescription { get; set; }
        public int Position { get; set; }
    }
}
