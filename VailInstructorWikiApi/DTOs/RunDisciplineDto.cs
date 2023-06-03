using System;
using VailInstructorWikiApi.Enums;

namespace VailInstructorWikiApi.DTOs
{
	public class RunDisciplineDto
	{
        public Discipline Discipline { get; set; }

        public string Description { get; set; }

        public int RunId { get; set; }

        public int LevelId { get; set; }
    }
}

