﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySportShop.Data;
using MySportShop.Models;
using MySportShop.Models.ViewModel;
using MySportShop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MySportShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<CartController> _logger;
        public CartController(ApplicationDbContext db, UserManager<AppUser> manager, ILogger<CartController> logger)
        {
            _logger = logger;
            _db = db;
            _userManager = manager;
            _logger.LogDebug(1, "NLog injected into CartController");
        }
        public IActionResult Index()
        {
           List<ShoppingCart> items = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
               && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                items = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

                List<CartVM> cartVM = new List<CartVM>();
            foreach(var item in items)
            {
                cartVM.Add( new CartVM(_db.Products.Find(item.ProductId),item.Quantity));
            }

            _logger.LogInformation("GET Cart.Index called");
            return View(cartVM);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            Product product = _db.Products.Find(id);
            if (id == null)
                return NotFound();
            _logger.LogInformation("GET Cart.Delete called");
            return View(product);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            bool InList = _db.Products.FirstOrDefault(x => x.ProductId == id) != null;

           if(InList)
            {
                List<ShoppingCart> items = new List<ShoppingCart>();
                if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                   && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
                {
                    items = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
                }
                if(items.Count() > 0)
                {
                    items.Remove(items.First(x => x.ProductId == id));
                    _logger.LogInformation("Remove item from cart");
                }
                HttpContext.Session.Set<List<ShoppingCart>>(WC.SessionCart, items);
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(IFormCollection cart)
        {
            if (cart == null )
                return RedirectToAction("Index");
            

            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            Order order = new Order()
            {
                Id = user.Id,
                Creation_Date = DateTime.Now

            };

            _db.Orders.Add(order);
            _db.SaveChanges();
            int id = order.OrderId;
            for (int i = 0; i < (cart.Count-1)/2; i++)
            {
                OrderInfo oInfo = new OrderInfo()
                {
                    ProductId = int.Parse(cart[$"[{i}].Product.ProductId"]),
                    Amount = int.Parse(cart[$"[{i}].Quantity"]),
                    OrderId = id
                };
                _db.OrderInfo.Add(oInfo);
            }
            _db.SaveChanges();

            _logger.LogInformation("Create an order");
            List<ShoppingCart> items = new List<ShoppingCart>();
            HttpContext.Session.Set<List<ShoppingCart>>(WC.SessionCart, items);
            _logger.LogInformation("Clear cart");
            return RedirectToAction("Index");
        }




    }
}
