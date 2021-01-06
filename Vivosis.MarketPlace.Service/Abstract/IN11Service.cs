using System.Collections.Generic;
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
        StoreProduct SendProduct(Product productFromDb, Dictionary<string, string> attributePairs, ref string errorMessage);
        IEnumerable<ShipmentTemplate> GetShipmentTemplates();
        bool DeleteProduct(long productCode);
    }
}
