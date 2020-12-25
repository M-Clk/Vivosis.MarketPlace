using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class CategoryToAttributeFromStore
    {
        public long Id { get; set; }
        
        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual CategoryFromStore Category { get; set; }

        public long AttributeId { get; set; }
        [ForeignKey("AttributeId")]
        public virtual CategoryFromStoreAttribute Attribute { get; set; }
    }
}
