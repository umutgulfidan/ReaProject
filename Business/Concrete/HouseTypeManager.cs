using Business.Abstract;
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
    public class HouseTypeManager : IHouseTypeService
    {
        IHouseTypeDal _houseTypeDal;
        public HouseTypeManager(IHouseTypeDal houseTypeDal)
        {

            _houseTypeDal = houseTypeDal;

        }
        public IResult Add(HouseType houseType)
        {
            _houseTypeDal.Add(houseType);
            return new SuccessResult();
        }

        public IResult Delete(HouseType houseType)
        {
            _houseTypeDal.Delete(houseType);
            return new SuccessResult();
        }

        public IDataResult<List<HouseType>> GetAll()
        {
            return new SuccessDataResult<List<HouseType>>(_houseTypeDal.GetAll());
        }

        public IDataResult<HouseType> GetById(int id)
        {
            return new SuccessDataResult<HouseType>(_houseTypeDal.Get(ht=> ht.Id == id));
        }

        public IResult Update(HouseType houseType)
        {
            _houseTypeDal.Update(houseType);
            return new SuccessResult();
        }
    }
}
