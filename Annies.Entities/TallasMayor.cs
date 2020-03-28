using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annies.Entities
{
    public class TallasMayor
    {
        public int Cod_Prod { get; set; }
        public int Talla { get; set; }
        public int Cantidad { get; set; }
        public double Precio_Prod_Mayor { get; set; }
        public Producto Producto { get; set; }

    }
}
