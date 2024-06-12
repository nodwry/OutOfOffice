using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OutOfOffice.Data;
using OutOfOffice.Models;

namespace OutOfOffice.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public EmployeeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult ViewEmployees()
        {
            var employees = _dbContext.Employees.ToList();

            return View(employees);
        }

        public IActionResult AddEmployee()
        {
            ViewBag.PeoplePartners = new SelectList(_dbContext.Employees.Where(e => e.Position == Position.HR), "Id", "FullName");
            return View();
        }

        [HttpPost]
        public IActionResult PostEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Employees.Add(employee);
                _dbContext.SaveChanges();
                return RedirectToAction("ViewEmployees");
            }

            ViewBag.PeoplePartners = new SelectList(_dbContext.Employees.Where(e => e.Position == Position.HR), "Id", "FullName");
            return View("AddEmployee", employee);
        }
    }
}

