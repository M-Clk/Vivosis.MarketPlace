using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Service.Abstract
{
    public interface IStoreService
    {
        bool AddStore(Store store);
        bool DeleteStore(int storeId);
        IEnumerable<Store> GetStores();
        Store GetStoreById(int id);
        bool UpdateStore(Store store);
    }
}
