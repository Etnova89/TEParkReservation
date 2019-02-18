using System;
using System.Collections.Generic;
using System.Text;
using dao_exercises.Models;
using System.Data.SqlClient;

namespace dao_exercises.DAL
{
    class EmployeeSqlDAL
    {
        private string connectionString;
        private const string SQL_GetEmployeeNames = @"SELECT * FROM employee";
        private const string SQL_GetNoProjectEmployees = @"SELECT * FROM employee LEFT JOIN project_employee ON employee.employee_id = project_employee.employee_id WHERE project_id IS NULL;";
        private const string SQL_SearchFirstLastNames = @"SELECT * FROM employee WHERE first_name LIKE @firstname OR last_name LIKE @lastname;";

        // Single Parameter Constructor
        public EmployeeSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the employees.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        public IList<Employee> GetAllEmployees()
        {
            List<Employee> output = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SQL_GetEmployeeNames;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();

                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.Gender = Convert.ToString(reader["gender"]);
                        employee.HireDate = Convert.ToDateTime(reader["hire_date"]);
                        
                        output.Add(employee);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return output;
        }

        /// <summary>
        /// Searches the system for an employee by first name or last name.
        /// </summary>
        /// <remarks>The search performed is a wildcard search.</remarks>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns>A list of employees that match the search.</returns>
        /// 

            //TODO: Finish this
        
        public IList<Employee> Search(string firstname, string lastname)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_SearchFirstLastNames, conn);

                    cmd.Parameters.AddWithValue("@firstname", "%" + firstname + "%");
                    cmd.Parameters.AddWithValue("@lastname", "%" + lastname + "%");

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();

                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.Gender = Convert.ToString(reader["gender"]);
                        employee.HireDate = Convert.ToDateTime(reader["hire_date"]);

                        employees.Add(employee);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw;
            }

            return employees;
        }

        /// <summary>
        /// Gets a list of employees who are not assigned to any active projects.
        /// </summary>
        /// <returns></returns>
        public IList<Employee> GetEmployeesWithoutProjects()
        {
            List<Employee> output = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SQL_GetNoProjectEmployees;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();

                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.Gender = Convert.ToString(reader["gender"]);
                        employee.HireDate = Convert.ToDateTime(reader["hire_date"]);

                        output.Add(employee);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return output;
        }
    }
}
