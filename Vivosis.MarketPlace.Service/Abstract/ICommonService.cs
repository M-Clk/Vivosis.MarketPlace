using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Service.Abstract
{
    public interface ICommonService
    {
        void SyncDatabase();
        void SaveShipmentTemplates(IEnumerable<ShipmentTemplate> templates);
        IEnumerable<ShipmentTemplate> GetShipmentTemplate();
        Product GetProductToSendStore(StoreProduct productStore);
    }
}
