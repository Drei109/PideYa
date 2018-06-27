using System.Collections.Generic;
using PideYa.Models;

namespace PideYa.Areas.User.Models
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
        public restaurante Restaurante{ get; set; }
    }

    public class EstadoPedidoCabecera
    {
        public static readonly string Pidiendo = "Pidiendo";
        public static readonly string Enviado = "Enviado";
        public static readonly string Preparando = "Preparando";
        public static readonly string Terminado = "Terminado";
        public static readonly string Cancelado = "Cancelado";
        public static readonly string Generado = "Generado";
    }

    public class EstadoBoletaCabecera
    {
        public static readonly string Generado = "Generado";
        public static readonly string Pagado = "Pagado";
    }
}