using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Complaint : IEntity
    {
        public int ComplaintId { get; set; }
        public int UserId { get; set; } // Şikayeti yapan kullanıcı
        public DateTime ComplaintDate { get; set; }
        public int ComplaintStatusId { get; set; } // Şikayet durumu (Sonuçlandı, Beklemede vs.)
        public DateTime? ComplaintResponseDate { get; set; }

    }
}
