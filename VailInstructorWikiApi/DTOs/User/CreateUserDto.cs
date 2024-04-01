using System;
using System.ComponentModel.DataAnnotations;
using VailInstructorWikiApi.Enums;

namespace VailInstructorWikiApi.DTOs.User
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}

