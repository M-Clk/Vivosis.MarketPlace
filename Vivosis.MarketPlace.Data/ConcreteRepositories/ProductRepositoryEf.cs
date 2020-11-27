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
        public IEnumerable<Product> GetAll()
        {
            return _dbContext.Products;
        }

        public IEnumerable<Product> GetByIdList(IEnumerable<int> idList)
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
