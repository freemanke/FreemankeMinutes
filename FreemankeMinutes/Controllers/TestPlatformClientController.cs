using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreemankeMinutes.Models;
using FreemankeMinutes.Services;

namespace FreemankeMinutes.Controllers
{
    public class TestPlatformClientController : Controller
    {
        private TestPlatformClient _tp;

        public TestPlatformClientController(TestPlatformClient tp)
        {
            _tp = tp;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Message = await _tp.GetSikuliMasterSlaveVersionAsync();
            return View();
        }
    }
}
