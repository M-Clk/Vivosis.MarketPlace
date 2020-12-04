using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Service.Abstract;
using N11CategoryService;
using Microsoft.EntityFrameworkCore;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Data;
using System.Linq;

namespace Vivosis.MarketPlace.Service.Concrete
{
    public class N11Service :IN11Service
    {
        Store _store;
        Authentication _auth;
        public N11Service(MarketPlaceDbContext dbContex)
        {
            //TODO burasi hatali
            var store = dbContex.Stores.Where(store => store.name.ToLower().Equals("n11")) ?? throw new InvalidOperationException("N11 sisteminizde kayitli degil.");
            _store = store.Include(s=>s.StoreProducts).Include(s=>s.StoreCategories).First();
            //_auth = new Authentication
            //{
            //    appKey = _store.api_key,
            //    appSecret = _store.secret_key
            //};
        }
        public GetTopLevelCategoriesResponse1 Test()
        {
            CategoryServicePortClient c = new CategoryServicePortClient();
            GetTopLevelCategoriesRequest req = new GetTopLevelCategoriesRequest();
            req.auth = _auth;
            GetTopLevelCategoriesResponse1 res = c.GetTopLevelCategoriesAsync(req).Result;
            return res;
        }
        public GetSubCategoriesResponse1 Test2()
        {
            CategoryServicePortClient c = new CategoryServicePortClient();
            var req = new GetCategoryAttributesRequest();
            req.auth = _auth;
            req.categoryId = 1002841;
            var res = c.GetCategoryAttributesAsync(req).Result;
            return new GetSubCategoriesResponse1();
        }
    }
}
