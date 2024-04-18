using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests
{
    public class UpdateHouseListingReq
    {
        public int UserId { get; set; }
        public int CityId { get; set; }
        public int ListingTypeId { get; set; }
        public int PropertyTypeId { get; set; } = 1;
        public int DistrictId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public int SquareMeter { get; set; }
        public bool Status { get; set; }

        // -------------------
        public int HouseListingId { get; set; }
        public int TypeId { get; set; }
        public int RoomCount { get; set; }
        public int BathroomCount { get; set; }
        public int LivingRoomCount { get; set; }
        public int FloorCount { get; set; }
        public int CurrentFloor { get; set; }
        public bool? HasGarden { get; set; } = false;
        public bool? HasBalcony { get; set; } = false;
        public bool? HasElevator { get; set; } = false;
        public bool? HasParking { get; set; } = false;
        public bool? HasFurniture { get; set; } = false;
        public bool? IsInGatedCommunity { get; set; } = false;
        public int BuildingAge { get; set; }
        public string Address { get; set; }
    }
}
