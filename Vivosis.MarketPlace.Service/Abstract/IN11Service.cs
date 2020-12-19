using N11CategoryService;
using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Service.Abstract
{
    public interface IN11Service
    {
        GetTopLevelCategoriesResponse1 Test();
        GetSubCategoriesResponse1 Test2();
        IEnumerable<StoreCategory> GetTopCategories();
        IEnumerable<StoreCategory> GetSubCategories(int categoryId);
        StoreCategory GetCategoryWithParentsName(long categoryId);
    }
}
