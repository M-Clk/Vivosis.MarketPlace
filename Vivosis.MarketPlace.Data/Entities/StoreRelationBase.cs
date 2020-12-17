using System;
using System.Collections.Generic;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class StoreRelationBase
    {
        public int commission { get; set; }
        public string currency { get; set; }
        public decimal shipping_fee { get; set; }
    }
}
