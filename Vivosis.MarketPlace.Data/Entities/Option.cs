using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class Option
    {
        [Key]
        public int option_id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public int sort_order { get; set; }
        public virtual IList<ProductOption> ProductOptions { get; set; }
        public virtual IList<OptionValue> OptionValues { get; set; }
    }
}