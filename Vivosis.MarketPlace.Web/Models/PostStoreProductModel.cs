using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Web.Models
{
    public class PostStoreProductModel
    {
        public StoreProduct StoreProduct { get; set; }
        public string AttributesQuery { get; set; }
    }
}
