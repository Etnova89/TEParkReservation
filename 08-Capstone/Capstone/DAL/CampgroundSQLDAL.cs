using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampgroundSQLDAL
    {
        private static string connectionString;
        private const string SQL_GetAllCampgrounds = @"SELECT * FROM campground WHERE park_id = @parkID";
        public CampgroundSQLDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public static List<Campground> GetAllCampgrounds(int parkID)
        {
            List<Campground> campgrounds = new List<Campground>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(SQL_GetAllCampgrounds, connection);
                    command.Parameters.AddWithValue("@parkID", parkID);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground campground = new Campground()
                        {
                            CampgroundID = Convert.ToInt32(reader["campground_id"]),
                            ParkID = Convert.ToInt32(reader["park_id"]),
                            CampgroundName = Convert.ToString(reader["name"]),
                            OpenMonth = Convert.ToInt32(reader["open_from_mm"]),
                            CloseMonth = Convert.ToInt32(reader["open_to_mm"]),
                            DailyFee = Convert.ToDecimal(reader["daily_fee"]),
                        };

                        campgrounds.Add(campground);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return campgrounds;
        }

        public static bool GetAvailableCampgrounds(int parkID, int campgroundID)
        {
            bool result = false;

            List<int> campgroundIDList = new List<int>();

            List<Campground> campgrounds = GetAllCampgrounds(parkID);

            foreach (Campground campground in campgrounds)
            {
                campgroundIDList.Add(campground.CampgroundID);
            }

            if (campgroundIDList.Contains(campgroundID))
            {
                result = true;
            }

            return result;
        }
    }
}
