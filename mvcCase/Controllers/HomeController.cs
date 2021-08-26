using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvcCase.Models;


namespace mvcCase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["message"] = "Your application description page.";
            ViewData["timenow"] = DateTime.Now;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Text()
        {
            return new Microsoft.AspNetCore.Mvc.ContentResult
            {
                Content = "Hi there! ☺",
                ContentType = "text/plain; charset=utf-8"
            };
        }
        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}