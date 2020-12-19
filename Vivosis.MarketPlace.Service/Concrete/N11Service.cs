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
        Authentication _auth;
        public N11Service(IStoreService storeService)
        {
            //TODO burasi hatali
            var _store = storeService.GetBoughtStores().FirstOrDefault(su => su.Store.name.ToLower().Equals("n11"))/* ?? throw new InvalidOperationException("N11 sisteminizde kayitli degil.")*/;
            _auth = new Authentication
            {
                appKey = _store?.api_key,
                appSecret = _store?.secret_key
            };
        }

        public bool CheckApiConnection()
        {
            throw new NotImplementedException();
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
        public IEnumerable<StoreCategory> GetTopCategories()
        {
            CategoryServicePortClient proxy = new CategoryServicePortClient();
            var request = new GetTopLevelCategoriesRequest();
            request.auth = _auth;
            var categories = proxy.GetTopLevelCategoriesAsync(request).Result;
            var categoryList = new List<StoreCategory>();
            foreach(var category in categories.GetTopLevelCategoriesResponse.categoryList)
            {
                var newCategory = new StoreCategory
                {
                    matched_category_name = category.name,
                    matched_category_code = category.id.ToString()
                };
                categoryList.Add(newCategory);
            }
            return categoryList;
        }
        public IEnumerable<StoreCategory> GetSubCategories(int categoryId)
        {
            var categoryList = new List<StoreCategory>();

            CategoryServicePortClient proxy = new CategoryServicePortClient();
            var request = new GetSubCategoriesRequest();
            request.auth = _auth;
            request.categoryId = categoryId;

            var subCategories = proxy.GetSubCategoriesAsync(request).Result;
            if(subCategories.GetSubCategoriesResponse.category?.FirstOrDefault().subCategoryList == null)
                return categoryList;
            foreach(var category in subCategories.GetSubCategoriesResponse.category.SelectMany(c => c.subCategoryList))
            {
                var newCategory = new StoreCategory
                {
                    matched_category_name = category.name,
                    matched_category_code = category.id.ToString()
                };
                categoryList.Add(newCategory);
            }
            return categoryList;
        }

        public StoreCategory GetCategoryWithParentsName(long categoryId)
        {
            var category = new StoreCategory();
            CategoryServicePortClient proxy = new CategoryServicePortClient();
            var request = new GetParentCategoryRequest();
            request.auth = _auth;
            while(true)
            {
                request.categoryId = categoryId;
                var parentCategory = proxy.GetParentCategoryAsync(request).Result;
                if(parentCategory.GetParentCategoryResponse.category == null)
                    return null;
                if(string.IsNullOrEmpty(category.matched_category_code))
                {
                    category.matched_category_code = parentCategory.GetParentCategoryResponse.category.id.ToString();
                    category.matched_category_name = parentCategory.GetParentCategoryResponse.category.name;
                }
                if(parentCategory.GetParentCategoryResponse.category?.parentCategory?.name == null)
                    break;
                category.matched_category_name = $"{parentCategory.GetParentCategoryResponse.category.parentCategory.name} > {category.matched_category_name}";
                categoryId = parentCategory.GetParentCategoryResponse.category.id;
            }
            return category;
        }
    }
}
