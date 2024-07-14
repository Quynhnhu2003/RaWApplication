using Microsoft.AspNetCore.Mvc;
using RaWMVC.Commons;
using System.ComponentModel.DataAnnotations;

namespace RaWMVC.ViewModels
{
    [Bind("tagId, tagName, tagDescription")]
    public class TagViewModel
    {
        public Guid tagId { get; set; }
        [MaxLength(Constants.MAXLENGTH_EntitiesName)]
        public string tagName { get; set; }
        [MaxLength(Constants.MAXLENGTH_EntitiesDescription)]
        public string? tagDescription { get; set; }
        public int Position { get; set; }
    }
}
