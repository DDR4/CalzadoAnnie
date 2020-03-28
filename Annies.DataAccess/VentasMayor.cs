using Annies.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annies.DataAccess
{
    public class VentasMayor
    {
        public IEnumerable<Entities.VentasMayor> GetVentasMayor(Entities.VentasMayor obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@FECHA", obj.Fecha);
                parm.Add("@NumPagina", obj.Operacion.Inicio);
                parm.Add("@TamPagina", obj.Operacion.Fin);

                var result = connection.Query(
                     sql: "SP_BUSCAR_VENTA_MAYOR",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Entities.VentasMayor
                     {
                         Cod_Venta = n.Single(d => d.Key.Equals("Cod_Venta")).Value.Parse<int>(),
                         Producto = new Annies.Entities.Producto
                         {
                             Marca_Prod = n.Single(d => d.Key.Equals("Marca_Prod")).Value.Parse<string>(),
                         },
                         Precio_Prod = n.Single(d => d.Key.Equals("PrecioProducto")).Value.Parse<int>(),
                         Precio_Venta = n.Single(d => d.Key.Equals("PrecioVenta")).Value.Parse<int>(),
                         Descuento_Venta = n.Single(d => d.Key.Equals("Descuento")).Value.Parse<int>(),
                         Precio_Final = n.Single(d => d.Key.Equals("PrecioTotal")).Value.Parse<int>(),
                         Fecha = n.Single(d => d.Key.Equals("Fecha")).Value.Parse<int>(),
                         Auditoria = new Entities.Auditoria
                         {
                             TipoUsuario = obj.Auditoria.TipoUsuario
                         },
                         Operacion = new Entities.Operacion
                         {
                             TotalRows = n.Single(d => d.Key.Equals("TotalRows")).Value.Parse<int>()
                         }
                     });

                return result;
            }
        }
        public int InsertUpdateVentas(Entities.VentasMayor obj)
        {

            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cod_Venta", obj.Cod_Venta);
                parm.Add("@PrecioProducto", obj.Precio_Prod);
                parm.Add("@PrecioVenta", obj.Precio_Venta);
                parm.Add("@Descuento", obj.Descuento_Venta);
                parm.Add("@PrecioTotal", obj.Precio_Final);
                parm.Add("@Usuario", obj.Auditoria.UsuarioCreacion);
                parm.Add("@Talla_Ventas", obj.Talla_Ventas);

                var result = connection.Execute(
                    sql: "SP_INSERTAR_VENTA_MAYOR",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public IEnumerable<Entities.TallasMayor> TallasVentaMayor(string Cod_Venta)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cod_Venta", Cod_Venta);

                var result = connection.Query(
                     sql: "SP_TALLAS_VENTA_MAYOR",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Entities.TallasMayor
                     {
                         Talla = n.Single(d => d.Key.Equals("Talla")).Value.Parse<int>(),
                         Cod_Prod = n.Single(d => d.Key.Equals("Cod_Prod")).Value.Parse<int>(),
                         Producto = new Annies.Entities.Producto
                         {
                             Marca_Prod = n.Single(d => d.Key.Equals("Marca_Prod")).Value.Parse<string>(),
                         },
                         Cantidad = n.Single(d => d.Key.Equals("Cantidad")).Value.Parse<int>(),
                         Precio_Prod_Mayor = n.Single(d => d.Key.Equals("Precio")).Value.Parse<int>()
                     });

                return result;
            }
        }

        public int DeleteVentasMayor(Entities.VentasMayor obj)
        {

            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cod_Venta", obj.Cod_Venta);
                parm.Add("@Usuario", obj.Auditoria.UsuarioModificacion);

                var result = connection.Execute(
                    sql: "SP_ELIMINAR_VENTA_MAYOR",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public IEnumerable<Entities.VentasMayor> GetAllVentasMayor(Entities.VentasMayor obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@FechaDesde", obj.FechaDesde);
                parm.Add("@FechaHasta", obj.FechaHasta);

                var result = connection.Query(
                     sql: "SP_FILTRAR_VENTA_MAYOR",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                          .Select(n => new Entities.VentasMayor
                          {
                              Cod_Venta = n.Single(d => d.Key.Equals("Cod_Venta")).Value.Parse<int>(),
                              Producto = new Annies.Entities.Producto
                              {
                                Marca_Prod = n.Single(d => d.Key.Equals("Marca_Prod")).Value.Parse<string>(),
                              },
                              Precio_Prod = n.Single(d => d.Key.Equals("PrecioProducto")).Value.Parse<int>(),
                              Precio_Venta = n.Single(d => d.Key.Equals("PrecioVenta")).Value.Parse<int>(),
                              Descuento_Venta = n.Single(d => d.Key.Equals("Descuento")).Value.Parse<int>(),
                              Precio_Final = n.Single(d => d.Key.Equals("PrecioTotal")).Value.Parse<int>(),
                              Fecha = n.Single(d => d.Key.Equals("Fecha")).Value.Parse<int>()
                          });

                return result;
            }
        }
    }
}
