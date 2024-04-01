using System;
using System.ComponentModel.DataAnnotations;

namespace VailInstructorWikiApi.DTOs.User
{
    public class LoginDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Password { get; set; }
    }
}

