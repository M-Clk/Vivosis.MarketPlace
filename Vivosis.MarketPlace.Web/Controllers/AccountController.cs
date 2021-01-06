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
        ICommonService _commonService;
        public AccountController(IAccountService accountService, MarketPlaceDbContext dbContext, ICommonService commonService)
        {
            _accountService = accountService;
            _dbContext = dbContext;
            _commonService = commonService;
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
        public IActionResult Logout()
        {
            _accountService.Logout();
            return RedirectToAction("Login", "Account");
        }

        #region User CRUD islemleri

        [HttpGet("[Controller]/Admin/Customers")]
        [Authorize(Roles = "Admin")]
        public IActionResult Customers()
        {
            var users = _accountService.GetCustomerUsers();
            return View(users);
        }
        [HttpGet("[Controller]/Admin/Users")]
        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            var users = _accountService.GetAdminUsers();
            return View(users);
        }
        [HttpGet("[Controller]/Admin/Add")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost("[Controller]/Admin/Add")]
        [Authorize(Roles = "Admin")] //TODO admin icin ayri customer icin ayri bir add ve update sayfasi olacak. O yuzden bunlarin adini degistir ve admin icin bir tane daha ekle.
        public IActionResult AddAdmin([FromForm] SystemUser user)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var result = _accountService.AddUser(user, true);
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
        [HttpGet("[Controller]/Customer/Add")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost("[Controller]/Customer/Add")]
        [Authorize(Roles = "Admin")] //TODO admin icin ayri customer icin ayri bir add ve update sayfasi olacak. O yuzden bunlarin adini degistir ve admin icin bir tane daha ekle.
        public IActionResult AddCustomer([FromForm] SystemUser user)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var result = _accountService.AddUser(user, false);
                    if(result.Succeeded)
                    {
                        _commonService.SyncDatabase(user.UserName);
                        return RedirectToAction("Index", "Account");
                    }
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
        [HttpGet("[Controller]/Delete/{userId}")]
        public IActionResult Delete(int userId, string returnUrl)
        {
            _accountService.DeleteUser(userId);
            if(string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index");
            return LocalRedirect(returnUrl);
        }

        #endregion

        #region MyRegion
        public IActionResult AddStore()
        {
            return View();
        }
        public IActionResult AccessDenied(string returnUrl, bool turnBack = false)
        {
            return View();
        }
        #endregion

    }
}
