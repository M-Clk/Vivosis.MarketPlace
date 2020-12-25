using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Web.Models
{
    public class MatchedCategoryOptionModel
    {
        public CategoryOption FromLocal { get; set; }
        public CategoryFromStoreAttribute FromStore { get; set; }
        public bool IsSetted { get; set; }
    }
}
