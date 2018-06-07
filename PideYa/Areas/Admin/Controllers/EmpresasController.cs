using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PideYa.Models;

namespace PideYa.Areas.Admin.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Manager/empresas
        public ActionResult Index()
        {
            var empresa = _context.empresa.Include(e => e.estado_empresa);
            return View(empresa.ToList());
        }

        // GET: Manager/empresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresa empresa = _context.empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: Manager/empresas/Create
        public ActionResult Create()
        {
            ViewBag.estado_empresa_id_fk = new SelectList(_context.estado_empresa, "estado_empresa_id", "nombre");
            return View();
        }

        // POST: Manager/empresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "empresa_id,estado_empresa_id_fk,nombre,descripcion,ruc")] empresa empresa)
        {
            if (ModelState.IsValid)
            {
                _context.empresa.Add(empresa);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.estado_empresa_id_fk = new SelectList(_context.estado_empresa, "estado_empresa_id", "nombre", empresa.estado_empresa_id_fk);
            return View(empresa);
        }

        // GET: Manager/empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresa empresa = _context.empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.estado_empresa_id_fk = new SelectList(_context.estado_empresa, "estado_empresa_id", "nombre", empresa.estado_empresa_id_fk);
            return View(empresa);
        }

        // POST: Manager/empresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "empresa_id,estado_empresa_id_fk,nombre,descripcion,ruc")] empresa empresa)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(empresa).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.estado_empresa_id_fk = new SelectList(_context.estado_empresa, "estado_empresa_id", "nombre", empresa.estado_empresa_id_fk);
            return View(empresa);
        }

        // GET: Manager/empresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresa empresa = _context.empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Manager/empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var empresa = _context.empresa.Find(id);
            _context.empresa.Remove(empresa ?? throw new InvalidOperationException());
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
