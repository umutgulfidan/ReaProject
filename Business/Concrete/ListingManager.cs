using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
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
        public void Add(Listing entity)
        {
            _listingDal.Add(entity);
        }

        public void Delete(Listing entity)
        {
            _listingDal.Delete(entity);
        }

        public List<Listing> GetAll()
        {
            return _listingDal.GetAll();
        }

        public Listing GetById(int id)
        {
            return _listingDal.Get(l => l.ListingId==id);
        }

        public void Update(Listing entity)
        {
            _listingDal.Update(entity);
        }
    }
}
