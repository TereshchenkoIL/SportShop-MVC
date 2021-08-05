using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySportShop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MySportShop.Repository.Interfaces;
using MySportShop.Models.Models;
using MySportShop.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MySportShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<CartController> _logger;
        public CartController(IRepositoryManager repoManager, UserManager<AppUser> manager, ILogger<CartController> logger)
        {
            _logger = logger;
            _repositoryManager = repoManager;
            _userManager = manager;
            _logger.LogDebug(1, "NLog injected into CartController");
        }
        public async Task<IActionResult> Index()
        {
            List<ShoppingCart> items = WC.GetCartItems(HttpContext).ToList();

            List<CartVM> cartVM = new List<CartVM>();
            foreach(var item in items)
            {
                cartVM.Add( new CartVM(await _repositoryManager.Product.GetById(item.ProductId, false),item.Quantity));
            }

            _logger.LogInformation("GET Cart.Index called");
            return View(cartVM);
        }

        //GET
        public async Task<IActionResult> Delete(int? id)
        {
            Product product = await _repositoryManager.Product.GetById(id, false);
            if (id == null)
                return NotFound();
            _logger.LogInformation("GET Cart.Delete called");
            return View(product);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            Product product = await _repositoryManager.Product.GetById(id, false);
            bool InList = product != null;

           if(InList)
            {
                List<ShoppingCart> items = WC.GetCartItems(HttpContext).ToList();
                if(items.Count() > 0)
                {
                    items.Remove(items.First(x => x.ProductId == id));
                    _logger.LogInformation("Remove item from cart");
                }
                HttpContext.Session.Set<List<ShoppingCart>>(WC.SessionCart, items);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(IFormCollection cart)
        {
            if (cart == null )
                return RedirectToAction("Index");
            

            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            Order order = new Order()
            {
                Id = user.Id,
                Creation_Date = DateTime.Now,
                OrdersInfo = new List<OrderInfo>()

            };

            await _repositoryManager.Order.AddAsync(order);
            await _repositoryManager.Save();
            int id = order.OrderId;
            for (int i = 0; i < (cart.Count-1)/3; i++)
            {
                OrderInfo oInfo = new OrderInfo()
                {
                    ProductId = int.Parse(cart[$"[{i}].Product.ProductId"]),
                    Amount = int.Parse(cart[$"[{i}].Quantity"]),
                    Size = int.Parse(cart[$"[{i}].Size"]),
                    OrderId = id
                };
                order.OrdersInfo.Add(oInfo);
            }
            await _repositoryManager.Save();

            _logger.LogInformation("Create an order");
            List<ShoppingCart> items = new List<ShoppingCart>();
            HttpContext.Session.Set<List<ShoppingCart>>(WC.SessionCart, items);
            _logger.LogInformation("Clear cart");
            return RedirectToAction("Index");
        }
        //GET
        [HttpGet]
        public async Task<IActionResult> OrderInfo()
        {
            List<ShoppingCart> items = WC.GetCartItems(HttpContext).ToList();

            var cartVM = await Task.WhenAll(items.Select( async x => 
                        new CartVM( await _repositoryManager.Product
                        .GetById(x.ProductId, false), x.Quantity)));
            OrderInfoVM orderInfoVM = new OrderInfoVM() { 
            Products = cartVM,
            Properties = (await _repositoryManager.Property.GetAllAsync(false))
            .Select(x => new SelectListItem { Text = x.Size.ToString(), Value = x.Size.ToString() })
            
            };
            return View(orderInfoVM);
        }




    }
}
