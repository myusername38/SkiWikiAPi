using System;
using System.ComponentModel.DataAnnotations;
using VailInstructorWikiApi.Enums;

namespace VailInstructorWikiApi.Models
{
	public class Level
	{
		public int Id;

		public Discipline Discpline;

		public int LevelNumber;

		public string Description;

		public string Url;

		public virtual ICollection<Skill> Skills { get; set; }
    }
}

