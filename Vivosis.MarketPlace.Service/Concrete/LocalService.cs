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
                return _dbContext.Products.Where(p => idList.Contains(p.product_id)).Include(p=>p.ProductStores).Include(p=>p.ProductCategories).ThenInclude(pc=>pc.Category).ThenInclude(c=>c.CategoryStores);
            else
                return _dbContext.Products.Include(p => p.ProductStores).Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).ThenInclude(c => c.CategoryStores);
        }
        public IEnumerable<Category> GetCategories(IEnumerable<int> idList = null)
        {
            if(idList?.Any() ?? false)
                return _dbContext.Categories.Where(c => idList.Contains(c.category_id)).Include(c=>c.CategoryStores);
            else
                return _dbContext.Categories.Include(c => c.CategoryStores);
        }
        public StoreCategory GetStoreCategory(int storeId, int categoryId)
        {
            var categoryStore = _dbContext.StoreCategories.Include(cs=>cs.CategoryOptions).FirstOrDefault(sc=>sc.store_id == storeId && sc.category_id == categoryId);
            return categoryStore;
        }
        public StoreCategory GetStoreCategory(int storeCategoryId)
        {
            var categoryStore = _dbContext.StoreCategories.Include(cs=>cs.CategoryOptions).FirstOrDefault(sc=>sc.store_category_id == storeCategoryId);
            return categoryStore;
        }
        public StoreProduct GetStoreProduct(int storeId, int productId)
        {
            var storeProduct = _dbContext.StoreProducts.FirstOrDefault(sc=>sc.store_id == storeId && sc.product_id == productId);
            return storeProduct;
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
            if(string.IsNullOrEmpty(storeCategory?.matched_category_code) || string.IsNullOrEmpty(storeCategory?.matched_category_name))
                    return false;
            storeCategory.is_matched = true;
            if(_dbContext.StoreCategories.Any(sc => sc.category_id == storeCategory.category_id && sc.store_id == storeCategory.store_id))
                _dbContext.StoreCategories.Update(storeCategory);
            else
                _dbContext.StoreCategories.Add(storeCategory);
            return _dbContext.SaveChanges() > 0;
        }

        public IEnumerable<Option> GetAllOptions() => _dbContext.Options.Include(o => o.OptionValues);
    }
}
