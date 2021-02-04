using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySportShop.Data;
using MySportShop.Models;
using MySportShop.Models.ViewModel;
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
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SneakersController(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _db = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Products.Include(u => u.Properties);
            return View(products);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                Properties = _db.Properties.Select(x=> new SelectListItem { Text = x.Size.ToString(), Value = x.Size.ToString() }),
                ProductInfos = new List<ProductInfo>()
            };

            if(id == null)
            {
                return View(productVM);
            }
            else
            {
                Product product = _db.Products.Find(id);
                if (product == null)
                    return NotFound();
                productVM.Product = product;
                return View(productVM);
            }

         
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
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
                    _db.Products.Add(productVM.Product);
                }else
                {
                Product objFromDb = _db.Products.AsNoTracking().FirstOrDefault(x => x.ProductId == productVM.Product.ProductId);
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
                    _db.Products.Update(productVM.Product);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            productVM.Properties =  _db.Properties.Select(x => new SelectListItem { Text = x.Size.ToString(), Value = x.Size.ToString() });
            return View(productVM);


        }


        //GET
        public  IActionResult Delete(int? id)
        {
            
            Product product = _db.Products.Find(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            Product product = _db.Products.Find(id);
            if (product == null)
                return NotFound();

            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
            var oldFile = Path.Combine(upload, product.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }


            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult AddProperty(int? id)
        {
            Product obj = _db.Products.Find(id);
            if (obj == null)
                return NotFound();

            ProductVM productVM = new ProductVM {
                Product = obj,
                Properties = _db.Properties.Select(x => new SelectListItem { Text = x.Size.ToString(), Value = x.Size.ToString() }),
                ProductInfos = _db.ProductInfo.Where(x => x.ProductId == obj.ProductId),
                Info = new ProductInfo()
            };
            return View(productVM);
        }

        //POST
        [HttpPost, ActionName("AddProperty")]
        [ValidateAntiForgeryToken]
        public IActionResult AddPropertyPost(ProductVM productVM)
        {
            ProductInfo obj = productVM.Info;
            obj.ProductId = productVM.Product.ProductId;

            _db.ProductInfo.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Upsert",new { id = obj.ProductId });
        }

      
    }
}
