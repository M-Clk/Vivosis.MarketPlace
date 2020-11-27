using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class Category: BaseEntity
    {
        [Key]
        public int category_id { get; set; }
        [NotMapped]
        public string name { get; set; }
        [NotMapped]
        public string description { get; set; }
        public virtual IList<ProductCategory> CategoryProducts { get; set; }
        public virtual IList<StoreCategory> CategoryStores { get; set; }
    }
}
