using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class HouseListingDetailDto
    {
        public int Id { get; set; }
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


        // Listing Tablosundan Gelecekler
        public int ListingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public decimal Price { get; set; }

        //City Tablosu  
        public string CityName { get; set; }

        //District Tablosu
        public string DistrictName { get; set; }

        // User Tablosu
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }

        //House Type Tablosundan
        public string HouseTypeName { get; set; }

        //ListingType Tablosu
        public string ListingTypeName { get; set; }
    }
}
