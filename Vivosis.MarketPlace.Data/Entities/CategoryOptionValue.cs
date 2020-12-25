using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class CategoryOptionValue
    {
        [Key]
        public int category_option_value_id { get; set; }
        public int category_option_id { get; set; }
        public virtual CategoryOption CategoryOption { get; set; }
        public int option_value_id { get; set; }
        public virtual OptionValue OptionValue { get; set; }

        public int store_category_value_id { get; set; }
        public string store_category_value_name { get; set; }
    }
}
