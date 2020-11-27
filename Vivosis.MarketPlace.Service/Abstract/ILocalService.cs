using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Service.Abstract
{
    public interface ILocalService
    {
        IEnumerable<Product> GetAllProducts(); // TODO : productservice degisecek sitedeki veritabaniyla islem yapan servis ile n11 ile islem yapan iki servis olacak. Bu ikisini kullanan bir servis de gerekiyorsa olabilir.
        bool SendProductsToMarket(Store store, IEnumerable<int> productIdList);
        bool SendCategoriesToMarket(Store store, IEnumerable<int> categoryIdList);
        IEnumerable<Product> GetProductsFromMarket();
    }
}
