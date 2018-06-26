using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PideYa.Models;

namespace PideYa.Areas.Manager.Controllers
{
    public class PlatosController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Manager/platos
        public ActionResult Index()
        {
            var plato = _db.plato.Include(p => p.plato_categoria).Include(p => p.restaurante);
            return View(plato.ToList());
        }

        // GET: Manager/platos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = _db.plato.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            return View(plato);
        }

        // GET: Manager/platos/Create
        public ActionResult Create()
        {
            if (System.Web.HttpContext.Current.User != null)
            {
                var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                ViewBag.restaurante_id_fk = new SelectList(
                    _db.empresa_restaurante_usuario.Include(r => r.restaurante).Where(m => m.usuarioASP_fk_Id == user).Select(res => res.restaurante).ToList(), 
                    "restaurante_id", "nombre");
            }
            else
            {
                ViewBag.restaurante_id_fk = new SelectList(_db.restaurante, "restaurante_id", "nombre");
            }
            ViewBag.categoria_plato_id_fk = new SelectList(_db.plato_categoria, "plato_categoria_id", "nombre");
            return View();
        }

        // POST: Manager/platos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "plato_id,restaurante_id_fk,nombre,precio,categoria_plato_id_fk,descripcion,foto,estado")] plato plato)
        {
            if (ModelState.IsValid)
            {
                _db.plato.Add(plato);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoria_plato_id_fk = new SelectList(_db.plato_categoria, "plato_categoria_id", "nombre", plato.categoria_plato_id_fk);
            ViewBag.restaurante_id_fk = new SelectList(_db.restaurante, "restaurante_id", "nombre", plato.restaurante_id_fk);
            return View(plato);
        }

        // GET: Manager/platos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = _db.plato.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoria_plato_id_fk = new SelectList(_db.plato_categoria, "plato_categoria_id", "nombre", plato.categoria_plato_id_fk);
            ViewBag.restaurante_id_fk = new SelectList(_db.restaurante, "restaurante_id", "nombre", plato.restaurante_id_fk);
            return View(plato);
        }

        // POST: Manager/platos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "plato_id,restaurante_id_fk,nombre,precio,categoria_plato_id_fk,descripcion,foto,estado")] plato plato)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(plato).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoria_plato_id_fk = new SelectList(_db.plato_categoria, "plato_categoria_id", "nombre", plato.categoria_plato_id_fk);
            ViewBag.restaurante_id_fk = new SelectList(_db.restaurante, "restaurante_id", "nombre", plato.restaurante_id_fk);
            return View(plato);
        }

        // GET: Manager/platos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = _db.plato.Find(id);
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
            plato plato = _db.plato.Find(id);
            _db.plato.Remove(plato ?? throw new InvalidOperationException());
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

        [HttpGet]
        public JsonResult GetRestaurantDishes(int? id)
        {
            var platos = _db.plato.Where(m =>
                    m.restaurante_id_fk == id &&
                    m.estado != "Inactivo")
                .Select(x => new
                {
                    id = x.plato_id,
                    nombre = x.nombre + " => S/." + x.precio
                })
                .ToList();
            return Json(platos, JsonRequestBehavior.AllowGet); ;
        }
    }
}
