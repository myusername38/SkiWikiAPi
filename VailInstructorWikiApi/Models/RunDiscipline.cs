using System;
using VailInstructorWikiApi.DTOs;
using VailInstructorWikiApi.Enums;

namespace VailInstructorWikiApi.Models
{
	public class RunDiscipline
	{
		public int Id { get; set; }

		public Discipline Discipline { get; set; }

		public string Description { get; set; }

		public int RunId { get; set; }

		public virtual Run Run { get; set; }

		public int LevelId { get; set; }

		public virtual Level Level { get; set; }

		public virtual ICollection<RunDisciplineDrill> RunDisciplineDrills { get; set; }
    }
}

