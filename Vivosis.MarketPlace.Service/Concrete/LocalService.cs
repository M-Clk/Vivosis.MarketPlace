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
        AccountDbContext _accountDbContext;
        public LocalService(MarketPlaceDbContext dbContext, AccountDbContext accountDbContext)
        {
            _dbContext = dbContext;
            _accountDbContext = accountDbContext;
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
                return _dbContext.Products.Where(p => idList.Contains(p.product_id)).Include(p => p.ProductStores).Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).ThenInclude(c => c.CategoryStores);
            else
                return _dbContext.Products.Include(p => p.ProductStores).Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).ThenInclude(c => c.CategoryStores);
        }
        public IEnumerable<Category> GetCategories(IEnumerable<int> idList = null)
        {
            if(idList?.Any() ?? false)
                return _dbContext.Categories.Where(c => idList.Contains(c.category_id)).Include(c => c.CategoryStores);
            else
                return _dbContext.Categories.Include(c => c.CategoryStores);
        }
        public StoreCategory GetStoreCategory(int storeId, int categoryId)
        {
            var categoryStore = _dbContext.StoreCategories.Include(cs => cs.CategoryOptions).FirstOrDefault(sc => sc.store_id == storeId && sc.category_id == categoryId);
            return categoryStore;
        }
        public StoreCategory GetStoreCategory(int storeCategoryId)
        {
            var categoryStore = _dbContext.StoreCategories.Include(cs => cs.CategoryOptions).FirstOrDefault(sc => sc.store_category_id == storeCategoryId);
            return categoryStore;
        }
        public StoreProduct GetStoreProduct(int storeId, int productId)
        {
            var storeProduct = _dbContext.StoreProducts.Include(sp => sp.Product).ThenInclude(p => p.ProductCategories).ThenInclude(pc => pc.Category).FirstOrDefault(sc => sc.store_id == storeId && sc.product_id == productId);
            if(storeProduct == null)
            {
                storeProduct = new StoreProduct();
                storeProduct.product_id = productId;
                storeProduct.store_id = storeId;
                storeProduct.Product = _dbContext.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).FirstOrDefault(p => p.product_id == productId);
            }
            return storeProduct;
        }
        public StoreProduct GetStoreProduct(int storeProductId) => _dbContext.StoreProducts.FirstOrDefault(sc => sc.store_product_id == storeProductId);

        public bool DeleteStoreProduct(StoreProduct storeProduct)
        {
            _dbContext.StoreProducts.Remove(storeProduct);
            return _dbContext.SaveChanges() > 0;
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
            var productOptions = _dbContext.ProductOptions.Where(po => po.product_id == productId).Include(po => po.ProductOptionValues).ThenInclude(pov => pov.OptionValue).Include(po => po.Option);
            return productOptions;
        }

        public bool AddOrUpdateStoreProduct(StoreProduct storeProduct)
        {
            if(!_dbContext.StoreProducts.Any(sp => sp.product_id == storeProduct.product_id && sp.store_id == storeProduct.store_id))
                _dbContext.StoreProducts.Add(storeProduct);
            else
                _dbContext.StoreProducts.Update(storeProduct);
            return _dbContext.SaveChanges() > 0;
        }
        public bool AddOrUpdateStoreCategory(StoreCategory storeCategory)
        {
            if(string.IsNullOrEmpty(storeCategory?.matched_category_code) || string.IsNullOrEmpty(storeCategory?.matched_category_name))
                return false;
            storeCategory.is_matched = true;
            if(_dbContext.StoreCategories.Any(sc => sc.category_id == storeCategory.category_id && sc.store_id == storeCategory.store_id))
            {
                var toBeDeletedCategoryOptions = _dbContext.CategoryOptions.Include(co => co.CategoryOptionValues).Where(co => co.store_category_id == storeCategory.store_category_id);
                _dbContext.CategoryOptions.RemoveRange(toBeDeletedCategoryOptions);
                _dbContext.CategoryOptions.AddRange(storeCategory.CategoryOptions);
                storeCategory.CategoryOptions = null;
                _dbContext.StoreCategories.Update(storeCategory);
            }
            else
                _dbContext.StoreCategories.Add(storeCategory);
            return _dbContext.SaveChanges() > 0;
        }

        public IEnumerable<CategoryOptionValue> GetCategoryOptionValues(int categoryOptionId)
        {
            var categoryOptionValues = _dbContext.CategoryOptionValues.Where(cov => cov.category_option_id == categoryOptionId);
            return categoryOptionValues;
        }
        public IEnumerable<Option> GetAllOptions() => _dbContext.Options.Include(o => o.OptionValues);

        public IEnumerable<OptionValue> GetOptionValues(int optionId)
        {
            var optionValues = _dbContext.OptionValues.Where(ov => ov.option_id == optionId);
            return optionValues;
        }

        public IEnumerable<CategoryFromStoreAttribute> GetCategoryOptions(int categoryId, int storeId)
        {
            var category = _dbContext.StoreCategories.First(cs => cs.category_id == categoryId && cs.store_id == storeId);
            var categoryToAttributes = _accountDbContext.CategoryToAttributeFromStores.Where(ca => ca.CategoryId.ToString() == category.matched_category_code).Include(ca => ca.Attribute).ThenInclude(a => a.AttributeValues);
            return categoryToAttributes.Select(ca => ca.Attribute);
        }

    }
}
