using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Service.Abstract
{
    public interface ILocalService
    {
        IEnumerable<Product> GetProducts(IEnumerable<int> productIdList = null);
        IEnumerable<Category> GetCategories(IEnumerable<int> categoryIdList = null);
        StoreCategory GetStoreCategory(int storeId, int categoryId);
        StoreCategory GetStoreCategory(int storeCategoryId);
        IEnumerable<CategoryOptionValue> GetCategoryOptionValues(int categoryOptionId);
        StoreProduct GetStoreProduct(int storeId, int productId);
        bool AddOrUpdateStoreCategory(StoreCategory storeCategory);
        int AddProducts(IEnumerable<Product> products);
        int UpdateProducts(IEnumerable<Product> products);
        int AddCategories(IEnumerable<Category> categories);
        int UpdateCategories(IEnumerable<Category> categories);
        IEnumerable<ProductOption> GetProductOptions(int productId);
        IEnumerable<Option> GetAllOptions();
        IEnumerable<OptionValue> GetOptionValues(int optionId);
        IEnumerable<CategoryFromStoreAttribute> GetCategoryOptions(int categoryId, int storeId);
    }
}
