using System.Collections.Generic;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Web.Models
{
    public class EditStoreProductModel
    {
        public StoreProduct StoreProduct { get; set; }
        public IEnumerable<ShipmentTemplate> ShipmentTemplates { get; set; }
    }
}
