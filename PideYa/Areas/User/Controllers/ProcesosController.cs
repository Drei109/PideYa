using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PideYa.Areas.User.Models;
using PideYa.Models;

namespace PideYa.Areas.User.Controllers
{
    public class ProcesosController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: User/Procesos
        public ActionResult PedidoCabecera()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var user = _context.Users.Find(userId);
            var restaurante = _context.empresa_restaurante_usuario.First(m => m.usuarioASP_fk_Id == userId).restaurante;
            var restauranteList = new ArrayList {restaurante};
            //ViewBag.restaurante_id = new SelectList(_context.restaurante, "restaurante_id", "nombre");
            ViewBag.restaurante_id = new SelectList(restauranteList, "restaurante_id", "nombre");
            return View();
        }

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

            return View("PedidoDetalle",pedidoViewModel);
        }
        
        public ActionResult PedidoDetalle(PedidoViewModel pedidoViewModel)
        {
            return View();
        }

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

        public ActionResult TerminarPedido(PedidoViewModel pedidoViewModel)
        {
            var pedidoCabecera = _context.pedido_cabecera.Find(pedidoViewModel.PedidoCabecera.pedido_cabecera_id);
            if (pedidoCabecera != null) pedidoCabecera.estado = EstadoPedidoCabecera.Enviado;
            _context.SaveChanges();

            return RedirectToAction("PedidoCabecera");
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