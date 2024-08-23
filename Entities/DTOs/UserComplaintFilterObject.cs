using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserComplaintFilterObject
    {

        public string? SearchText { get; set; }
        public DateTime? MinComplaintDate { get; set; }
        public DateTime? MaxComplaintDate { get; set; }
        public int? UserId { get; set; }
        public int? ReportedUserId { get; set; }
        public int? ComplaintStatusId { get; set; }
        public int? UserComplaintReasonId { get; set; } // Şikayet nedeni (FK)
    }
}
