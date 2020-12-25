using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController :Controller
    {
        IN11Service _n11Service;
        public CategoriesController(IN11Service n11Service)
        {
            _n11Service = n11Service;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
