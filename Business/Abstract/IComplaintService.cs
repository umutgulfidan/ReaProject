using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IComplaintService
    {
        IDataResult<List<Complaint>> GetAll();
        IDataResult<Complaint> GetById(int id);
        IResult Add(Complaint complaint);
        IResult Delete(Complaint complaint);
        IResult Update(Complaint complaint);
    }


}
