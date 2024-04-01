using System;
using VailInstructorWikiApi.Enums;

namespace VailInstructorWikiApi.Models
{
	public class Run
	{
		public int Id { get; set; }

		public string Name{ get; set; }

		public string Description { get; set; }

        public TrailRating TrailRating { get; set; }

        public int AreaId{ get; set; }

		public virtual Area Area{ get; set; }

		public virtual ICollection<RunDiscipline> RunDisciplines{ get; set; }
	}
}

