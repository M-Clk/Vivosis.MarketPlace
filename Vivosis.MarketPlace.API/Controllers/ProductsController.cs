using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
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

        // GET: api/Products
        [HttpGet("Products")]
        public ActionResult<IEnumerable<Product>> GetProduct()
        {
            var products = _globalService.GetProducts();
            return Ok(products);
        }
        [HttpPatch("Products")]
        public ActionResult<IEnumerable<Product>> GetProduct(IEnumerable<int> idList)
        {
            var products = _globalService.GetProducts(idList);
            return Ok(products);
        }
        // GET: api/Products/5
        [HttpGet("Products/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = _globalService.GetProducts(new List<int> {id});

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        // GET: api/Categories
        [HttpGet("Categories")]
        public ActionResult<IEnumerable<Category>> GetCategory()
        {
            var products = _globalService.GetCategories();
            return Ok(products);
        }
        [HttpPatch("Categories")]
        public ActionResult<IEnumerable<Category>> GetCategory(IEnumerable<int> idList)
        {
            var products = _globalService.GetCategories(idList);
            return Ok(products);
        }
        // GET: api/Categories/5
        [HttpGet("Categories/{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var product = _globalService.GetCategories(new List<int> { id });

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        [HttpGet("Sync")]
        public ActionResult SyncProducts()
        {
            _commonService.SyncLocalProducts();
            return Ok();
        }
    }
}
