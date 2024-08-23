using Core.Entities;

namespace Entities.Concrete
{
    public class ListingComplaint : IEntity
    {
        public int ListingComplaintId { get; set; }
        public int ComplaintId { get; set; }
        public int ListingId { get; set; } // Şikayet edilen ilan
        public string ComplaintTitle { get; set; } // Şikayet başlığı
        public string ComplaintReason { get; set; } // Şikayet detayı
        public int ListingComplaintReasonId { get; set; } // Şikayet nedeni (FK)
    }
}
