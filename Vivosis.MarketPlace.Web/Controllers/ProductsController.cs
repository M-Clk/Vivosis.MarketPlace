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
        ICommonService _commonService;
        ILocalService _localService;
        public ProductsController(MarketPlaceDbContext context, ICommonService commonService, ILocalService localService)
        {
            _context = context;
            _localService = localService;
            _commonService = commonService;
        }
        // GET: Products
        public IActionResult Index()
        {
            if(ModelState.IsValid)
            {
                var products = _localService.GetProducts();
                return View(products);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Selecteds(IEnumerable<int> idList)
        {
            var products = _localService.GetProducts(idList);
            return Ok(products);
        }
        // GET: Products/5
        public IActionResult Get(int id)
        {
            var product = _localService.GetProducts(new List<int> { id });
            return View(product);
        }
        public IActionResult Settings()
        {
            return View();
        }
        public IActionResult Sync()
        {
            _commonService.SyncDatabase();
            var updateModel = new UpdateModel
            {
                IsSucceed = true,
                IsUpdated = true,
                Message = "Ürünler ve kategoriler başarıyla senkronize edildi."
            };
            return View("Settings", updateModel);
        }
        public IActionResult Options()
        {
            return View();
        }
    }
}
