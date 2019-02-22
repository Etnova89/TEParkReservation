using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampsiteSQLDAL
    {
        private string connectionString;
        private const string SQL_SearchReservations =
            @"SELECT TOP 5
	            site_number,
	            max_occupancy,
	            accessible,
	            max_rv_length,
	            utilities,
	            campground.daily_fee
            FROM site
            JOIN campground ON site.campground_id = campground.campground_id
            JOIN reservation ON site.site_id = reservation.site_id
            WHERE	campground.campground_id = @campgroundID AND
		            @arrivalDate NOT BETWEEN to_date AND from_date AND
		            @departureDate NOT BETWEEN to_date AND from_date
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

            return campsites;
        }
    }
}
