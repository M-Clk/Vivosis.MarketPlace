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
        public string name { get; set; }
        [NotMapped]
        public string description { get; set; }
        public string image_url { get; set; }
        [NotMapped]
        public string model { get; set; }
        public decimal price { get; set; }
        public decimal quantity{ get; set; }
        public virtual IList<ProductOption> ProductOptions { get; set; }
        public virtual IList<ProductCategory> ProductCategories { get; set; }
        public virtual IList<StoreProduct> ProductStores { get; set; }
        public virtual IList<ProductImage> ProductImages { get; set; }
    }
}
