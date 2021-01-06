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
                model.Stores = _storeService.GetBoughtStores().Where(us => us.is_confirmed && !string.IsNullOrEmpty(us.api_key) && !string.IsNullOrEmpty(us.secret_key));
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
                ShipmentTemplates = templates,
                AttributesQuery = storeProduct.attribute_query
            };
            if(storeProduct.Product?.ProductCategories?.Any() ?? false)
            {
                var category = storeProduct.Product.ProductCategories.First().Category;
                model.CategoryAttributes = _localService.GetCategoryOptions(category.category_id, storeId).OrderByDescending(co => co.IsRequired).ToList();
                model.CategoryName = category.path_name;
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
        public IActionResult SendProductToStore(PostStoreProductModel storeProductModel)
        {
            var product = _commonService.GetProductToSendStore(storeProductModel.StoreProduct);
            var attributePairs = new Dictionary<string, string>();
            if(!string.IsNullOrEmpty(storeProductModel.AttributesQuery))
            {
                foreach(var pair in storeProductModel.AttributesQuery.Split("&&"))
                {
                    var splitedPair = pair.Split("==");
                    attributePairs.Add(splitedPair.First(), splitedPair.Last());
                }
            }
            var errorMessage = "";
            var storeProduct = _n11Service.SendProduct(product, attributePairs, ref errorMessage);
            if(storeProduct != null)
            {
                storeProduct.attribute_query = storeProductModel.AttributesQuery;
                _localService.AddOrUpdateStoreProduct(storeProduct);
                var textStyle = storeProduct.sale_price > product.price ? "success" : storeProduct.sale_price < product.price ? "warning" : "info";
                return Json(new { isSucced = true, productId = storeProduct.product_id, storeId = storeProduct.store_id, price = storeProduct.sale_price, currency= storeProduct.currency??"TL", textStyle = textStyle });
            }
            return Json(new { isSucced = false, errorMessage = errorMessage });
        }
        public IActionResult DeleteProductFromStore(int storeProductId)
        {
            var storeProduct = _localService.GetStoreProduct(storeProductId);
            var errorMessage = "";
            if(_n11Service.DeleteProduct(long.Parse(storeProduct.matched_product_code), ref errorMessage))
            {
                _localService.DeleteStoreProduct(storeProduct);
                return Json(new { isDeleted = true, productId = storeProduct.product_id, storeId = storeProduct.store_id });
            }
            return Json(new { isDeleted = false, errorMessage = errorMessage });
        }
        public IActionResult Options(int productId)
        {
            var productOptions = _localService.GetProductOptions(productId);
            return PartialView("Options", productOptions);
        }
    }
}
