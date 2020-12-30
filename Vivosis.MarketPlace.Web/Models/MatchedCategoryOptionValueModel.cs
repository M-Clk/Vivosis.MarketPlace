using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Web.Models
{
    public class MatchedCategoryOptionValueModel
    {
        public OptionValue OptionValue { get; set; }
        public CategoryOptionValue CategoryOptionValue { get; set; }
        public bool IsSetted { get; set; }
    }
}
