﻿var Ventas = (function ($, win, doc) {

    var $cboTipoBusqueda = $('#cboTipoBusqueda');

    var $tipoCodigo = $('#tipoCodigo');
    var $tipoFecha = $('#tipoFecha');
    var $tipoMarca = $('#tipoMarca');
    var $tipoTalla = $('#tipoTalla');

    var $txtCodigo = $('#txtCodigo');
    var $txtFecha = $('#txtFecha');
    var $txtMarca = $('#txtMarca');
    var $txtTalla = $('#txtTalla');
    var $tblListadoVentas = $('#tblListadoVentas');
    var $btnBuscar = $('#btnBuscar');
    var $btnNuevaVenta = $('#btnNuevaVenta');
    // variables del modal 

    var $formModal = $('#formModal');
    var $modalVentas = $('#modalVentas');
    var $txtModalCatVenta = $('#txtModalCatVenta');
    var $txtModalFecha = $('#txtModalFecha');
    var $txtModalCodigo = $('#txtModalCodigo');
    var $cboModalTalla = $('#cboModalTalla');
    var $txtModalMarca = $('#txtModalMarca');
    var $txtModalPrecioVenta = $('#txtModalPrecioVenta');
    var $txtModalPrecioProducto = $('#txtModalPrecioProducto');
    var $txtModalDescuento = $('#txtModalDescuento');
    var $txtModalTotal = $('#txtModalTotal');

    var $btnProducto = $('#btnProducto');
    var $btnSaveVenta = $('#btnSaveVenta');

    //Modal Producto 
    var $modalProducto = $('#modalProducto');

    var $tblListadoProductos = $('#tblListadoProductos');
    var $btnSaveProducto = $('#btnSaveProducto');

    var $cboTipoBusquedaModal = $('#cboTipoBusquedaModal');
    var $txtCodigoModal = $('#txtCodigoModal');
    var $txtMarcaModal = $('#txtMarcaModal');
    var $btnBuscarModal = $('#btnBuscarModal');

    var $btnGenerarExcel = $('#btnGenerarExcel');
    var $txtFechaDesde = $('#txtFechaDesde');
    var $txtFechaHasta = $('#txtFechaHasta');

    var Message = {
        ObtenerTipoBusqueda: "Obteniendo los tipos de busqueda, Por favor espere...",
        GuardarSuccess: "Los datos se guardaron satisfactoriamente",
        EliminarSuccess: "El registro se elimino satisfactoriamente",
    }

    // Constructor
    $(Initialize);

    // Implementacion del constructor
    function Initialize() {
        $cboTipoBusqueda.change($cboTipoBusqueda_change);
        $cboTipoBusquedaModal.change($cboTipoBusquedaModal_change);
        GetVentas();
        app.Event.Datepicker($txtFecha);
        app.Event.SetDateDatepicket($txtModalFecha);

        $btnBuscar.click($btnBuscar_click);
        $btnNuevaVenta.click($btnNuevaVenta_click);
        $btnProducto.click($btnProducto_click);
        $btnSaveProducto.click($btnSaveProducto_click);
        $btnSaveVenta.click($btnSaveVenta_click);
        $btnBuscarModal.click($btnBuscarModal_click);
        $txtModalPrecioVenta.blur($txtModalPrecioVenta_keypress);

        //$cboModalTalla.select2();
        $cboModalTalla.change($cboModalTalla_change);

        app.Event.ForceDecimalOnly($txtModalPrecioVenta);

        $btnGenerarExcel.click($btnGenerarExcel_click);
        $txtFechaDesde.change(ValidarGenerarExcel);
        $txtFechaHasta.change(ValidarGenerarExcel);
        var date = new Date();
        var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
        $txtFechaHasta.datepicker({
            endDate: "today",
            todayHighlight: true
        }).datepicker('setDate', today);
        $txtFechaDesde.datepicker({
            endDate: "today",
            todayHighlight: true
        });
    }

    function $cboTipoBusqueda_change() {
        var codSelec = $(this).val();
        $tipoCodigo.hide();
        $tipoFecha.hide();
        $tipoMarca.hide();
        $tipoTalla.hide();

        if (codSelec == "1") {
            $tipoCodigo.show();
        } else if (codSelec == "2") {
            $tipoFecha.show();
        } else if (codSelec == "3") {
            $tipoMarca.show();
        } else if (codSelec == "4") {
            $tipoTalla.show();
        } else if (codSelec == "5") {
            $txtCodigo.val(null); $txtFecha.val(null);
            $txtMarca.val(null); $txtTalla.val(null);
        }


    }

    function $cboTipoBusquedaModal_change() {

        var codSelec = $(this).val();
        //$('#form1')[0].reset();
        $('#tipoCodigoModal').hide();
        $('#tipoMarcaModal').hide();

        $txtCodigoModal.val("");
        $txtMarcaModal.val("");

        if (codSelec == "1") {
            $('#tipoCodigoModal').show();
        }
        else if (codSelec == "2") {
            $('#tipoMarcaModal').show();
        }
    }

    function GetVentas() {

        var url = "Ventas/GetVentas";

        var parms = {
            Fecha: app.ConvertDatetimeToInt($txtFecha.val(), '/'),
            Talla_Venta: $txtTalla.val(),
            Producto: {
                Cod_Prod: $txtCodigo.val(),
                Marca_Prod: $txtMarca.val(),
            }

        }
        
        var columns = [
            { data: "Cod_Venta" },
            { data: "Producto.Cod_Prod" },
            { data: "Producto.Marca_Prod" },
            { data: "Cant_Venta" },
            { data: "Fecha" },
            { data: "Precio_Final" },
            { data: "Talla_Venta" },
            { data: "Auditoria.TipoUsuario" }
        ];
        var columnDefs = [
            {
                "targets": [4],
                'render': function (data, type, full, meta) {
                    return '' + app.ConvertIntToDatetimeDT(data) + '';
                }
            },
            {
                "targets": [5],
                "className": "text-right",
                'render': function (data, type, full, meta) {
                    return '' + app.FormatNumber(data) + '';
                }
            },
            {
                "targets": [7],
                "visible": true,
                "orderable": false,
                "className": "text-center",
                'render': function (data, type, full, meta) {
                    if (data == 1) {
                        return "<center>" +
                            '<a class="btn btn-default btn-xs"  title="Eliminar" href="javascript:Ventas.EliminarVenta(' + meta.row + ')"><i class="fa fa-trash" aria-hidden="true"></i></a>' +
                            "</center> ";
                    } else {
                        return "";
                    }
                }
            }
        ];

        var buttons = [{
            extend: 'excelHtml5',
            className: 'btn btn-success btn-sm ',
            customizeData: function (data) {
                for (var i = 0; i < data.body.length; i++) {
                    for (var j = 0; j < data.body[i].length; j++) {
                        data.body[i][j] = '\u200C' + data.body[i][j];
                    }
                }
            },

        }];

        var filters = {
            pageLength: app.Defaults.TablasPageLength
        }


        app.FillDataTableAjaxPaging($tblListadoVentas, url, parms, columns, columnDefs, filters, null, null);
    

        //var method = "POST";
        //var url = "Ventas/GetVentas";
        //var data = obj;
        //var fnDoneCallback = function (data) {
        //    FillTable(data, 1);
        //};
        //app.CallAjax(method, url, data, fnDoneCallback, null, null, null);
    }

    function FillTable(data, tipo) {

        switch (tipo) {
            case 1:
                var columns = [
                    { data: "Cod_Venta" },
                    { data: "Producto.Cod_Prod" },
                    { data: "Producto.Marca_Prod" },
                    { data: "Cant_Venta" },
                    { data: "Fecha" },
                    { data: "Precio_Final" },
                    { data: "Talla_Venta" },
                    { data: "Auditoria.TipoUsuario" }
                ];
                var columnDefs = [
                    {
                        "targets": [4],
                        'render': function (data, type, full, meta) {
                            return '' + app.ConvertIntToDatetimeDT(data) + '';
                        }
                    },
                    {
                        "targets": [5],
                        "className": "text-right",
                        'render': function (data, type, full, meta) {
                            return '' + app.FormatNumber(data) + '';
                        }
                    },
                    {
                        "targets": [7],
                        "visible": true,
                        "orderable": false,
                        "className": "text-center",
                        'render': function (data, type, full, meta) {
                            if (data == 1) {
                                return "<center>" +
                                    '<a class="btn btn-default btn-xs"  title="Eliminar" href="javascript:Ventas.EliminarVenta(' + meta.row + ')"><i class="fa fa-trash" aria-hidden="true"></i></a>' +
                                    "</center> ";
                            } else {
                                return "";
                            }
                        }
                    }
                ];

                var buttons = [{
                    extend: 'excelHtml5',
                    className: 'btn btn-success btn-sm ',
                    customizeData: function (data) {
                        for (var i = 0; i < data.body.length; i++) {
                            for (var j = 0; j < data.body[i].length; j++) {
                                data.body[i][j] = '\u200C' + data.body[i][j];
                            }
                        }
                    },

                }];


                app.FillDataTable($tblListadoVentas, data, columns, columnDefs, "#tblListadoVentas", false, null, null, null, null, null);
                break;
            case 2:
                var columns = [
                    { data: "Cod_Prod" },
                    { data: "Stock_Prod" },
                    { data: "Codigo_Al" },
                    { data: "Marca_Prod" },
                    { data: "Talla_Prod" },
                    { data: "Talla_Vendida_Prod" },
                    { data: "Precio_Prod" },
                ];
                var columnDefs = [
                    {
                        "targets": [6],
                        "className": "text-right",
                        'render': function (data, type, full, meta) {
                            return '' + app.FormatNumber(data) + '';
                        }
                    }
                ];

                app.FillDataTableFiltros($tblListadoProductos, data, columns, columnDefs, "#tblListadoProductos", false);
                break;
            default:
        }

    }

    function $btnBuscar_click() {
        if (ValidaBusqueda()) {
            GetVentas();
        }
    }

    function ValidaBusqueda() {
        var flag = true;
        var br = "<br>"
        var msg = "";

        var vcboTipoBusqueda = parseInt($cboTipoBusqueda.val());

        var Cod_Prod = $txtCodigo.val().trim();
        var Fecha = app.ConvertDatetimeToInt($txtFecha.val(), '/');
        var Marca_Prod = $txtMarca.val().trim();
        var Talla_Venta = $txtTalla.val().trim();

        switch (vcboTipoBusqueda) {
            case 1:
                msg += app.ValidarCampo(Cod_Prod, "• El código.");
                break;
            case 2:
                msg += app.ValidarCampo(Fecha, "• La fecha.");
                break;
            case 3:
                msg += app.ValidarCampo(Marca_Prod, "• La marca.");
                break;
            case 4:
                msg += app.ValidarCampo(Talla_Venta, "• La talla.");
                break;

            default:
                msg = "";
                break;
        }

        if (msg != "") {
            flag = false;
            var msgTotal = "Por favor, Ingrese los siguientes campos de la venta: " + br + msg;
            app.Message.Info("Aviso", msgTotal);
        }

        return flag;
    }

    function $btnNuevaVenta_click() {
        $modalVentas.modal();
        $formModal[0].reset();
        app.Event.SetDateDatepicket($txtModalFecha);
        $cboModalTalla.html("");

        $cboModalTalla.append("<option value='1'>Seleccione</option>");
    }

    function $btnSaveVenta_click() {
        var total = app.FormatNumber($txtModalTotal.val());
        if (total > 0) {
            InsertUpdateVenta();
        } else {
            app.Message.Info("ERROR", "El precio total no puede ser 0", null, null);
        }
    }

    function Validar() {
        var flag = true;
        var br = "<br>"
        var msg = "";
        var Cod_Prod = $txtModalCodigo.val();
        var Marca_Prod = $txtModalMarca.val();
        var Talla_Prod = $txtModalTalla.val();
        var Precio_Prod = $txtModalPrecio.val();
        var Stock_Prod = $txtModalStock.val();

        msg += app.ValidarCampo(Cod_Prod, "• El código.");
        msg += app.ValidarCampo(Marca_Prod, "• La marca.");
        //msg += app.ValidarCampo(Talla_Prod, "• La talla.");
        msg += app.ValidarCampo(Precio_Prod, "• El precio.");
        msg += app.ValidarCampo(Stock_Prod, "• El stock.");
        msg += app.ValidarCampo($txtModalCodigoAlmacen.val(), "• El código almacén.");
        msg += app.ValidarCampo($cboModalTipoProducto.val(), "• El tipo de producto.");
        if (dataTallas.Data.length == 0) {
            msg += "• Las tallas.";
        }


        if (msg != "") {
            flag = false;
            var msgTotal = "Por favor, Ingrese los siguientes campos del producto: " + br + msg;
            app.Message.Info("Aviso", msgTotal);
        }

        return flag;
    }

    function InsertUpdateVenta() {
        var obj = {
            "Cod_Venta": "",
            "Cant_Venta": $txtModalCatVenta.val(),
            "Fecha": app.ConvertDatetimeToInt($txtModalFecha.val(), '/'),
            "Producto.Cod_Prod": $txtModalCodigo.val(),
            "Producto.Marca_Prod": $txtModalMarca.val(),
            "Talla_Venta": $cboModalTalla.val(), //.join(),
            "Precio_Venta": $txtModalPrecioVenta.val(),
            "Producto.Precio_Prod": $txtModalPrecioProducto.val(),
            "Descuento_Venta": $txtModalDescuento.val()
        }

        var method = "POST";
        var url = "Ventas/InsertUpdateVentas";
        var data = obj;
        var fnDoneCallback = function (data) {
            app.Message.Success("Grabar", Message.GuardarSuccess, "Aceptar", null);
            GetVentas();
            $modalVentas.modal('hide');
        }
        app.CallAjax(method, url, data, fnDoneCallback, null, null, null);

    }

    // funciones del modal producto 
    function $btnProducto_click() {
        $modalProducto.modal();
        $cboModalTalla.html("");
        $cboTipoBusquedaModal.val(0).change();
        LoadProductos();
    }

    function LoadProductos() {

        var url = "Ventas/GetProducto";
        var parms = {
            Cod_Prod: $txtCodigoModal.val().trim(),
            Marca_Prod: $txtMarcaModal.val().trim(),
        }
        var columns = [
            { data: "Cod_Prod" },
            { data: "Stock_Prod" },
            { data: "Codigo_Al" },
            { data: "Marca_Prod" },
            { data: "Talla_Prod" },
            { data: "Talla_Vendida_Prod" },
            { data: "Precio_Prod" },
        ];
        var columnDefs = [
            {
                "targets": [6],
                "className": "text-right",
                'render': function (data, type, full, meta) {
                    return '' + app.FormatNumber(data) + '';
                }
            }
        ];


        app.FillDataTableAjaxPaging($tblListadoProductos, url, parms, columns, columnDefs, null, null, null);




        //var method = "POST";
        //var url = "Ventas/GetProducto";
        //var data = "";
        //var fnDoneCallback = function (data) {
        //    FillTable(data, 2);
        //};
        //app.CallAjax(method, url, data, fnDoneCallback, null, null, null);
    }

    function $btnSaveProducto_click() {
        var rowSelect = app.GetDataOfDataTable($tblListadoProductos);

        if (rowSelect == null) {
            app.Event.Info("Aviso", "Por favor, seleccione un produto.", "Aceptar");
            return false;
        }

        var tallap = new Array();
        var tallav = new Array();
        tallap = app.ToSplit(rowSelect.Talla_Prod, ",");
        tallav = app.ToSplit(rowSelect.Talla_Vendida_Prod, ",");
        //if (tallav = null) { tallav = 0 };
        var tallas = new Array();
        if (tallav == null) {
            tallas = tallap;
        } else {
            //$.each(tallav, function (i, v) {
            //    var index = $.inArray(v, tallap);
            //    tallap.splice(index, 1);
            //});
            tallas = tallap;
        }

        $cboModalTalla.append("<option value='1'>Seleccione</option>");

        $.each(tallas, function (key, value) {
            $cboModalTalla.append("<option value=" + value + ">" + value + "</option>");
        });

        $txtModalCodigo.val(rowSelect.Cod_Prod);
        $txtModalMarca.val(rowSelect.Marca_Prod);
        $txtModalPrecioProducto.val(app.FormatNumber(rowSelect.Precio_Prod));

        $modalProducto.modal('hide');

    }

    function $txtModalPrecioVenta_keypress() {
        var PrecioProducto = parseFloat(app.FormatNumber($txtModalPrecioProducto.val()));
        var PrecioVenta = parseFloat(app.FormatNumber($txtModalPrecioVenta.val()));
        var Cantidad = parseInt(app.FormatNumber($txtModalCatVenta.val()));

        if (Cantidad == 0) {
            app.Message.Info("Aviso", "Seleccione una Talla", "Aceptar");
            return false;
        }

        var PrecioFinal = PrecioVenta * Cantidad;

        if (PrecioProducto >= PrecioVenta) {
            var Descuento = PrecioProducto - PrecioVenta;
            $txtModalDescuento.val(app.FormatNumber(Descuento));
            $txtModalTotal.val(app.FormatNumber(PrecioFinal));
        } else {
            app.Message.Info("ERROR", "El precio del producto no puede ser mayor al  de venta", null, null);
            $txtModalPrecioVenta.val("");
        }
    }

    function $cboModalTalla_change() {
        var tallas = $cboModalTalla.val();
        //var cant = tallas.length;
        $txtModalCatVenta.val(1);
        $txtModalPrecioVenta_keypress();
    }

    function EliminarVenta(row) {
        var fnAceptarCallback = function () {
            var rowSelect = app.GetValueRowCellOfDataTable($tblListadoVentas, row);
            var obj = {
                "Cod_Venta": rowSelect.Cod_Venta
            }

            var method = "POST";
            var url = "Ventas/DeleteVentas";
            var data = obj;
            var fnDoneCallback = function (data) {
                app.Message.Success("Aviso", Message.EliminarSuccess, "Aceptar", null);
                GetVentas();
                $modalVentas.modal('hide');
            }
            app.CallAjax(method, url, data, fnDoneCallback, null, null, null);

        }
        app.Message.Confirm("Aviso", "Esta seguro que desea eliminar el registro?", "Aceptar", "Cancelar", fnAceptarCallback, null);
    }

    function $btnBuscarModal_click() {
        LoadProductos();
    }

    function $btnGenerarExcel_click() {
        var FechaDesde = app.ConvertDatetimeToInt($txtFechaDesde.val(), '/');
        var FechaHasta = app.ConvertDatetimeToInt($txtFechaHasta.val(), '/');

        if (FechaDesde !== "" && FechaHasta !== "") {
            GenerarExcel();
        } else if (FechaDesde === "") {
            app.Message.Info("Aviso", "Falta ingresar la Fecha Desde", "Aceptar", null);
        }

    }

    function GenerarExcel() {

        var data = {
            Producto: {
                Cod_Prod: $txtCodigo.val(),
                Marca_Prod: $txtMarca.val()
            },
            Talla_Venta: $txtTalla.val(),
            FechaDesde: app.ConvertDatetimeToInt($txtFechaDesde.val(), '/'),
            FechaHasta: app.ConvertDatetimeToInt($txtFechaHasta.val(), '/')
        }

        var fnDoneCallback = function (data) {
            if (data.InternalStatus == 1 && data.Data.length > 0) {
                app.RedirectTo("Ventas/GenerarExcel");
            } else {
                app.Message.Info("Aviso", "No hay ventas con esas fechas", "Aceptar");
            }
        }
        app.CallAjax("POST", "Ventas/GetAllVentas", data, fnDoneCallback);
    }

    function ValidarGenerarExcel() {
        var FechaDesde = app.ConvertDatetimeToInt($txtFechaDesde.val(), '/');
        var FechaHasta = app.ConvertDatetimeToInt($txtFechaHasta.val(), '/');

        if (FechaDesde > FechaHasta) {
            $txtFechaDesde.val("");
        }
    }

    return {
        EliminarVenta: EliminarVenta,
    }



})(window.jQuery, window, document);