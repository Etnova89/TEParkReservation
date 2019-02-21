using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampgroundSQLDAL
    {
        private string connectionString;

        public CampgroundSQLDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Campground> GetAllCampgrounds(int parkID)
        {
            List<Campground> campgrounds = new List<Campground>();
            return campgrounds;
        }
    }
}
