using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.Service
{
    public class LocalService :ILocalService
    {
        MarketPlaceDbContext _dbContext;
        public LocalService(MarketPlaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddCategories(IEnumerable<Category> categories)
        {
            _dbContext.Categories.AddRange(categories);
            return _dbContext.SaveChanges();
        }

        public int AddProducts(IEnumerable<Product> products)
        {
            _dbContext.Products.AddRange(products);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Product> GetProducts(IEnumerable<int> idList = null)
        {
            if(idList?.Any() ?? false)
                return _dbContext.Products.Include(p=>p.ProductStores).Where(p => idList.Contains(p.product_id));
            else
                return _dbContext.Products.Include(p => p.ProductStores);
        }
        public IEnumerable<Category> GetCategories(IEnumerable<int> idList = null)
        {
            if(idList?.Any() ?? false)
                return _dbContext.Categories.Include(c=>c.CategoryStores).Where(c => idList.Contains(c.category_id));
            else
                return _dbContext.Categories.Include(c => c.CategoryStores);
        }
        public StoreCategory GetStoreCategory(int storeId, int categoryId)
        {
            var categoryStore = _dbContext.StoreCategories.FirstOrDefault(sc=>sc.store_id == storeId && sc.category_id == categoryId);
            return categoryStore;
        }

        public int UpdateCategories(IEnumerable<Category> categories)
        {
            _dbContext.Categories.UpdateRange(categories);
            return _dbContext.SaveChanges();
        }

        public int UpdateProducts(IEnumerable<Product> products)
        {
            _dbContext.Products.UpdateRange(products);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<ProductOption> GetProductOptions(int productId)
        {
            var productOptions = _dbContext.ProductOptions.Where(po => po.product_id == productId).Include(po => po.ProductOptionValues).ThenInclude(pov=>pov.OptionValue).Include(po=>po.Option);
            return productOptions;
        }

        public bool AddOrUpdateStoreCategory(StoreCategory storeCategory)
        {
            if(_dbContext.StoreCategories.Any(sc => sc.category_id == storeCategory.category_id && sc.store_id == storeCategory.store_id))
                _dbContext.StoreCategories.Update(storeCategory);
            else
                _dbContext.StoreCategories.Add(storeCategory);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
