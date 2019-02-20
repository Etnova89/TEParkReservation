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
    public class ProjectSqlDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source =.\sqlexpress;Initial Catalog = employeeDB; Integrated Security = True";
        private int projectCount = 0;
        private int maxProjectID = 0;
        private int maxEmployeeID = 0;
        private int FakeProjectID = 0;


        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM project;", connection);
                projectCount = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO project(name, from_date, to_date) VALUES ('Liability', '10-12-1992','10-12-2017');  SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                maxProjectID = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO employee(department_id, job_title, first_name, last_name, birth_date, gender, hire_date) VALUES (1, 'Liability', 'Kyle', 'Thomas', '10-12-1992', 'M', '10-12-2017');  SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                maxEmployeeID = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO project_employee(project_id, employee_id) VALUES (@maxProjectID, @maxEmployeeID);", connection);
                cmd.Parameters.AddWithValue("@maxProjectID", maxProjectID);
                cmd.Parameters.AddWithValue("@maxEmployeeID", maxEmployeeID);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }



        [TestMethod]
        public void GetAllProjectTest()
        {
            ProjectSqlDAL project = new ProjectSqlDAL(connectionString);
            IList<Project> list = project.GetAllProjects();
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count<= maxProjectID);
        }

        [TestMethod]
        public void AssignEmployeeProjectTest()
        {
            ProjectSqlDAL project = new ProjectSqlDAL(connectionString);
            bool result = false;
            result = project.AssignEmployeeToProject(maxProjectID, maxEmployeeID);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveEmployeeProjectTest()
        {
            ProjectSqlDAL project = new ProjectSqlDAL(connectionString);
            Project removeProject = new Project()
            {
                ProjectId = 1
            };
            Employee removeEmployee = new Employee()
            {
                EmployeeId = 3,
            };
            bool result = false;
            result = project.RemoveEmployeeFromProject(removeProject.ProjectId,removeEmployee.EmployeeId);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateProjectTest()
        {
            ProjectSqlDAL project = new ProjectSqlDAL(connectionString);
            Project newProject = new Project
            {
                Name = "Vending Machine",
                StartDate = new DateTime(2018, 10, 12),
                EndDate = new DateTime(2019, 10, 12),
            };

            int vendingID = project.CreateProject(newProject);
            Assert.AreEqual(maxProjectID + 1, newProject.ProjectId);
        }
    }
}
