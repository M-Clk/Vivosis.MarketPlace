using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Service.Abstract;
using Vivosis.MarketPlace.Web.Models;

namespace Vivosis.MarketPlace.Web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class ProductsController :Controller
    {
        private readonly MarketPlaceDbContext _context;
        IGlobalService _globalService;
        ICommonService _commonService;
        public ProductsController(MarketPlaceDbContext context, IGlobalService globalService, ICommonService commonService)
        {
            _context = context;
            _globalService = globalService;
            _commonService = commonService;
        }
        // GET: Products
        public IActionResult Index()
        {
            if(ModelState.IsValid)
            {
                var products = _globalService.GetProducts();
                return View(products);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Selecteds(IEnumerable<int> idList)
        {
            var products = _globalService.GetProducts(idList);
            return Ok(products);
        }
        // GET: Products/5
        public IActionResult Get(int id)
        {
            var product = _globalService.GetProducts(new List<int> { id });
            return View(product);
        }
        public IActionResult Settings()
        {
            return View();
        }
        [HttpGet("[controller]/Sync")]
        public IActionResult SyncProducts()
        {
            _commonService.SyncLocalProducts();
            var updateModel = new UpdateModel
            {
                IsSucceed = true,
                IsUpdated = true,
                Message = "Ürünler başarıyla senkronize edildi."
            };
            return View("Settings", updateModel);
        }
        [HttpGet("[controller]/Sync/Options")]
        public IActionResult SyncOptions()
        {
            _commonService.SyncLocalOptions();
            var updateModel = new UpdateModel
            {
                IsSucceed = true,
                IsUpdated = true,
                Message = "Varyantlar başarıyla senkronize edildi."
            };
            return View("Settings", updateModel);
        }
    }
}
