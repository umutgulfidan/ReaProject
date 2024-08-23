using Core.Entities;

namespace Entities.Concrete
{
    public class ListingComplaintReason : IEntity
    {
        public int Id { get; set; }
        public string ReasonName { get; set; } // Şikayet nedeni (İlan resmi uygunsuz, İlan açıklaması uygunsuz vs.)
    }
}
