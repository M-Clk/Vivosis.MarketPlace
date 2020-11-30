using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Data.AbstractRepositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(IEnumerable<int> idList);
        IEnumerable<Category> GetCategories(IEnumerable<int> idList);
        int Update(IEnumerable<Product> products);
    }
}
