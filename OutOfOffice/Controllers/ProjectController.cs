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
        public IActionResult PostAssignEmployeeToProject(int employeeId, int projectId)
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

        public IActionResult AddProject()
        {
            ViewBag.ProjectManagers = new SelectList(
                _dbContext.Employees
                    .Where(e => e.Position == Position.PM)
                    .OrderBy(e => e.FullName)
                    .ToList(),
                "Id",
                "FullName"
            );

            return View();
        }

        [HttpPost]
        public IActionResult PostAddProject(Project project)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Projects.Add(project);
                _dbContext.SaveChanges();
                return RedirectToAction("ViewProjects");
            }

            ViewBag.ProjectManagers = new SelectList(
                _dbContext.Employees
                    .Where(e => e.Position == Position.PM)
                    .OrderBy(e => e.FullName)
                    .ToList(),
                "Id",
                "FullName"
            );


            return View("AddProject", project);
        }

        //

        [HttpGet]
        public IActionResult UpdateProject(int projectId)
        {
            var project = _dbContext.Projects.Find(projectId);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.ProjectManagers = new SelectList(
                _dbContext.Employees
                    .Where(e => e.Position == Position.PM)
                    .OrderBy(e => e.FullName)
                    .ToList(),
                "Id",
                "FullName"
            );

            return View("UpdateProject", project);
        }

        [HttpPost]
        public IActionResult PostUpdateProject(Project project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingProject = _dbContext.Projects.Find(project.Id);
                    if (existingProject == null)
                    {
                        return NotFound();
                    }

                    existingProject.Name = project.Name;
                    existingProject.ProjectType = project.ProjectType;
                    existingProject.StartDate = project.StartDate;
                    existingProject.EndDate = project.EndDate;
                    existingProject.ProjectManagerID = project.ProjectManagerID;
                    existingProject.Comment = project.Comment;
                    existingProject.Status = project.Status;

                    _dbContext.Update(existingProject);
                    _dbContext.SaveChanges();

                    return RedirectToAction("ViewProjects");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error updating project: " + ex.Message);
                }
            }

            ViewBag.ProjectManagers = new SelectList(
                _dbContext.Employees
                    .Where(e => e.Position == Position.PM)
                    .OrderBy(e => e.FullName)
                    .ToList(),
                "Id",
                "FullName"
            );

            return View("UpdateProject", project);
        }

        [HttpPost]
        public IActionResult DeactivateProject(int projectId)
        {
            var project = _dbContext.Projects.Find(projectId);
            if (project == null)
            {
                return NotFound();
            }

            project.Status = ProjectStatus.Inactive;
            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return RedirectToAction("ViewProjects", project);
        }

        [HttpGet]
        public IActionResult GetProjectDetails(int projectId)
        {
            var project = _dbContext.Projects
                .Include(project => project.ProjectManager)
                .Include(project => project.AssignedEmployees)
                .FirstOrDefault(project => project.Id == projectId);

            if (project == null)
            {
                return NotFound();
            }

            var projectDetails = new
            {
                id = project.Id,
                name = project.Name,
                startDate = project.StartDate,
                endDate = project.EndDate,
                projectManager = project.ProjectManager?.FullName,
                comment = project.Comment ?? "N/A",
                status = project.Status.ToString(),
                assignedEmployees = project.AssignedEmployees.Select(e => new { fullName = e.FullName }).ToList()
            };

            return Json(projectDetails);
        }

    }
}

