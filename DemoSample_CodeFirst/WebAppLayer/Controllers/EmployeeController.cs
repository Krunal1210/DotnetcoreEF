using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContextLayer.DataContext;
using DataContextLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAppLayer.Controllers
{
    public class EmployeeController : Controller
    {
        EFDataContext _dbContext = new EFDataContext();

        public IActionResult Index()
        {
            var data = (from emps in _dbContext.Employees
                        join desig in _dbContext.Designations
                        on emps.DesignationId equals desig.DesignationId
                        select new Employee
                        {
                            EmployeeId = emps.EmployeeId,
                            EmployeeCode = emps.EmployeeCode,
                            EmployeeName = emps.EmployeeName,
                            DesignationId = emps.DesignationId,
                            DesignationName = desig.DesignationName
                        }).ToList();

            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Designations = _dbContext.Designations.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee model)
        {
            ModelState.Remove("EmployeeId");
            if (ModelState.IsValid)
            {
                _dbContext.Employees.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Designations = _dbContext.Designations.ToList();
            return View();
        }

        public IActionResult Edit(int id)
        {
            Employee data = _dbContext.Employees.Where(p => p.EmployeeId == id).FirstOrDefault();
            ViewBag.Designations = _dbContext.Designations.ToList();
            return View("Create", data);
        }

        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            ModelState.Remove("EmployeeId");
            if (ModelState.IsValid)
            {
                _dbContext.Employees.Update(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Designations = _dbContext.Designations.ToList();
            return View("Create", model);
        }

        public IActionResult Delete(int id)
        {
            Employee data = _dbContext.Employees.Where(p => p.EmployeeId == id).FirstOrDefault();
            if (data != null)
            {
                _dbContext.Employees.Remove(data);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}