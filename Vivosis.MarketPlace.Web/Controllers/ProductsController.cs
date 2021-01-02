using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;
using Vivosis.MarketPlace.Web.Models;

namespace Vivosis.MarketPlace.Web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class ProductsController :Controller
    {
        ICommonService _commonService;
        ILocalService _localService;
        IStoreService _storeService;
        IN11Service _n11Service;
        public ProductsController(ICommonService commonService, ILocalService localService, IStoreService storeService, IN11Service n11Service)
        {
            _localService = localService;
            _commonService = commonService;
            _storeService = storeService;
            _n11Service = n11Service;
        }
        // GET: Products
        public IActionResult Index()
        {
            if(ModelState.IsValid)
            {
                var model = new UserStoreProductModel();
                model.Products = _localService.GetProducts();
                model.Stores = _storeService.GetBoughtStores().Where(us=>us.is_confirmed && !string.IsNullOrEmpty(us.api_key) && !string.IsNullOrEmpty(us.secret_key));
                return View(model);
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
        public IActionResult EditStoreProduct(int storeId, int productId)
        {
            var storeProduct = _localService.GetStoreProduct(storeId, productId);
            var templates = _commonService.GetShipmentTemplate();
            if(!templates.Any())
            {
                templates = _n11Service.GetShipmentTemplates();
                _commonService.SaveShipmentTemplates(templates);
            }
            var model = new EditStoreProductModel
            {
                StoreProduct = storeProduct,
                ShipmentTemplates = templates
            };
            if(storeProduct.Product?.ProductCategories?.Any()??false)
            {
                var categoryId = storeProduct.Product.ProductCategories.First().category_id;
                model.CategoryAttributes = _localService.GetCategoryOptions(categoryId, storeId);
            }
            return PartialView("_EditStoreProduct", model);
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
        public IActionResult SendProductToStore(StoreProduct storeProduct)
        {
            var product = _commonService.GetProductToSendStore(storeProduct);
            var result = _n11Service.SendProduct(product);
            return Json(new { isSucced = result });
        }
        public IActionResult Options(int productId)
        {
            var productOptions = _localService.GetProductOptions(productId);
            return PartialView("Options", productOptions);
        }
    }
}
