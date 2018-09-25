using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Annies.Common;
using Annies.Entities;

namespace Annies.BusinessLogic
{
    public class Producto
    {
        private DataAccess.Producto repository;

        public Producto()
        {
            repository = new DataAccess.Producto();
        }

        public Response<IEnumerable<Entities.Producto>> GetProducto(Entities.Producto obj  )
        {
            try
            {
                obj.Operacion = new Operacion
                {
                    TipoOperacion = "P",
                    Opcion = "S",
                };

                var result = repository.GetProducto(obj);
                return new Response<IEnumerable<Entities.Producto>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Entities.Producto>>(ex);
            }
        }

        public Response<int> InsertUpdateProducto(Entities.Producto obj)
        {
            try
            {
        
                var result = repository.InsertUpdateProducto(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<int> DeleteProducto(Entities.Producto obj)
        {
            try
            {
                obj.Operacion = new Operacion
                {
                    TipoOperacion = "P",
                    Opcion = "D"
                };

                var result = repository.DeleteProducto(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }
    }
}
