using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContextLayer.DataContext;
using DataContextLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAppLayer.Controllers
{
    public class DesignationsController : Controller
    {
        EFDataContext _dbContext = new EFDataContext();

        public IActionResult Index()
        {
            //List<Designation> data = _dbContext.Designations.ToList();

            var data = (from dept in _dbContext.Departments
                        join desig in _dbContext.Designations
                        on dept.DepartmentId equals desig.DepartmentId
                        select new Designation
                        {
                            DesignationId = desig.DesignationId,
                            DesignationCode = desig.DesignationCode,
                            DesignationName = desig.DesignationName,
                            DepartmentId = desig.DepartmentId,
                            DepartmentName = dept.DepartmentName
                        }).ToList();

            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _dbContext.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Designation model)
        {
            ModelState.Remove("DesignationId");
            if (ModelState.IsValid)
            {
                _dbContext.Designations.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = _dbContext.Departments.ToList();
            return View();
        }

        public IActionResult Edit(int id)
        {
            Designation data = _dbContext.Designations.Where(p => p.DesignationId == id).FirstOrDefault();
            ViewBag.Departments = _dbContext.Departments.ToList();
            return View("Create", data);
        }

        [HttpPost]
        public IActionResult Edit(Designation model)
        {
            ModelState.Remove("DesignationId");
            if (ModelState.IsValid)
            {
                _dbContext.Designations.Update(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = _dbContext.Departments.ToList();
            return View("Create", model);
        }

        public IActionResult Delete(int id)
        {
            Designation data = _dbContext.Designations.Where(p => p.DesignationId == id).FirstOrDefault();
            if (data != null)
            {
                _dbContext.Designations.Remove(data);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}