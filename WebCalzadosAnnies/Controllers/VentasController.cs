using Annies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCalzadosAnnies.Controllers
{
    public class VentasController : Controller
    {
        // GET: Ventas
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetVentas(Annies.Entities.Ventas obj)
        {
            var ctx = HttpContext.GetOwinContext();
            var tipoUsuario = ctx.Authentication.User.Claims.FirstOrDefault().Value;
            obj.Auditoria = new Auditoria
            {
                TipoUsuario = tipoUsuario
            };
            var bussingLogic = new Annies.BusinessLogic.Ventas();
            var response = bussingLogic.GetVentas(obj);

            return Json(response);
        }

        public JsonResult InsertUpdateVentas(Annies.Entities.Ventas obj)
        {
            var bussingLogic = new Annies.BusinessLogic.Ventas();
            obj.Auditoria = new Auditoria
            {
                UsuarioCreacion = User.Identity.Name,
                UsuarioModificacion = User.Identity.Name
            };
            obj.Operacion = new Operacion
            {
                TipoOperacion = "V",
                Opcion = obj.Cod_Venta == null ? "I" : "U"
            };
            var response = bussingLogic.InsertUpdateVentas(obj);

            return Json(response);
        }
        public JsonResult DeleteVentas(Annies.Entities.Ventas obj)
        {
            var bussingLogic = new Annies.BusinessLogic.Ventas();
            obj.Operacion = new Operacion
            {
                TipoOperacion = "V",
                Opcion = "D"
            };
            var response = bussingLogic.DeleteVentas(obj);

            return Json(response);

        }



        public JsonResult GetProducto(Annies.Entities.Producto obj)
        {
            var bussingLogic = new Annies.BusinessLogic.Producto();
            obj.Stock_Prod = 1;
            obj.Estado_Prod = 1;
            var ctx = HttpContext.GetOwinContext();
            var tipoUsuario = ctx.Authentication.User.Claims.FirstOrDefault().Value;
            obj.Auditoria = new Auditoria
            {
                TipoUsuario = tipoUsuario
            };
            var response = bussingLogic.GetProducto(obj);
            return Json(response);
        }

    }
}