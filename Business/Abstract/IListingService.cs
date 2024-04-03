﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IListingService
    {
        void Add(Listing entity);
        void Delete(Listing entity);
        void Update(Listing entity);
        Listing GetById(int id);
        List<Listing> GetAll();

    }
}
