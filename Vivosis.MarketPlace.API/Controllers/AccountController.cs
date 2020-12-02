using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vivosis.MarketPlace.API.Models;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;
using Vivosis.MarketPlace.Service.Concrete;

namespace Vivosis.MarketPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController :Controller
    {
        IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginModel loginModel)
        {
            var result = _accountService.Login(loginModel.UserName, loginModel.Password, true);
            if(result)
                return Ok();
            else
            return BadRequest();
        }
        [HttpPost]
        public IActionResult AddUser([FromBody]SystemUser user)
        {
            var result = _accountService.AddUser(user);
            if(result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }
        [HttpPut]
        public IActionResult UpdateUser([FromBody]SystemUser user)
        {
            var result = _accountService.UpdateUser(user);
            if(result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }
        [HttpDelete]
        public IActionResult DeleteUser(int userId)
        {
            var result = _accountService.DeleteUser(userId);
            if(result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
