using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Annies.Common;
using Annies.Entities;

namespace Annies.BusinessLogic
{
    public class Ventas
    {
        private DataAccess.Ventas repository;

        public Ventas()
        {
            repository = new DataAccess.Ventas();
        }

        public Response<IEnumerable<Entities.Ventas>> GetVentas(Entities.Ventas obj)
        {
            try
            {             
                obj.Producto.Cod_Prod = (obj.Producto.Cod_Prod == 0) ? null : obj.Producto.Cod_Prod;
                obj.Fecha = (obj.Fecha == 0) ? null : obj.Fecha;
                var result = repository.GetVentas(obj);                            
                return new Response<IEnumerable<Entities.Ventas>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response<int> InsertUpdateVentas(Entities.Ventas obj)
        {
            try
            {
                var result = repository.InsertUpdateVentas(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<int> DeleteVentas(Entities.Ventas obj)
        {
            try
            {
                var result = repository.DeleteVentas(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<IEnumerable<Entities.Ventas>> GetAllVentas(Entities.Ventas obj)
        {
            try
            {
                var result = repository.GetAllVentas(obj);
                return new Response<IEnumerable<Entities.Ventas>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
