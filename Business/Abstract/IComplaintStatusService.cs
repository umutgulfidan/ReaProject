using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IComplaintStatusService
    {
        IDataResult<List<ComplaintStatus>> GetAll();
        IResult Add(ComplaintStatus complaintStatus);
        IResult Delete(ComplaintStatus complaintStatus);
        IResult Update(ComplaintStatus complaintStatus);
    }


}
