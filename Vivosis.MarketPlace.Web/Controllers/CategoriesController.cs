using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Service.Abstract;
using Vivosis.MarketPlace.Web.Models;

namespace Vivosis.MarketPlace.Web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CategoriesController :Controller
    {
        private readonly MarketPlaceDbContext _context;
        IGlobalService _globalService;
        ILocalService _localService;
        IStoreService _storeService;
        public CategoriesController(MarketPlaceDbContext context, IGlobalService globalService, ILocalService localService, IStoreService storeService)
        {
            _context = context;
            _globalService = globalService;
            _localService = localService;
            _storeService = storeService;
        }
        // GET: Categories
        public IActionResult Index()
        {
            if(ModelState.IsValid)
            {
                var model = new UserStoreCategoryModel();
                model.Categories = _localService.GetCategories();
                model.Stores = _storeService.GetBoughtStores().Where(us => us.is_confirmed);
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Selecteds(IEnumerable<int> idList)
        {
            if(ModelState.IsValid)
            {
                var categories = _localService.GetCategories(idList);
                return View(categories);
            }
            return View();
        }
        // GET: Categories/5
        public IActionResult Get(int id)
        {
            var category = _localService.GetCategories(new List<int> { id });
            return View(category);
        }
    }
}
