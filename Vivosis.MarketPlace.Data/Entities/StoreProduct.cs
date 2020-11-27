using System;
using System.Collections.Generic;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class StoreProduct
    {
        public virtual Store Store { get; set; }
        public int store_id { get; set; }

        public virtual Product Product { get; set; }
        public int product_id { get; set; }
    }
}
