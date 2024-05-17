using BussinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace EmployeePayRoll.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeBL employeeBL;
        public EmployeeController(IEmployeeBL employeeBL)
        {
            this.employeeBL = employeeBL;
        }

        //Retrieve
        [HttpGet]
        [Route("GetAllEmployees")]
        public IActionResult Index()
        {
            List<Employee> lstEmployees = new List<Employee>();
            lstEmployees = employeeBL.GetAllEmployees().ToList();

            return View(lstEmployees);
        }

        //Insert
        [HttpGet]
        [Route("Add")]
        public IActionResult Create()
        {
            ViewBag.date = DateTime.Now.ToString();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Add")]
        public IActionResult Create([Bind] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employeeBL.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //Update
        [HttpGet]
        [Route("Update/{id}")]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee employee = employeeBL.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [Route("Update/{id}")]
        public IActionResult Edit(int id, [Bind] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    employeeBL.UpdateEmployee(employee);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occured on processing your request.");
                return View(employee);
            }
        }

        //GetById
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            //id = (int)HttpContext.Session.GetInt32("id");
            if (id == null)
            {
                return NotFound();
            }
            Employee employee = employeeBL.GetById(id);

            if (employee == null)
            {
                return NotFound("something went wrong");
            }
            return View(employee);
        }

        //Delete
        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound("Something Wrong");
            }
            Employee employee = employeeBL.GetById(id);

            if (employee == null)
            {
                return NotFound("something went wrong");
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var res = employeeBL.DeleteEmployee(id);
            if (res)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        //Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            var result = employeeBL.Login(login);
            if (result == null)
            {
                return Content("Invalid Credentials");
            }
            else
            {
                //HttpContext.Session.SetInt32("id", result.EmployeeId);
                //return RedirectToAction("GetById");
                return RedirectToAction("GetById", new { id = result.EmployeeId });
            }
        }

        //Review

        //take input of employeename and show the details, 
        //if more than one employee of same name exist show list of their data

        [HttpGet]
        [Route("SearchByName")]
        public IActionResult SearchByName()
        {
            return View();
        }

        [HttpPost]
        [Route("SearchByName")]
        public IActionResult SearchByName(string name)
        {
            if(name == null)
            {
                return NotFound();  
            }

            var employees = employeeBL.GetByAllNames(name);

            int count = employees.Count();

            if(employees == null || count == 0)
            {
                return NotFound();
            }

            var emp = employeeBL.GetBySingleName(name);

            if (count == 1)
            {
                if (emp == null)
                {
                    return NotFound();
                }
                return View("GetByName", emp);
            }
            return View("Index", employees);
        }
    }
}

