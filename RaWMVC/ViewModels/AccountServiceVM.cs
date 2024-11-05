using Microsoft.AspNetCore.Mvc;
using RaWMVC.Areas.Identity.Data;

namespace RaWMVC.ViewModels
{
    [Bind("User, Role")]
    public class AccountServiceVM
    {
        public RaWMVCUser User { get; set; }
        public string Role { get; set; }
    }
}
