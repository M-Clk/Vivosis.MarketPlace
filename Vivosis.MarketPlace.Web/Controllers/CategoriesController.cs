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
        ILocalService _localService;
        IStoreService _storeService;
        IN11Service _n11Service;
        public CategoriesController( ILocalService localService, IStoreService storeService, IN11Service n11Service)
        {
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
            var options = _localService.GetAllOptions();
            if(storeCategory != null && options.Any())
            {
                var model = new CategoryOptionListModel();
                model.SelectedStoreName = storeName;
                var categoryOptionFromStore = _n11Service.GetCategoryOptions(long.Parse(storeCategory.matched_category_code));
                var categoryOptionsFromLocal = _localService.GetStoreCategory(storeCategory.store_category_id)?.CategoryOptions;
                //if(categoryOptionFromStore != null && categoryOptionFromStore.Any())
                //{
                //    var matchedCategoryOptions = new List<MatchedCategoryOptionModel>();
                //    foreach(var fromStore in categoryOptionFromStore.OrderByDescending(c => c.IsRequired))
                //    {
                //        var fromLocal = categoryOptionsFromLocal?.FirstOrDefault(co => co.category_option_id == fromStore.Id);
                //        matchedCategoryOptions.Add(
                //            new MatchedCategoryOptionModel
                //            {
                //                FromStore = fromStore,
                //                FromLocal = fromLocal,
                //                IsSetted = fromLocal != null
                //            });
                //    }
                //    model.CategoryOptions = matchedCategoryOptions;
                //    model.Options = _localService.GetAllOptions();
                //    storeCategoryModel.OptionsModel = model;
                //}
            }
            storeCategoryModel.StoreCategory = storeCategory ?? new StoreCategory { category_id = categoryId, store_id = storeId };

            storeCategoryModel.SelectedStoreName = storeName;
            return PartialView("_EditStoreCategory", storeCategoryModel);
        }
        [HttpPost]
        public IActionResult EditStoreCategory([FromBody] StoreCategory storeCategory)
        {
            if(ModelState.IsValid)
            {
                if(_localService.AddOrUpdateStoreCategory(storeCategory))
                    return Json(new { isValid = true });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_EditStoreCategory", storeCategory) });
        }
        public IActionResult GetCategoryOptionValues(long storeCategoryAttributeId, int optionId, int categoryOptionId)
        {
            //if(ModelState.IsValid)
            //{
            //    var model = new CategoryOptionValueModel();
            //    model.CategoryOptionIdFromStore = storeCategoryAttributeId;
            //    var optionValues = _localService.GetOptionValues(optionId);
            //    var categoryOptionValues = _localService.GetCategoryOptionValues(categoryOptionId)?.ToList();
            //    if(optionValues?.Any() ?? false)
            //    {
            //        model.MatchedCategoryOptionValues = optionValues.Select(ov => new MatchedCategoryOptionValueModel
            //        {
            //            OptionValue = ov,
            //            CategoryOptionValue = categoryOptionValues?.FirstOrDefault(cav => cav.option_value_id == ov.option_value_id)
            //        });
            //        model.OptionValuesFromStore = _n11Service.GetCategoryOptionValues(storeCategoryAttributeId);
            //        model.LocalCategoryOptionValuesExist = categoryOptionValues?.Any() ?? false;
            //    }
            //    return Json(new { isEmpty = false, html = Helper.RenderRazorViewToString(this, "_ListCategoryOptionValues", model) });
            //}
            return Json(new { isEmpty = true });
        }
        public IActionResult GetCategoryOptions(long categoryCode, int categoryStoreId, string storeName)
        {
            if(ModelState.IsValid)
            {
                var model = new CategoryOptionListModel();
                model.SelectedStoreName = storeName;
                var categoryOptionFromStore = _n11Service.GetCategoryOptions(categoryCode);
                //var categoryOptionsFromLocal = _localService.GetStoreCategory(categoryStoreId)?.CategoryOptions;
                //if(categoryOptionFromStore != null && categoryOptionFromStore.Any())
                //{
                //    var matchedCategoryOptions = new List<MatchedCategoryOptionModel>();
                //    var localCategoryOptionExist = false;
                //    foreach(var fromStore in categoryOptionFromStore.OrderByDescending(c => c.IsRequired))
                //    {
                //        var fromLocal = categoryOptionsFromLocal?.FirstOrDefault(co => co.matched_store_option_id == fromStore.Id.ToString());
                //        matchedCategoryOptions.Add(
                //            new MatchedCategoryOptionModel
                //            {
                //                FromStore = fromStore,
                //                FromLocal = fromLocal,
                //                IsSetted = fromLocal != null
                //            });
                //        if(fromLocal != null)
                //            localCategoryOptionExist = true;
                //    }
                //    model.CategoryOptions = matchedCategoryOptions;
                //    model.LocalCategoryOptionsExist = localCategoryOptionExist;
                //    model.Options = _localService.GetAllOptions();
                //    return Json(new { isEmpty = false, html = Helper.RenderRazorViewToString(this, "_ListCategoryOptions", model) });
                //}
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
        public IActionResult GetParentCategoriesIdList(int categoryId)
        {
            var category = _n11Service.GetCategoryWithParents(categoryId);
            var categoryNameArray = new List<string>();
            categoryNameArray.Add(category.Name);
            while(category.ParentCategory != null)
            {
                category = category.ParentCategory;
                categoryNameArray.Add(category.Name);
            }
            categoryNameArray.Reverse();
            var result = categoryNameArray.Select(c => $"\"{c}\"");
            return Json(new { isEmpty = false, idList = $"[{string.Join(", ", result)}]" });
        }
    }
}
