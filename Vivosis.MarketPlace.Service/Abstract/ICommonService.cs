using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Service.Abstract
{
    public interface ICommonService
    {
        void SyncDatabase();
        Product GetProductToSendStore(StoreProduct productStore);
    }
}
