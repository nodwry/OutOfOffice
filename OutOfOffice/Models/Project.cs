using System;
namespace OutOfOffice.Models
{
    public enum ProjectTypes
    {
        Billiable,
        NonBilliable
    }

    public enum ProjectStatus
    {
        Active,
        Inactive
    }

    public class Project
	{
        public int Id { get; set; }

        public ProjectTypes ProjectType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Comment { get; set; }

        public ProjectStatus Status { get; set; }

        //project manager

    }
}

