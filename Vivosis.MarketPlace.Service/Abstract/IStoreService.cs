using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Service.Abstract
{
    public interface IStoreService
    {
        bool AddStore(StoreUser store);
        bool AddStoreToUser(int storeId);
        bool AddStoresToUser(List<int> storeIdList);
        bool DeleteStore(int storeId);
        IEnumerable<StoreUser> GetBoughtStores();
        IEnumerable<StoreUser> GetRequests();
        IEnumerable<Store> GetStores();
        StoreUser GetStoreById(int id);
        bool ConfirmStoreUser(int userId, int storeId);
        bool RejectStoreUser(int userId, int storeId);
        bool UpdateStore(StoreUser store);
    }
}
