using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.Data;
using OutOfOffice.Models;

namespace OutOfOffice.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public LeaveRequestController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult ViewLeaveRequests()
        {
            var leaveRequests = _dbContext.LeaveRequests.ToList();

            return View(leaveRequests);
        }

        public IActionResult SubmitLeaveRequest()
        {
            ViewBag.Employees = new SelectList(_dbContext.Employees, "Id", "FullName");
            ViewBag.AbsenceReasons = Enum.GetValues(typeof(AbsenseReason)).Cast<AbsenseReason>().Select(e => new SelectListItem { Value = e.ToString(), Text = e.ToString() });
            ViewBag.Statuses = Enum.GetValues(typeof(RequestStatus)).Cast<RequestStatus>().Select(e => new SelectListItem { Value = e.ToString(), Text = e.ToString() });

            return View();
        }

        [HttpPost]
        public IActionResult SubmitLeaveRequest(LeaveRequest leaveRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    leaveRequest.SubmittedTime = DateTime.Now;
                    leaveRequest.LastStatusChange = DateTime.Now;
                    leaveRequest.LeaveRequestStatus = RequestStatus.Submitted;

                    _dbContext.LeaveRequests.Add(leaveRequest);

                    _dbContext.ApprovalRequests.Add(new ApprovalRequest
                    {
                        ApprovalRequestStatus = RequestStatus.New,
                        LastStatusChange = DateTime.Now,
                        EmployeeId = leaveRequest.EmployeeId,
                        LeaveRequestId = leaveRequest.ID
                    });
                    _dbContext.SaveChanges();

                    return RedirectToAction("ViewLeaveRequests");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error submitting leave request: " + ex.Message);
                }
            }

            ViewBag.Employees = new SelectList(_dbContext.Employees, "Id", "FullName");
            ViewBag.AbsenceReasons = Enum.GetValues(typeof(AbsenseReason)).Cast<AbsenseReason>().Select(e => new SelectListItem { Value = e.ToString(), Text = e.ToString() });
            ViewBag.Statuses = Enum.GetValues(typeof(RequestStatus)).Cast<RequestStatus>().Select(e => new SelectListItem { Value = e.ToString(), Text = e.ToString() });

            return View(leaveRequest);
        }

    }
}

