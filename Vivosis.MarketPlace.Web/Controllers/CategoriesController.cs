using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.Web.Controllers
{
    public class CategoriesController :Controller
    {
        private readonly MarketPlaceDbContext _context;
        IGlobalService _globalService;
        public CategoriesController(MarketPlaceDbContext context, IGlobalService globalService)
        {
            _context = context;
            _globalService = globalService;
        }
        // GET: Categories
        public IActionResult Index()
        {
            if(ModelState.IsValid)
            {
                var categories = _globalService.GetCategories();
                return View(categories);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Selecteds(IEnumerable<int> idList)
        {
            if(ModelState.IsValid)
            {
                var categories = _globalService.GetCategories(idList);
                return View(categories);
            }
            return View();
        }
        // GET: Categories/5
        public IActionResult Get(int id)
        {
            var category = _globalService.GetCategories(new List<int> { id });
            return View(category);
        }
    }
}
