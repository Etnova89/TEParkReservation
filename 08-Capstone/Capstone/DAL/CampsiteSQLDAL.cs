using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampsiteSQLDAL
    {
        private string connectionString;
        private const string SQL_SearchReservations =
            @"SELECT top 5
            site_number,
            max_occupancy,
            accessible,
            max_rv_length,
            utilities,
            s.campground_id,
            s.site_id,
			c.daily_fee

            FROM site s
			JOIN campground c on s.campground_id = c.campground_id
            where s.campground_id = @campground_id
            AND s.site_id NOT IN
            (
            SELECT s.site_id from reservation r
            JOIN site s on r.site_id = s.site_id
            WHERE s.campground_id = @campground_id
            AND r.to_date > @req_from_date AND r.from_date < @req_to_date)";

        public CampsiteSQLDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Campsite> SearchCampsites(int campgroundID, DateTime arrivalDate, DateTime departureDate)
        {
            List<Campsite> campsites = new List<Campsite>();

            Reservation reservation = new Reservation();
            reservation.FromDate = new DateTime(arrivalDate.Year, arrivalDate.Month, arrivalDate.Day, 15, 0, 0);
            reservation.ToDate = new DateTime(departureDate.Year, departureDate.Month, departureDate.Day, 11, 0, 0);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(SQL_SearchReservations, connection);
                    command.Parameters.AddWithValue("@campground_id", campgroundID);
                    command.Parameters.AddWithValue("@req_from_date", reservation.FromDate);
                    command.Parameters.AddWithValue("@req_to_date", reservation.ToDate);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Campsite campsite = new Campsite()
                        {
                            SiteID = Convert.ToInt32(reader["site_id"]),
                            CampgroundID = Convert.ToInt32(reader["campground_id"]),
                            SiteNumber = Convert.ToInt32(reader["site_number"]),
                            MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]),
                            Accessible = Convert.ToBoolean(reader["accessible"]),
                            MaxRVLength = Convert.ToInt32(reader["max_rv_length"]),
                            Utilities = Convert.ToBoolean(reader["utilities"]),
                            DailyFee = Convert.ToDecimal(reader["daily_fee"])
                        };

                        campsites.Add(campsite);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return campsites;
        }
    }
}
