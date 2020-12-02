using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.Web.Controllers
{
    [Authorize]
    public class ProductsController :Controller
    {
        private readonly MarketPlaceDbContext _context;
        IGlobalService _globalService;
        public ProductsController(MarketPlaceDbContext context, IGlobalService globalService)
        {
            _context = context;
            _globalService = globalService;
        }
        // GET: api/Products
        public IActionResult Index()
        {
            var products = _globalService.GetProducts();
            return Ok(products);
        }
        [HttpPost]
        public IActionResult Index(IEnumerable<int> idList)
        {
            var products = _globalService.GetProducts(idList);
            return Ok(products);
        }
        // GET: api/Products/5
        [HttpGet("Products/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = _globalService.GetProducts(new List<int> { id });

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        // GET: api/Categories
        [HttpGet("Categories")]
        public IActionResult GetCategory()
        {
            var products = _globalService.GetCategories();
            return Ok(products);
        }
        [HttpPatch("Categories")]
        public IActionResult GetCategory(IEnumerable<int> idList)
        {
            var products = _globalService.GetCategories(idList);
            return Ok(products);
        }
        // GET: api/Categories/5
        [HttpGet("Categories/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var product = _globalService.GetCategories(new List<int> { id });

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
