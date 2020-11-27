using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class ProductCategory
    {
        public virtual Product Product { get; set; }
        public int product_id { get; set; }

        public virtual Category Category { get; set; }
        public int category_id { get; set; }
    }
}
