using Business.Dtos.Requests;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IHouseListingService
    {
        void Add(HouseListing entity);
        void Add(CreateHouseListingDto req);
        void Delete(HouseListing entity);
        void Update(HouseListing entity);
        HouseListing GetById(int id);
        List<HouseListing> GetAll();
    }
}
