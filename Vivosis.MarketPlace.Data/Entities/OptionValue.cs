using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class OptionValue
    {
        [Key]
        public int option_value_id { get; set; }
        public int option_id { get; set; }
        public int sort_order { get; set; }
        public string name { get; set; }
        public virtual Option Option { get; set; }
        public virtual IList<ProductOptionValue> ProductOptionValues { get; set; }
    }
}
