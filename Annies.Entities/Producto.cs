using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annies.Entities
{
    public class Producto
    {
        public int? IdProducto { get; set; }
        public int? Cod_Prod { get; set; }
        public string Marca_Prod { get; set; }
        public string Talla_Prod { get; set; }
        public double Precio_Prod { get; set; }
        public string Talla_Vendida_Prod { get; set; }
        public int Stock_Prod { get; set; }
        public int Tipo_Prod { get; set; }
        public string Codigo_Al { get; set; }
        public int Estado_Prod { get; set; }
        public Operacion Operacion { get; set; }
        public Auditoria Auditoria { get; set; }

    }
}
