using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PideYa.Models;

namespace PideYa.Areas.Employee.Models
{
    public class PedidoViewModel
    {
        public PedidoViewModel()
        {
            PedidoCabecera = new pedido_cabecera();
            PedidoDetalleList = new List<pedido_detalle>();
            PedidoDetalle = new pedido_detalle();
            Restaurante = new restaurante();
        }

        public pedido_cabecera PedidoCabecera { get; set; }
        public List<pedido_detalle> PedidoDetalleList { get; set; }
        public pedido_detalle PedidoDetalle { get; set; }
        public restaurante Restaurante { get; set; }
    }

    public class CategoriasViewModel
    {
        public CategoriasViewModel()
        {
            Platos = new List<plato>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<plato> Platos { get; set; }
    }

}