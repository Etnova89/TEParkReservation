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
            @"DECLARE
            @topNumber int = (SELECT COUNT(*)
            FROM site
            JOIN campground ON site.campground_id = campground.campground_id
            JOIN reservation ON site.site_id = reservation.site_id
            WHERE campground.campground_id = @campgroundID AND NOT
                    ((CASE WHEN @arrivalDate > from_date
                        THEN @arrivalDate
                        ELSE from_date
                        END) >=
					(CASE WHEN @departureDate<to_date
                        THEN @departureDate
                        ELSE to_date
                        END)))
;
            SELECT DISTINCT TOP(@topNumber + 5)
                site_number,
	            max_occupancy,
	            accessible,
	            max_rv_length,
	            utilities,
	            campground.daily_fee,
                site.campground_id,
                site.site_id
            FROM site
            JOIN campground ON site.campground_id = campground.campground_id
            JOIN reservation ON site.site_id = reservation.site_id
            WHERE campground.campground_id = @campgroundID

            EXCEPT

            SELECT
                site_number,
                max_occupancy,
                accessible,
                max_rv_length,
                utilities,

                campground.daily_fee,
                site.campground_id,
                site.site_id
            FROM site
            JOIN campground ON site.campground_id = campground.campground_id
            JOIN reservation ON site.site_id = reservation.site_id
            WHERE campground.campground_id = @campgroundID AND NOT
                    ((CASE WHEN @arrivalDate > from_date
                        THEN @arrivalDate
                        ELSE from_date
                        END) >=
					(CASE WHEN @departureDate<to_date
                        THEN @departureDate
                        ELSE to_date
                        END))
			
            ORDER BY site_number;";

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
                    command.Parameters.AddWithValue("@campgroundID", campgroundID);
                    command.Parameters.AddWithValue("@arrivalDate", reservation.FromDate);
                    command.Parameters.AddWithValue("@departureDate", reservation.ToDate);

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
