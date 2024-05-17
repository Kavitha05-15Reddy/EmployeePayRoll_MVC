using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IEmployeeRL
    {
        public bool AddEmployee(Employee employee);
        public IEnumerable<Employee> GetAllEmployees();
        public bool UpdateEmployee(Employee employee);
        public bool DeleteEmployee(int id);
        public Employee GetById(int id);
        public Employee Login(LoginModel login);
        public List<Employee> GetByAllNames(string name);
        public Employee GetBySingleName(string name);
    }
}
