using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PideYa.Models;

namespace PideYa.Areas.Admin.Controllers
{
    public class RestaurantesController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Admin/Restaurantes
        public ActionResult Index()
        {
            var restaurante = _context.restaurante.Include(r => r.empresa);
            return View(restaurante.ToList());
        }

        // POST: Admin/Restaurantes/Search
        [HttpGet]
        public JsonResult Search(string term)
        {
            if (term.IsNullOrWhiteSpace())
            {
                var restaurante = _context.empresa.
                    Select(x => new
                    {
                        id = x.empresa_id,
                        nombre = x.nombre
                    }).ToList();
                return Json(restaurante, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var restaurante = _context.empresa.
                    Where(m => m.nombre.StartsWith(term.ToLower())).
                    Select(x => new
                    {
                        id = x.empresa_id,
                        nombre = x.nombre
                    }).ToList();
                return Json(restaurante, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/Restaurantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurante restaurante = _context.restaurante.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // GET: Admin/Restaurantes/Create
        public ActionResult Create()
        {
            ViewBag.empresa_id_fk = new SelectList(_context.empresa, "empresa_id", "nombre");
            return View();
        }

        // POST: Admin/Restaurantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "restaurante_id,empresa_id_fk,nombre,foto")] restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                _context.restaurante.Add(restaurante);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.empresa_id_fk = new SelectList(_context.empresa, "empresa_id", "nombre", restaurante.empresa_id_fk);
            return View(restaurante);
        }

        // GET: Admin/Restaurantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurante restaurante = _context.restaurante.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            ViewBag.empresa_id_fk = new SelectList(_context.empresa, "empresa_id", "nombre", restaurante.empresa_id_fk);
            return View(restaurante);
        }

        // POST: Admin/Restaurantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "restaurante_id,empresa_id_fk,nombre,foto")] restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(restaurante).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.empresa_id_fk = new SelectList(_context.empresa, "empresa_id", "nombre", restaurante.empresa_id_fk);
            return View(restaurante);
        }

        // GET: Admin/Restaurantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurante restaurante = _context.restaurante.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // POST: Admin/Restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            restaurante restaurante = _context.restaurante.Find(id);
            _context.restaurante.Remove(restaurante);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
