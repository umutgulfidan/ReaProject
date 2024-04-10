using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests.UserImageReq
{
    public class DeleteUserImageReq : IDto
    {
        public int Id { get; set; }
    }
}
