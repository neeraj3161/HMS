using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace HMS.Models
{
	public class LoginModal
	{
        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

    }
}