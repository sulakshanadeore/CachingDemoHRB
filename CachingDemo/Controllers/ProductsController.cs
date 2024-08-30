using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using CachingDemo.Models;
namespace CachingDemo.Controllers
{
    public class ProductsController : Controller
    {
        List<Product> products = new List<Product>() {
            new Product {ProductID=1,ProductName="Tea" },
            new Product { ProductID=2,ProductName="Coffee"},
            new Product {ProductID=3,ProductName="Milk" },
            new Product { ProductID=4,ProductName="sugar"},
            };

        IMemoryCache _memoryCache;
        string cacheKey = "pkey";
        public ProductsController(IMemoryCache cache)
        {
                _memoryCache = cache;
        }
        
        public  IActionResult MemoryCache()
        {
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration
                    (new DateTime(2024, 8, 30, 11,3,1));
            //var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1));

            var data = products;
               
                 _memoryCache.Set(cacheKey,data, cacheEntryOptions);
                            
                return RedirectToAction("Index");   
            
        }

        public IActionResult Index() 
        {
            List<Product> plist = new List<Product>();
            plist=(List<Product>) _memoryCache.Get(cacheKey);
            return View(plist);

        }
    }
}
