using System;
namespace VailInstructorWikiApi.Models
{
	public class RunDisciplineDrill
	{
		public int Id { get; set; }

        public int RunDisciplineId { get; set; }

        public virtual RunDiscipline RunDiscipline { get; set; }

        public int DrillId { get; set; }

		public virtual Drill Drill { get; set; }
	}
}

