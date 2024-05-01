using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IListingService
    {
        IDataResult<List<ListingDetailsView>> GetView();
        IResult Add(Listing entity);
        IResult Delete(Listing entity);
        IResult DeleteById(int id);
        IResult Update(Listing entity);
        IDataResult<Listing> GetById(int id);
        IDataResult<List<ListingDto>> GetByUserId(int userId);
        IDataResult<List<Listing>> GetAll();
        IDataResult<List<ListingDto>> GetListingDetails();
        IDataResult<List<ListingDto>> GetByFilter(ListingFilterObject filter);
        IDataResult<List<ListingDto>> GetPaginatedListingsWithFilterAndSorting(ListingFilterObject filter, SortingObject sorting, int pageNumber, int pageSize);
    }
}
