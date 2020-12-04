using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.Service.Concrete
{
    public class StoreService :IStoreService
    {
        AccountDbContext _dbContext;
        SystemUser _customer;
        public StoreService(AccountDbContext dbContext, IHttpContextAccessor httpContextAccessor, UserManager<SystemUser> userManager)
        {
            _dbContext = dbContext;
            _customer = userManager.FindByNameAsync(httpContextAccessor.HttpContext.User.Identity.Name).Result;
        }
        public bool AddStore(StoreUser storeUser)
        {
            if(storeUser == null)
                return false;
            //TODO api_key ve api_secret kontrolilp oyle kaydedilecek.
            _dbContext.StoreUsers.Add(storeUser);
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

        public StoreUser GetStoreById(int id) => _dbContext.StoreUsers.FirstOrDefault(sU => sU.store_id == id && sU.user_id == _customer.Id);

        public IEnumerable<StoreUser> GetStores() => _dbContext.StoreUsers.Where(sU=>sU.user_id == _customer.Id);

        public bool UpdateStore(StoreUser storeUser)
        {
            if(storeUser == null)
                return false;
            var oldStore = _dbContext.StoreUsers.FirstOrDefault(sU=>sU.store_id == storeUser.store_id && sU.user_id == storeUser.user_id);
            if(oldStore == null)
                return false;
            //TODO api_key ve api_secret kontrolilp oyle kaydedilecek.
            oldStore.api_key = storeUser.api_key;
            oldStore.secret_key = storeUser.secret_key;

            _dbContext.StoreUsers.Update(oldStore);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
