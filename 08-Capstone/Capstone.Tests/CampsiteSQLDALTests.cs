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

                //cmd = new SqlCommand("SELECT COUNT(*) FROM department;", connection); //TODO
                //departmentCount = (int)cmd.ExecuteScalar();

                //cmd = new SqlCommand("INSERT INTO department(name) VALUES ('Kyles Place'); SELECT CAST(SCOPE_IDENTITY() as int);", connection);//TODO
                //maxID = (int)cmd.ExecuteScalar();
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
            List<Campsite> campsites = reservation.SearchCampsites(1);
            Assert.IsNotNull(campsites);
            Assert.AreEqual(3, campsites.Count);
        }
    }
}
