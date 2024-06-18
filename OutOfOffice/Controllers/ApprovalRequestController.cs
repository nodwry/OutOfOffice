using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            var leaveRequest = _dbContext.LeaveRequests.Find(approvalRequest.LeaveRequestId);

            if (leaveRequest == null)
            {
                return NotFound();
            }

            TimeSpan leaveDuration = leaveRequest.EndDate - leaveRequest.StartDate;
            int daysAbsent = (int)leaveDuration.TotalDays + 1; 

            var employee = _dbContext.Employees.Find(leaveRequest.EmployeeId);
            if (employee == null)
            {
                return NotFound();
            }

            if (employee.Balance < daysAbsent)
            {
               TempData["ErrorMessage"] = "Employee balance is not enough.";
               return RedirectToAction("ViewApprovalRequests");
            }

            approvalRequest.ApprovalRequestStatus = RequestStatus.Approved;
            approvalRequest.LastStatusChange = DateTime.Now;

            leaveRequest.LeaveRequestStatus = RequestStatus.Approved;
            leaveRequest.LastStatusChange = DateTime.Now;

            employee.Balance -= daysAbsent;
           
            _dbContext.UpdateRange(approvalRequest, leaveRequest, employee);
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
            var approvalRequest = _dbContext.ApprovalRequests
                .Include(approvalRequest => approvalRequest.Employee.PeoplePartner)
                .Include(approvalRequest => approvalRequest.LeaveRequest)
                .FirstOrDefault(approvalRequest => approvalRequest.Id == id);

            if (approvalRequest == null)
            {
                return NotFound();
            }

            var data = new
            {
                id = approvalRequest.Id,
                leaveRequestId = approvalRequest.LeaveRequestId,
                employeeFullName = approvalRequest.Employee?.FullName,
                leaveRequestComment = approvalRequest.LeaveRequest?.Comment ?? "N/A",
                absenceReason = approvalRequest?.LeaveRequest?.AbsenseReason,
                leaveRequestStatus = approvalRequest?.LeaveRequest?.LeaveRequestStatus.ToString(),
                approverFullName = approvalRequest?.Employee?.PeoplePartner?.FullName,
                approvalRequestStatus = approvalRequest?.ApprovalRequestStatus.ToString(),
                lastStatusChange = approvalRequest?.LastStatusChange.ToString("yyyy-MM-ddTHH:mm:ss.fffffff")
            };

            return Json(data);
        }


        //add details to leave requests
        //project details
        //employee details
        //update/deactivate projects in the list
    }
}

