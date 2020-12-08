using System;
using System.Collections.Generic;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class StoreCategory :StoreRelationBase
    {
        public virtual Store Store { get; set; }
        public int store_id { get; set; }

        public virtual Category Category { get; set; }
        public int category_id { get; set; }
    }
}
