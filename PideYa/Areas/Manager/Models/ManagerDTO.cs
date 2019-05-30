using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PideYa.Areas.Manager.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel(List<VentasDiarias> ventasDiariasList, VentasDiaSemanaMes ventasDiaSemanaMes)
        {
            VentasDiariasList = ventasDiariasList;
            VentasDiaSemanaMes = ventasDiaSemanaMes;
        }

        public List<VentasDiarias> VentasDiariasList { get; set; }
        public VentasDiaSemanaMes VentasDiaSemanaMes { get; set; }
    }

    public class VentasDiarias
    {
        public VentasDiarias(DateTime dia, decimal promedioVenta)
        {
            Dia = dia;
            PromedioVenta = promedioVenta;
        }

        public DateTime Dia { get; set; }
        public decimal PromedioVenta { get; set; }
    }

    public class VentasDiaSemanaMes
    {
        public VentasDiaSemanaMes(){}

        public VentasDiaSemanaMes(decimal ventasDiarias, decimal ventasSemanales, decimal ventasMensuales)
        {
            VentasDiarias = ventasDiarias;
            VentasSemanales = ventasSemanales;
            VentasMensuales = ventasMensuales;
        }

        public decimal VentasDiarias { get; set; }
        public decimal VentasSemanales { get; set; }
        public decimal VentasMensuales { get; set; }
    }
}