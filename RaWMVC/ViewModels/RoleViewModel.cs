using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RaWMVC.ViewModels
{
    [Bind("Id, Name, Description, Position")]
    public class RoleViewModel
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Position { get; set; }
    }
}
