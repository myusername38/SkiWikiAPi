using System;
using System.ComponentModel.DataAnnotations;

namespace VailInstructorWikiApi.DTOs.User
{
	public class VerifyEmailDto
	{
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; }
    }
}

