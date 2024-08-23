using Core.Entities;

namespace Entities.Concrete
{
    public class UserComplaintReason : IEntity
    {
        public int Id { get; set; }
        public string ReasonName { get; set; } // Şikayet nedeni (Hakaret, Spam vs.)
    }
}
