﻿using System;
namespace VailInstructorWikiApi.Models
{
	public class Area
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public virtual ICollection<Run> Runs { get; set; }
    }
}

