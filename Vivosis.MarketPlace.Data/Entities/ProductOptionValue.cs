using System.ComponentModel.DataAnnotations;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class ProductOptionValue
    {
        [Key]
        public int product_option_value_id { get; set; }
        public int product_option_id { get; set; }
        public virtual ProductOption ProductOption { get; set; }
        public int option_value_id { get; set; }
        public virtual OptionValue OptionValue { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal weight { get; set; }
        public int point { get; set; }

    }
}
