using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annies.Entities
{
    public class Ventas
    {
        public int? Cod_Venta { get; set; }
        public string Talla_Venta { get; set; }
        public int Cant_Venta { get; set; }
        public int? Fecha { get; set; }
        public double Descuento_Venta { get; set; }
        public double Precio_Venta { get; set; }
        public double Precio_Final { get; set; }
        public Operacion Operacion { get; set; }
        public Auditoria Auditoria { get; set; }
        public Producto Producto { get; set; }
    }
}
