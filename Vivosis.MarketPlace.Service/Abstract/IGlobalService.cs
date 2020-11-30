using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Service.Abstract
{
    public interface IGlobalService
    {
        IEnumerable<Product> GetProducts(IEnumerable<int> productIdList = null);
        IEnumerable<Category> GetCategories(IEnumerable<int> categoryIdList = null);
    }
}
