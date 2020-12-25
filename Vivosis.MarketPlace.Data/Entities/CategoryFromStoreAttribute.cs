using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class CategoryFromStoreAttribute
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public virtual IList<CategoryFromStoreAttributeValue> AttributeValues { get; set; }
        public virtual IList<CategoryToAttributeFromStore> CategoryToAttributeFromStores { get; set; }
    }
}
