using Business.Dtos.Requests;
using Business.Dtos.Requests.LandListingReq;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILandListingService
    {
        IResult Add(CreateLandListingReq req);
        IResult Delete(DeleteLandListingReq req);
        IResult Update(UpdateLandListingReq req);
        IDataResult<List<LandListing>> GetAll();
        IDataResult<LandListing> GetById(int id);
    }
}
