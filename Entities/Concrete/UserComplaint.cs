using Core.Entities;

namespace Entities.Concrete
{
    public class UserComplaint: IEntity
    {
        public int UserComplaintId { get; set; }
        public int ComplaintId { get; set; }
        public int ReportedUserId { get; set; } // Şikayet edilen kullanıcı
        public string ComplaintTitle { get; set; } // Şikayet başlığı
        public string ComplaintReason { get; set; } // Şikayet detayı
        public int UserComplaintReasonId { get; set; } // Şikayet nedeni (FK)
    }
}
