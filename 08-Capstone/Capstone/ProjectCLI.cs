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
            PrintMenu();
            int maxID = PrintAllParks();

            while (true)
            {
                string command = Console.ReadLine();
                Console.Clear();

                if (command.ToUpper() == "Q")
                {
                    break;
                }
                else if (Convert.ToInt32(command) >= 1 && Convert.ToInt32(command) <= maxID)
                {
                    PrintParkInformation(Convert.ToInt32(command));
                }
            }
        }

        private void PrintMenu()
        {

            Console.WriteLine("View Parks Interface");
            Console.WriteLine("Select a Park for Further Details.");
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
            Console.WriteLine("Park Information Screen.");
            Console.WriteLine(park.ParkName);
            Console.WriteLine("Location:" + park.Location.PadLeft(30));
            Console.WriteLine("Established:" + park.EstablishedDate.ToShortDateString().PadLeft(30));
            Console.WriteLine("Area:" + park.AreaInAcres.ToString().PadLeft(30)+" sq km");
            Console.WriteLine("Annual Visitors:" + park.AnnualVisitors.ToString().PadLeft(30));
            Console.WriteLine();
            Console.WriteLine(park.Description);
            Console.WriteLine();

        }
    }
}
