using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
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
        public IResult Add(Listing entity)
        {
            entity.ListingId = GenerateListingId(entity);
            _listingDal.Add(entity);
            return new SuccessResult(Messages.ListingAdded);
        }

        public IResult Delete(Listing entity)
        {
            _listingDal.Delete(entity);
            return new SuccessResult(Messages.ListingDeleted);
        }

        public IDataResult<List<Listing>> GetAll()
        {
            return new SuccessDataResult<List<Listing>>(_listingDal.GetAll(),Messages.ListingListed);
            
        }

        public IDataResult<Listing> GetById(int id)
        {
            return new SuccessDataResult<Listing>(_listingDal.Get(l => l.ListingId==id),Messages.ListingListed);
        }

        public IDataResult<List<ListingDto>> GetListingDetails()
        {
            return new SuccessDataResult<List<ListingDto>>(_listingDal.GetListingDetails(),Messages.GetListingDetails);

        }

        [ValidationAspect(typeof(ListingValidator))]
        public IResult Update(Listing entity)
        {
            _listingDal.Update(entity);
            return new SuccessResult(Messages.ListingUpdated);
        }

        private int GenerateListingId(Listing item)
        {
            // Belirtilen formata göre ListingId oluşturmak için bu metodu kullanabilirsiniz
            // Örneğin, bileşenleri birleştirme ve otomatik artan bir dizi numarası ekleyebilirsiniz
            string listingIdString = $"{item.PropertyTypeId:D1}{item.ListingTypeId:D1}{item.CityId:D2}{GetNextSequenceNumber():D4}";
            return int.Parse(listingIdString);
        }

        private int GetNextSequenceNumber()
        {
            // Veritabanından veya başka bir depolama mekanizmasından bir sonraki dizi numarasını almak için bu metodu uygulamanız gerekmektedir
            // Örneğin, mevcut maksimum dizi numarasını sorgulama ve artırma
            int result = _listingDal.GetAll().Count + 1;
            return result; // Bu kısmı kendiniz uygulamanız gerekmektedir
        }
    }
}
