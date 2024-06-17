using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OutOfOffice.Models
{

	public enum AbsenseReason
	{
		Vacation,
        [Display(Name = "Sick Leave")]
        SickLeave,
        [Display(Name = "Family Event")]
        FamilyEvent
	}

	public enum RequestStatus
	{
        [Display(Name = "New")]
        New,
        [Display(Name = "Submitted")]
        Submitted,
        [Display(Name = "Rejected")]
        Rejected,
        [Display(Name = "Approved")]
        Approved,
        [Display(Name = "Cancelled")]
        Cancelled
	}

	public class LeaveRequest
	{
		public int Id { get; set; }

		public Employee? Employee { get; set; }

		public int EmployeeId { get; set; }

		public AbsenseReason AbsenseReason { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

		public string? Comment { get; set; }

		public RequestStatus LeaveRequestStatus { get; set; }

		public DateTime SubmittedTime { get; set; }

		public DateTime LastStatusChange { get; set; }

    }
}

