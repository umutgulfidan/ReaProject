using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DistrictManager : IDistrictService
    {
        IDistrictDal _districtDal;
        public DistrictManager(IDistrictDal districtDal)
        {
            _districtDal = districtDal;
        }
        public IResult Add(District district)
        {
            _districtDal.Add(district);
            return new SuccessResult(Messages.DistrictAdded);
        }

        public IResult Delete(District district)
        {
            _districtDal.Delete(district);
            return new SuccessResult(Messages.DistrictDeleted);
        }

        public IDataResult<List<District>> GetAll()
        {
            return new SuccessDataResult<List<District>>(_districtDal.GetAll(),Messages.DistrictListed);
        }

        public IDataResult<List<District>> GetByCityId(int cityId)
        {
            return new SuccessDataResult<List<District>>(_districtDal.GetAll(d=> d.CityId==cityId),Messages.DistrictListed);
        }

        public IDataResult<District> GetById(int id)
        {
            return new SuccessDataResult<District>(_districtDal.Get(d=> d.Id==id),Messages.DistrictListed);
        }

        public IResult Update(District district)
        {
            _districtDal.Update(district);
            return new SuccessResult(Messages.DistrictUpdated);
        }
    }
}
