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
    }
}
