using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using PideYa.Areas.User.Models;
using PideYa.Models;

namespace PideYa.Areas.Employee.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Employee/Pedido
        public ActionResult Index()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var user = _context.Users.Find(userId);
            var restaurante = _context.empresa_restaurante_usuario.First(m => m.usuarioASP_fk_Id == userId).restaurante;
            ViewBag.restauranteNombre = restaurante.nombre;
            var platos = _context.plato.Where(m => m.restaurante_id_fk == restaurante.restaurante_id).OrderBy(m => m.categoria_plato_id_fk).ToList().GroupBy(m => m.categoria_plato_id_fk);
            ViewBag.mesa_id = new SelectList(_context.mesa.Where(m => m .restaurante_id_fk == restaurante.restaurante_id && m.estado == "Activo"), "mesa_id", "mesa_id");

            return View();
        }

        [HttpPost]
        public ActionResult PedidoDetalle(int mesa_id)
        {
            var mesa = _context.mesa.Find(mesa_id);
            var restaurante = _context.restaurante.Find(mesa.restaurante_id_fk);
            var categorias = (
                from p in _context.plato.Distinct()
                join pc in _context.plato_categoria 
                    on p.categoria_plato_id_fk 
                    equals pc.plato_categoria_id
                where p.restaurante_id_fk == restaurante.restaurante_id
                select new Plato_CategoriaRestauranteDTO()
                {
                    plato_categoria_id = pc.plato_categoria_id,
                    nombre = pc.nombre,
                    restaurante_id_fk = p.restaurante_id_fk,
                    imagen = pc.imagen
                }).Distinct().ToList();

            ViewBag.mesaId = mesa_id;
            ViewBag.categorias = categorias;

            return View();
        }

        [HttpGet]
        public JsonResult CargarPlatos(int categoriaId)
        {
            var restaurante = _context.plato.Where(m => m.categoria_plato_id_fk == categoriaId)
                .Select(x => new
                {
                    id = x.plato_id,
                    x.nombre,
                    x.foto,
                    x.precio
                }).ToList();
            return Json(restaurante, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EnviarPedido(List<string> platoIds, List<string> cantidad, int mesaId)
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            var pedidoCabecera = new pedido_cabecera
            {
                mesa_id_fk = mesaId,
                estado = EstadoPedidoCabecera.Enviado,
                fecha = DateTime.Now,
                precio_final = 0
            };

            _context.pedido_cabecera.Add(pedidoCabecera);
            _context.SaveChanges();

            for (var i = 0; i < platoIds.Count; i++)
            {
                var platoId = Convert.ToInt32(platoIds[i]);
                var plato = _context.plato.SingleOrDefault(p => p.plato_id == platoId);
                var pedidoDetalle = new pedido_detalle
                {
                    plato_id_fk = Convert.ToInt32(platoIds[i]),
                    cantidad = Convert.ToInt32(cantidad[i]),
                    precio = plato.precio * Convert.ToInt32(cantidad[i]),
                    pedido_cabecera_id_fk = pedidoCabecera.pedido_cabecera_id
                };

                _context.pedido_detalle.Add(pedidoDetalle);
                _context.SaveChanges();
            }
            return Json(platoIds, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult PedidoCabecera()
        //{
        //    var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //    var user = _context.Users.Find(userId);
        //    var restaurante = _context.empresa_restaurante_usuario.First(m => m.usuarioASP_fk_Id == userId).restaurante;
        //    var restauranteList = new ArrayList { restaurante };
        //    //ViewBag.restaurante_id = new SelectList(_context.restaurante, "restaurante_id", "nombre");
        //    ViewBag.restaurante_id = new SelectList(restauranteList, "restaurante_id", "nombre");
        //    return View();
        //}

        [HttpPost]
        public ActionResult GuardarPedidoCabecera(PedidoViewModel pedidoViewModel)
        {
            var pedidoCabecera = new pedido_cabecera
            {
                mesa_id_fk = pedidoViewModel.PedidoCabecera.mesa_id_fk,
                estado = EstadoPedidoCabecera.Pidiendo,
                fecha = DateTime.Now,
                precio_final = 0
            };

            _context.pedido_cabecera.Add(pedidoCabecera);
            _context.SaveChanges();
            pedidoViewModel.PedidoCabecera = pedidoCabecera;

            var restaurante = _context.mesa
                .Include(m => m.restaurante)
                .SingleOrDefault(m => m.mesa_id == pedidoCabecera.mesa_id_fk)
                ?.restaurante;
            pedidoViewModel.Restaurante = restaurante;

            return View("PedidoDetalle", pedidoViewModel);
        }

        //public ActionResult PedidoDetalle(PedidoViewModel pedidoViewModel)
        //{
        //    return View();
        //}

        [HttpPost]
        public ActionResult GuardarDetalle(PedidoViewModel pedidoViewModel)
        {
            var plato = _context.plato.SingleOrDefault(p => p.plato_id == pedidoViewModel
                                                                .PedidoDetalle.plato_id_fk);
            var pedidoDetalle = new pedido_detalle
            {
                plato_id_fk = pedidoViewModel.PedidoDetalle.plato_id_fk,
                cantidad = pedidoViewModel.PedidoDetalle.cantidad,
                precio = plato?.precio * pedidoViewModel.PedidoDetalle.cantidad ?? 0,
                pedido_cabecera_id_fk = pedidoViewModel.PedidoCabecera.pedido_cabecera_id
            };

            _context.pedido_detalle.Add(pedidoDetalle);
            _context.SaveChanges();

            var pedidoCabecera = _context.pedido_cabecera.Find(pedidoDetalle.pedido_cabecera_id_fk);
            if (pedidoCabecera != null) pedidoCabecera.precio_final += pedidoDetalle.precio;
            _context.SaveChanges();
            pedidoViewModel.PedidoCabecera = pedidoCabecera;

            pedidoViewModel.PedidoDetalle = pedidoDetalle;
            pedidoViewModel.PedidoDetalleList = _context.pedido_detalle
                .Where(p => p.pedido_cabecera_id_fk == pedidoViewModel.PedidoCabecera.pedido_cabecera_id).ToList();

            return View("PedidoDetalle", pedidoViewModel);
        }

        //public ActionResult TerminarPedido(PedidoViewModel pedidoViewModel)
        //{
        //    var pedidoCabecera = _context.pedido_cabecera.Find(pedidoViewModel.PedidoCabecera.pedido_cabecera_id);
        //    if (pedidoCabecera != null) pedidoCabecera.estado = EstadoPedidoCabecera.Enviado;
        //    _context.SaveChanges();

        //    return RedirectToAction("PedidoCabecera");
        //}

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