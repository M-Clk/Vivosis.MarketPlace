using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.AbstractRepositories;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Data.ConcreteRepositories
{
    public class ProductRepositoryEf :IProductRepositoryEf
    {
        MarketPlaceDbContext _dbContext;
        public ProductRepositoryEf(MarketPlaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _dbContext.Products;
        }

        public IEnumerable<Category> GetCategories(IEnumerable<int> idList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts(IEnumerable<int> idList)
        {
            throw new NotImplementedException();
        }

        public int Update(IEnumerable<Product> products)
        {
            _dbContext.Products.UpdateRange(products);
            return _dbContext.SaveChanges();
        }
    }
}
