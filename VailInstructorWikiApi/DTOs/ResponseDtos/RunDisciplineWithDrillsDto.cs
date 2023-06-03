using System;
using VailInstructorWikiApi.Models;

namespace VailInstructorWikiApi.DTOs.ResponseDtos
{
	public class RunDisciplineWithDrillsDto 
	{
		public RunDiscipline RunDiscipline { get; set; }
		public IEnumerable<Drill> Drills { get; set; }
    }
}

