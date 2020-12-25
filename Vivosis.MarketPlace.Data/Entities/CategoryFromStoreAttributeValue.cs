using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class CategoryFromStoreAttributeValue
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long AttributeId { get; set; }
        [ForeignKey("AttributeId")]
        public virtual CategoryFromStoreAttribute Attribute { get; set; }
    }
}
