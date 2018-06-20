using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PideYa.Models;

namespace PideYa.Areas.Manager.Controllers
{
    public class PlatosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manager/platos
        public ActionResult Index()
        {
            var plato = db.plato.Include(p => p.plato_categoria).Include(p => p.restaurante);
            return View(plato.ToList());
        }

        // GET: Manager/platos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = db.plato.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            return View(plato);
        }

        // GET: Manager/platos/Create
        public ActionResult Create()
        {
            ViewBag.categoria_plato_id_fk = new SelectList(db.plato_categoria, "plato_categoria_id", "nombre");
            ViewBag.restaurante_id_fk = new SelectList(db.restaurante, "restaurante_id", "nombre");
            return View();
        }

        // POST: Manager/platos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "plato_id,restaurante_id_fk,nombre,precio,categoria_plato_id_fk,descripcion,foto,estado")] plato plato)
        {
            if (ModelState.IsValid)
            {
                db.plato.Add(plato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoria_plato_id_fk = new SelectList(db.plato_categoria, "plato_categoria_id", "nombre", plato.categoria_plato_id_fk);
            ViewBag.restaurante_id_fk = new SelectList(db.restaurante, "restaurante_id", "nombre", plato.restaurante_id_fk);
            return View(plato);
        }

        // GET: Manager/platos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = db.plato.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoria_plato_id_fk = new SelectList(db.plato_categoria, "plato_categoria_id", "nombre", plato.categoria_plato_id_fk);
            ViewBag.restaurante_id_fk = new SelectList(db.restaurante, "restaurante_id", "nombre", plato.restaurante_id_fk);
            return View(plato);
        }

        // POST: Manager/platos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "plato_id,restaurante_id_fk,nombre,precio,categoria_plato_id_fk,descripcion,foto,estado")] plato plato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoria_plato_id_fk = new SelectList(db.plato_categoria, "plato_categoria_id", "nombre", plato.categoria_plato_id_fk);
            ViewBag.restaurante_id_fk = new SelectList(db.restaurante, "restaurante_id", "nombre", plato.restaurante_id_fk);
            return View(plato);
        }

        // GET: Manager/platos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = db.plato.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            return View(plato);
        }

        // POST: Manager/platos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            plato plato = db.plato.Find(id);
            db.plato.Remove(plato);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
