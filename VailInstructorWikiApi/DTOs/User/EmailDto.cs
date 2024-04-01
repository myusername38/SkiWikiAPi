using System;
using System.ComponentModel.DataAnnotations;

namespace VailInstructorWikiApi.DTOs.User
{
    public class EmailDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}

