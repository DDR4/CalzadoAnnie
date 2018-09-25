var Producto = (function ($, win, doc) {

    var $btnNuevaProducto = $('#btnNuevaProducto');
    var $cboTipoBusqueda = $('#cboTipoBusqueda');

    var $tipoCodigo = $('#tipoCodigo');
    var $tipoMarca = $('#tipoMarca');
    var $tipoEstado = $('#tipoEstado');

    var $txtCodigo = $('#txtCodigo');
    var $txtMarca = $('#txtMarca');
    var $cboEstado = $('#cboEstado');

    var $btnBuscar = $('#btnBuscar');

    var $tblListadoProductos = $('#tblListadoProductos');

    // Modal
    var $modalProducto = $('#modalProducto');
    var $titleModalProducto = $('#titleModalProducto');
    var $formModal = $('#formModal');
    var $txtModalCodigo = $('#txtModalCodigo');
    var $txtModalMarca = $('#txtModalMarca');
    var $txtModalTalla = $('#txtModalTalla');
    var $txtModalPrecio = $('#txtModalPrecio');
    var $txtModalStock = $('#txtModalStock');
    var $txtModalCodigoAlmacen = $('#txtModalCodigoAlmacen');
    var $btnSaveProducto = $('#btnSaveProducto');
    var $txtModalTallaVendida = $('#txtModalTallaVendida');

    var $btnAgregarTalla = $('#btnAgregarTalla');
    var $tblListadoTallas = $('#tblListadoTallas');
    var $cboModalTipoProducto = $('#cboModalTipoProducto');
    var $cboModalEstado = $('#cboModalEstado');

    var Message = {
        ObtenerTipoBusqueda: "Obteniendo los tipos de busqueda, Por favor espere...",
        GuardarSuccess: "Los datos se guardaron satisfactoriamente",
    }


    var Global = {
        IdProducto: null
    }

    var dataTallas = { Data: [] };


    // Constructor
    $(Initialize);

    // Implementacion del constructor
    function Initialize() {

        $cboTipoBusqueda.change($cboTipoBusqueda_change);

        $btnBuscar.click($btnBuscar_click);
        $btnSaveProducto.click($btnSaveProducto_click);
        $btnNuevaProducto.click($btnNuevaProducto_click);
        $btnAgregarTalla.click($btnAgregarTalla_click);
        GetProducto();


        app.Event.ForceNumericOnly($txtModalCodigo);
        app.Event.Number($txtModalStock);
        app.Event.ForceDecimalOnly($txtModalPrecio);
        app.Event.Number($txtModalTalla);
        app.Event.Number($txtCodigo);

        app.Event.Blur($txtModalPrecio, "N");
    };

    function $cboTipoBusqueda_change() {
        var codSelec = $(this).val();
        //$('#form1')[0].reset();
        $tipoCodigo.hide();
        $tipoMarca.hide();
        $tipoEstado.hide();

        $txtCodigo.val("");
        $txtMarca.val("");
        $cboEstado.val(0);

        if (codSelec == "1") {
            $tipoCodigo.show();
        }
        else if (codSelec == "2") {
            $tipoMarca.show();
        }
        else if (codSelec == "3") {
            $tipoEstado.show();
        }

    }

    function $btnNuevaProducto_click() {
        $formModal[0].reset();
        app.Event.Enable($txtModalCodigo);
        Global.IdProducto = null;
        $modalProducto.modal();
        dataTallas = { Data: [] };
        LoadTallas(dataTallas);
        $cboModalEstado.val(1).trigger('change');
        $titleModalProducto.html("Agregar Producto");
        app.Event.Disabled($cboModalEstado);

    }

    function $btnBuscar_click() {
        if (ValidaBusqueda()) {
            GetProducto();
        }
    }

    function $btnSaveProducto_click() {
        if (Validar()) {
            InsertUpdateProducto();
        }
    }

    function $btnAgregarTalla_click(e) {


        if ($txtModalTalla.val() == '' || $txtModalTalla.val() == null) {
            app.Message.Info("Aviso", "Ingrese la talla", "Aceptar", null);
            return false;
        }

        var obj = {
            "Talla": $txtModalTalla.val()
        };

        dataTallas.Data.push(obj);

        LoadTallas(dataTallas);

        $txtModalTalla.val('')

        return false;

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

    function ValidaBusqueda() {
        var flag = true;
        var br = "<br>"
        var msg = "";

        var vcboTipoBusqueda = parseInt($cboTipoBusqueda.val());

        var Cod_Prod = $txtCodigo.val().trim();
        var Marca_Prod = $txtMarca.val().trim();
        var Estado_Prod = $cboEstado.val().trim();

        switch (vcboTipoBusqueda) {
            case 1:
                msg += app.ValidarCampo(Cod_Prod, "• El código.");
                break;
            case 2:
                msg += app.ValidarCampo(Marca_Prod, "• La marca.");
                break;
            case 3:
                msg += app.ValidarCampo(Estado_Prod, "• El Estado.");
                break;

            default:
                msg = "";
                break;
        }

        if (msg != "") {
            flag = false;
            var msgTotal = "Por favor, Ingrese los siguientes campos del producto: " + br + msg;
            app.Message.Info("Aviso", msgTotal);
        }

        return flag;
    }

    function InsertUpdateProducto() {
        var tallas = [];
        dataTallas.Data.map(function (v, i) {
            tallas.push(v.Talla);
        });


        var obj = {
            "IdProducto": Global.IdProducto,
            "Cod_Prod": $txtModalCodigo.val(),
            "Marca_Prod": $txtModalMarca.val(),
            "Talla_Prod": tallas.join(),
            "Precio_Prod": app.UnformatNumber($txtModalPrecio.val()),
            "Stock_Prod": $txtModalStock.val(),
            "Codigo_Al": $txtModalCodigoAlmacen.val(),
            "Tipo_Prod": $cboModalTipoProducto.val(),
            "Estado_Prod": $cboModalEstado.val()
        }

        var method = "POST";
        var url = "Producto/InsertUpdateProducto";
        var data = obj;
        var fnDoneCallback = function (data) {
            app.Message.Success("Grabar", Message.GuardarSuccess, "Aceptar", null);
            GetProducto();
            $modalProducto.modal('hide');
            dataTallas = { Data: [] };
        }
        app.CallAjax(method, url, data, fnDoneCallback, null, null, null);

    }

    function GetProducto() {
        var obj = {
            Cod_Prod: $txtCodigo.val(),
            Marca_Prod: $txtMarca.val(),
            Estado_Prod: $cboEstado.val()
        }
        var method = "POST";
        var url = "Producto/GetProducto";
        var data = obj;
        var fnDoneCallback = function (data) {
            FillTable(data);
        };
        app.CallAjax(method, url, data, fnDoneCallback, null, null, null);
    }

    function FillTable(data) {

        var columns = [
            { data: "Cod_Prod" },
            { data: "Stock_Prod" },
            { data: "Codigo_Al" },
            { data: "Marca_Prod" },
            { data: "Talla_Prod" },
            { data: "Talla_Vendida_Prod" },
            { data: "Precio_Prod" },
            { data: "Estado_Prod" },
            { data: "Auditoria.TipoUsuario" }
        ];
        var columnDefs = [
            {
                "targets": [6],
                "className": "text-right",
                'render': function (data, type, full, meta) {
                    return '' + app.FormatNumber(data) + '';
                }
            },
            {
                "targets": [7],
                "className": "text-right",
                'render': function (data, type, full, meta) {
                    if (data == 1) {
                        return "Activo";
                    } else return "Inactivo";

                }
            },
            {
                "targets": [8],
                "visible": true,
                "orderable": false,
                "className": "text-center",
                'render': function (data, type, full, meta) {
                    if (data == 1) {
                        return "<center>" +
                            '<a class="btn btn-default btn-xs" title="Editar" href="javascript:Producto.EditarProducto(' + meta.row + ');"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></a>' +
                            '<a class="btn btn-default btn-xs"  title="Eliminar" href="javascript:Producto.EliminarProducto(' + meta.row + ')"><i class="fa fa-trash" aria-hidden="true"></i></a>' +
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



        app.FillDataTable($tblListadoProductos, data, columns, columnDefs, "#tblListadoProductos", false, null, null, null, null, null , 1);
    }

    function EditarProducto(row) {
        var data = app.GetValueRowCellOfDataTable($tblListadoProductos, row);
        $titleModalProducto.html("Editar Producto");

        $modalProducto.modal();
        Global.IdProducto = data.IdProducto;
        $txtModalCodigo.val(data.Cod_Prod);
        $txtModalMarca.val(data.Marca_Prod);
        $txtModalTallaVendida.val(data.Talla_Vendida_Prod);
        $txtModalPrecio.val(app.FormatNumber(data.Precio_Prod));
        $txtModalStock.val(data.Stock_Prod);
        $txtModalCodigoAlmacen.val(data.Codigo_Al);
        app.Event.Disabled($txtModalCodigo);
        app.Event.Enable($cboModalEstado);
        $cboModalEstado.val(data.Estado_Prod).trigger('change');
        $cboModalTipoProducto.val(data.Tipo_Prod).trigger('change');

        console.log(data);
        FillTableTalla(data.Talla_Prod, data.Talla_Vendida_Prod);
    }

    function EliminarProducto(row) {
        var fnAceptarCallback = function () {
            var data = app.GetValueRowCellOfDataTable($tblListadoProductos, row);

            var obj = {
                "IdProducto": data.IdProducto,
                "Cod_Prod": data.Cod_Prod
            }

            var method = "POST";
            var url = "Producto/DeleteProducto";
            var data = obj;
            var fnDoneCallback = function (data) {
                GetProducto();
            };
            app.CallAjax(method, url, data, fnDoneCallback, null, null, null);
        }
        app.Message.Confirm("Aviso", "Esta seguro que desea eliminar el producto?", "Aceptar", "Cancelar", fnAceptarCallback, null);
    }

    function FillTableTalla(Talla_Prod, Talla_Vend) {
        dataTallas = { Data: [] };

        if (Talla_Prod != '' && Talla_Prod != null) {
            var talla = app.ToSplit(Talla_Prod, ',');
            var tallaV = app.ToSplit(Talla_Vend, ',');

            //var index = $.inArray(talla, tallaV);
            //talla.splice(index, 1);

            $.each(talla, function (index, value) {
                var obj = {
                    "Talla": value
                };
                dataTallas.Data.push(obj);
            });

        }

        LoadTallas(dataTallas);
    }

    function LoadTallas(dataTallas) {
        var columns = [
            { data: "Talla" },
            { data: "Talla" }
        ];
        var columnDefs = [

            {
                "targets": [1],
                "visible": true,
                "className": "text-center",
                'render': function (data, type, full, meta) {
                    return "<center>" +
                        '<a class="btn btn-default btn-xs"  title="Eliminar" href="javascript:Producto.EliminarNroTalla(' + meta.row + ')"><i class="fa fa-trash" aria-hidden="true"></i></a>' +
                        "</center> ";
                }
            }
        ];

        app.FillDataTable($tblListadoTallas, dataTallas, columns, columnDefs, "#tblListadoTallas", false);
    }

    function EliminarNroTalla(row) {
        var data = app.GetValueRowCellOfDataTable($tblListadoTallas, row);

        var tallas = [];
        dataTallas.Data.map(function (v, i) {
            tallas.push(v.Talla);
        });

        var index = $.inArray(data.Talla, tallas);
        tallas.splice(index, 1);

        dataTallas = { Data: [] };
        $.each(tallas, function (index, value) {
            var obj = {
                "Talla": value
            };
            dataTallas.Data.push(obj);
        });

        LoadTallas(dataTallas);

    }




    return {
        EliminarProducto: EliminarProducto,
        EditarProducto: EditarProducto,
        EliminarNroTalla: EliminarNroTalla
    }


})(window.jQuery, window, document);