using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vivosis.MarketPlace.API.Middleware
{
    public class Error
    {
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
    }
}
