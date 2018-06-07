using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PideYa.Models;

namespace PideYa.Areas.Admin.Controllers
{
    public class TiposRestauranteController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Admin/TiposRestaurante
        public ActionResult Index()
        {
            return View(_context.tipo_restaurante.ToList());
        }

        // GET: Admin/TiposRestaurante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_restaurante tipo_restaurante = _context.tipo_restaurante.Find(id);
            if (tipo_restaurante == null)
            {
                return HttpNotFound();
            }
            return View(tipo_restaurante);
        }

        // GET: Admin/TiposRestaurante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TiposRestaurante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tipo_restaurante_id,descripcion")] tipo_restaurante tipo_restaurante)
        {
            if (ModelState.IsValid)
            {
                _context.tipo_restaurante.Add(tipo_restaurante);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_restaurante);
        }

        // GET: Admin/TiposRestaurante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_restaurante tipo_restaurante = _context.tipo_restaurante.Find(id);
            if (tipo_restaurante == null)
            {
                return HttpNotFound();
            }
            return View(tipo_restaurante);
        }

        // POST: Admin/TiposRestaurante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tipo_restaurante_id,descripcion")] tipo_restaurante tipo_restaurante)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(tipo_restaurante).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_restaurante);
        }

        // GET: Admin/TiposRestaurante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_restaurante tipo_restaurante = _context.tipo_restaurante.Find(id);
            if (tipo_restaurante == null)
            {
                return HttpNotFound();
            }
            return View(tipo_restaurante);
        }

        // POST: Admin/TiposRestaurante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tipo_restaurante tipo_restaurante = _context.tipo_restaurante.Find(id);
            _context.tipo_restaurante.Remove(tipo_restaurante);
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
