using System;
using VailInstructorWikiApi.Enums;

namespace VailInstructorWikiApi.DTOs
{
	public class DrillDto
	{
        public Discipline Discipline { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

    }
}

