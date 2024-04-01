using System;
using System.ComponentModel.DataAnnotations;
using VailInstructorWikiApi.Enums;

namespace VailInstructorWikiApi.Models
{
	public class Level
	{
		public int Id { get; set; }

		public Discipline Discpline { get; set; }

        public int LevelNumber { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public virtual ICollection<RunDiscipline> RunDisciplines { get; set; }
    }
}

