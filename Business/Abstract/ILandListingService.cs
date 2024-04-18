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
        IResult Add(LandListing landListing);
        IResult Delete(LandListing landListing);
        IResult Update(LandListing landListing);
        IDataResult<List<LandListing>> GetAll();
        IDataResult<LandListing> GetById(int id);
    }
}
