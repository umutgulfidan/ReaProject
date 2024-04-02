using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class HouseListing
    {
        public int HouseListingId { get; set; }
        public int ListingId { get; set; }
        public int TypeId { get; set; }
        public int RoomCount { get; set; }
        public int BathroomCount { get; set; }
        public int LivingRoomCount { get; set; }
        public int FloorCount { get; set; }
        public int CurrentFloor { get; set; }
        public bool? HasGarden { get; set; }
        public bool? HasBalcony { get; set; }
        public bool? HasElevator { get; set; }
        public bool? HasParking { get; set; }
        public bool? HasFurniture { get; set; }
        public bool? IsInGatedCommunity { get; set; }
        public int BuildingAge { get; set; }
        public string Address { get; set; }
    }
}
