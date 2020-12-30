using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Web.Models
{
    public class CategoryOptionValueModel
    {
        public string StoreName { get; set; }
        public string CategoryNameFromStore { get; set; }
        public long CategoryOptionIdFromStore { get; set; }
        public bool LocalCategoryOptionValuesExist { get; set; }
        public IEnumerable<CategoryFromStoreAttributeValue> OptionValuesFromStore { get; set; }
        public IEnumerable<MatchedCategoryOptionValueModel> MatchedCategoryOptionValues { get; set; }
    }
}
