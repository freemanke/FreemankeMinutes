using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreemankeMinutes.Models;

namespace FreemankeMinutes.Controllers
{
    public class UseHttpClientFactoryController : Controller
    {
        private IHttpClientFactory _httpClientFactory;

        public UseHttpClientFactoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var createClient = _httpClientFactory.CreateClient("TPAPI");
            ViewBag.Message = await createClient.GetStringAsync("api/sikulimasterslave/version");
            return View();
        }
    }
}
