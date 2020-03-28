using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Annies.Common;
using Annies.Entities;
using Dapper;

namespace Annies.DataAccess
{
    public class Producto
    {
        public IEnumerable<Entities.Producto> GetProducto(Entities.Producto obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Tipo_Opc", obj.Operacion.TipoOperacion);
                parm.Add("@Opc", obj.Operacion.Opcion);
                parm.Add("@Cod_Prod", obj.Cod_Prod);
                parm.Add("@Stock_Prod", obj.Stock_Prod);
                parm.Add("@Marca_Prod", obj.Marca_Prod);
                parm.Add("@Estado", obj.Estado_Prod );
                parm.Add("@NumPagina", obj.Operacion.Inicio);
                parm.Add("@TamPagina", obj.Operacion.Fin);

                var result = connection.Query(
                     sql: "SP_SCRUM_VENTAS",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Entities.Producto
                     {
                         Cod_Prod = n.Single(d => d.Key.Equals("Cod_Prod")).Value.Parse<int>(),
                         Marca_Prod = n.Single(d => d.Key.Equals("Marca_Prod")).Value.Parse<string>(),
                         Talla_Prod = n.Single(d => d.Key.Equals("Talla_Prod")).Value.Parse<string>(),
                         Precio_Prod = n.Single(d => d.Key.Equals("Precio_Prod")).Value.Parse<double>(),
                         Talla_Vendida_Prod = n.Single(d => d.Key.Equals("Talla_Vendida_Prod")).Value.Parse<string>(),
                         Stock_Prod = n.Single(d => d.Key.Equals("Stock_Prod")).Value.Parse<int>(),
                         Tipo_Prod = n.Single(d => d.Key.Equals("Tipo_Prod")).Value.Parse<int>(),
                         Codigo_Al = n.Single(d => d.Key.Equals("Cod_Almacen")).Value.Parse<string>(),
                         IdProducto = n.Single(d => d.Key.Equals("IdProducto")).Value.Parse<int>(),
                         Estado_Prod = n.Single(d => d.Key.Equals("Estado_Prod")).Value.Parse<int>(),
                         Auditoria = new Auditoria
                         {
                             TipoUsuario = obj.Auditoria.TipoUsuario,
                         },
                         Operacion = new Operacion
                         {
                             TotalRows = n.Single(d => d.Key.Equals("TotalRows")).Value.Parse<int>(),
                         },
                        FechaDesde =  n.Single(d => d.Key.Equals("Fecha")).Value.Parse<int>(),
                     });

                return result;
            }
        }

        public int InsertUpdateProducto(Entities.Producto obj)
        {

            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Tipo_Opc", obj.Operacion.TipoOperacion);
                parm.Add("@Opc", obj.Operacion.Opcion);
                parm.Add("@Cod_Prod", obj.Cod_Prod);
                parm.Add("@Cod_Almacen", obj.Codigo_Al);
                parm.Add("@Marca_Prod", obj.Marca_Prod);
                parm.Add("@Precio_Prod", obj.Precio_Prod);
                parm.Add("@Talla_Prod", obj.Talla_Prod);
                parm.Add("@Stock_Prod", obj.Stock_Prod);
                parm.Add("@Tipo_Prod", obj.Tipo_Prod);
                parm.Add("@Usuario", obj.Auditoria.UsuarioCreacion);
                parm.Add("@Estado", obj.Estado_Prod);
                var result = connection.Execute(
                    sql: "SP_SCRUM_VENTAS",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public int DeleteProducto(Entities.Producto obj)
        {

            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Tipo_Opc", obj.Operacion.TipoOperacion);
                parm.Add("@Opc", obj.Operacion.Opcion);
                parm.Add("@Cod_Prod", obj.Cod_Prod);

                var result = connection.Execute(
                    sql: "SP_SCRUM_VENTAS",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public IEnumerable<Entities.Producto> GetAllProductos(Entities.Producto obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cod_Prod", obj.Cod_Prod);
                parm.Add("@Marca_Prod", obj.Marca_Prod);
                parm.Add("@Estado", obj.Estado_Prod);
                parm.Add("@FechaDesde", obj.FechaDesde);
                parm.Add("@FechaHasta", obj.FechaHasta);

                var result = connection.Query(
                     sql: "SP_FILTRAR_PRODUCTO",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Entities.Producto
                     {
                         Cod_Prod = n.Single(d => d.Key.Equals("Cod_Prod")).Value.Parse<int>(),
                         Stock_Prod = n.Single(d => d.Key.Equals("Stock_Prod")).Value.Parse<int>(),
                         Codigo_Al = n.Single(d => d.Key.Equals("Cod_Almacen")).Value.Parse<string>(),
                         Marca_Prod = n.Single(d => d.Key.Equals("Marca_Prod")).Value.Parse<string>(),
                         Talla_Prod = n.Single(d => d.Key.Equals("Talla_Prod")).Value.Parse<string>(),
                         Talla_Vendida_Prod = n.Single(d => d.Key.Equals("Talla_Vendida_Prod")).Value.Parse<string>(),
                         Precio_Prod = n.Single(d => d.Key.Equals("Precio_Prod")).Value.Parse<double>(),
                         Estado_Prod = n.Single(d => d.Key.Equals("Estado_Prod")).Value.Parse<int>(),
                         FechaDesde = n.Single(d => d.Key.Equals("Fecha")).Value.Parse<int>()
                     });

                return result;
            }
        }




    }
}
