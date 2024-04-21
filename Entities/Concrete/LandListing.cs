using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class LandListing : IEntity
    {
        public int Id { get; set; }
        public string Address { get; set; }

        public int ListingId { get; set; }

        public int ParcelNo { get; set; }

        public int IslandNo { get; set; }

        public int SheetNo { get; set; }

        public bool FloorEquivalent { get; set; }
    }
}
