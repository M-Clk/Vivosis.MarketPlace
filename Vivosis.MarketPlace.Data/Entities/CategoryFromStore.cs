using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class CategoryFromStore
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }
        [NotMapped]
        public CategoryFromStore ParentCategory { get; set; }
        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }
        public virtual IList<CategoryToAttributeFromStore> CategoryToAttributeFromStores { get; set; }
    }
}
