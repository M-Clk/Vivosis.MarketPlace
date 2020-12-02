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
                return _dbContext.Products.Where(p => idList.Contains(p.product_id));
            else
                return _dbContext.Products;
        }
        public IEnumerable<Category> GetCategories(IEnumerable<int> idList = null)
        {
            if(idList?.Any() ?? false)
                return _dbContext.Categories.Where(c => idList.Contains(c.category_id));
            else
                return _dbContext.Categories;
        }

        public IEnumerable<Product> GetProductsFromMarket()
        {
            throw new NotImplementedException();
        }
        public bool RefreshProducts()
        {
            throw new NotImplementedException();
        }

        public bool SendCategoriesToMarket(Store store, IEnumerable<int> categoryIdList)
        {
            throw new NotImplementedException();
        }

        public bool SendProductsToMarket(Store store, IEnumerable<int> productIdList)
        {
            throw new NotImplementedException();
        }

        public int UpdateCategories(IEnumerable<Category> categories)
        {
            throw new NotImplementedException();
        }

        public int UpdateProducts(IEnumerable<Product> products)
        {
            _dbContext.Products.UpdateRange(products);
            return _dbContext.SaveChanges();
        }
    }
}
