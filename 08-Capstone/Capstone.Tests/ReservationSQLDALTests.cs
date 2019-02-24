//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;
//using System.Transactions;
//using System.Data.SqlClient;
//using Capstone.Models;
//using Capstone;
//using Capstone.DAL;

//namespace Capstone.Tests
//{
//    [TestClass]
//    public class ReservationSQLDALTests
//    {
//        private TransactionScope tran;
//        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NationalParkReservation;Integrated Security=True";
//        private int departmentCount = 0; //TODO
//        int maxID = 0;

//        [TestInitialize]
//        public void Initialize()
//        {
//            tran = new TransactionScope();

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                SqlCommand cmd;
//                connection.Open();

//                //cmd = new SqlCommand("SELECT COUNT(*) FROM department;", connection); //TODO
//                //departmentCount = (int)cmd.ExecuteScalar();

//                //cmd = new SqlCommand("INSERT INTO department(name) VALUES ('Kyles Place'); SELECT CAST(SCOPE_IDENTITY() as int);", connection);//TODO
//                //maxID = (int)cmd.ExecuteScalar();
//            }
//        }

//        [TestCleanup]
//        public void Cleanup()
//        {
//            tran.Dispose();
//        }

        //[TestMethod]
        //public void TrueSearchReservationsTest()
        //{
        //    ReservationSQLDAL reservation = new ReservationSQLDAL(connectionString);
        //    bool trueResult = reservation.SearchReservations(1, new System.DateTime(1980, 7, 4), new System.DateTime(1980, 7, 11));
        //    Assert.IsTrue(trueResult);
        //}

        //[TestMethod]
        //public void FalseSearchReservationsTest()
        //{
        //    // TODO: Add SQL that inserts reservation in 1970
        //    ReservationSQLDAL reservation = new ReservationSQLDAL(connectionString);
        //    bool falseResult = reservation.SearchReservations(1, new System.DateTime(1970, 7, 4), new System.DateTime(1970, 7, 11));
        //    Assert.IsFalse(falseResult);
//        //}

//        [TestMethod]
//        public void TrueBookReservationTest()
//        {
//            // TODO: Add SQL that inserts reservation in 1970
//            ReservationSQLDAL reservation = new ReservationSQLDAL(connectionString);
//            bool trueResult = reservation.BookReservation(1, "Dale");
//            Assert.IsTrue(trueResult);
//        }
//    }
//}
