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
    public class MesasController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Manager/mesas
        public ActionResult Index()
        {
            var mesa = _db.mesa.Include(m => m.restaurante);
            return View(mesa.ToList());
        }

        // GET: Manager/mesas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mesa mesa = _db.mesa.Find(id);
            if (mesa == null)
            {
                return HttpNotFound();
            }
            return View(mesa);
        }

        // GET: Manager/mesas/Create
        public ActionResult Create()
        {
            ViewBag.restaurante_id_fk = new SelectList(_db.restaurante, "restaurante_id", "nombre");
            return View();
        }

        // POST: Manager/mesas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mesa_id,restaurante_id_fk,estado")] mesa mesa)
        {
            if (ModelState.IsValid)
            {
                _db.mesa.Add(mesa);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.restaurante_id_fk = new SelectList(_db.restaurante, "restaurante_id", "nombre", mesa.restaurante_id_fk);
            return View(mesa);
        }

        // GET: Manager/mesas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mesa mesa = _db.mesa.Find(id);
            if (mesa == null)
            {
                return HttpNotFound();
            }
            ViewBag.restaurante_id_fk = new SelectList(_db.restaurante, "restaurante_id", "nombre", mesa.restaurante_id_fk);
            return View(mesa);
        }

        // POST: Manager/mesas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mesa_id,restaurante_id_fk,estado")] mesa mesa)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(mesa).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.restaurante_id_fk = new SelectList(_db.restaurante, "restaurante_id", "nombre", mesa.restaurante_id_fk);
            return View(mesa);
        }

        // GET: Manager/mesas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mesa mesa = _db.mesa.Find(id);
            if (mesa == null)
            {
                return HttpNotFound();
            }
            return View(mesa);
        }

        // POST: Manager/mesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mesa mesa = _db.mesa.Find(id);
            _db.mesa.Remove(mesa);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
