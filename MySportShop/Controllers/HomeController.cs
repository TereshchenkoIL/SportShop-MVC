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
using MySportShop.Repository.Interfaces;
using MySportShop.Models.Models;
using MySportShop.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MySportShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositoryManager _manager;



        public HomeController(ILogger<HomeController> logger, IRepositoryManager manager )
        {
            _logger = logger;
            _manager = manager;
            _logger.LogDebug(1, "NLog injected into HomeController");
            
        }

        public async Task<IActionResult> Index()
        {
            var products = await _manager.Product.GetAllAsync(false);

          
            //var products = await _db.Products.ToListAsync() ;
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
