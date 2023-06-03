using System;
using System.ComponentModel.DataAnnotations;
using VailInstructorWikiApi.Enums;

namespace VailInstructorWikiApi.Models
{
	public class Drill
	{
		public int Id;

		public Discipline Discipline;

		public string Name;

		public string Url;

        public virtual ICollection<RunDisciplineDrill> RunDisciplineDrills { get; set; }
    }
}

