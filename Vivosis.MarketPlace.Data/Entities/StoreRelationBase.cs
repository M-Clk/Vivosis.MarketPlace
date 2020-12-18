using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class StoreRelationBase
    {
        public int commission { get; set; }
        public string currency { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal shipping_fee { get; set; }
    }
}
