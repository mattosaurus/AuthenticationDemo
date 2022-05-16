using AuthenticationDemo.Ui.Models;
using AuthenticationDemo.WeatherForecast.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using System.Diagnostics;

namespace AuthenticationDemo.Ui.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IWeatherForecastService _weatherForecastService;

        public HomeController(ILogger<HomeController> logger, ITokenAcquisition tokenAcquisition, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;
            _weatherForecastService = weatherForecastService;
        }

        // Forces incremental consent, because we're using AddInMemoryTokenCaches we lose the tokens on restart but we're still logged in
        // If tokens are properly persisted this can be removved
        [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        public async Task<IActionResult> Index()
        {
            var weatherForecasts = await _weatherForecastService.GetWeatherForecastsAsync();
            ViewData["ApiResult"] = weatherForecasts;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}