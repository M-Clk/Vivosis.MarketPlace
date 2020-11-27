using System;
using System.Collections.Generic;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.AbstractRepositories;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.Service
{
    public class LocalService :ILocalService
    {
        IProductRepositoryEf _productRepository;
        public LocalService(IProductRepositoryEf productRepository)
        {
            _productRepository = productRepository;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            throw new NotImplementedException();
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
    }
}
