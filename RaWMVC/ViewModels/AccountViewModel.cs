using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RaWMVC.ViewModels
{
	[Bind("Id, Email, Username, Password, ConfirmPassword, Role, ProfilePicture")]
	public class AccountViewModel
	{
        public string Id { get; set; }
        [Required]
		[EmailAddress]
		public string Email { get; set; }
		public string Username { get; set; }

        [Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
		public string? ProfilePicture{ get; set; }

		public string Role { get; set; }
		public IList<string> Roles { get; set; }
	}
}
