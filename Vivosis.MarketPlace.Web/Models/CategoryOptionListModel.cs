using System.Collections.Generic;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Web.Models
{
    public class CategoryOptionListModel
    {
        public string SelectedStoreName { get; set; }
        public IEnumerable<MatchedCategoryOptionModel> CategoryOptions { get; set; }
        public IEnumerable<Option> Options { get; set; }
        public bool LocalCategoryOptionsExist { get; set; }

    }
}
