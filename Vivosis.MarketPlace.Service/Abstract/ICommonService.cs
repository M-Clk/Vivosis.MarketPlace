using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Service.Abstract
{
    public interface ICommonService
    {
        void SyncDatabase();
        void SaveShipmentTemplate(List<ShipmentTemplate> templates);
        Product GetProductToSendStore(StoreProduct productStore);
    }
}
