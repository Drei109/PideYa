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
    public class insumosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manager/insumos
        public async Task<ActionResult> Index()
        {
            var insumo = db.insumo.Include(i => i.proveedor);
            return View(await insumo.ToListAsync());
        }

        // GET: Manager/insumos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            insumo insumo = await db.insumo.FindAsync(id);
            if (insumo == null)
            {
                return HttpNotFound();
            }
            return View(insumo);
        }

        // GET: Manager/insumos/Create
        public ActionResult Create()
        {
            ViewBag.proveedor_id_fk = new SelectList(db.proveedor, "proveedor_id", "nombre");
            return View();
        }

        // POST: Manager/insumos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "insumo_id,proveedor_id_fk,nombre,precio,unidad,cantidad,estado")] insumo insumo)
        {
            if (ModelState.IsValid)
            {
                db.insumo.Add(insumo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.proveedor_id_fk = new SelectList(db.proveedor, "proveedor_id", "nombre", insumo.proveedor_id_fk);
            return View(insumo);
        }

        // GET: Manager/insumos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            insumo insumo = await db.insumo.FindAsync(id);
            if (insumo == null)
            {
                return HttpNotFound();
            }
            ViewBag.proveedor_id_fk = new SelectList(db.proveedor, "proveedor_id", "nombre", insumo.proveedor_id_fk);
            return View(insumo);
        }

        // POST: Manager/insumos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "insumo_id,proveedor_id_fk,nombre,precio,unidad,cantidad,estado")] insumo insumo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insumo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.proveedor_id_fk = new SelectList(db.proveedor, "proveedor_id", "nombre", insumo.proveedor_id_fk);
            return View(insumo);
        }

        // GET: Manager/insumos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            insumo insumo = await db.insumo.FindAsync(id);
            if (insumo == null)
            {
                return HttpNotFound();
            }
            return View(insumo);
        }

        // POST: Manager/insumos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            insumo insumo = await db.insumo.FindAsync(id);
            db.insumo.Remove(insumo);
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
