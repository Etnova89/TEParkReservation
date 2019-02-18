using System;
using System.Collections.Generic;
using System.Text;
using dao_exercises.Models;
using System.Data.SqlClient;

namespace dao_exercises.DAL
{
    class DepartmentSqlDAL
    {
        private const string SQL_GetDepartmentNames = "SELECT * FROM department";
        private const string SQL_InsertDepartment = @"INSERT INTO department (name) VALUES (@name);";
        private const string SQL_UpdateDepartment = "UPDATE department SET name = @name;";
        private string connectionString;

        // Single Parameter Constructor
        public DepartmentSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the departments.
        /// </summary>
        /// <returns></returns>
        public IList<Department> GetDepartments()
        {
            List<Department> output = new List<Department>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SQL_GetDepartmentNames;
                    cmd.Connection = conn;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Department department = new Department();

                        department.Name = Convert.ToString(reader["name"]);
                        department.Id = Convert.ToInt32(reader["department_id"]);

                        output.Add(department);
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
        /// Creates a new department.
        /// </summary>
        /// <param name="newDepartment">The department object.</param>
        /// <returns>The id of the new department (if successful).</returns>
        public int CreateDepartment(Department newDepartment)
        {
            int count = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_InsertDepartment, conn);

                    cmd.Parameters.AddWithValue("@name", newDepartment.Name);

                    count = newDepartment.Id;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return count;
        }

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="updatedDepartment">The department object.</param>
        /// <returns>True, if successful.</returns>
        public bool UpdateDepartment(Department updatedDepartment)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_UpdateDepartment, conn);

                    cmd.Parameters.AddWithValue("@name", updatedDepartment.Name);

                    int count = cmd.ExecuteNonQuery();

                    if (count>0)
                    {
                        result = true;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw;
            }

            return result;
        }

    }
}
