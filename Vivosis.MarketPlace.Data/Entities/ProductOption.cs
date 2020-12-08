using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class ProductOption
    {
        [Key]
        public int product_option_id { get; set; }
        public int product_id { get; set; }
        public virtual Product Product { get; set; }
        public int option_id { get; set; }
        public virtual Option Option { get; set; }

        public bool is_required { get; set; }
        public virtual IList<ProductOptionValue> ProductOptionValues { get; set; }
    }
}
