using N11CategoryService;
using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Service.Abstract
{
    public interface IN11Service
    {
        IEnumerable<CategoryFromStore> GetTopCategories();
        IEnumerable<CategoryFromStore> GetSubCategories(int categoryId);
        CategoryFromStore GetCategoryWithParents(long categoryId);
        IEnumerable<CategoryFromStoreAttribute> GetCategoryOptions(long categoryId);
        IEnumerable<CategoryFromStoreAttributeValue> GetCategoryOptionValues(long categoryOptionId);
        bool SendProduct(Data.Entities.Product productFromDb);
    }
}
