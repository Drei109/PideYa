using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PideYa.Areas.User.Models;
using PideYa.Models;

namespace PideYa.Areas.User.Controllers
{
    public class ProcesosController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: User/Procesos
        public ActionResult Pedido()
        {
            ViewBag.restaurante_id = new SelectList(_context.restaurante, "restaurante_id", "nombre");
            return View();
        }

        [HttpPost]
        public ActionResult GuardarPedido(PedidoViewModel pedidoViewModel)
        {
            var pedidoCabecera = pedidoViewModel.PedidoCabecera;
            var pedidoDetalle = pedidoViewModel.PedidoDetalle;
            return View("Pedido");
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