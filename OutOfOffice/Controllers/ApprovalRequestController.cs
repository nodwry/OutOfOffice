using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.Data;
using OutOfOffice.Models;

namespace OutOfOffice.Controllers
{
    public class ApprovalRequestController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public ApprovalRequestController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult ViewApprovalRequests()
        {
            var approvalRequests = _dbContext.ApprovalRequests.ToList();

            return View(approvalRequests);
        }

        [HttpPost]
        public IActionResult ApproveRequest(int approvalRequestId)
        {
            var approvalRequest = _dbContext.ApprovalRequests.Find(approvalRequestId);
            if (approvalRequest == null)
            {
                return NotFound();
            }

            approvalRequest.ApprovalRequestStatus = RequestStatus.Approved;
            approvalRequest.LastStatusChange = DateTime.Now;

            var leaveRequest = _dbContext.LeaveRequests.Find(approvalRequest.LeaveRequestId);
            leaveRequest.LeaveRequestStatus = RequestStatus.Approved;
            leaveRequest.LastStatusChange = DateTime.Now;
            
            _dbContext.UpdateRange(approvalRequest, leaveRequest);
            _dbContext.SaveChanges();

            return RedirectToAction("ViewApprovalRequests", approvalRequest);
        }

        [HttpPost]
        public IActionResult RejectRequest(int approvalRequestId, string comment)
        {
            var approvalRequest = _dbContext.ApprovalRequests.Find(approvalRequestId);
            if (approvalRequest == null)
            {
                return NotFound();
            }

            approvalRequest.ApprovalRequestStatus = RequestStatus.Rejected;
            approvalRequest.LastStatusChange = DateTime.Now;
            approvalRequest.Comment = comment;

            var leaveRequest = _dbContext.LeaveRequests.Find(approvalRequest.LeaveRequestId);
            leaveRequest.LeaveRequestStatus = RequestStatus.Rejected;
            leaveRequest.LastStatusChange = DateTime.Now;

            _dbContext.UpdateRange(approvalRequest, leaveRequest);
            _dbContext.SaveChanges();

            return RedirectToAction("ViewApprovalRequests", approvalRequest);
        }

        [HttpGet]
        public IActionResult GetApprovalRequestDetails(int id)
        {
            var approvalRequestDetails = _dbContext.ApprovalRequests
                .Where(approvalRequest => approvalRequest.Id == id)
                .Select(approvalRequest => new
                {
                    approvalRequest.Id,
                    EmployeeFullName = _dbContext.Employees.FirstOrDefault(employee => employee.Id == approvalRequest.EmployeeId).FullName,
                    approvalRequest.LeaveRequestId,
                    LeaveRequestComment = _dbContext.LeaveRequests.FirstOrDefault(leaveRequest => leaveRequest.Id == approvalRequest.LeaveRequestId).Comment,
                    LeaveRequestAbsenceReason = _dbContext.LeaveRequests.FirstOrDefault(leaveRequest => leaveRequest.Id == approvalRequest.LeaveRequestId).AbsenseReason,
                    approvalRequest.ApprovalRequestStatus,
                    approvalRequest.LastStatusChange
                })
                .FirstOrDefault();

            if (approvalRequestDetails == null)
            {
                return NotFound();
            }

            return Json(approvalRequestDetails);
        }

    }
}

