using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Annies.Entities
{
    public class VentasMayor
    {
        public int? Cod_Venta { get; set; }
        public double Precio_Prod { get; set; }
        public double Descuento_Venta { get; set; }
        public double Precio_Venta { get; set; }
        public double Precio_Final { get; set; }
        public int? Fecha { get; set; }
        public int? FechaDesde { get; set; }
        public int? FechaHasta { get; set; }
        public Producto Producto { get; set; }
        public Operacion Operacion { get; set; }
        public Auditoria Auditoria { get; set; }
        public XDocument Talla_Ventas { get; set; }
        public List<TallasMayor> Tallas_Venta { get; set; }
    }
}
