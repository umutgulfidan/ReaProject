using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Listing : IEntity
    {
        public int ListingId { get; set; }
        public int UserId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }
        public int SquareMeter { get; set; }
    }
}
