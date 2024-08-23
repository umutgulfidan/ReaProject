using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests.UserComplaintReq
{
    public class AddUserComplaintReq
    {

        // User Complaint
        public int ReportedUserId { get; set; } // Şikayet edilen kullanıcı
        public string ComplaintTitle { get; set; } // Şikayet başlığı
        public string ComplaintReason { get; set; } // Şikayet detayı
        public int UserComplaintReasonId { get; set; } // Şikayet nedeni (FK)
    }
}
