using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;
using System.Data.SqlClient;
using dao_exercises.Models;
using dao_exercises;
using dao_exercises.DAL;
using System;

namespace dao_exercises.test
{
    [TestClass]
    public class EmployeeSqlDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source =.\sqlexpress;Initial Catalog = employeeDB; Integrated Security = True";
        private int employeeCount = 0;
        int maxID = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM employee;", connection);
                employeeCount = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO employee(department_id, job_title, first_name, last_name, birth_date, gender, hire_date) VALUES (1, 'Liability', 'Kyle', 'Thomas', '10-12-1992', 'M', '10-12-2017');  SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                maxID = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetAllEmployeesTest()
        {
            EmployeeSqlDAL employee = new EmployeeSqlDAL(connectionString);
            IList<Employee> names = employee.GetAllEmployees();
            Assert.IsNotNull(names);
            Assert.AreEqual(employeeCount, names.Count);
        }

        [TestMethod]
        public void SearchTest()
        {
            EmployeeSqlDAL employee = new EmployeeSqlDAL(connectionString);
            Employee person = new Employee
            {
                FirstName = "Kyle",
                LastName = "Thomas",
                DepartmentId = 1,
                EmployeeId = maxID,
                Gender = "M",
                JobTitle = "Liability",
                BirthDate = new DateTime(1992,10,12),
                HireDate = new DateTime(2017, 10, 12)
            };
            IList<Employee> list = employee.Search("Kyle", "Thomas");
            Assert.IsNotNull(list);
            //Assert.IsTrue(list.Contains(person));
            //CollectionAssert.Contains(list, person);
        }
    }
}
