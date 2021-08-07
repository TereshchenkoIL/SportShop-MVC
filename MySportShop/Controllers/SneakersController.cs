using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySportShop.Models.Models;
using MySportShop.Models.ViewModel;
using MySportShop.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class SneakersController : Controller
    {
        private readonly IRepositoryManager _manager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<SneakersController> _logger;
        public SneakersController(IRepositoryManager manager , IWebHostEnvironment webHostEnvironment, ILogger<SneakersController> logger)
        {
            _logger = logger;
            _manager = manager;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products =  await _manager.Product.GetAllWithProps(false);
            _logger.LogInformation("GET Sneakers.Index called");
            return View(products);
            
        }

      

        //GET
        public async Task<IActionResult> Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                Properties = (await _manager.Property.GetAllAsync(false)).Select(x=> new SelectListItem { Text = x.Size.ToString(), Value = x.Size.ToString() }),
                ProductInfos = new List<ProductInfo>()
            };

            if(id == null)
            {
                _logger.LogWarning("id parametr of GET Sneakers.Upsert == null");
                return View(productVM);
            }
            else
            {
                Product product = await _manager.Product.GetById(id, true);
                if (product == null)
                {
                    _logger.LogWarning("Product not found in Sneakers.Upsert");
                    return NotFound();
                }
                productVM.Product = product;
                _logger.LogInformation("GET Sneakers.Upsert called");
                return View(productVM);
            }

         
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductVM productVM)
        {
            if(ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if(productVM.Product.ProductId == 0)
                {
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productVM.Product.Image = fileName + extension;
                    
                  await _manager.Product.AddAsync(productVM.Product);
                }else
                {
                Product objFromDb = (await _manager.Product.GetByCondition(x => x.ProductId == productVM.Product.ProductId, false)).FirstOrDefault();
                if(files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.Product.Image = fileName + extension;
                    }
                    else
                    {
                        productVM.Product.Image = objFromDb.Image;
                    }
                   _manager.Product.Update(productVM.Product);
                }

                await _manager.Save();
                _logger.LogInformation("Item has been upserted");
                return RedirectToAction("Index");
            }
            
            productVM.Properties =  (await _manager.Property.GetAllAsync(false)).Select(x => new SelectListItem { Text = x.Size.ToString(), Value = x.Size.ToString() });
            return View(productVM);


        }


        //GET
        public async  Task<IActionResult> Delete(int? id)
        {
            
            Product product = await _manager.Product.GetById(id, false);
            if (product == null)
            {
                _logger.LogWarning("Product not found in Sneakers.Delete");
                return NotFound();
            }
            return View(product);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            Product product = await _manager.Product.GetById(id, true);
            if (product == null)
            {
                _logger.LogWarning("Product not found in Sneakers.Delete Post");
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
            var oldFile = Path.Combine(upload, product.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }


            _manager.Product.Delete(product);
            await _manager.Save();
            _logger.LogInformation("Product has been deleted");
            return RedirectToAction("Index");
        }

        //GET
        public async Task<IActionResult> AddProperty(int? id)
        {
            Product obj = await _manager.Product.GetById(id, false);
            if (obj == null)
            {
                _logger.LogWarning("Product not found in Sneakers.AddProperty");
                return NotFound();
            }

            ProductVM productVM = new ProductVM {
                Product = obj,
                Properties = (await _manager.Property.GetAllAsync(false)).Select(x => new SelectListItem { Text = x.Size.ToString(), Value = x.Size.ToString() }),
                ProductInfos = await _manager.Product.GetProductInfos(obj.ProductId, false),
                Info = new ProductInfo()
            };
            _logger.LogInformation("GET Sneakers.AddPropery called");
            return View(productVM);
        }

        //POST
        [HttpPost, ActionName("AddProperty")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPropertyPost(ProductVM productVM)
        {
            ProductInfo obj = productVM.Info;
            obj.ProductId = productVM.Product.ProductId;
            Product objFromDb = await _manager.Product.GetById(obj.ProductId, true);
            objFromDb.Properties.Add(obj);
            _logger.LogInformation("Property has been added");
            _manager.Save();
            return RedirectToAction("Upsert",new { id = obj.ProductId });
        }


       // GET
       [HttpGet]
       public async Task<IActionResult> Details(int? id)
        {
            Product product = await _manager.Product.GetById(id, false);
            return View(product);
        }

        // GET
        [HttpGet, ActionName("MaleSneakers")]
        public async Task<IActionResult> GetMaleSneakers()
        {
            var users = await _manager.Product.GetByCondition(x => x.Sex == "Male" || x.Sex == "All", false);
            return View("../Home/Index", users);
        }

        // GET
        [HttpGet, ActionName("FemaleSneakers")]
        public async Task<IActionResult> GetFemaleSneakers()
        {
            var users = await _manager.Product.GetByCondition(x => x.Sex == "Female" || x.Sex == "All", false);
            return View("../Home/Index", users);
        }



    }
}
