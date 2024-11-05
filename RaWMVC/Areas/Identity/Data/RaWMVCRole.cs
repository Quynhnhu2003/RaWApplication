using Microsoft.AspNetCore.Identity;

namespace RaWMVC.Areas.Identity.Data
{
    public class RaWMVCRole : IdentityRole
    {
        public string? Description{ get; set; }
        public int Position{ get; set; }
    }
}
