using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlueCrestHomework.Extensions;
using BlueCrestHomework.Models;
using DataModel.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlueCrestHomework.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HomeController> _logger;
        private readonly JsonSerializerOptions _options;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _logger.Log(LogLevel.Information, "Controller [HomeController] created");
        }

        public async Task<IActionResult> Index(RequestBinding requestBindingPosted)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var request = new Request { RequestId = "Empty" };

                var response = await client.GetAsync("https://localhost:5001/api/Query/query/all");

                if (await response.Content.ReadAsStreamAsync() is MemoryStream stream)
                {
                    var decoded = Encoding.UTF8.GetString(stream.ToArray());
                    request = JsonSerializer.Deserialize<Request>(decoded, _options);
                }

                var requestBinding = request.ToBindingObject();
                requestBinding.ShowDetails = requestBindingPosted.ShowDetails;

                return View(requestBinding);
            }
            catch (Exception e)
            {
                return View("Error");
            }
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