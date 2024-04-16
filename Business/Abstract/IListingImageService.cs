using Business.Dtos.Requests.ListingImageReq;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IListingImageService
    {
        IResult Add(IFormFile file, CreateListingImageReq listingImageReq,int userIdentifier);
        IResult Update(IFormFile file, ListingImage listingImageReq);
        IResult Delete(DeleteListingImageReq listingImageReq);
        IResult DeleteAllByListingId(int listingId);

        IDataResult<List<ListingImage>> GetAll();
        IDataResult<List<ListingImage>> GetByListingId(int listingId);
    }
}
