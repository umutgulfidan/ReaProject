using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ListingDetailDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string TypeName { get; set; }
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }
        public int SquareMeter { get; set; }
        public string ImagePath { get; set; }

    }
}
