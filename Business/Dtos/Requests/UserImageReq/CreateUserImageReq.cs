using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests.UserImageReq
{
    public class CreateUserImageReq : IDto
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool Status { get; set; } = true;
    }
}
