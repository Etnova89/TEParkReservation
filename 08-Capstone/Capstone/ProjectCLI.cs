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
                    Console.Write("Selection: ");
                    string command = Console.ReadLine();
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
                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Press enter to continue.");
                    Console.WriteLine();
                    Console.ReadLine();
                    Console.WriteLine();
                }
            }
        }

        private void PrintStartMenu()
        {

            Console.WriteLine("View Parks Interface");
            Console.WriteLine("Select a Park for Further Details");
            Console.WriteLine();

        }

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
                Console.WriteLine();
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
                    Console.Write("Selection: ");

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
                            Console.WriteLine();
                            Console.WriteLine("Invalid Input. Press enter to continue.");
                            Console.WriteLine();
                            Console.ReadLine();
                            Console.WriteLine();
                            break;
                    }
                }

                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid Input. Press enter to continue.");
                    Console.WriteLine();
                    Console.ReadLine();
                    Console.WriteLine();
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
                Console.WriteLine();
                Console.WriteLine("Park Information Screen");
                Console.WriteLine();
                Console.WriteLine(park.ParkName);
                Console.WriteLine("Location:".PadRight(30) + park.Location);
                Console.WriteLine("Established:".PadRight(30) + park.EstablishedDate.ToShortDateString());
                Console.WriteLine("Area:".PadRight(30) + park.AreaInAcres.ToString("#,###") + " Acres");
                Console.WriteLine("Annual Visitors:".PadRight(30) + park.AnnualVisitors.ToString("#,###"));
                Console.WriteLine();
                Console.WriteLine(park.Description);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Invalid Input. Press enter to continue.");
                Console.WriteLine();
                Console.ReadLine();
                Console.WriteLine();
            }
        }

        public void PrintCampgrounds(int parkID)
        {
            CampgroundSQLDAL dal = new CampgroundSQLDAL(DatabaseConnection);
            List<Campground> campgrounds = CampgroundSQLDAL.GetAllCampgrounds(parkID);

            if (campgrounds.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Name".PadLeft(11).PadRight(43) + "Open".PadRight(15) + "Close".PadRight(15) + "Fee");
                Console.WriteLine(new String('=', 79));
                foreach (Campground campground in campgrounds)
                {
                    Console.WriteLine($"#{campground.CampgroundID.ToString().PadRight(5)} {campground.CampgroundName.ToString().PadRight(35)} {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(campground.OpenMonth).PadRight(14)} {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(campground.CloseMonth).PadRight(14)} {campground.DailyFee.ToString("C2")}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("NO RESULTS");
                Console.WriteLine();
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
                Console.Write("Selection: ");

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
                        Console.WriteLine();
                        Console.WriteLine("Invalid Input. Press enter to continue.");
                        Console.WriteLine();
                        Console.ReadLine();
                        Console.WriteLine();
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

                Console.WriteLine();
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
                else if (!(CampgroundSQLDAL.GetAvailableCampgrounds(parkID, campgroundID)))
                {
                    Console.WriteLine();
                    Console.WriteLine("Not a valid selection. Press enter to continue.");
                    Console.WriteLine();
                    Console.ReadLine();
                    continue;
                }
                Console.WriteLine("What is the arrival date? (MM/DD/YYYY)");
                DateTime arrivalDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("What is the departure date? (MM/DD/YYYY)");
                DateTime departureDate = DateTime.Parse(Console.ReadLine());

                if (arrivalDate > departureDate)
                {
                    Console.WriteLine("Invalid date selection. Press enter to continue.");
                    Console.WriteLine();
                    Console.ReadLine();
                    continue;
                }

                PrintAvailableCampsites(campgroundID, arrivalDate, departureDate);
            }
        }

        public void PrintAvailableCampsites(int campgroundID, DateTime arrivalDate, DateTime departureDate)
        {
            CampsiteSQLDAL dal = new CampsiteSQLDAL(DatabaseConnection);

            List<Campsite> campsites = CampsiteSQLDAL.SearchCampsites(campgroundID, arrivalDate, departureDate);

            if (campsites.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Results Matching Your Search Criteria");
                Console.WriteLine();
                Console.WriteLine("Site No.".PadRight(10) + "Max Occupancy".PadRight(15) + "Accessible?".PadRight(15) + "Max RV Length".PadRight(15) + "Utility".PadRight(15) + "Cost");
                Console.WriteLine(new String('=', 77));
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
                else if (!(CampsiteSQLDAL.GetAvailableCampsites(campgroundID, arrivalDate, departureDate, siteNumber)))
                {
                    Console.WriteLine();
                    Console.WriteLine("Not a valid selection. Press enter to continue.");
                    Console.WriteLine();
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("What Name Should the Reservation be Made Under?");
                string reservationName = Console.ReadLine();

                reservationID = dal.BookReservation(siteNumber, campgroundID, arrivalDate, departureDate, reservationName);

                if (reservationID > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"The reservation has been made and the confirmation id is {reservationID}. Press enter to continue.");
                    Console.ReadLine();
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
