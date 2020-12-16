using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;
using Vivosis.MarketPlace.Web.Helpers;
using Vivosis.MarketPlace.Web.Models;

namespace Vivosis.MarketPlace.Web.Controllers
{
    [Authorize]
    public class StoresController :Controller
    {
        IStoreService _storeService;
        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        [Authorize(Roles = "Customer")]
        public IActionResult Index()
        {
            return View(GetStoreUsers());
        }
        [Authorize(Roles = "Customer")]
        public IActionResult AddStoreToUser(int id)
        {
            if(ModelState.IsValid)
                _storeService.AddStoreToUser(id);
            return RedirectToAction("Index", "Stores");
        }
        [Authorize(Roles = "Customer")]
        public IActionResult AddStoresToUser(List<int> storeIdList)
        {
            if(ModelState.IsValid)
            {
                if(_storeService.AddStoresToUser(storeIdList))
                    return View();
            }
            return View(storeIdList);
        }
        [Authorize(Roles = "Customer")]
        public IActionResult EditStore(int storeId)
        {
            var storeUser = _storeService.GetStoreById(storeId);
            return PartialView("EditStore", storeUser);
        }
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public IActionResult EditStore(StoreUser storeUser)
        {
            if(ModelState.IsValid)
            {
                if(_storeService.UpdateStore(storeUser))
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ListUserStores", GetStoreUsers()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "EditStore", storeUser)});
        }
        [Authorize(Roles = "Customer")]
        public IActionResult ChangeStatus(int storeId)
        {
            if(ModelState.IsValid)
            {
                var store = _storeService.GetStoreById(storeId);
                if(store.is_confirmed)
                    store.is_active = !store.is_active;
                _storeService.UpdateStore(store);
            }
            return RedirectToAction("Index", "Stores");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult StoreRequests()
        {
            if(ModelState.IsValid)
            {
                var requests = _storeService.GetRequests();
                return View(requests);
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("StoreRequests/{userId}/{storeId}/{reject?}")]
        public IActionResult StoreRequests(int userId, int storeId, bool reject)
        {
            if(ModelState.IsValid)
            {
                if(reject)
                    _storeService.RejectStoreUser(userId, storeId);
                else
                    _storeService.ConfirmStoreUser(userId, storeId);
            }
            return StoreRequests();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost()]
        public IActionResult StoreRequests(IEnumerable<StoreUser> storeUsers)
        {
            if(ModelState.IsValid)
            {
                foreach(var user in storeUsers)
                    user.is_confirmed = true;
            }
            return StoreRequests();
        }
        private IEnumerable<StoreModel> GetStoreUsers()
        {
            var stores = _storeService.GetStores();
            var boughtStores = _storeService.GetBoughtStores().ToList();
            var models = stores.Select(s => new StoreModel
            {
                StoreId = s.store_id,
                Name = s.name,
                Image = s.image,
                IsBought = boughtStores?.Any(b => b.store_id == s.store_id) ?? false,
                IsConfirmed = boughtStores?.Any(b => b.store_id == s.store_id && b.is_confirmed) ?? false,
                IsActive = boughtStores?.Any(b => b.store_id == s.store_id && b.is_active) ?? false,
                RemainingDays = (int)((boughtStores.FirstOrDefault(b => b.store_id == s.store_id)?.expire_time ?? DateTime.Now.AddDays(-1)) - DateTime.Now).TotalDays
            });
            return models;
        }
    }
}
