using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationSQLDAL
    {
        private string connectionString;

        private const string SQL_BookReservation =
            @"INSERT INTO reservation (name, from_date, to_date, create_date, site_id)
            VALUES ( @name,
		             @fromDate, 
		             @toDate, 
		             GETDATE(),
		             (SELECT site_id
		             FROM site
		             WHERE campground_id = @campgroundID
			            AND site_number = @siteNumber));
            SELECT CAST(SCOPE_IDENTITY() as int);";

        public ReservationSQLDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int BookReservation(int siteNumber, int campgroundID, DateTime arrivalDate, DateTime departureDate, string reservationName)
        {
            int reservationID = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(SQL_BookReservation, connection);
                    command.Parameters.AddWithValue("@siteNumber", siteNumber);
                    command.Parameters.AddWithValue("@campgroundID", campgroundID);
                    command.Parameters.AddWithValue("@fromDate", arrivalDate);
                    command.Parameters.AddWithValue("@toDate", departureDate);
                    command.Parameters.AddWithValue("@name", reservationName);

                    reservationID = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return reservationID;
        }
    }
}
