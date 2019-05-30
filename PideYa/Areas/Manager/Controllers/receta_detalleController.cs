using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PideYa.Models;

namespace PideYa.Areas.Manager.Controllers
{
    public class receta_detalleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manager/receta_detalle
        public async Task<ActionResult> Index()
        {
            var receta_detalle = db.receta_detalle.Include(r => r.insumo).Include(r => r.plato);
            return View(await receta_detalle.ToListAsync());
        }

        // GET: Manager/receta_detalle/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receta_detalle receta_detalle = await db.receta_detalle.FindAsync(id);
            if (receta_detalle == null)
            {
                return HttpNotFound();
            }
            return View(receta_detalle);
        }

        // GET: Manager/receta_detalle/Create
        public ActionResult Create()
        {
            ViewBag.insumo_id = new SelectList(db.insumo, "insumo_id", "nombre");
            ViewBag.plato_id = new SelectList(db.plato, "plato_id", "nombre");
            return View();
        }

        // POST: Manager/receta_detalle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "plato_id,insumo_id,cantidad")] receta_detalle receta_detalle)
        {
            if (ModelState.IsValid)
            {
                db.receta_detalle.Add(receta_detalle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.insumo_id = new SelectList(db.insumo, "insumo_id", "nombre", receta_detalle.insumo_id);
            ViewBag.plato_id = new SelectList(db.plato, "plato_id", "nombre", receta_detalle.plato_id);
            return View(receta_detalle);
        }

        // GET: Manager/receta_detalle/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receta_detalle receta_detalle = await db.receta_detalle.FindAsync(id);
            if (receta_detalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.insumo_id = new SelectList(db.insumo, "insumo_id", "nombre", receta_detalle.insumo_id);
            ViewBag.plato_id = new SelectList(db.plato, "plato_id", "nombre", receta_detalle.plato_id);
            return View(receta_detalle);
        }

        // POST: Manager/receta_detalle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "plato_id,insumo_id,cantidad")] receta_detalle receta_detalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receta_detalle).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.insumo_id = new SelectList(db.insumo, "insumo_id", "nombre", receta_detalle.insumo_id);
            ViewBag.plato_id = new SelectList(db.plato, "plato_id", "nombre", receta_detalle.plato_id);
            return View(receta_detalle);
        }

        // GET: Manager/receta_detalle/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receta_detalle receta_detalle = await db.receta_detalle.FindAsync(id);
            if (receta_detalle == null)
            {
                return HttpNotFound();
            }
            return View(receta_detalle);
        }

        // POST: Manager/receta_detalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            receta_detalle receta_detalle = await db.receta_detalle.FindAsync(id);
            db.receta_detalle.Remove(receta_detalle);
            await db.SaveChangesAsync();
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
