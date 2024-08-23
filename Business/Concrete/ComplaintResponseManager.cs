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
    public class ComplaintResponseManager : IComplaintResponseService
    {
        IComplaintResponseDal _complaintResponseDal;
        public ComplaintResponseManager(IComplaintResponseDal complaintResponseDal)
        {
            _complaintResponseDal = complaintResponseDal;
        }

        public IResult Add(ComplaintResponse complaintResponse)
        {
            _complaintResponseDal.Add(complaintResponse);
            return new SuccessResult(Messages.ComplaintResponseAdded);
        }

        public IResult Delete(ComplaintResponse complaintResponse)
        {

            _complaintResponseDal.Delete(complaintResponse);
            return new SuccessResult(Messages.ComplaintResponseDeleted);
        }

        public IDataResult<List<ComplaintResponse>> GetAll()
        {
            return new SuccessDataResult<List<ComplaintResponse>>(_complaintResponseDal.GetAll(),Messages.ComplaintResponseListed);
        }

        public IResult Update(ComplaintResponse complaintResponse)
        {
            _complaintResponseDal.Update(complaintResponse);
            return new SuccessResult(Messages.ComplaintResponseUpdated);
        }
    }
}
