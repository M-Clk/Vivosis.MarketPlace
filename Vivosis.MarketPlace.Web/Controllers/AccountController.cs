using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;
using Vivosis.MarketPlace.Web.Models;

namespace Vivosis.MarketPlace.Web.Controllers
{
    [Authorize]
    public class AccountController :Controller
    {
        IAccountService _accountService;
        MarketPlaceDbContext _dbContext;
        public AccountController(IAccountService accountService, MarketPlaceDbContext dbContext)
        {
            _accountService = accountService;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromForm] LoginModel loginModel, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var result = _accountService.Login(loginModel.UserName, loginModel.Password, loginModel.RememberMe);
                if(result)
                {
                    if(Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Error", "Kullanici adi veya sifre yanlis.");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(loginModel);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")] //TODO admin icin ayri customer icin ayri bir add ve update sayfasi olacak. O yuzden bunlarin adini degistir ve admin icin bir tane daha ekle.
        public IActionResult Add([FromForm] SystemUser user)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var result = _accountService.AddUser(user, false);
                    if(result.Succeeded)
                        return RedirectToAction("Index", "Account");
                    else
                    {
                        foreach(var error in result.Errors)
                            ModelState.AddModelError(error.Code, error.Description);
                    }
                }
                catch(DBConcurrencyException ex)
                {
                    ModelState.AddModelError("DbConnectionResult", ex.Message);
                }
            }
            return View(user);
        }
        public IActionResult Update()
        {
            return View();
        }
        [HttpPut]
        public IActionResult Update([FromForm] SystemUser user)
        {
            var result = _accountService.UpdateUser(user);
            if(result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int userId)
        {
            var result = _accountService.DeleteUser(userId);
            if(result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
