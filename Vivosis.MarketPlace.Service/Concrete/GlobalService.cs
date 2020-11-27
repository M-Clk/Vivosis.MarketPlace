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
        public IEnumerable<Product> GetProductsByIdList(IEnumerable<int> productIdList) => _productRepository.GetByIdList(productIdList);

        public IEnumerable<Product> GetAllProducts() => _productRepository.GetAll();
    }
}
