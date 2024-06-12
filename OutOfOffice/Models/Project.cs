using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OutOfOffice.Models
{
    public enum ProjectTypes
    {
        Billiable,
        [Display(Name = "Non-billiable")]
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

        public Employee? ProjectManager { get; set; }

        public int ProjectManagerID { get; set; }

        public string? Comment { get; set; }

        public ProjectStatus Status { get; set; }

    }
}

