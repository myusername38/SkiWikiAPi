using System;
using VailInstructorWikiApi.Enums;

namespace VailInstructorWikiApi.DTOs
{
	public class SkillDto
	{
        public Discipline Discipline { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int LevelId { get; set; }
    }
}

