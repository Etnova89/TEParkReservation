using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Runtime.Serialization;

namespace Capstone
{
    public class ProjectCLI
    {
        const string DatabaseConnection = @"Data Source=.\sqlexpress;Initial Catalog=NationalParkReservation;Integrated Security=True";

        public void RunCli()
        {
            bool done = false;
            while (!done)
            {
                PrintStartMenu();
                PrintAllParks();

                try
                {
                    string command = Console.ReadLine();
                    //int number = 0; TODO?
                    if (command.ToUpper() == "Q")
                    {
                        done = true;
                        break;
                    }

                    else
                    {
                        PrintParkMenu(Convert.ToInt32(command));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid input. Enter one of the selections above.");
                }
            }
        }

        private void PrintStartMenu()
        {

            Console.WriteLine("View Parks Interface");
            Console.WriteLine("Select a Park for Further Details");
            Console.WriteLine();

        }

        // TODO: CLI methods should not be testable, so they should be void
        public void PrintAllParks()
        {
            ParkSQLDAL dal = new ParkSQLDAL(DatabaseConnection);
            List<Park> parks = dal.GetAllParks();

            if (parks.Count > 0)
            {
                foreach (Park park in parks)
                {
                    Console.WriteLine($"{park.ParkID}) {park.ParkName}");
                }
                Console.WriteLine();
                Console.WriteLine("Q) quit");
            }
            else
            {
                Console.WriteLine("NO RESULTS");
            }
        }

        public void PrintParkMenu(int parkID)
        {
            bool done = false;
            int userInput = 0;
            while (!done)
            {
                ParkSQLDAL dal = new ParkSQLDAL(DatabaseConnection);
                Park park = dal.GetParkInformation(parkID);

                if (park.Location != null || park.Description != null)
                {
                    PrintParkInformation(parkID);
                    Console.WriteLine("Select a Command");
                    Console.WriteLine("1) View Campgrounds");
                    Console.WriteLine("2) Search For Reservation");
                    Console.WriteLine("3) Return to Previous Screen");
                    Console.WriteLine();

                    try
                    {
                        userInput = int.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        userInput = 0;
                    }

                    switch (userInput)
                    {
                        case 1:
                            PrintCampgrounds(parkID);
                            PrintCampgroundMenu(parkID);
                            break;

                        case 2:
                            SearchForReservation(parkID);
                            break;

                        case 3:
                            done = true;
                            break;

                        default:
                            Console.WriteLine("Invalid Input. Please Enter 1, 2, or 3.");
                            break;
                    }
                }

                else
                {
                    Console.WriteLine("Invalid Selection.");
                    done = true;
                }
            }
        }

        public void PrintParkInformation(int parkID)
        {
            ParkSQLDAL dal = new ParkSQLDAL(DatabaseConnection);
            Park park = dal.GetParkInformation(parkID);

            if (park.Location != null || park.Description != null)
            {

                Console.WriteLine("Park Information Screen");
                Console.WriteLine(park.ParkName);
                Console.WriteLine("Location:".PadRight(30) + park.Location);
                Console.WriteLine("Established:".PadRight(30) + park.EstablishedDate.ToShortDateString());
                Console.WriteLine("Area:".PadRight(30) + park.AreaInAcres.ToString() + "Acres");
                Console.WriteLine("Annual Visitors:".PadRight(30) + park.AnnualVisitors.ToString());
                Console.WriteLine();
                Console.WriteLine(park.Description);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Invalid Input.");
            }
        }

        public void PrintCampgrounds(int parkID)
        {
            CampgroundSQLDAL dal = new CampgroundSQLDAL(DatabaseConnection);
            List<Campground> campgrounds = dal.GetAllCampgrounds(parkID);

            if (campgrounds.Count > 0)
            {
                Console.WriteLine("Name".PadLeft(11).PadRight(33) + "Open".PadRight(15) + "Close".PadRight(15) + "Fee");
                Console.WriteLine(new String('=', 69));
                foreach (Campground campground in campgrounds)
                {
                    Console.WriteLine($"#{campground.CampgroundID.ToString().PadRight(5)} {campground.CampgroundName.ToString().PadRight(25)} {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(campground.OpenMonth).PadRight(14)} {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(campground.CloseMonth).PadRight(14)} {campground.DailyFee.ToString("C2")}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("NO RESULTS");
            }
        }

        public void PrintCampgroundMenu(int parkID)
        {
            bool done = false;
            int userInput = 0;
            while (!done)
            {
                Console.WriteLine("Select a Command");
                Console.WriteLine("1) Search For Available Reservations");
                Console.WriteLine("2) Return to Previous Screen");
                Console.WriteLine();

                try
                {
                    userInput = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    userInput = 0;
                }

                switch (userInput)
                {
                    case 1:
                        SearchForReservation(parkID);
                        break;

                    case 2:
                        done = true;
                        break;

                    default:
                        Console.WriteLine("Invalid Input. Please Enter 1, 2, or 3.");
                        break;
                }

            }
        }

        public void SearchForReservation(int parkID)
        {
            bool done = false;

            while (!done)
            {


                Reservation reservation = new Reservation();

                Console.WriteLine("Search for Campground Reservation");
                PrintCampgrounds(parkID);
                Console.WriteLine();
                Console.WriteLine("Which campground? (enter 0 to cancel)");
                int campgroundID = int.Parse(Console.ReadLine());
                if (campgroundID == 0)
                {
                    done = true;
                    break;
                }
                Console.WriteLine("What is the arrival date? (MM/DD/YYYY)");
                DateTime arrivalDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("What is the departure date? (MM/DD/YYYY)");
                DateTime departureDate = DateTime.Parse(Console.ReadLine());

                PrintAvailableCampsites(campgroundID, arrivalDate, departureDate);
            }
        }

        public void PrintAvailableCampsites(int campgroundID, DateTime arrivalDate, DateTime departureDate)
        {
            CampsiteSQLDAL dal = new CampsiteSQLDAL(DatabaseConnection);

            List<Campsite> campsites = dal.SearchCampsites(campgroundID, arrivalDate, departureDate);

            if (campsites.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Results Matching Your Search Criteria");
                Console.WriteLine("Site No.".PadRight(10) + "Max Occupancy".PadRight(15) + "Accessible?".PadRight(15) + "Max RV Length".PadRight(15) + "Utility".PadRight(15) + "Cost");
                foreach (Campsite campsite in campsites)
                {
                    decimal totalFee = CLIHelper.GetTotalFee(campsite.DailyFee, arrivalDate, departureDate);
                    Console.Write(campsite.SiteNumber.ToString().PadRight(10));
                    Console.Write(campsite.MaxOccupancy.ToString().PadRight(15));
                    Console.Write((campsite.Accessible == true ? "YES" : "NO").PadRight(15));
                    Console.Write((campsite.MaxRVLength != 0 ? campsite.MaxRVLength.ToString() : "N/A").PadRight(15));
                    Console.Write((campsite.Utilities == true ? "YES" : "N/A").PadRight(15));
                    Console.WriteLine(totalFee.ToString("C2"));
                }
                Console.WriteLine();

                BookingMenu(campgroundID, arrivalDate, departureDate);
            }
            else
            {
                Console.WriteLine("No available sites. Would you like to enter an alternate date range?");
                Console.WriteLine();
            }
        }

        public void BookingMenu(int campgroundID, DateTime arrivalDate, DateTime departureDate)
        {
            int reservationID = 0;
            ReservationSQLDAL dal = new ReservationSQLDAL(DatabaseConnection);
            bool done = false;

            while (!done)
            {
                Console.WriteLine();
                Console.WriteLine("Which Site Should be Reserved? (0 To Cancel)");
                int siteNumber = int.Parse(Console.ReadLine());
                if (siteNumber == 0)
                {
                    done = true;
                    break;
                }
                Console.WriteLine("What Name Should the Reservation be Made Under?");
                string reservationName = Console.ReadLine();

                reservationID = dal.BookReservation(siteNumber, campgroundID, arrivalDate, departureDate, reservationName);

                if (reservationID > 0)
                {
                    Console.WriteLine($"The reservation has been made and the confirmation id is {reservationID}");
                    done = true;
                }
                else
                {
                    Console.WriteLine("Error making reservation. Please try again.");
                }
            }
        }
    }
}
