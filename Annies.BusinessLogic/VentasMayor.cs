using Annies.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annies.BusinessLogic
{
    public class VentasMayor
    {
        private DataAccess.VentasMayor repository;

        public VentasMayor()
        {
            repository = new DataAccess.VentasMayor();
        }

        public Response<IEnumerable<Entities.VentasMayor>> GetVentasMayor(Entities.VentasMayor obj)
        {
            try
            {                
                var result = repository.GetVentasMayor(obj);
                return new Response<IEnumerable<Entities.VentasMayor>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response<int> InsertUpdateVentas(Entities.VentasMayor obj)
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

        public Response<IEnumerable<Entities.TallasMayor>> TallasVentaMayor(string Cod_Venta)
        {
            try
            {
                var result = repository.TallasVentaMayor(Cod_Venta);
                return new Response<IEnumerable<Entities.TallasMayor>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response<int> DeleteVentasMayor(Entities.VentasMayor obj)
        {
            try
            {
                var result = repository.DeleteVentasMayor(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<IEnumerable<Entities.VentasMayor>> GetAllVentasMayor(Entities.VentasMayor obj)
        {
            try
            {
                var result = repository.GetAllVentasMayor(obj);
                return new Response<IEnumerable<Entities.VentasMayor>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
