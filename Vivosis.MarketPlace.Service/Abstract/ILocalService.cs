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
        int AddProducts(IEnumerable<Product> products);
        int UpdateProducts(IEnumerable<Product> products);
        int AddCategories(IEnumerable<Category> categories);
        int UpdateCategories(IEnumerable<Category> categories);
        bool SendProductsToMarket(Store store, IEnumerable<int> productIdList);
        bool SendCategoriesToMarket(Store store, IEnumerable<int> categoryIdList);
        IEnumerable<Product> GetProductsFromMarket();
    }
}
