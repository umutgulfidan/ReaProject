using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IHouseTypeService
    {
        IDataResult<List<HouseType>> GetAll();
        IDataResult<HouseType> GetById(int id);
        IResult Add(HouseType houseType);
        IResult Delete(HouseType houseType);
        IResult Update(HouseType houseType);
    }
}
