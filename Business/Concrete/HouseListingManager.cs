using Business.Abstract;
using Business.Dtos.Requests;
using Business.Entities.Requests;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Add(CreateHouseListingDto entity)
        {

            var listingToAdd = new Listing
            {
                CityId = entity.CityId,
                Description = entity.Description,
                Price = entity.Price,
                DistrictId = entity.DistrictId,
                Date = entity.Date,
                SquareMeter = entity.SquareMeter,
                Title = entity.Title,
                UserId = entity.UserId,
            };
            
            _listingService.Add(listingToAdd);

            var id = listingToAdd.ListingId;

            var houseListingToAdd = new HouseListing
            {
                HasGarden = entity.HasGarden,
                HasFurniture = entity.HasFurniture,
                HasElevator = entity.HasElevator,
                HasBalcony = entity.HasBalcony,
                FloorCount = entity.FloorCount,
                Address = entity.Address,
                BathroomCount = entity.BathroomCount,
                BuildingAge = entity.BuildingAge,
                CurrentFloor = entity.CurrentFloor,
                HasParking = entity.HasParking,
                IsInGatedCommunity = entity.IsInGatedCommunity,
                ListingId = id,
                LivingRoomCount = entity.LivingRoomCount,
                RoomCount = entity.RoomCount,
                TypeId = entity.TypeId,

            };

            this.Add(houseListingToAdd);
        }

        public void Add(HouseListing entity)
        {
            _houseListingDal.Add(entity);
        }

        public void Delete(HouseListing entity)
        {
            _houseListingDal.Delete(entity);
        }
        public List<HouseListing> GetAll()
        {
            return _houseListingDal.GetAll();
        }

        public HouseListing GetById(int id)
        {
             return _houseListingDal.Get(hl=>hl.HouseListingId==id); ;
        }

        public void Update(HouseListing entity)
        {
            _houseListingDal.Update(entity);
        }
    }
}
