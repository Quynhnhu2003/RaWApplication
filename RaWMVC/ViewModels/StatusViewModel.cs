using Microsoft.AspNetCore.Mvc;
using RaWMVC.Commons;
using System.ComponentModel.DataAnnotations;

namespace RaWMVC.ViewModels
{
    [Bind("statusId, statusName, statusDescription")]
    public class StatusViewModel
    {
        public Guid statusId { get; set; }
        [MaxLength(Constants.MAXLENGTH_EntitiesName)]
        public string? statusName { get; set; }
        [MaxLength(Constants.MAXLENGTH_EntitiesDescription)]
        public string? statusDescription { get; set; }
        public int Position { get; set; }
    }
}
