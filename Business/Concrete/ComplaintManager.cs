using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ComplaintManager : IComplaintService
    {
        IComplaintDal _complaintDal;
        public ComplaintManager(IComplaintDal complaintDal)
        {
            _complaintDal = complaintDal;
        }

        public IResult Add(Complaint complaint)
        {
            _complaintDal.Add(complaint);
            return new SuccessResult(Messages.ComplaintAdded);
        }

        public IResult Delete(Complaint complaint)
        {
            _complaintDal.Delete(complaint);
            return new SuccessResult(Messages.ComplaintDeleted);
        }

        public IDataResult<List<Complaint>> GetAll()
        {
            return new SuccessDataResult<List<Complaint>>(_complaintDal.GetAll(), Messages.ComplaintListed);
        }

        public IDataResult<Complaint> GetById(int id)
        {
            return new SuccessDataResult<Complaint>(_complaintDal.Get(c=> c.ComplaintId==id),Messages.ComplaintListed);
        }

        public IResult Update(Complaint complaint)
        {
            _complaintDal.Update(complaint);
            return new SuccessResult(Messages.ComplaintUpdated);
        }
    }
}
