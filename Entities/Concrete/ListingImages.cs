using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ListingImages
    {
        public int Id { get; set; }
        public int ListingId { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}
