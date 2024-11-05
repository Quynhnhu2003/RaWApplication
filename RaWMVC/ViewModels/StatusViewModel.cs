using Microsoft.AspNetCore.Mvc;
using RaWMVC.Commons;
using System.ComponentModel.DataAnnotations;

namespace RaWMVC.ViewModels
{
    [Bind("StatusId, StatusName, StatusDescription")]
    public class StatusViewModel
    {
        public Guid StatusId { get; set; }
        [MaxLength(Constants.MAXLENGTH_EntitiesName)]
        public string? StatusName { get; set; }
        [MaxLength(Constants.MAXLENGTH_EntitiesDescription)]
        public string? StatusDescription { get; set; }
        public int Position { get; set; }
    }
}
