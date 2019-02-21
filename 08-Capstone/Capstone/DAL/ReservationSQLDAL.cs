using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationSQLDAL
    {
        private string connectionString;

        public ReservationSQLDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Park> GetParks()
        {
            List<Park> parks = new List<Park>();
            return parks;
        }

        public List<Campground> GetCampgrounds(int parkID)
        {
            List<Campground> campgrounds = new List<Campground>();
            return campgrounds;
        }

        public List<Campsite> GetAvailableCampsite(int campgroundID)
        {
            List<Campsite> campsites = new List<Campsite>();
            return campsites;
        }

        public bool SearchReservations(int campgroundID, DateTime arrivalDate, DateTime departureDate)
        {
            bool result = false;
            return result;
        }

        public bool BookReservation(int campsiteID, string reservationName)
        {
            bool result = false;
            return result;
        }


    }
}
