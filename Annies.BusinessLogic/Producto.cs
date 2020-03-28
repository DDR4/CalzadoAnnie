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

        public Response<IEnumerable<Entities.Producto>> GetProducto(Entities.Producto obj)
        {
            try
            {
                var result = repository.GetProducto(obj);
                return new Response<IEnumerable<Entities.Producto>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                var result = repository.DeleteProducto(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<IEnumerable<Entities.Producto>> GetAllProductos(Entities.Producto obj)
        {
            try
            {
                var result = repository.GetAllProductos(obj);
                return new Response<IEnumerable<Entities.Producto>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response<IEnumerable<Entities.Tallas>> TallasProducto(string cod_prod)
        {
            try
            {
                var result = repository.TallasProducto(cod_prod);
                return new Response<IEnumerable<Entities.Tallas>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response<IEnumerable<Entities.Tallas>> TallasVenta(string cod_prod)
        {
            try
            {
                var result = repository.TallasVenta(cod_prod);
                return new Response<IEnumerable<Entities.Tallas>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response<int> CrearOferta(int cod_prod, string flag,string usuario)
        {
            try
            {
                var result = repository.CrearOferta(cod_prod, flag, usuario);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }
 
    }
}
