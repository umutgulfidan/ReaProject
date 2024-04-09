using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IListingTypeService
    {
        IDataResult<List<ListingType>> GetAll();
        IDataResult<ListingType> GetById(int id);
        IResult Add(ListingType listingType);
        IResult Delete(ListingType listingType);
        IResult Update(ListingType listingType);
    }
}
