using System;
namespace OutOfOffice.Models
{

	public enum AbsenseReason
	{
		Vacation,
		SickLeave,
		FamilyEvent
	}

	public enum RequestStatus
	{
		New,
		Rejected,
		Approved,
		Cancelled
	}

	public class LeaveRequest
	{
		public int ID { get; set; }

        public Employee Employee { get; set; }

        public int EmployeeId { get; set; }

		public AbsenseReason AbsenseReason { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

		public string? Comment { get; set; }

		public RequestStatus LeaveRequestStatus { get; set; } 

    }
}

