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
    public class ListingTypeManager : IListingTypeService
    {
        IListingTypeDal _listingTypeDal;
        public ListingTypeManager(IListingTypeDal listingTypeDal)
        {
            _listingTypeDal = listingTypeDal;
        }
        public IResult Add(ListingType listingType)
        {
            _listingTypeDal.Add(listingType);
            return new SuccessResult(Messages.ListingTypeAdded);
        }

        public IResult Delete(ListingType listingType)
        {
            _listingTypeDal.Delete(listingType);
            return new SuccessResult(Messages.ListingTypeDeleted);
        }

        public IDataResult<List<ListingType>> GetAll()
        {
            return new SuccessDataResult<List<ListingType>>(_listingTypeDal.GetAll(),Messages.ListingTypeListed);
        }

        public IDataResult<ListingType> GetById(int id)
        {
            return new SuccessDataResult<ListingType>(_listingTypeDal.Get(lt=> lt.Id==id),Messages.ListingTypeListed);
        }

        public IResult Update(ListingType listingType)
        {
            _listingTypeDal.Update(listingType);
            return new SuccessResult(Messages.ListingTypeUpdated);
        }
    }
}
