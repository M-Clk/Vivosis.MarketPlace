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
                model.Stores = _storeService.GetBoughtStores().Where(us => us.is_confirmed && !string.IsNullOrEmpty(us.api_key) && !string.IsNullOrEmpty(us.secret_key));
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
        public IActionResult EditStoreCategory(int storeId, int categoryId, string storeName)
        {
            var storeCategoryModel = new EditCategoryStoreModel();
            var storeCategory = _localService.GetStoreCategory(storeId, categoryId);
            storeCategoryModel.IsStoreCategoryExist = storeCategory != null;
            storeCategoryModel.StoreCategory = storeCategory ?? new StoreCategory { category_id = categoryId, store_id = storeId };
            storeCategoryModel.Options = _localService.GetAllOptions();
            storeCategoryModel.SelectedStoreName = storeName;
            return PartialView("_EditStoreCategory", storeCategoryModel);
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
        public IActionResult GetCategoryOptions(long categoryCode, int categoryStoreId, string storeName)
        {
            if(ModelState.IsValid)
            {
                var model = new CategoryOptionListModel();
                model.SelectedStoreName = storeName;
                var categoryOptionFromStore = _n11Service.GetCategoryOptisons(categoryCode);
                var categoryOptionsFromLocal = _localService.GetStoreCategory(categoryStoreId)?.CategoryOptions;
                if(categoryOptionFromStore != null && categoryOptionFromStore.Any())
                {
                    var matchedCategoryOptions = new List<MatchedCategoryOptionModel>();
                    foreach(var fromStore in categoryOptionFromStore.OrderByDescending(c=>c.IsRequired))
                    {
                        var fromLocal = categoryOptionsFromLocal?.FirstOrDefault(co => co.category_option_id == fromStore.Id);
                        matchedCategoryOptions.Add(
                            new MatchedCategoryOptionModel
                            {
                                FromStore = fromStore,
                                FromLocal = fromLocal,
                                IsSetted = fromLocal != null
                            });
                    }
                    model.CategoryOptions = matchedCategoryOptions;
                    model.Options = _localService.GetAllOptions();
                    return Json(new { isEmpty = false, html = Helper.RenderRazorViewToString(this, "_ListCategoryOptions", model) });
                }
            }
            return Json(new { isEmpty = true });
        }
        public IActionResult GetCategories(int parentId)
        {
            var categories = parentId == 0 ? _n11Service.GetTopCategories() : _n11Service.GetSubCategories(parentId);
            if(categories.Any())
                return Json(new { isEmpty = false, html = Helper.RenderRazorViewToString(this, "_CategoryList", categories) });
            else
                return Json(new { isEmpty = true });
        }
    }
}
