using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class HouseFilterObject
    {
        public string? SearchText { get; set; }
        public int? RoomCount { get; set; }
        public int? BathroomCount { get; set; }
        public int? LivingRoomCount { get; set; }

        public int? ListingTypeId { get; set; }
        public int? HouseTypeId { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }

        public int? MaxPrice { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxBuildAge { get; set; }
        public int? MinSquareMeter { get; set; }
        public int? MaxSquareMeter { get; set; }

        public bool? HasGarden { get; set; }
        public bool? HasElevator { get; set; }
        public bool? HasFurniture { get; set; }
        public bool? HasParking { get; set; }
        public bool? HasBalcony { get; set; }
        public bool? IsInGatedCommunity { get; set; }

    }
}
