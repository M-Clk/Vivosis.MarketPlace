using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Web.Models
{
    public class EditCategoryStoreModel
    {
        public bool IsStoreCategoryExist { get; set; }
        public StoreCategory StoreCategory { get; set; }
        public CategoryOptionListModel OptionsModel { get; set; }
        public string SelectedStoreName { get; set; }
    }
}
