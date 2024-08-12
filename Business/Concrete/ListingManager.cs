using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
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
    public class ListingManager : IListingService
    {
        IListingDal _listingDal;
        public ListingManager(IListingDal listingDal)
        {
            _listingDal = listingDal;
        }

        [ValidationAspect(typeof(ListingValidator))]
        [CacheRemoveAspect("IListingService.Get")]
        public IResult Add(Listing entity)
        {
            entity.ListingId = GenerateListingId(entity);
            _listingDal.Add(entity);
            return new SuccessResult(Messages.ListingAdded);
        }
        public IDataResult<List<ListingDetailsView>> GetView()
        {
            return new SuccessDataResult<List<ListingDetailsView>>(_listingDal.GetView(),"View başarıyla getirildi");
        }


        [CacheRemoveAspect("IListingService.Get")]
        public IResult Delete(Listing entity)
        {
            entity.Status = false;
            _listingDal.Update(entity);
            return new SuccessResult(Messages.ListingDeleted);
        }

        [CacheRemoveAspect("IListingService.Get")]
        public IResult DeleteById(int id)
        {
            var result = this.GetById(id).Data;
            if(result == null)
            {
                return new ErrorResult("İlan bulunamadı");
            }

            result.Status = false;
            _listingDal.Update(result);
            return new SuccessResult(Messages.ListingDeleted);
        }

        public IDataResult<List<ListingDto>> GetByFilter(ListingFilterObject filter)
        {
            return new SuccessDataResult<List<ListingDto>>(_listingDal.GetListingsByFilter(filter));
        }

        [CacheAspect(10)]
        public IDataResult<List<Listing>> GetAll()
        {
            return new SuccessDataResult<List<Listing>>(_listingDal.GetAll(),Messages.ListingListed);
            
        }

        public IDataResult<Listing> GetById(int id)
        {
            return new SuccessDataResult<Listing>(_listingDal.Get(l=>l.ListingId==id),Messages.ListingListed);
        }

        public IDataResult<List<ListingDto>> GetListingDetails()
        {
            return new SuccessDataResult<List<ListingDto>>(_listingDal.GetListingDetails(),Messages.GetListingDetails);

        }

        [ValidationAspect(typeof(ListingValidator))]
        [CacheRemoveAspect("IListingService.Get")]
        public IResult Update(Listing entity)
        {
            _listingDal.Update(entity);
            return new SuccessResult(Messages.ListingUpdated);
        }

        private int GenerateListingId(Listing item)
        {
            // Belirtilen formata göre ListingId oluşturmak için bu metodu kullanabilirsiniz
            // Örneğin, bileşenleri birleştirme ve otomatik artan bir dizi numarası ekleyebilirsiniz
            string listingIdString = $"{item.PropertyTypeId:D1}{item.ListingTypeId:D1}{item.CityId:D2}{GetNextSequenceNumber(item):D4}";
            return int.Parse(listingIdString);
        }

        private int GetNextSequenceNumber(Listing item)
        {
            // Veritabanından veya başka bir depolama mekanizmasından bir sonraki dizi numarasını almak için bu metodu uygulamanız gerekmektedir
            // Örneğin, mevcut maksimum dizi numarasını sorgulama ve artırma
            int result = _listingDal.GetAll(l=> l.PropertyTypeId == item.PropertyTypeId).Count + 1;
            return result; // Bu kısmı kendiniz uygulamanız gerekmektedir
        }

        public IDataResult<List<ListingDto>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<ListingDto>>(_listingDal.GetListingDetailsByUserId(userId),Messages.ListingListed);
        }

        public IDataResult<List<ListingDto>> GetPaginatedListingsWithFilterAndSorting(ListingFilterObject filter, SortingObject sorting, int pageNumber, int pageSize)
        {
            return new SuccessDataResult<List<ListingDto>>(_listingDal.GetPaginatedListingsWithFilterAndSorting( filter, sorting, pageNumber, pageSize));
        }

        public IDataResult<int> GetListingCount()
        {
            return new SuccessDataResult<int>(_listingDal.GetAll().Count);
        }

        public IDataResult<int> GetActiveListingCount()
        {
            return new SuccessDataResult<int>(_listingDal.GetActiveListingCount());
        }

        public IDataResult<int> GetPassiveListingCount()
        {
            return new SuccessDataResult<int>(_listingDal.GetPassiveListingCount());
        }

        public IDataResult<bool> GetListingStatus(int listingId)
        {
            return new SuccessDataResult<bool>(_listingDal.Get(l=> l.ListingId==listingId).Status);
        }
    }
}
