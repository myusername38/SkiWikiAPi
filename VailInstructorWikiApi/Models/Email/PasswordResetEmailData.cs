using System;
namespace VailInstructorWikiApi.Models
{
    public class PasswordResetEmailData : TemplateData
    {
        public string? Name { get; set; }
        public string? ActionUrl { get; set; }
    }
}

