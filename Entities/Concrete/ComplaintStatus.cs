using Core.Entities;

namespace Entities.Concrete
{
    public class ComplaintStatus : IEntity
    {
        public int Id { get; set; }
        public string StatusName { get; set; } // Durum adı (Sonuçlandı, Beklemede vs.)
    }
}
