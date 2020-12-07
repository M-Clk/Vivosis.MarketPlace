using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.Web.Controllers
{
    public class StoresController :Controller
    {
        IStoreService _storeService;
        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index([FromForm] StoreUser storeUser)
        {
            if(ModelState.IsValid)
            {
                if(_storeService.AddStore(storeUser))
                    return View();
            }
            return View(storeUser);
        }
    }
}
