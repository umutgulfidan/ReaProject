namespace Entities.DTOs
{
    public class ListingComplaintDto
    {
        public int ComplaintId { get; set; }
        public int UserId { get; set; } // Şikayeti yapan kullanıcı
        public DateTime ComplaintDate { get; set; }
        public int ComplaintStatusId { get; set; } // Şikayet durumu (Sonuçlandı, Beklemede vs.)
        public DateTime? ComplaintResponseDate { get; set; }


        public int ListingComplaintId { get; set; }
        public int ListingId { get; set; } // Şikayet edilen ilan
        public string ComplaintTitle { get; set; } // Şikayet başlığı
        public string ComplaintReason { get; set; } // Şikayet detayı
        public int ListingComplaintReasonId { get; set; }
        public string ListingComplaintReasonName { get; set; }
    }
}
