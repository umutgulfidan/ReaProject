using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ComplaintResponse : IEntity
    {
        public int Id { get; set; }
        public int ComplaintId { get; set; }
        public int AdminUserId { get; set; }
        public string ResponseText { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}
