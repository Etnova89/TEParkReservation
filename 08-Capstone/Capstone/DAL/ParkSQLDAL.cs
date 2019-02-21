using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ParkSQLDAL
    {
        private string connectionString;
        private const string SQL_GetAllParks = @"SELECT * FROM park;";

        public ParkSQLDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Park> GetAllParks()
        {
            List<Park> parks = new List<Park>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(SQL_GetAllParks, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Park park = new Park()
                        {
                            ParkID = Convert.ToInt32(reader["park_id"]),
                            ParkName = Convert.ToString(reader["name"]),
                            Location = Convert.ToString(reader["location"]),
                            EstablishedDate = Convert.ToDateTime(reader["establish_date"]),
                            AreaInAcres = Convert.ToInt32(reader["area"]),
                            AnnualVisitors = Convert.ToInt32(reader["visitors"]),
                            Description = Convert.ToString(reader["description"])
                        };

                        parks.Add(park);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return parks;
        }

        public Park GetParkInformation(int parkID)
        {
            Park park = new Park();
            return park;
        }
    }
}
