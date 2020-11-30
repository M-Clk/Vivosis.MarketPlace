﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class Store//Orn:N11
    {
        [Key]
        public int store_id { get; set; }
        public string name { get; set; } 
        public string url { get; set; }
        public string ssl { get; set; }
        public string api_key { get; set; }
        public string secret_key { get; set; }
        public int user_id { get; set; }
        [ForeignKey("user_id")]
        public virtual SystemUser User { get; set; }
        public virtual IList<StoreCategory> StoreCategories { get; set; }
        public virtual IList<StoreProduct> StoreProducts{ get; set; }
    }
}
