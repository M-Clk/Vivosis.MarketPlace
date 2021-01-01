using System;
using System.Collections.Generic;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int product_id { get; set; }
        public int order { get; set; }
        public string url { get; set; }
    }
}
