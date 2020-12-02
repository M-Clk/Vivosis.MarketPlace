using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Category> GetCategories(IEnumerable<int> idList = null)
        {
            if(idList?.Any() ?? false)
                return _dbContext.Categories.Where(c => idList.Contains(c.category_id));
            else
                return _dbContext.Categories;
        }

        public IEnumerable<Product> GetProducts(IEnumerable<int> idList = null)
        {
            if(idList?.Any() ?? false)
                return _dbContext.Products.Where(p => idList.Contains(p.product_id));
            else
                return _dbContext.Products;
        }

        public int Update(IEnumerable<Product> products)
        {
            _dbContext.Products.UpdateRange(products);
            return _dbContext.SaveChanges();
        }
    }
}
