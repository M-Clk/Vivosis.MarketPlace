using System;
using System.Collections.Generic;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class StoreUser
    {
        public string api_key { get; set; }
        public string secret_key { get; set; }

        public virtual Store Store { get; set; }
        public int store_id { get; set; }

        public virtual SystemUser User { get; set; }
        public int user_id { get; set; }

    }
}
