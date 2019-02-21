using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class ProjectCLI
    {
        const string DatabaseConnection = @"Data Source=.\sqlexpress;Initial Catalog=NationalParkReservation;Integrated Security=True";

        public void RunCli()
        {

            while (true)
            {
                PrintStartMenu();
                int maxID = PrintAllParks();

                try
                {
                    string command = Console.ReadLine();
                    Console.Clear();
                    //int number = 0; TODO?

                    if (command.ToUpper() == "Q")
                    {
                        break;
                    }

                    else if (Convert.ToInt32(command) > 0 && Convert.ToInt32(command) <= maxID)
                    {
                        PrintParkInformation(Convert.ToInt32(command));
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Enter one of the selections below.\n");
                        continue;
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

        private int PrintAllParks()
        {
            ParkSQLDAL dal = new ParkSQLDAL(DatabaseConnection);
            List<Park> parks = dal.GetAllParks();
            int MaxID = 0;

            if (parks.Count > 0)
            {
                foreach (Park park in parks)
                {
                    Console.WriteLine($"{park.ParkID}) {park.ParkName}");
                    if (park.ParkID > MaxID)
                    {
                        MaxID = park.ParkID;
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Q) quit");
            }
            else
            {
                Console.WriteLine("NO RESULTS");
            }
            return MaxID;
        }

        public void PrintParkInformation(int parkID)
        {
            ParkSQLDAL dal = new ParkSQLDAL(DatabaseConnection);
            Park park = dal.GetParkInformation(parkID);
            Console.WriteLine("Park Information Screen");
            Console.WriteLine(park.ParkName);
            Console.WriteLine("Location:".PadRight(30) + park.Location);
            Console.WriteLine("Established:".PadRight(30) + park.EstablishedDate.ToShortDateString());
            Console.WriteLine("Area:".PadRight(30) + park.AreaInAcres.ToString() + "Acres");
            Console.WriteLine("Annual Visitors:".PadRight(30) + park.AnnualVisitors.ToString());
            Console.WriteLine();
            Console.WriteLine(park.Description);
            Console.WriteLine();
            PrintParkMenu(parkID);
        }

        public void PrintParkMenu(int parkID)
        {
            bool done = false;
            int userInput = 0;
            while (!done)
            {
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
        }

        public void PrintCampgrounds(int parkID)
        {
            CampgroundSQLDAL dal = new CampgroundSQLDAL(DatabaseConnection);
            List<Campground> campgrounds = dal.GetAllCampgrounds(parkID);
            //int MaxID = 0;

            if (campgrounds.Count > 0)
            {
                Console.WriteLine("name, open, close, fee");
                foreach (Campground campground in campgrounds)
                {
                    Console.WriteLine($"#{campground.CampgroundID.ToString().PadRight(5)} {campground.CampgroundName.ToString().PadRight(20)} {campground.OpenMonth.ToString().PadRight(15)} {campground.CloseMonth.ToString().PadRight(15)} {campground.DailyFee.ToString("C2")}");
                    //if (campground.ID > MaxID)
                    //{
                    //    MaxID = park.ParkID;
                    //}
                }
                Console.WriteLine();
                Console.WriteLine("Q) quit");
            }
            else
            {
                Console.WriteLine("NO RESULTS");
            }
            //return MaxID;
        }

        public void SearchForReservation(int parkID)
        {

        }
    }
}
