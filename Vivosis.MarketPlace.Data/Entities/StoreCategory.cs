using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    [Table("storecategory")]
    public class StoreCategory :StoreRelationBase
    {
        [Key]
        public int store_category_id { get; set; }

        public virtual Store Store { get; set; }
        public int store_id { get; set; }

        public virtual Category Category { get; set; }
        public int category_id { get; set; }
        
        public bool is_matched { get; set; }
        public string matched_category_name { get; set; }
        public string matched_category_code { get; set; }
        
        public virtual IList<CategoryOption> CategoryOptions { get; set; }
    }
}
