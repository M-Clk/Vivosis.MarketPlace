using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            var logedInUser = userManager.FindByNameAsync(httpContextAccessor.HttpContext.User.Identity.Name).Result;
            if(userManager.IsInRoleAsync(logedInUser, "Customer").Result)
                _customer = logedInUser;
        }
        public bool AddStore(StoreUser storeUser)
        {
            if(storeUser == null)
                return false;
            //TODO api_key ve api_secret kontrolilp oyle kaydedilecek.
            _dbContext.StoreUsers.Add(storeUser);
            return _dbContext.SaveChanges() > 0;
        }

        public bool AddStoresToUser(List<int> storeIdList)
        {
            var newStoreUserList = storeIdList.Select(id => new StoreUser
            {
                store_id = id,
                user_id = _customer.Id
            });
            _dbContext.StoreUsers.AddRange(newStoreUserList);
            return _dbContext.SaveChanges() > 0;
        }

        public bool AddStoreToUser(int storeId)
        {
            var newStoreUser = new StoreUser
            {
                Store = null,
                store_id = storeId,
                user_id = _customer.Id
            };
            _dbContext.StoreUsers.Add(newStoreUser);
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

        public IEnumerable<StoreUser> GetBoughtStores() => _dbContext.StoreUsers.Where(sU => sU.user_id == _customer.Id);
        public IEnumerable<Store> GetStores() => _dbContext.Stores;

        public bool UpdateStore(StoreUser storeUser)
        {
            if(storeUser == null)
                return false;
            var oldStore = _dbContext.StoreUsers.FirstOrDefault(sU => sU.store_id == storeUser.store_id && sU.user_id == storeUser.user_id);
            if(oldStore == null)
                return false;
            //TODO api_key ve api_secret kontrolilp oyle kaydedilecek.
            oldStore.api_key = storeUser.api_key;
            oldStore.secret_key = storeUser.secret_key;

            _dbContext.StoreUsers.Update(oldStore);
            return _dbContext.SaveChanges() > 0;
        }

        public IEnumerable<StoreUser> GetRequests() => _dbContext.StoreUsers.Where(su => !su.is_confirmed).Include(su => su.User).Include(su => su.Store);

        public bool ConfirmStoreUser(int userId, int storeId)
        {
            var storeUser = _dbContext.StoreUsers.FirstOrDefault(us => us.user_id == userId && us.store_id == storeId);
            storeUser.is_confirmed = true;
            storeUser.expire_time = DateTime.Now.AddMonths(1);
            storeUser.is_active = true;
            _dbContext.StoreUsers.Update(storeUser);
            return _dbContext.SaveChanges() > 0;
        }
        public bool RejectStoreUser(int userId, int storeId)
        {
            var storeUser = _dbContext.StoreUsers.FirstOrDefault(us => us.user_id == userId && us.store_id == storeId);
            _dbContext.StoreUsers.Remove(storeUser);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
