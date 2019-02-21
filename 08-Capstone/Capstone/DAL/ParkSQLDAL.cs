using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public class ParkSQLDAL
    {
        private string connectionString;

        public ParkSQLDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Park> GetAllParks()
        {
            List<Park> parks = new List<Park>();
            return parks;
        }

        public Park GetParkInformation(int parkID)
        {
            Park park = new Park();
            return park;
        }
    }
}
