using System;
using VailInstructorWikiApi.Enums;

namespace VailInstructorWikiApi.Models
{
	public class Skill
	{
		public int Id { get; set; }

		public Discipline Discpline { get; set; }

		public string Description { get; set; }

		public string Url { get; set; }

		public int LevelId { get; set; }

		public virtual Level Level { get; set; }
    }
}

