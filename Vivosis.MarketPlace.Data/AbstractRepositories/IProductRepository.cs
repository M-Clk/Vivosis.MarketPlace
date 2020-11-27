using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Data.AbstractRepositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByIdList(IEnumerable<int> idList);
        int Update(IEnumerable<Product> products);
    }
}
