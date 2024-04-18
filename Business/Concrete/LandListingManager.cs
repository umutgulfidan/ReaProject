using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
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

        public LandListingManager(ILandListingDal landListingDal)
        {
           _landListingDal = landListingDal;
        }
        [ValidationAspect(typeof(LandListingValidator))]
        public IResult Add(LandListing landListing)
        {
            _landListingDal.Add(landListing);
            return new SuccessResult();
        }

        public IResult Delete(LandListing landListing)
        {
            _landListingDal.Delete(landListing);
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
    }
}
