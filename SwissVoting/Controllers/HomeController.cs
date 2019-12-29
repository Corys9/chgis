using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SwissVoting.Models;

namespace SwissVoting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IHttpClientFactory _clientFactory { get; set; }

        public static string ApiURL { get; set; }

        public static string GeoServerURL { get; set; }

        public HomeController(
            ILogger<HomeController> logger,
            IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var http = _clientFactory.CreateClient("chgisAPI");
            var lawsJson = await http.GetStringAsync("laws");
            var laws = JsonConvert.DeserializeObject<List<Law>>(lawsJson);

            var vm = new HomeViewModel
            {
                Laws = laws
            };

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
