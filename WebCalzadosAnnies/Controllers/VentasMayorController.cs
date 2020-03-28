using Annies.Common;
using Annies.Entities;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace WebCalzadosAnnies.Controllers
{
    public class VentasMayorController : Controller
    {
        // GET: Ventas
        public ActionResult Index()
        {
            return View();
        }

        // cla cla estuvo aqui
        public JsonResult GetVentasMayor(Annies.Entities.VentasMayor obj)
        {
            try
            {
                var ctx = HttpContext.GetOwinContext();
                var tipoUsuario = ctx.Authentication.User.Claims.FirstOrDefault().Value;

                string draw = Request.Form.GetValues("draw")[0];
                int inicio = Convert.ToInt32(Request.Form.GetValues("start").FirstOrDefault());
                int fin = Convert.ToInt32(Request.Form.GetValues("length").FirstOrDefault());

                obj.Auditoria = new Auditoria
                {
                    TipoUsuario = tipoUsuario
                };
                obj.Operacion = new Operacion
                {
                    Inicio = (inicio / fin),
                    Fin = fin
                };

                var bussingLogic = new Annies.BusinessLogic.VentasMayor();
                var response = bussingLogic.GetVentasMayor(obj);

                var Datos = response.Data;
                int totalRecords = Datos.Any() ? Datos.FirstOrDefault().Operacion.TotalRows : 0;
                int recFilter = totalRecords;

                var result = (new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = Datos
                });

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(Annies.Common.ConfigurationUtilities.ErrorCatchDataTable(ex));
            }

        }

        public JsonResult InsertUpdateVentasMayor(Annies.Entities.VentasMayor obj)
        {
            var bussingLogic = new Annies.BusinessLogic.VentasMayor();
            obj.Auditoria = new Auditoria
            {
                UsuarioCreacion = User.Identity.Name
            };

            var tallasxml = obj.Tallas_Venta.Select(i => new XElement("TallaVenta",
                       new XElement("Talla", i.Talla),
                       new XElement("Cod_prod", i.Cod_Prod),
                       new XElement("Cantidad", i.Cantidad),
                       new XElement("Precio", i.Precio_Prod_Mayor)));
            obj.Talla_Ventas = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("TallasVenta", tallasxml));

            var response = bussingLogic.InsertUpdateVentas(obj);

            return Json(response);
        }

        public JsonResult TallasVentaMayor(string Cod_Venta)
        {
            var bussingLogic = new Annies.BusinessLogic.VentasMayor();
            var response = bussingLogic.TallasVentaMayor(Cod_Venta);

            return Json(response);

        }

        public JsonResult DeleteVentasMayor(Annies.Entities.VentasMayor obj)
        {
            var bussingLogic = new Annies.BusinessLogic.VentasMayor();
            obj.Auditoria = new Auditoria
            {
                UsuarioModificacion = User.Identity.Name
            };
            var response = bussingLogic.DeleteVentasMayor(obj);
            return Json(response);

        }

        public JsonResult GetAllVentasMayor(Annies.Entities.VentasMayor obj)
        {
            try
            {
                Session["ReporteVentaMayor"] = null;
                var bussingLogic = new Annies.BusinessLogic.VentasMayor();
                var response = bussingLogic.GetAllVentasMayor(obj);
                Session["ReporteVentaMayor"] = response.Data.ToList();
                return Json(response);
            }
            catch (Exception ex)
            {
                var result = new Annies.Common.Response<List<Annies.Entities.VentasMayor>>(ex);
                return Json(result);
            }

        }

        public void GenerarExcel()
        {
            var NombreExcel = "VentaMayor - Sistemas de Ventas ";

            // Recuperamos la data  de las consulta DB
            var data = (List<Annies.Entities.VentasMayor>)Session["ReporteVentaMayor"];

            // Creación del libro excel xlsx.
            var wb = new XSSFWorkbook();

            // Creación del la hoja y se especifica un nombre
            var fileName = WorkbookUtil.CreateSafeSheetName("VentasMayor");
            ISheet sheet = wb.CreateSheet(fileName);

            // Contadores para filas y columnas.
            int rownum = 0;
            int cellnum = 0;

            // Creacion del estilo de la letra para las cabeceras.
            var fontCab = wb.CreateFont();
            fontCab.FontHeightInPoints = 10;
            fontCab.FontName = "Calibri";
            fontCab.Boldweight = (short)FontBoldWeight.Bold;
            fontCab.Color = HSSFColor.White.Index;

            // Creacion del color del estilo.
            var colorCab = new XSSFColor(new byte[] { 7, 105, 173 });

            // Se crea el estilo y se agrega el font al estilo
            var styleCab = (XSSFCellStyle)wb.CreateCellStyle();
            styleCab.SetFont(fontCab);
            styleCab.FillForegroundXSSFColor = colorCab;
            styleCab.FillPattern = FillPattern.SolidForeground;


            string[] Cabezeras = {
                    "Código Venta","Marca","Precio Producto","Precio Venta","Descuento","Precio Total","Fecha"
             };

            // Se crea la primera fila para las cabceras.
            IRow row = sheet.CreateRow(rownum++);
            ICell cell;

            foreach (var item in Cabezeras)
            {
                // Se crea celdas y se agrega las cabeceras
                cell = row.CreateCell(cellnum++);
                cell.SetCellValue(item);
                cell.CellStyle = styleCab;

                sheet.AutoSizeColumn(cellnum);
            }

            // Creacion del estilo de la letra para la data.
            var fontBody = wb.CreateFont();
            fontBody.FontHeightInPoints = 10;
            fontBody.FontName = "Arial";

            var styleBody = (XSSFCellStyle)wb.CreateCellStyle();
            styleBody.SetFont(fontBody);


            // Impresión de la data
            foreach (var item in data)
            {
                cellnum = 0;
                row = sheet.CreateRow(rownum++);

                sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.Cod_Venta.ToString(), styleBody); sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.Producto.Marca_Prod.ToString(), styleBody); sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.Precio_Prod.ToString(), styleBody); sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.Precio_Venta.ToString(), styleBody); sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.Descuento_Venta.ToString(), styleBody); sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.Precio_Final.ToString(), styleBody); sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.Fecha.ToString().Substring(6, 2) + "/" + item.Fecha.ToString().Substring(4, 2) + "/" + item.Fecha.ToString().Substring(0, 4), styleBody); sheet.AutoSizeColumn(cellnum);

            }

            var nameFile = NombreExcel + DateTime.Now.ToString("dd_MM_yyyy HH:mm:ss") + ".xlsx";
            Response.AddHeader("content-disposition", "attachment; filename=" + nameFile);
            Response.ContentType = "application/octet-stream";
            Stream outStream = Response.OutputStream;
            wb.Write(outStream);
            outStream.Close();
            Response.End();
        }

        public void AddValue(IRow row, int cellnum, string value, ICellStyle styleBody)
        {
            ICell cell;
            cell = row.CreateCell(cellnum);
            cell.SetCellValue(value);
            cell.CellStyle = styleBody;

        }

    }
}