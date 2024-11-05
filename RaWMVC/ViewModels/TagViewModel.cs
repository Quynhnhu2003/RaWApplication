using Microsoft.AspNetCore.Mvc;
using RaWMVC.Commons;
using System.ComponentModel.DataAnnotations;

namespace RaWMVC.ViewModels
{
    [Bind("TagId, TagName, TagDescription")]
    public class TagViewModel
    {
        public Guid TagId { get; set; }
        [MaxLength(Constants.MAXLENGTH_EntitiesName)]
        public string TagName { get; set; }
        [MaxLength(Constants.MAXLENGTH_EntitiesDescription)]
        public string? TagDescription { get; set; }
        public int Position { get; set; }
    }
}
