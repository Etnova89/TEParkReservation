using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampsiteSQLDAL
    {
        private string connectionString;

        public CampsiteSQLDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Campsite> SearchCampsites(int campgroundID)
        {
            List<Campsite> campsites = new List<Campsite>();

            return campsites;
        }
    }
}
