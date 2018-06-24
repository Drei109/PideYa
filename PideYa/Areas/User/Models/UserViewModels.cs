using System.Collections.Generic;
using PideYa.Models;

namespace PideYa.Areas.User.Models
{
    public class PedidoViewModel
    {
        public pedido_cabecera PedidoCabecera { get; set; }
        public List<pedido_detalle> PedidoDetalle { get; set; }
    }
}