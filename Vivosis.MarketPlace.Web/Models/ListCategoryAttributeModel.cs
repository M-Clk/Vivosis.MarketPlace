using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Web.Models
{
    public class ListCategoryAttributeModel
    {
        public List<CategoryFromStoreAttribute> Attributes { get; set; }
        public string CategoryName { get; set; }
    }
}
