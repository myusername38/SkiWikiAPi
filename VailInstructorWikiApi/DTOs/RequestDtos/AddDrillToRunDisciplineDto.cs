using System;
using System.ComponentModel.DataAnnotations;

namespace VailInstructorWikiApi.DTOs.RequestDtos
{
	public class AddDrillToRunDisciplineDto
	{
        [Required(ErrorMessage = "RunDisciplineId is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "RunDisciplineId must be a non-negative integer.")]
        public int RunDisciplineId { get; set; }

        [Required(ErrorMessage = "DrillId is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "DrillId must be a non-negative integer.")]
        public int DrillId { get; set; }
    }
}

