using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class CategoryOption
    {
        [Key]
        public int category_option_id { get; set; }

        public int store_category_id { get; set; }
        public virtual StoreCategory StoreCategory { get; set; }

        public int option_id { get; set; }
        public virtual Option Option { get; set; }

        public bool is_required { get; set; }
        public string matched_store_option_name { get; set; }

        public virtual IList<CategoryOptionValue> CategoryOptionValues { get; set; }
    }
}
