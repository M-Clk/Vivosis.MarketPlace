using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vivosis.MarketPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController :ControllerBase
    {
        IStoreService _storeService;
        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        // GET: api/<StoresController>
        [HttpGet]
        public ActionResult<IEnumerable<Store>> Get()
        { 
            var stores = _storeService.GetStores();
            if(stores == null)
                return NoContent();
            else
                return Ok(stores);
        }

        // GET api/<StoresController>/5
        [HttpGet("{id}")]
        public ActionResult<Store> Get(int id)
        {
            var store = _storeService.GetStoreById(id);
            if(store == null)
                return NotFound();
            else
                return Ok(store);
        }

        // POST api/<StoresController>
        [HttpPost]
        public IActionResult Post([FromBody] Store store)
        {
            if(_storeService.AddStore(store))
                return Ok();
            else
                return BadRequest();
        }

        // PUT api/<StoresController>
        [HttpPut]
        public IActionResult Put([FromBody] Store store)
        {
            if(_storeService.UpdateStore(store))
                return Ok();
            else
                return BadRequest();
        }

        // DELETE api/<StoresController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(_storeService.DeleteStore(id))
                return Ok();
            else
                return BadRequest();
        }
    }
}
