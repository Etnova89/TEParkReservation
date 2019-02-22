using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationSQLDAL
    {
        private string connectionString;


        //TODO: Address this method: ProjectCLI vs SQLDAL?
        //public List<Campsite> SearchCampsites(int campgroundID)
        //{
        //    List<Campsite> campsites = new List<Campsite>();
        //    return campsites;
        //}

        public bool BadSearchReservations(int campgroundID, DateTime arrivalDate, DateTime departureDate)
        {
            return false;
        }

        public int BookReservation(int campsiteID, string reservationName)
        {
            int reservationID = 0;
            return reservationID;
        }


    }
}
