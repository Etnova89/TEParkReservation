using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class CLIHelper
    {
        public static decimal GetTotalFee(decimal dailyFee, DateTime arrivalDate, DateTime departureDate)
        {
            int dayDifference = (int)Math.Ceiling((departureDate - arrivalDate).TotalDays);
            decimal totalFee = dailyFee * dayDifference;
            return totalFee;
        }
    }
}
