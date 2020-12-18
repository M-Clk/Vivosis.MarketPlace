using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;
using Vivosis.MarketPlace.Web.Helpers;
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
        IN11Service _n11Service;
        public CategoriesController(MarketPlaceDbContext context, IGlobalService globalService, ILocalService localService, IStoreService storeService, IN11Service n11Service)
        {
            _context = context;
            _globalService = globalService;
            _localService = localService;
            _storeService = storeService;
            _n11Service = n11Service;
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
        public IActionResult EditStoreCategory(int storeId, int categoryId)
        {
            var storeCategory = _localService.GetStoreCategory(storeId, categoryId) ?? new StoreCategory { store_id = storeId, category_id = categoryId };

            return PartialView("_EditStoreCategory", storeCategory);
        }
        [HttpPost]
        public IActionResult EditStoreCategory(StoreCategory storeCategory)
        {
            if(ModelState.IsValid)
            {
                if(_localService.AddOrUpdateStoreCategory(storeCategory))
                    return Json(new { isValid = true });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_EditStoreCategory", storeCategory) });
        }
        public IActionResult GetCategories(int parentId)
        {
            var categories = parentId == 0 ? _n11Service.GetTopCategories() : _n11Service.GetSubCategories(parentId);
            if(categories.Any())
                return PartialView("_CategoryList", categories);
            else
                return Json(new { isEmpty = true });
        }
    }
}
