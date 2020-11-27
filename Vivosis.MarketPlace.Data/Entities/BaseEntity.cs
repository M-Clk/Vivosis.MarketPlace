using System;
using System.Collections.Generic;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public abstract class BaseEntity
    {
        public bool status { get; set; }
        public DateTime date_added { get; set; }
        public DateTime date_modified { get; set; }
    }
}
