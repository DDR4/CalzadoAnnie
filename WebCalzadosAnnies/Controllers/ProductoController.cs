using Annies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace WebCalzadosAnnies.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetProducto(Annies.Entities.Producto obj)
        {
            var ctx = HttpContext.GetOwinContext();
            var tipoUsuario = ctx.Authentication.User.Claims.FirstOrDefault().Value;
            obj.Auditoria = new Auditoria
            {
                TipoUsuario = tipoUsuario
            };
            var bussingLogic = new Annies.BusinessLogic.Producto();
            var response = bussingLogic.GetProducto(obj);

            if (response.InternalStatus == Annies.Common.EnumTypes.InternalStatus.Success)
            {
                Session["listado"] = response.Data.ToList();
            }


            return Json(response);
        }

        public JsonResult InsertUpdateProducto(Annies.Entities.Producto obj)
        {
            var bussingLogic = new Annies.BusinessLogic.Producto();

            obj.Auditoria = new Annies.Entities.Auditoria
            {
                UsuarioCreacion = User.Identity.Name,
                UsuarioModificacion = User.Identity.Name
            };
            obj.Operacion = new Operacion
            {
                TipoOperacion = "P",
                Opcion = obj.IdProducto == null ? "I" : "U"
            };
            if (obj.Operacion.Opcion == "I")
            {
                if (Session["listado"] != null)
                {
                    var data = (List<Annies.Entities.Producto>)Session["listado"];
                    var val = data.Where(x => x.Cod_Prod == obj.Cod_Prod);
                    if (val.Any())
                    {
                        var res = new Annies.Common.Response<int>("El codigo ya existe");
                        return Json(res);
                    }
                }
            }
            var response = bussingLogic.InsertUpdateProducto(obj);

            return Json(response);
        }


        public JsonResult DeleteProducto(Annies.Entities.Producto obj)
        {
            var bussingLogic = new Annies.BusinessLogic.Producto();
            var response = bussingLogic.DeleteProducto(obj);

            return Json(response);

        }




    }
}