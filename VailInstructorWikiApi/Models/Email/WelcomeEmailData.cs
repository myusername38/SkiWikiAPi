using System;
namespace VailInstructorWikiApi.Models
{
	public class WelcomeEmailData : TemplateData
	{
        public string? Name { get; set; }
        public string? ActionUrl { get; set; }
    }
}

