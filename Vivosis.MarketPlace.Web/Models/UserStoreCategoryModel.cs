using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Web.Models
{
    public class UserStoreCategoryModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<StoreUser> Stores { get; set; }
    }
}
