using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class EmployeeRL : IEmployeeRL
    {
        string connectionString = @"Data Source=Ramanjaneya\SQLEXPRESS;Initial Catalog=MVC;Integrated Security=True";
        
        //Insert
        public bool AddEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Employee_Insert_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@Notes", employee.Notes);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return false;
            }
        }

        //Retrieve
        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Employee_List_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee();

                        employee.EmployeeId = (int)reader["EmployeeId"];
                        employee.Name = (string)reader["Name"];
                        employee.ProfileImage = (string)reader["ProfileImage"];
                        employee.Gender = (string)reader["Gender"];
                        employee.Department = (string)reader["Department"];
                        employee.Salary = Convert.ToInt64(reader["Salary"]);
                        employee.StartDate = (DateTime)reader["StartDate"];
                        employee.Notes = (string)reader["Notes"];

                        employees.Add(employee);
                    }
                    return employees;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }

        //Update
        public bool UpdateEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Employee_Update_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@Notes", employee.Notes);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return false;
            }
        }

        //Delete
        public bool DeleteEmployee(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Employee_Delete_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return false;
            }
        }

        //GetById
        public Employee GetById(int id)
        {
            Employee employee = new Employee();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Employee_GetById_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", id);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        employee.EmployeeId = (int)reader["EmployeeId"];
                        employee.Name = (string)reader["Name"];
                        employee.ProfileImage = (string)reader["ProfileImage"];
                        employee.Gender = (string)reader["Gender"];
                        employee.Department = (string)reader["Department"];
                        employee.Salary = Convert.ToInt64(reader["Salary"]);
                        employee.StartDate = (DateTime)reader["StartDate"];
                        employee.Notes = (string)reader["Notes"];
                    }
                    return employee;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }

        //Login
        public Employee Login(LoginModel login)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Employee_Login_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId",login.EmployeeId);
                    cmd.Parameters.AddWithValue("@Name", login.Name);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeId = (int)reader["EmployeeId"];
                        employee.Name = (string)reader["Name"];
                        return employee;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }

        //GetByAllNames
        public List<Employee> GetByAllNames(string name)
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Employee_GetByName_SP",conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue ("@Name", name);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee();

                        employee.EmployeeId = (int)reader["EmployeeId"];
                        employee.Name = (string)reader["Name"];
                        employee.ProfileImage = (string)reader["ProfileImage"];
                        employee.Gender = (string)reader["Gender"];
                        employee.Department = (string)reader["Department"];
                        employee.Salary = Convert.ToInt64(reader["Salary"]);
                        employee.StartDate = (DateTime)reader["StartDate"];
                        employee.Notes = (string)reader["Notes"];

                        employees.Add(employee);
                    }
                    return employees;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }
        //GetBySingleName
        public Employee GetBySingleName(string name)
        {
            Employee employee = new Employee();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Employee_GetByName_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        employee.EmployeeId = (int)reader["EmployeeId"];
                        employee.Name = (string)reader["Name"];
                        employee.ProfileImage = (string)reader["ProfileImage"];
                        employee.Gender = (string)reader["Gender"];
                        employee.Department = (string)reader["Department"];
                        employee.Salary = Convert.ToInt64(reader["Salary"]);
                        employee.StartDate = (DateTime)reader["StartDate"];
                        employee.Notes = (string)reader["Notes"];
                    }
                    return employee;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }
    }
}
