using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone;
using Capstone.DAL;

namespace Capstone.Tests
{
    [TestClass]
    class CampgroundSQLDALTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NationalParkReservation;Integrated Security=True";
        private int departmentCount = 0; //TODO
        int maxID = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM department;", connection); //TODO
                departmentCount = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO department(name) VALUES ('Kyles Place'); SELECT CAST(SCOPE_IDENTITY() as int);", connection);//TODO
                maxID = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetCampgroundsTest()
        {
            CampgroundSQLDAL campground = new CampgroundSQLDAL(connectionString);
            List<Campground> campgrounds = campground.GetAllCampgrounds(1);
            Assert.IsNotNull(campgrounds);
            Assert.AreEqual(3, campgrounds.Count);
        }
    }
}
