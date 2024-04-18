using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests.LandListingReq
{
    public class CreateLandListingReq : IDto
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
        public bool? Status { get; set; } = true;

        // -----------

        public int ParcelNo { get; set; }

        public int IslandNo { get; set; }

        public int SheetNo { get; set; }

        public bool FloorEquivalent { get; set; }
    }
}
