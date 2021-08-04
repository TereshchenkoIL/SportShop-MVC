using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySportShop.Data;
using MySportShop.Models;
using MySportShop.Models.Models;
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
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<Property> properties  = await _manager.Property.GetAllAsync(false);
            _logger.LogInformation("GET Property.Index called");
            return View(properties);
        }

        //GET
        public async Task<IActionResult> Upsert(double? size)
        {
            if (size == null) return View(new Property());

            Property prop = (await _manager.Property.GetByCondition(x => x.Size == size, false)).FirstOrDefault();
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
        public async Task<IActionResult> Upsert(Property prop)
        {
            
            if (ModelState.IsValid)
            {
                Property objFromDb = (await _manager.Property.GetByCondition(x => x.Size == prop.Size, true)).FirstOrDefault();
                if ( objFromDb == null)
                {
                    await _manager.Property.AddAsync(prop);
                    _logger.LogInformation("Add property");
                }
                else
                {
                    _manager.Property.Update(prop);
                    _logger.LogInformation("Update property");
                }

                await _manager.Save();
                return RedirectToAction("Index");
            }
            return View(prop);
        }


        //GET
        public async Task<IActionResult> Delete(double? size)
        {
            Property property = (await _manager.Property.GetByCondition(x => x.Size == size, true)).FirstOrDefault();

            _logger.LogInformation("GET Property.Delete called");
            return View(property);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(double? size)
        {
            Property prop = (await _manager.Property.GetByCondition(x => x.Size == size, true)).FirstOrDefault();
            if (prop == null)
            {
                _logger.LogWarning("Property not found in Property.DeletePost");
                return NotFound();
            }

            _manager.Property.Delete(prop);
            await _manager.Save();
            _logger.LogInformation("Property has been deleted");
            return RedirectToAction("Index");
        }
        
    }
}
