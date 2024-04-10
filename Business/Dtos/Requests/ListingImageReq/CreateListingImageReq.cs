using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests.ListingImageReq
{
    public class CreateListingImageReq : IDto
    {
        public int ListingId { get; set; }
    }
}
