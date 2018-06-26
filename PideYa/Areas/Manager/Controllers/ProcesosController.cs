using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PideYa.Areas.User.Models;
using PideYa.Models;

namespace PideYa.Areas.Manager.Controllers
{
    public class ProcesosController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Pedidos()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var pedidosList = new List<pedido_cabecera>();
            if (userId != null)
            {
                var pedidos = _context.empresa_restaurante_usuario
                    .Where(x => x.usuarioASP_fk_Id == userId)
                    .Select(x => x.restaurante.mesa
                        .Select(y => y.pedido_cabecera
                            .Where(z => z.estado == EstadoPedidoCabecera.Enviado ||
                                        z.estado == EstadoPedidoCabecera.Preparando ||
                                        z.estado == EstadoPedidoCabecera.Cancelado)))
                    .ToList();

                foreach (var res in pedidos)
                {
                    pedidosList.AddRange(res.SelectMany(mesa => mesa));
                }

                return View(pedidosList);
            }
            else
            {
                var pedidos = _context.pedido_cabecera
                    .Include(x => x.pedido_detalle.Select(y => y.plato))
                    .ToList();
                return View(pedidos);
            }
        }

        public ActionResult TerminarPedido(int id)
        {
            var pedido = _context.pedido_cabecera.Find(id);
            if (pedido != null) pedido.estado = EstadoPedidoCabecera.Terminado;
            _context.SaveChanges();

            return RedirectToAction("Pedidos");
        }

        public ActionResult CancelarPedido(int id)
        {
            var pedido = _context.pedido_cabecera.Find(id);
            if (pedido != null) pedido.estado = EstadoPedidoCabecera.Cancelado;
            _context.SaveChanges();

            return RedirectToAction("Pedidos");
        }

        public ActionResult Boleta()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var pedidosList = new List<pedido_cabecera>();
            if (userId != null)
            {
                var pedidos = _context.empresa_restaurante_usuario
                    .Where(x => x.usuarioASP_fk_Id == userId)
                    .Select(x => x.restaurante.mesa
                        .Select(y => y.pedido_cabecera
                            .Where(z => z.estado == EstadoPedidoCabecera.Terminado)))
                    .ToList();
                foreach (var res in pedidos)
                {
                    pedidosList.AddRange(res.SelectMany(mesa => mesa));
                }
            }
            return View(pedidosList);
        }

        public ActionResult GenerarBoleta()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var pedidosList = new List<pedido_cabecera>();
            if (userId != null)
            {
                var pedidos = _context.empresa_restaurante_usuario
                    .Where(x => x.usuarioASP_fk_Id == userId)
                    .Select(x => x.restaurante.mesa
                        .Select(y => y.pedido_cabecera
                            .Where(z => z.estado == EstadoPedidoCabecera.Terminado)))
                    .ToList();
                foreach (var res in pedidos)
                {
                    pedidosList.AddRange(res.SelectMany(mesa => mesa));
                }
            }
            return View(pedidosList);
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