using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class HouseListingDto : IDto
    {
        public int Id { get; set; }
        public int ListingId { get; set; }
        public string HouseTypeName { get; set; }
        public string ListingTypeName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int BathroomCount { get; set; }
        public int LivingRoomCount { get; set; }
        public int RoomCount { get; set; }
        public decimal Price { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string ImagePath { get; set; }

    }
}
