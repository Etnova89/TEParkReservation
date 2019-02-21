﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Campsite
    {
        public int SiteID { get; set; }
        public int CampgroundID { get; set; }
        public int SiteNumber { get; set; }
        public int MaxOccupancy { get; set; }
        public int Accessible { get; set; }
        public int MaxRVLength { get; set; }
        public int Utilities { get; set; }
    }
}
