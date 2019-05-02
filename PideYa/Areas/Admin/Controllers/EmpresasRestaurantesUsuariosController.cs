using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PideYa.Models;

namespace PideYa.Areas.Admin.Controllers
{
    public class EmpresasRestaurantesUsuariosController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Admin/EmpresasRestaurantesUsuarios
        public ActionResult Index()
        {
            var empresaRestauranteUsuario = _context.empresa_restaurante_usuario.Include(e => e.empresa).Include(e => e.restaurante).Include(e => e.user);
            return View(empresaRestauranteUsuario.ToList());
        }

        // GET: Admin/EmpresasRestaurantesUsuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresa_restaurante_usuario empresa_restaurante_usuario = _context.empresa_restaurante_usuario.Find(id);
            if (empresa_restaurante_usuario == null)
            {
                return HttpNotFound();
            }
            return View(empresa_restaurante_usuario);
        }

        // GET: Admin/EmpresasRestaurantesUsuarios/Create
        public ActionResult Create()
        {
            ViewBag.empresa_id_fk = new SelectList(_context.empresa, "empresa_id", "nombre");
            ViewBag.restaurante_id_fk = new SelectList(_context.restaurante, "restaurante_id", "nombre");
            return View();
        }

        // POST: Admin/EmpresasRestaurantesUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "restaurante_usuario_id,empresa_id_fk,restaurante_id_fk,usuarioASP_fk_Id")] empresa_restaurante_usuario empresa_restaurante_usuario)
        {
            if (ModelState.IsValid)
            {
                _context.empresa_restaurante_usuario.Add(empresa_restaurante_usuario);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.empresa_id_fk = new SelectList(_context.empresa, "empresa_id", "nombre", empresa_restaurante_usuario.empresa_id_fk);
            ViewBag.restaurante_id_fk = new SelectList(_context.restaurante, "restaurante_id", "nombre", empresa_restaurante_usuario.restaurante_id_fk);
            return View(empresa_restaurante_usuario);
        }

        // GET: Admin/EmpresasRestaurantesUsuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresa_restaurante_usuario empresa_restaurante_usuario = _context.empresa_restaurante_usuario.Find(id);
            if (empresa_restaurante_usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.empresa_id_fk = new SelectList(_context.empresa, "empresa_id", "nombre", empresa_restaurante_usuario.empresa_id_fk);
            ViewBag.restaurante_id_fk = new SelectList(_context.restaurante, "restaurante_id", "nombre", empresa_restaurante_usuario.restaurante_id_fk);
            return View(empresa_restaurante_usuario);
        }

        // POST: Admin/EmpresasRestaurantesUsuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "restaurante_usuario_id,empresa_id_fk,restaurante_id_fk,usuarioASP_fk_Id")] empresa_restaurante_usuario empresa_restaurante_usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(empresa_restaurante_usuario).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.empresa_id_fk = new SelectList(_context.empresa, "empresa_id", "nombre", empresa_restaurante_usuario.empresa_id_fk);
            ViewBag.restaurante_id_fk = new SelectList(_context.restaurante, "restaurante_id", "nombre", empresa_restaurante_usuario.restaurante_id_fk);
            return View(empresa_restaurante_usuario);
        }

        // GET: Admin/EmpresasRestaurantesUsuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresa_restaurante_usuario empresa_restaurante_usuario = _context.empresa_restaurante_usuario.Find(id);
            if (empresa_restaurante_usuario == null)
            {
                return HttpNotFound();
            }
            return View(empresa_restaurante_usuario);
        }

        // POST: Admin/EmpresasRestaurantesUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            empresa_restaurante_usuario empresa_restaurante_usuario = _context.empresa_restaurante_usuario.Find(id);
            _context.empresa_restaurante_usuario.Remove(empresa_restaurante_usuario);
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

        // POST: Admin/EmpresasRestaurantesUsuarios/SearchUser
        [HttpGet]
        public JsonResult SearchUser(string term)
        {
            if (term.IsNullOrWhiteSpace())
            {
                var userList = _context.Users.
                    Select(x => new
                    {
                        id = x.Id,
                        nombre = x.Email
                    }).Take(10).ToList();
                return Json(userList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var userList = _context.Users.
                    Where   (m => m.Email.StartsWith(term.ToLower())).
                    Select(x => new
                    {
                        id = x.Id,
                        nombre = x.Email
                    }).Take(10).ToList();
                return Json(userList, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SearchRestaurante(string term, int restId)
        {
            if (term.IsNullOrWhiteSpace())
            {
                var restauranteList = _context.restaurante.
                    Where(m => m.empresa_id_fk == restId).
                    Select(x => new
                    {
                        id = x.restaurante_id,
                        nombre = x.nombre
                    }).Take(10).ToList();
                return Json(restauranteList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var restauranteList = _context.restaurante.
                    Where(m => m.nombre.StartsWith(term.ToLower()) && m.empresa_id_fk == restId).
                    Select(x => new
                    {
                        id = x.restaurante_id,
                        nombre = x.nombre
                    }).Take(10).ToList();
                return Json(restauranteList, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
