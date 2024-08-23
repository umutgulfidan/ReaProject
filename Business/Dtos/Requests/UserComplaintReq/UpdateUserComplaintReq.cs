using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests.UserComplaintReq
{
    public class UpdateUserComplaintReq
    {
        public int ComplaintId { get; set; }
        public int UserId { get; set; } // Şikayeti yapan kullanıcı
        public DateTime ComplaintDate { get; set; }
        public int ComplaintStatusId { get; set; } // Şikayet durumu (Sonuçlandı, Beklemede vs.)
        public DateTime? ComplaintResponseDate { get; set; }


        public int ReportedUserId { get; set; } // Şikayet edilen kullanıcı
        public int UserComplaintId { get; set; } //PK
        public string ComplaintTitle { get; set; } // Şikayet başlığı
        public string ComplaintReason { get; set; } // Şikayet detayı
        public int UserComplaintReasonId { get; set; } // Şikayet nedeni (FK)
    }
}
