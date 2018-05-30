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
    public class ChatController : Controller
    {

        public ChatController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
