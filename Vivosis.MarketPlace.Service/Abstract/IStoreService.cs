using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Service.Abstract
{
    public interface IStoreService
    {
        bool AddStore(StoreUser store);
        bool DeleteStore(int storeId);
        IEnumerable<StoreUser> GetStores();
        StoreUser GetStoreById(int id);
        bool UpdateStore(StoreUser store);
    }
}
