using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vivosis.MarketPlace.Web.Models
{
    public class StoreModel
    {
        public int StoreId { get; set; }
        public bool IsBought { get; set; }
        public bool IsActive { get; set; }
        public bool IsConfirmed { get; set; }
        public int RemainingDays{ get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
