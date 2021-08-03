using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySportShop.Data;
using MySportShop.Models;
using MySportShop.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class PropertyController : Controller
    {
        private readonly IRepositoryManager _manager;
        private readonly ILogger<PropertyController> _logger;
        public PropertyController(IRepositoryManager manager, ILogger<PropertyController> logger )
        {
            _logger = logger;
            _manager = manager;
        }
        /*
        public IActionResult Index()
        {
            IEnumerable<Property> properties = _db.Properties;
            _logger.LogInformation("GET Property.Index called");
            return View(properties);
        }

        //GET
        public IActionResult Upsert(double? size)
        {
            if (size == null) return View(new Property());

            Property prop = _db.Properties.Find(size);
            if (prop == null)
            {
                _logger.LogWarning("Item not found in Property.Upsert");
                return NotFound(); 
            }
            _logger.LogInformation("GET Property.Upsert called");
            return View(prop);
            
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Property prop)
        {
            
            if (ModelState.IsValid)
            {
                if (_db.Properties.Find(prop.Size) == null)
                {
                    _db.Properties.Add(prop);
                    _logger.LogInformation("Add property");
                }
                else
                {
                    Property objFromDb = _db.Set<Property>().Local.FirstOrDefault(x => x.Size == prop.Size);
                    _db.Entry(objFromDb).State = EntityState.Detached;
                    _db.Entry(prop).State = EntityState.Modified;
                    _logger.LogInformation("Update property");
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prop);
        }


        //GET
        public IActionResult Delete(double? size)
        {
            Property property = _db.Properties.Find(size);

            _logger.LogInformation("GET Property.Delete called");
            return View(property);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(double? size)
        {
            Property prop = _db.Properties.Find(size);
            if (prop == null)
            {
                _logger.LogWarning("Property not found in Property.DeletePost");
                return NotFound();
            }

            _db.Properties.Remove(prop);
            _db.SaveChanges();
            _logger.LogInformation("Property has been deleted");
            return RedirectToAction("Index");
        }
        */
    }
}
