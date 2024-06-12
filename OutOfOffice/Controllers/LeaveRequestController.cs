using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.Data;

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
    }
}

