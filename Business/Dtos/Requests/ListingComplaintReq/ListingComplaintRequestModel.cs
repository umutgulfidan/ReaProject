using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests.ListingComplaintReq
{
    public class ListingComplaintRequestModel
    {
        public ListingComplaintFilterObject? Filter { get; set; }
        public SortingObject? Sorting { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
