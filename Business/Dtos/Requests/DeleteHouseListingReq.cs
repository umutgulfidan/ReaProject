using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests
{
    public class DeleteHouseListingReq : IDto
    {   
        public int HouseListingId { get; set; }
    }
}
