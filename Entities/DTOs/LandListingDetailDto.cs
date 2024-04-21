using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class LandListingDetailDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int ParcelNo { get; set; }
        public int IslandNo { get; set; }
        public int SheetNo { get; set; }
        public bool FloorEquivalent { get; set; }

        public int ListingId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string ListingTypeName { get; set; }
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }
        public int SquareMeter { get; set; }
    }
}
