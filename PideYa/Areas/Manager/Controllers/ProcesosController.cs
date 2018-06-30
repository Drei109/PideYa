using System;
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

                var orderedPedidosList = pedidosList.OrderByDescending(x => x.pedido_cabecera_id).ToList();

                return View(orderedPedidosList);
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

        public ActionResult Boletas()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var pedidosList = new List<pedido_cabecera>();
            if (userId != null)
            {
                var pedidos = _context.empresa_restaurante_usuario
                    .Where(x => x.usuarioASP_fk_Id == userId)
                    .Select(x => x.restaurante.mesa
                        .Select(y => y.pedido_cabecera
                            .Where(z => z.estado == EstadoPedidoCabecera.Terminado ||
                                        z.estado == EstadoPedidoCabecera.Generado)))
                    .ToList();
                foreach (var res in pedidos)
                {
                    pedidosList.AddRange(res.SelectMany(mesa => mesa));
                }
            }
            return View(pedidosList);
        }

        public ActionResult GenerarBoleta(int id)
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var pedidoCabeceraObj = _context.pedido_cabecera.Find(id);

            if (pedidoCabeceraObj == null)
                return View();
            
            //else
            pedidoCabeceraObj.estado = EstadoPedidoCabecera.Generado;

            var boletaCabecera = new boleta_cabecera
            {
                estado = EstadoBoletaCabecera.Generado,
                usuarioASP_fk_Id = userId,
                pedido_cabecera_fk_id = id,
                cliente = "",
                fecha = DateTime.Now,
                subtotal = pedidoCabeceraObj.precio_final * Convert.ToDecimal(0.16),
                total = pedidoCabeceraObj.precio_final * Convert.ToDecimal(1.16),
            };

            _context.boleta_cabecera.Add(boletaCabecera);
            _context.SaveChanges();

            foreach (var pedidoDetalle in pedidoCabeceraObj.pedido_detalle)
            {
                var boletaDetalle = new boleta_detalle
                {
                    cantidad = pedidoDetalle.cantidad,
                    plato = pedidoDetalle.plato,
                    plato_id_fk = pedidoDetalle.plato_id_fk,
                    boleta_cabecera_id_fk = boletaCabecera.boleta_cabecera_id,
                    total = pedidoDetalle.precio
                };
                _context.boleta_detalle.Add(boletaDetalle);
                _context.SaveChanges();

                boletaCabecera.boleta_detalle.Add(boletaDetalle);
            }

            return View(boletaCabecera);
        }

        public ActionResult VerBoleta(int id)
        {
            var boletaCabecera = _context.boleta_cabecera.SingleOrDefault(x => x.pedido_cabecera_fk_id == id);
            return View("GenerarBoleta", boletaCabecera);
        }
        public ActionResult VerReportes()
        {
            var galerias = _context.boleta_detalle.Include("plato").ToList();         
            return View("VerReportes", galerias);
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