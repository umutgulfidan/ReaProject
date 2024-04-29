using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ListingFilterObject
    {
        public int? ListingId { get; set; }
        public int? ListingTypeId { get; set; }
        public string? SearchText { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinSquareMeter { get; set; }
        public int? MaxSquareMeter { get; set; }
    }
}
