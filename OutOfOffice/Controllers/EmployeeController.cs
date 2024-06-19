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

            ViewBag.Projects = _dbContext.Projects.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name != null ? $"{p.Id} - {p.Name}" : p.Id.ToString()
            }).ToList();

            return View(employees);

        }

        public IActionResult AddEmployee()
        {
            ViewBag.PeoplePartners = new SelectList(_dbContext.Employees.Where(e => e.Position == Position.HR), "Id", "FullName");

            return View();
        }

        [HttpPost]
        public IActionResult PostAddEmployee(Employee employee)
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

        [HttpGet]
        public IActionResult UpdateEmployee(int employeeID)
        {
            var employee = _dbContext.Employees.Find(employeeID);
            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.PeoplePartners = new SelectList(_dbContext.Employees.Where(e => e.Position == Position.HR), "Id", "FullName", employee.PeoplePartnerId);

            return View("UpdateEmployee", employee);
        }

        [HttpPost]
        public IActionResult PostUpdateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingEmployee = _dbContext.Employees.Find(employee.Id);
                    if (existingEmployee == null)
                    {
                        return NotFound();
                    }

                    existingEmployee.FullName = employee.FullName;
                    existingEmployee.Subdivision = employee.Subdivision;
                    existingEmployee.Position = employee.Position;
                    existingEmployee.EmployeeStatus = employee.EmployeeStatus;
                    existingEmployee.PeoplePartnerId = employee.PeoplePartnerId;
                    existingEmployee.Balance = employee.Balance;

                    _dbContext.Update(existingEmployee);
                    _dbContext.SaveChanges();

                    return RedirectToAction("ViewEmployees");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error updating employee: " + ex.Message);
                }
            }

            ViewBag.PeoplePartners = new SelectList(_dbContext.Employees.Where(e => e.Position == Position.HR), "Id", "FullName", employee.PeoplePartnerId);

            return View("UpdateEmployee", employee);
        }

        [HttpPost]
        public IActionResult DeactivateEmployee(int employeeID)
        {
            var employee = _dbContext.Employees.Find(employeeID);
            if (employee == null)
            {
                return NotFound();
            }

            employee.EmployeeStatus = EmployeeStatus.Inactive;
            _dbContext.Update(employee);
            _dbContext.SaveChanges();

            return RedirectToAction("ViewEmployees", employee);
        }

        [HttpGet]
        public IActionResult GetEmployeeDetails(int employeeId)
        {
            var employee = _dbContext.Employees
                .Include(e => e.PeoplePartner)
                .Include(e => e.Projects)
                .FirstOrDefault(e => e.Id == employeeId);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDetails = new
            {
                id = employee.Id,
                fullName = employee.FullName,
                position = employee.Position.ToString(),
                subdivision = employee.Subdivision.ToString(),
                employeeStatus = employee.EmployeeStatus.ToString(),
                peoplePartnerId = employee.PeoplePartnerId,
                peoplePartner = new { fullName = employee.PeoplePartner?.FullName },
                balance = employee.Balance,
                projects = employee.Projects.Select(p => new { id = p.Id, name = p.Name }).ToList()
            };

            return Json(employeeDetails);
        }
    }

}




