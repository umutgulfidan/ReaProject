﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ListingDetailsView
    {
        public int ListingId { get; set; }
        public string CityName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string DistrictName { get; set; }
        public decimal Price { get; set; }
        public int SquareMeter { get; set; }
        public string Title { get; set; }
        public string ListingTypeName { get; set; }
        public string PropertyTypeName { get; set; }
        public string ImagePath { get; set; }
        public bool Status { get; set; }
    }
}
