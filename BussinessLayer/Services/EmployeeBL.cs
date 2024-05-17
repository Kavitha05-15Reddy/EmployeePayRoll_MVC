using BussinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class EmployeeBL : IEmployeeBL
    {
        private readonly IEmployeeRL employeeRL;
        public EmployeeBL(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
        }

        //Insert
        public bool AddEmployee(Employee employee)
        {
            return employeeRL.AddEmployee(employee);
        }

        //Retrieve
        public IEnumerable<Employee> GetAllEmployees()
        {
            return employeeRL.GetAllEmployees();
        }

        //Update
        public bool UpdateEmployee(Employee employee)
        {
            return employeeRL.UpdateEmployee(employee);
        }

        //Delete
        public bool DeleteEmployee(int id)
        {
            return employeeRL.DeleteEmployee(id);
        }

        //GetById
        public Employee GetById(int id)
        {
            return employeeRL.GetById(id);
        }

        //Login
        public Employee Login(LoginModel login)
        {
            return employeeRL.Login(login);
        }

        //GetByName
        public List<Employee> GetByAllNames(string name)
        {
            return employeeRL.GetByAllNames(name);
        }
        public Employee GetBySingleName(string name)
        {
            return employeeRL.GetBySingleName(name);
        }
    }
}
