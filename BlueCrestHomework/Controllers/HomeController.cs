using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlueCrestHomework.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlueCrestHomework.Models;
using DataModel.Dtos;
using Microsoft.AspNetCore.Http;

namespace BlueCrestHomework.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IActionResult> Index(RequestBinding requestBindingPosted)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            Request request = new Request{ RequestId = "Empty"};

            var response = await client.GetAsync("https://localhost:5001/api/Query/query/all");

            if (response.Content.ReadAsStream() is MemoryStream stream)
            {
                string decoded = Encoding.UTF8.GetString(stream.ToArray());
                request = JsonSerializer.Deserialize<Request>(decoded, _options);
            }

            var requestBinding = request.ToBindingObject();
            requestBinding.ShowDetails = requestBindingPosted.ShowDetails;
            
            return View(requestBinding);
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