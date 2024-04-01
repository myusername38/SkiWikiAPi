using System;
using System.ComponentModel.DataAnnotations;
using VailInstructorWikiApi.Enums;

namespace VailInstructorWikiApi.Models
{
	public class Drill
	{
		public int Id { get; set; }

		public Discipline Discipline { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public virtual ICollection<RunDisciplineDrill> RunDisciplineDrills { get; set; }
    }
}

