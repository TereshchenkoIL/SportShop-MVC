using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySportShop.Data;
using MySportShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class PropertyController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PropertyController(ApplicationDbContext db )
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Property> properties = _db.Properties;
            return View(properties);
        }

        //GET
        public IActionResult Upsert(double? size)
        {
            if (size == null) return View(new Property());

            Property prop = _db.Properties.Find(size);
            if (prop == null) return NotFound();

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

                }
                else
                {
                    Property objFromDb = _db.Set<Property>().Local.FirstOrDefault(x => x.Size == prop.Size);
                    _db.Entry(objFromDb).State = EntityState.Detached;
                    _db.Entry(prop).State = EntityState.Modified;
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
          

            return View(property);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(double? size)
        {
            Property prop = _db.Properties.Find(size);
            if (prop == null) return NotFound();

            _db.Properties.Remove(prop);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
