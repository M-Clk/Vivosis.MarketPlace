using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.Service.Concrete
{
    public class StoreService :IStoreService
    {
        MarketPlaceDbContext _dbContext;
        public StoreService(MarketPlaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool AddStore(Store store)
        {
            if(store == null)
                return false;
            _dbContext.Stores.Add(store);
            return _dbContext.SaveChanges() > 0;
        }

        public bool DeleteStore(int storeId)
        {
            var store = _dbContext.Stores.Find(storeId);
            if(store == null)
                return false;
            _dbContext.Stores.Remove(store);
            return _dbContext.SaveChanges() > 0;
        }

        public Store GetStoreById(int id) => _dbContext.Stores.Find(id);

        public IEnumerable<Store> GetStores() => _dbContext.Stores;

        public bool UpdateStore(Store store)
        {
            if(store == null)
                return false;
            var oldStore = _dbContext.Stores.Find(store.store_id);
            if(oldStore == null)
                return false;
            oldStore.api_key = store.api_key;
            oldStore.name = store.name;
            oldStore.secret_key = store.secret_key;
            oldStore.ssl = store.ssl;

            _dbContext.Stores.Update(oldStore);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
