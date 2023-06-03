using System;
using VailInstructorWikiApi.Enums;

namespace VailInstructorWikiApi.DTOs
{
	public class LevelDto
	{
        public Discipline Discipline { get; set; }

        public int LevelNumber { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }
}

