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
using Entities.DTOs;
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
        public IResult LandListingAdd(LandListing landListing)
        {
            _landListingDal.Add(landListing);
            return new SuccessResult(Messages.LandListingAdded);
        }


        [TransactionScopeAspect]
        public IDataResult<LandListing>Add(CreateLandListingReq req)
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
                Address = req.Address,
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

            this.LandListingAdd(landListingToAdd);

            return new SuccessDataResult<LandListing>(landListingToAdd,Messages.LandListingAdded);
            
        }

        public IResult LandListingDelete(LandListing landListing)
        {
            _landListingDal.Delete(landListing);
            return new SuccessResult(Messages.LandListingDeleted);
        }

        [TransactionScopeAspect]
        public IResult Delete(DeleteLandListingReq req)
        {
            var landListingToDelete = _landListingDal.Get(ll=>ll.Id == req.Id);
            this.LandListingDelete(landListingToDelete);

            var listingToDelete = _listingService.GetById(landListingToDelete.ListingId).Data;
            _listingService.Delete(listingToDelete);

            return new SuccessResult(Messages.LandListingDeleted);
        }

        public IDataResult<List<LandListing>> GetAll()
        {
            return new SuccessDataResult<List<LandListing>>(_landListingDal.GetAll(), Messages.LandListingListed);
        }

        public IDataResult<LandListing> GetById(int id)
        {
            return new SuccessDataResult<LandListing>(_landListingDal.Get(ll=>ll.Id==id),Messages.LandListingListed);
        }

        public IDataResult<List<LandListingDto>> GetLandListings()
        {
            return new SuccessDataResult<List<LandListingDto>>(_landListingDal.GetLandListings(),Messages.LandListingListed);
        }
        public IDataResult<LandListingDetailDto> GetLandListingDetail(int listingId)
        {
            return new SuccessDataResult<LandListingDetailDto>(_landListingDal.GetLandListingDetail(listingId),Messages.GetLandListingDetail);
        }

        [ValidationAspect(typeof (LandListingValidator))]
        public IResult LandListingUpdate(LandListing landListing)
        {

            _landListingDal.Update(landListing);
            return new SuccessResult(Messages.LandListingUpdated);
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
                SheetNo = req.SheetNo,
            };

            this.LandListingUpdate(landListingToUpdate);

            var listingToUpdate = new Listing()
            {
                Address = req.Address,
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
                UserId = req.UserId,
                PropertyTypeId = req.PropertyTypeId
            };

            _listingService.Update(listingToUpdate);

            return new SuccessResult(Messages.LandListingUpdated);
        }

        public IDataResult<List<LandListingDto>> GetAllByFilter(LandListingFilterObject filter)
        {
            return new SuccessDataResult<List<LandListingDto>>(_landListingDal.GetAllByFilter(filter),Messages.LandListingFiltered);
        }
    }
}
