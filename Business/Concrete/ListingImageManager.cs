using Business.Abstract;
using Business.Constants;
using Business.Dtos.Requests.ListingImageReq;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ListingImageManager : IListingImageService
    {
        IListingImageDal _listingImageDal;
        IFileHelper _fileHelper;
        public ListingImageManager(IListingImageDal listingImageDal,IFileHelper fileHelper)
        {
            _listingImageDal = listingImageDal;
            _fileHelper = fileHelper;
        }
        public IResult Add(IFormFile file, CreateListingImageReq listingImageReq)
        {
            ListingImage listingImage = new ListingImage();
            listingImage.ListingId = listingImageReq.ListingId;
            listingImage.ImagePath = _fileHelper.Upload(file, PathConstants.ListingImagePath);
            listingImage.Date = DateTime.Now;
            listingImage.Status = true;

            _listingImageDal.Add(listingImage);

            return new SuccessResult();
        }

        public IResult Delete(DeleteListingImageReq listingImageReq)
        {
            ListingImage listingImage = _listingImageDal.Get(li=> li.Id==listingImageReq.Id);

            _fileHelper.Delete(PathConstants.ListingImagePath+listingImage.ImagePath);
            _listingImageDal.Delete(listingImage);

            return new SuccessResult();
        }

        public IResult DeleteAllByListingId(int listingId)
        {
            var data = _listingImageDal.GetAll(li=> li.ListingId==listingId);
            foreach (var item in data)
            {

                _fileHelper.Delete(PathConstants.ListingImagePath + item.ImagePath);
                _listingImageDal.Delete(item);
            }
            return new SuccessResult();
        }

        public IDataResult<List<ListingImage>> GetAll()
        {
            return new SuccessDataResult<List<ListingImage>>(_listingImageDal.GetAll());
        }

        public IResult Update(IFormFile file, ListingImage listingImage)
        {
            listingImage.ImagePath = _fileHelper.Update(file, PathConstants.ListingImagePath+listingImage.ImagePath,PathConstants.ListingImagePath);
            _listingImageDal.Update(listingImage);
            return new SuccessResult();

        }
    }
}
