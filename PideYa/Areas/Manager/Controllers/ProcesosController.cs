﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FirebaseNet.Messaging;
using Microsoft.AspNet.Identity;
using PideYa.Areas.Manager.Models;
using PideYa.Areas.User.Models;
using PideYa.Models;

namespace PideYa.Areas.Manager.Controllers
{
    public class ProcesosController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        private const string FirebaseToken = "AAAAtM_wP0M:APA91bEXyqy3dRfNn7a_UiWx5MOWfKlK66d77zKGAFx05IR08ekXUwUr0BXqN_3ww5C68KazVZQuavm_QRuHbdnyF6BEQYoCPchLc5p3wRo8ioDQP8BGn5-J_P1fD6pfY1dMMdo32EbKAC95mr474IvPlezp7RU_vg";

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
                                        z.estado == EstadoPedidoCabecera.Preparando)))
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

            var token = pedido != null ? pedido.token : "";
            NotificacionTerminarPedido(token);
            return RedirectToAction("Pedidos");
        }

        public Task NotificacionTerminarPedido(string token)
        {
            var client = new FCMClient(FirebaseToken);
            var message = new Message()
            {
                To = token,
                Notification = new AndroidNotification()
                {
                    Title = "Su pedido está listo",
                    Body = "El mozo le estará trayendo su pedido en unos momentos"
                }

            };
            return client.SendMessageAsync(message);
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

            var token = pedidoCabeceraObj.token;
            NotificacionGenerarBoleta(token);

            return View(boletaCabecera);
        }

        public ActionResult Dashboard()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var user = _context.Users.Find(userId);
            var restaurante = _context.empresa_restaurante_usuario.First(m => m.usuarioASP_fk_Id == userId).restaurante;

            var criterioVentasDiariasSemanas = DateTime.Now.Date.AddDays(-14);

            var dailySales = _context.boleta_cabecera
                    .Where(m => m.fecha >= criterioVentasDiariasSemanas
                            &&  m.pedido_cabecera.mesa.restaurante_id_fk == restaurante.restaurante_id)
                    .GroupBy(m => m.fecha)
                    .Select(n => new
                    {
                        dia = n.Key,
                        promedioVenta = n.Average(m => m.total)
                    }).ToList();

            var ventasDiariasList = new List<VentasDiarias>();

            foreach (var dailySale in dailySales)
            {
                var newDailySale = new VentasDiarias(dailySale.dia, dailySale.promedioVenta);
                ventasDiariasList.Add(newDailySale);
            }

            var criterioVentasSemanales = DateTime.Now.Date.AddDays(-7);
            var criterioVentasMensuales = DateTime.Now.Date.AddDays(-30);
            var ventasDiaSemanaMes = new VentasDiaSemanaMes
            {
                VentasDiarias = _context.boleta_cabecera
                .Where(m => m.fecha >= DateTime.Today
                            && m.pedido_cabecera.mesa.restaurante_id_fk == restaurante.restaurante_id)
                .GroupBy(m => m.fecha)
                .Select(n => new
                {
                    promedioVenta = n.Sum(m => m.total)
                }).First().promedioVenta,

                VentasSemanales = _context.boleta_cabecera
                    .Where(m => m.fecha >= criterioVentasSemanales
                                && m.pedido_cabecera.mesa.restaurante_id_fk == restaurante.restaurante_id)
                    .Select(n => n.total).Sum(),

                VentasMensuales = _context.boleta_cabecera
                    .Where(m => m.fecha >= criterioVentasMensuales
                                && m.pedido_cabecera.mesa.restaurante_id_fk == restaurante.restaurante_id)
                    .Select(n => n.total).Sum()

            };


            var dashboardViewModel = new DashboardViewModel(ventasDiariasList, ventasDiaSemanaMes);
            //ViewBag.ventasDiariasList = ventasDiariasList;
            return View(dashboardViewModel);
        }

        public Task NotificacionGenerarBoleta(string token)
        {
            var client = new FCMClient(FirebaseToken);
            var message = new Message()
            {
                To = token,
                Notification = new AndroidNotification()
                {
                    Title = "Su boleta está lista",
                    Body = "El mozo le estará trayendo su boleta en unos momentos o puede acercarse a caja"
                }

            };
            return client.SendMessageAsync(message);
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