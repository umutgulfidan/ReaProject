using Business.Abstract;
using Business.Constants;
using Business.Dtos.Requests.LandListingReq;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LandListingManager : ILandListingService
    {
        ILandListingDal _landListingDal;
        IListingService _listingService;

        public LandListingManager(ILandListingDal landListingDal, IListingService listingService)
        {
            _landListingDal = landListingDal;
            _listingService = listingService;
        }

        [ValidationAspect(typeof(LandListingValidator))]
        public IResult Add(LandListing landListing)
        {
            _landListingDal.Add(landListing);
            return new SuccessResult();
        }


        [TransactionScopeAspect]
        public IResult Add(CreateLandListingReq req)
        {
            var listingToAdd = new Listing
            {
                ListingTypeId = req.ListingTypeId,
                PropertyTypeId = req.PropertyTypeId,
                CityId = req.CityId,
                Description = req.Description,
                Price = req.Price,
                DistrictId = req.DistrictId,
                Date = req.Date,
                SquareMeter = req.SquareMeter,
                Title = req.Title,
                UserId = req.UserId,
            };
            _listingService.Add(listingToAdd);

            var landListingToAdd = new LandListing()
            {
                ListingId = listingToAdd.ListingId,
                IslandNo = req.IslandNo,
                ParcelNo = req.ParcelNo,
                SheetNo = req.SheetNo,
                FloorEquivalent = req.FloorEquivalent,
            };

            this.Add(landListingToAdd);

            return new SuccessResult();
            
        }

        public IResult Delete(LandListing landListing)
        {
            _landListingDal.Delete(landListing);
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult Delete(DeleteLandListingReq req)
        {
            var landListingToDelete = _landListingDal.Get(ll=>ll.Id == req.Id);
            this.Delete(landListingToDelete);

            var listingToDelete = _listingService.GetById(landListingToDelete.ListingId).Data;
            _listingService.Delete(listingToDelete);

            return new SuccessResult();
        }

        public IDataResult<List<LandListing>> GetAll()
        {
            return new SuccessDataResult<List<LandListing>>(_landListingDal.GetAll());
        }

        public IDataResult<LandListing> GetById(int id)
        {
            return new SuccessDataResult<LandListing>(_landListingDal.Get(ll=>ll.Id==id));
        }

        [ValidationAspect(typeof (LandListingValidator))]
        public IResult Update(LandListing landListing)
        {

            _landListingDal.Update(landListing);
            return new SuccessResult();
        }

        [TransactionScopeAspect]

        public IResult Update(UpdateLandListingReq req)
        {
            var listingId = _landListingDal.Get(ll => ll.Id == req.Id).ListingId;

            var landListingToUpdate = new LandListing()
            {
                Id = req.Id,
                FloorEquivalent = req.FloorEquivalent,
                IslandNo = req.IslandNo,
                ListingId = listingId,
                ParcelNo = req.ParcelNo,
                SheetNo = req.SheetNo
            };

            this.Update(landListingToUpdate);

            var listingToUpdate = new Listing()
            {
                CityId = req.CityId,
                Date = req.Date,
                Description = req.Description,
                DistrictId = req.DistrictId,
                ListingId = listingId,
                ListingTypeId = req.ListingTypeId,
                Price = req.Price,
                SquareMeter = req.SquareMeter,
                Status = req.Status,
                Title = req.Title,
                UserId = req.UserId
            };

            _listingService.Update(listingToUpdate);

            return new SuccessResult(Messages.HouseListingUpdated);
        }
    }
}
