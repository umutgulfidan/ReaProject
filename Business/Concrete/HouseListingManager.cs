using Business.Abstract;
using Business.BusinessAspects;
using Business.Dtos.Requests;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Aspects.Autofac.Caching.CacheAspect;

namespace Business.Concrete
{
    public class HouseListingManager : IHouseListingService
    {
        IHouseListingDal _houseListingDal;
        IListingService _listingService;
        public HouseListingManager(IHouseListingDal houseListingDal,IListingService listingService)
        {
            _houseListingDal = houseListingDal;
            _listingService = listingService;
        }

        [ValidationAspect(typeof(HouseListing))]
        public void AddHouseListing(HouseListing houseListing)
        {
            _houseListingDal.Add(houseListing);
        }

        [SecuredOperation("admin",true)]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IHouseListingService.Get")]
        public IResult Add(CreateHouseListingReq req)
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

            var id = listingToAdd.ListingId;

            var houseListingToAdd = new HouseListing
            {
                HasGarden = req.HasGarden,
                HasFurniture = req.HasFurniture,
                HasElevator = req.HasElevator,
                HasBalcony = req.HasBalcony,
                FloorCount = req.FloorCount,
                Address = req.Address,
                BathroomCount = req.BathroomCount,
                BuildingAge = req.BuildingAge,
                CurrentFloor = req.CurrentFloor,
                HasParking = req.HasParking,
                IsInGatedCommunity = req.IsInGatedCommunity,
                ListingId = id,
                LivingRoomCount = req.LivingRoomCount,
                RoomCount = req.RoomCount,
                TypeId = req.TypeId,

            };

            this.AddHouseListing(houseListingToAdd);
            return new SuccessResult();
        }

        [SecuredOperation("admin",true)]
        [CacheRemoveAspect("IHouseListingService.Get")]
        public IResult Delete(DeleteHouseListingReq req)
        {
            var houseListingToDelete = _houseListingDal.Get(hl => hl.HouseListingId == req.HouseListingId);

            _houseListingDal.Delete(houseListingToDelete);

            var listingToDelete = _listingService.GetById(houseListingToDelete.ListingId).Data;

            _listingService.Delete(listingToDelete);

            return new SuccessResult();
            
        }

        [CacheAspect(10)]
        public IDataResult<List<HouseListing>> GetAll()
        {
            return new SuccessDataResult<List<HouseListing>>(_houseListingDal.GetAll());
        }

        [CacheAspect(10)]
        public IDataResult<HouseListing> GetById(int id)
        {
             return new SuccessDataResult<HouseListing>(_houseListingDal.Get(hl=>hl.HouseListingId==id)) ;
        }

        [SecuredOperation("admin,moderator",true)]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IHouseListingService.Get")]
        public IResult Update(UpdateHouseListingReq req)
        {
            var listingId = _houseListingDal.Get(hl=>hl.HouseListingId == req.HouseListingId).ListingId;

            var houseListingToUpdate = new HouseListing()
            {
                HouseListingId = req.HouseListingId,
                HasGarden = req.HasGarden,
                HasFurniture = req.HasFurniture,
                HasElevator = req.HasElevator,
                HasBalcony = req.HasBalcony,
                FloorCount = req.FloorCount,
                Address = req.Address,
                BathroomCount = req.BathroomCount,
                BuildingAge = req.BuildingAge,
                CurrentFloor = req.CurrentFloor,
                HasParking = req.HasParking,
                IsInGatedCommunity = req.IsInGatedCommunity,
                ListingId = listingId,
                LivingRoomCount = req.LivingRoomCount,
                RoomCount = req.RoomCount,
                TypeId = req.TypeId,
            };

            this.UpdateHouseListing(houseListingToUpdate);

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

            return new SuccessResult();
        }

        [ValidationAspect(typeof(HouseListing))]
        public void UpdateHouseListing(HouseListing houseListing)
        {
            _houseListingDal.Update(houseListing);
        }

        public IDataResult<List<HouseListingDto>> GetHouseListingDtos()
        {
            return new SuccessDataResult<List<HouseListingDto>>(_houseListingDal.GetHouseListings());
        }

        public IDataResult<HouseListingDetailDto> GetHouseListingDetail(int listingId)
        {
            return new SuccessDataResult<HouseListingDetailDto>(_houseListingDal.GetHouseListingDetails(listingId));
        }
    }
}
