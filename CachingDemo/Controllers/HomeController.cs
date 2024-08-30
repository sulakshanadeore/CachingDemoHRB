using CachingDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace CachingDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IMemoryCache _cache;
        string cacheKey = "MyDataKey";
        public HomeController(ILogger<HomeController> logger,IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache; 

        }
        
        public IActionResult Index()
        {
              
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration
                    (new DateTime(2024, 08, 30, 10, 37,0));
                //var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1));
                
                var data= "Welcome to app";
               
                 _cache.Set(cacheKey,data, cacheEntryOptions);

                return RedirectToAction("Privacy");
                      
           
        }

        public IActionResult Privacy()
        {
            string mydata = (string)_cache.Get(cacheKey);
            ViewBag.pdata = mydata;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
