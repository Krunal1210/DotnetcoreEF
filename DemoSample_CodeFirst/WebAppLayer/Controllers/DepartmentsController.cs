using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContextLayer.DataContext;
using DataContextLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAppLayer.Controllers
{
    public class DepartmentsController : Controller
    {

        EFDataContext _dbContext = new EFDataContext();
        private readonly ILogger _logger;

        public DepartmentsController(ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger<DepartmentsController>();
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Log message in the Index() method");
            List<Department> data = this._dbContext.Departments.ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department model)
        {
            ModelState.Remove("DepartmentId");
            if (ModelState.IsValid)
            {
                _dbContext.Departments.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            Department data = _dbContext.Departments.Where(p => p.DepartmentId == id).FirstOrDefault();
            return View("Create", data);
        }

        [HttpPost]
        public IActionResult Edit(Department model)
        {
            ModelState.Remove("DepartmentId");
            if (ModelState.IsValid)
            {
                _dbContext.Departments.Update(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", model);
        }

        public IActionResult Delete(int id)
        {
            Department data = _dbContext.Departments.Where(p => p.DepartmentId == id).FirstOrDefault();
            if (data != null)
            {
                _dbContext.Departments.Remove(data);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}