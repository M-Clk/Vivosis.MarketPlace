using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Vivosis.MarketPlace.Service.Abstract;
using Vivosis.MarketPlace.Web.Models;

namespace Vivosis.MarketPlace.Web.Controllers
{
    [Authorize]
    public class HomeController :Controller
    {
        private readonly ILogger<HomeController> _logger;
        ICommonService _commonService;

        public HomeController(ILogger<HomeController> logger, ICommonService commonService)
        {
            _logger = logger;
            _commonService = commonService;
        }

        public IActionResult Index()
        {
            _commonService.SyncLocalProducts();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
