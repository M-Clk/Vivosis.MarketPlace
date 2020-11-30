using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.AbstractRepositories;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.Service.Concrete
{
    public class GlobalService :IGlobalService
    {
        IProductRepositoryAdo _productRepository;
        public GlobalService(IProductRepositoryAdo productRepository)
        {
            _productRepository = productRepository;
        }
        public IEnumerable<Product> GetProducts(IEnumerable<int> productIdList = null) => _productRepository.GetProducts(productIdList);
        public IEnumerable<Category> GetCategories(IEnumerable<int> categoryIdList = null) => _productRepository.GetCategories(categoryIdList);
    }
}
