using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;
using System.Data.SqlClient;
using dao_exercises.Models;
using dao_exercises;
using dao_exercises.DAL;

namespace dao_exercises.test
{
    [TestClass]
    public class DepartmentSqlDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source =.\sqlexpress;Initial Catalog = employeeDB; Integrated Security = True";
        private int departmentCount = 0;
        int maxID = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM department;", connection);
                departmentCount = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO department(name) VALUES ('Kyles Place'); SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                maxID = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetDepartmentsTest()
        {
            DepartmentSqlDAL department = new DepartmentSqlDAL(connectionString);
            IList<Department> names = department.GetDepartments();
            Assert.IsNotNull(names);
            Assert.AreEqual(departmentCount + 1, names.Count);
        }

        [TestMethod]
        public void CreateDepartmentsTest()
        {
            DepartmentSqlDAL departmentDAL = new DepartmentSqlDAL(connectionString);
            Department department = new Department
            {
                Name = "Eric's Place",
            };

            int ericID = departmentDAL.CreateDepartment(department);

            Assert.AreEqual(maxID + 1, department.Id);
        }

        [TestMethod]
        public void UpdateDepartmentsTest()
        {
            DepartmentSqlDAL departmentDAL = new DepartmentSqlDAL(connectionString);
            Department department = new Department
            {
                Name = "Kyle's Place",
                Id= 1
            };

            bool departmentID = departmentDAL.UpdateDepartment(department);
            Assert.IsTrue(departmentID);
        }
    }

}