using System;
namespace OutOfOffice.Models
{

	public enum AbsenseReason
	{

	}

	public enum RequestStatus
	{
		New,
		Rejected,
		Approved
	}

	public class LeaveRequest
	{
		public int ID { get; set; }

		public int EmployeeId { get; set; }

		public AbsenseReason AbsenseReason { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

		public string? Comment { get; set; }

		public RequestStatus LeaveRequestStatus { get; set; } = RequestStatus.New;

    }
}

