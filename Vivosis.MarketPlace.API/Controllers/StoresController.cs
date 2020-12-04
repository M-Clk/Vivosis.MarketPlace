using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vivosis.MarketPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoresController :ControllerBase
    {
        IStoreService _storeService;
        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        // GET: api/<StoresController>
        [HttpGet]
        public ActionResult<IEnumerable<StoreUser>> Get()
        { 
            var storeUsers = _storeService.GetStores();
            if(storeUsers == null)
                return NoContent();
            else
                return Ok(storeUsers);
        }

        // GET api/<StoresController>/5
        [HttpGet("{id}")]
        public ActionResult<StoreUser> Get(int id)
        {
            var storeUser = _storeService.GetStoreById(id);
            if(storeUser == null)
                return NotFound();
            else
                return Ok(storeUser);
        }

        // POST api/<StoresController>
        [HttpPost]
        public IActionResult Post([FromBody] StoreUser storeUser)
        {
            if(_storeService.AddStore(storeUser))
                return Ok();
            else
                return BadRequest();
        }

        // PUT api/<StoresController>
        [HttpPut]
        public IActionResult Put([FromBody] StoreUser storeUser)
        {
            if(_storeService.UpdateStore(storeUser))
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
