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
        private const string SQL_GetAllParks = @"SELECT * FROM park;"; // TODO: Sort parks alphabetically
        private const string SQL_GetParkInformation = @"SELECT * FROM park WHERE park_id = @park_ID;";

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
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(SQL_GetParkInformation, connection);
                    command.Parameters.AddWithValue("@park_ID", parkID);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        park.ParkID = Convert.ToInt32(reader["park_id"]);
                        park.ParkName = Convert.ToString(reader["name"]);
                        park.Location = Convert.ToString(reader["location"]);
                        park.EstablishedDate = Convert.ToDateTime(reader["establish_date"]);
                        park.AreaInAcres = Convert.ToInt32(reader["area"]);
                        park.AnnualVisitors = Convert.ToInt32(reader["visitors"]);
                        park.Description = Convert.ToString(reader["description"]);

                        //if (park.ParkID = null)
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return park;
        }
    }
}
