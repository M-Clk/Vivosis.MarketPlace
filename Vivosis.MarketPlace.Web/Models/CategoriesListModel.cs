using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Web.Models
{
    public class CategoriesListModel
    {
        public IEnumerable<CategoryFromStore> Categories { get; set; }
        public long SelectedCategoryId { get; set; }
    }
}
