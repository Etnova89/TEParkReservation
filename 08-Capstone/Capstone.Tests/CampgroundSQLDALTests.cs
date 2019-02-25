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
    public class CampgroundSQLDALTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NationalParkReservation;Integrated Security=True";
       
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

                cmd = new SqlCommand("INSERT INTO campground (park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (1 ,'Kyles Place', 1,12,50); SELECT CAST(SCOPE_IDENTITY() as int);", connection);
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
            List<Campground> campgrounds = CampgroundSQLDAL.GetAllCampgrounds(1);
            Assert.IsNotNull(campgrounds);
        }

        [TestMethod]
        public void GetAvailableCampgroundsTest()
        {
            CampgroundSQLDAL campground = new CampgroundSQLDAL(connectionString);
            bool result = CampgroundSQLDAL.GetAvailableCampgrounds(1, maxID);
            Assert.IsTrue(result);

        }
    }
}
