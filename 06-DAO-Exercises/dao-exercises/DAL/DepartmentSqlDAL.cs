using System;
using System.Collections.Generic;
using System.Text;
using dao_exercises.Models;
using System.Data.SqlClient;

namespace dao_exercises.DAL
{
    class DepartmentSqlDAL
    {
        //private static string departmentName = "";
        private const string SQL_GetDepartmentNames = @"SELECT * FROM department";
        private const string SQL_InsertDepartment = @"INSERT INTO department (name) VALUES (@name);";
        private string SQL_SelectMaxDepartmentID = @"SELECT department_id FROM department WHERE name = @name;";
        private const string SQL_UpdateDepartment = @"UPDATE department SET name = @name WHERE department_id = @id;";
        
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
                    count = cmd.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand(SQL_SelectMaxDepartmentID, conn);
                    cmd2.Parameters.AddWithValue("@name", newDepartment.Name);
                    SqlDataReader reader = cmd2.ExecuteReader();

                    while (reader.Read())
                    {
                        newDepartment.Id = Convert.ToInt32(reader["department_id"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return newDepartment.Id;
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
                    cmd.Parameters.AddWithValue("@id", updatedDepartment.Id);

                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
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
