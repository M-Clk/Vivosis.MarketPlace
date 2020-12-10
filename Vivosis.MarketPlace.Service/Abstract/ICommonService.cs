using System;
using System.Collections.Generic;
using System.Text;

namespace Vivosis.MarketPlace.Service.Abstract
{
    public interface ICommonService
    {
        void SyncLocalProducts();
        void SyncLocalOptions();
    }
}
