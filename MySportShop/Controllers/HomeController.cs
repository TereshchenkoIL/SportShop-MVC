using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySportShop.Data;
using MySportShop.Models;
using MySportShop.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
            _logger.LogDebug(1, "NLog injected into HomeController");
        }

        public IActionResult Index()
        {
            var products = _db.Products;
            _logger.LogInformation("GET Home.Index called");
            return View(products);
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("GET Home.Privacy called");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int Id)
        {
         

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            if (shoppingCartList.Where(x => x.ProductId == Id).FirstOrDefault() == null)
            shoppingCartList.Add(new ShoppingCart() { ProductId = Id, Quantity = 1});

            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            _logger.LogInformation("Add an item to cart");
            return RedirectToAction("Index");
        }
    }
}
