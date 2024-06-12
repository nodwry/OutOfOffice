﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.Data;

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
    }
}

