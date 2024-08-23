using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests.ListingComplaintReq
{
    public class AddListingComplaintReq
    {

        //ListingComplaint
        public int ListingId { get; set; } // Şikayet edilen ilan
        public string ComplaintTitle { get; set; } // Şikayet başlığı
        public string ComplaintReason { get; set; } // Şikayet detayı
        public int ListingComplaintReasonId { get; set; } // Şikayet nedeni (FK)
    }
}
