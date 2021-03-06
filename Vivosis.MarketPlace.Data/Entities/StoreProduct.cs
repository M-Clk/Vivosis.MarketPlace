﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivosis.MarketPlace.Data.Entities
{
    [Table("storeproduct")]
    public class StoreProduct :StoreRelationBase
    {
        [Key]
        public int store_product_id { get; set; }
        public virtual Store Store { get; set; }
        public int store_id { get; set; }

        public virtual Product Product { get; set; }
        public int product_id { get; set; }

        public decimal sale_price { get; set; }
        public decimal strikethrough_price{ get; set; }
        public long catalog_id{ get; set; }
        public string origin { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string matched_product_code { get; set; }
        public string shipment_template { get; set; }
        public string attribute_query { get; set; }
        public bool is_active { get; set; }
        public bool is_sent { get; set; }
    }
}
