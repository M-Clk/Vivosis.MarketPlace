using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class Store //Orn:N11
    {
        [Key]
        public int store_id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string url { get; set; }
        public string ssl { get; set; }
        public virtual IList<StoreUser> UserStores { get; set; }
        public virtual IList<StoreCategory> StoreCategories { get; set; }
        public virtual IList<StoreProduct> StoreProducts{ get; set; }
    }
}
