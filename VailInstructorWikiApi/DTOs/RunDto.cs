using System;
using VailInstructorWikiApi.Enums;

namespace VailInstructorWikiApi.DTOs
{
	public class RunDto
	{
        public string Name { get; set; }

        public string Description { get; set; }

        public TrailRating TrailRating { get; set; }

        public int AreaId { get; set; }
    }
}

