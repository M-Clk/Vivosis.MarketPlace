using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.Web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class ProductsController :Controller
    {
        private readonly MarketPlaceDbContext _context;
        IGlobalService _globalService;
        public ProductsController(MarketPlaceDbContext context, IGlobalService globalService)
        {
            _context = context;
            _globalService = globalService;
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
    }
}
