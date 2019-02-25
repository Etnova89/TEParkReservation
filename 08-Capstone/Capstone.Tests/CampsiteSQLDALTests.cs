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
    public class CampsiteSQLDALTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NationalParkReservation;Integrated Security=True";
        int maxID = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand("INSERT INTO site (campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES (1, 1, 1,1,1,1); SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                maxID = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }
        
        [TestMethod]
        public void GetCampsiteTest()
        {
            CampsiteSQLDAL reservation = new CampsiteSQLDAL(connectionString);
            List<Campsite> campsites = CampsiteSQLDAL.SearchCampsites(1, new System.DateTime(2019, 01, 01), new System.DateTime(2019, 02, 01));
            Assert.IsNotNull(campsites);
        }

        [TestMethod]
        public void GetAvailableCampsitesTest()
        {
            CampsiteSQLDAL campsite= new CampsiteSQLDAL(connectionString);
            bool result = CampsiteSQLDAL.GetAvailableCampsites(1, new System.DateTime(1970,1,1), new System.DateTime(1970,2,1), 1);
            Assert.IsTrue(result);
        }
    }
}
