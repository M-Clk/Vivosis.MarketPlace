using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class Product:BaseEntity
    {
        [Key]
        public int product_id { get; set; }
        [NotMapped]
        public string name { get; set; }
        [NotMapped]
        public string description { get; set; }
        [NotMapped]
        public string image_url { get; set; }
        [NotMapped]
        public string model { get; set; }
        [NotMapped]
        public decimal price { get; set; }
        [NotMapped]
        public decimal quantity{ get; set; }
        public virtual IList<ProductCategory> ProductCategories { get; set; }
        public virtual IList<StoreProduct> ProductStores { get; set; }
    }
}
