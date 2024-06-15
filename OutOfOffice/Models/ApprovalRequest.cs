using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOffice.Models
{
	public class ApprovalRequest
	{
		public int ID { get; set; }

		public Employee? Employee { get; set; }

		[ForeignKey("Approver")]
		public int EmployeeId { get; set; }

        public LeaveRequest? LeaveRequest { get; set; }

        public int LeaveRequestId { get; set; }

		public RequestStatus ApprovalRequestStatus { get; set; }

		public string? Comment { get; set; }

        public DateTime LastStatusChange { get; set; }
    }
}

