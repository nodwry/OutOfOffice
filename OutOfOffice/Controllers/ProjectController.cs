using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.Data;

namespace OutOfOffice.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public ProjectController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult ViewProjects()
        {
            var projects = _dbContext.Projects.ToList();

            return View(projects);
        }

        public IActionResult AssignEmployeeToProject()
        {
            ViewBag.Projects = _dbContext.Projects.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name != null ? $"{p.Id} - {p.Name}" : p.Id.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult AssignEmployeeToProject(int employeeId, int projectId)
        {
            var employee = _dbContext.Employees.Include(e => e.Projects).FirstOrDefault(e => e.Id == employeeId);
            var project = _dbContext.Projects.Include(p => p.AssignedEmployees).FirstOrDefault(p => p.Id == projectId);

            if (employee == null || project == null)
            {
                return NotFound();
            }

            project.AssignedEmployees.Add(employee);
            _dbContext.SaveChanges();

            return RedirectToAction("ViewEmployees", "Employee");
        }
    }
}

