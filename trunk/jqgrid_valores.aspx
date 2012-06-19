<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jqgrid_valores.aspx.cs"
    Inherits="ESM.jqgrid_valores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="/Style/jqgrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="/Style/jquery-ui-1.8.15.custom.css" rel="stylesheet" type="text/css" />
    <link href="/Style/bancoproyectos.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script src="/Scripts/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <script src="/Scripts/jqgrid/grid.locale-es.js" type="text/javascript"></script>
    <script src="/Scripts/jqgrid/js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script type="text/javascript">
        var interval = null;
        var j = jQuery.noConflict();
        j(document).ready(function () {

            j.extend(j.jgrid.edit, { width: "500", afterComplete: function (response, postdata, formid) { alert('El proceso finalizó correctamente.'); } });

            j("#jqgrid_subp_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=subprocesos',
                datatype: "json",
                colNames: ['No.', 'Proceso/Objetivo', 'Subproceso', 'Indicador', 'Medios', 'Supuestos'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 30 },
   		                    { name: 'proceso', index: 'proceso', width: 90, editable: true, edittype: "select", editoptions: { value: j("#col_procesos").val()} },
                            { name: 'subproceso', index: 'subproceso', edittype: "textarea", width: 90, editable: true },
   		                    { name: 'indicador', index: 'indicador', edittype: "textarea", width: 90, editable: true },
                            { name: 'medios', index: 'medios', width: 90, edittype: "textarea", editable: true },
                            { name: 'supuestos', index: 'supuestos', width: 90, edittype: "textarea", editable: true }
   	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#jqgrid_subp_d',
                sortname: 'id',
                mytype: "POST",
                postData: { tabla: "subp", proyecto_id: function () { return j("#ban_proyecto_id").val(); } },
                viewrecords: true,
                sortorder: "desc",
                editurl: "ajaxBancoProyectos.aspx",
                caption: "Marco Lógico",
                onSelectRow: function (ids) {
                    var json_subproceso_select = j("#jqgrid_subp_t").jqGrid('getRowData', ids);
                    jqgrid_subproceso_id = json_subproceso_select.id;

                    jqgrid_actividades_url = 'ajaxBancoProyectos.aspx?modulo=actividades&subproceso_id=' + jqgrid_subproceso_id;

                    j("#group_subprocesos").html("<b>" + json_subproceso_select.subproceso.toUpperCase() + "</b>")

                    var columns_actgrid = [
   		                    { name: 'id', index: 'id', width: 30 },
   		                    { name: 'subproceso', index: 'subproceso', width: 100, editable: true, edittype: "select", editoptions: { value: json_subproceso_select.id.toString() + ' : ' + json_subproceso_select.subproceso.toString()} },
                            { name: 'actividad', index: 'actividad', width: 100, editable: true, edittype: "textarea" },
   		                    { name: 'fecha', index: 'fecha', width: 150, editable: true },
                            { name: 'presupuesto', index: 'presupuesto', width: 100, editable: true, formatter: 'number', formatoptions: { decimalSeparator: ".", thousandsSeparator: " ", decimalPlaces: 2, defaultValue: '0'} }
   	                    ];

                    j('#jqgrid_act_t').setGridParam({ colModel: columns_actgrid });

                    j("#jqgrid_act_t").setGridParam({ url: jqgrid_actividades_url });

                    j("#jqgrid_act_t").trigger('reloadGrid');
                }
            });

            j("#jqgrid_act_t").jqGrid({
                datatype: "json",
                colNames: ['No.', 'SubProcesos', 'Actividades', 'Fecha', 'Presupuesto'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 30 },
   		                    { name: 'subproceso', index: 'subproceso', width: 100, editable: true, edittype: "select", editoptions: { value: j("#col_sub_procesos").val()} },
                            { name: 'actividad', index: 'actividad', width: 100, editable: true, edittype: "textarea" },
   		                    { name: 'fecha', index: 'fecha', width: 150, editable: true },
                            { name: 'presupuesto', index: 'presupuesto', width: 100, editable: true, formatter: 'number', formatoptions: { decimalSeparator: ".", thousandsSeparator: " ", decimalPlaces: 2, defaultValue: '0'} }
   	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#jqgrid_act_d',
                sortname: 'id',
                onSelectRow: function (ids) {
                    var json_actividad_select = j("#jqgrid_act_t").jqGrid('getRowData', ids);
                    jqgrid_actividad_id = json_actividad_select.id;

                    jqgrid_actividades_url = 'ajaxBancoProyectos.aspx?modulo=indicador&actividad_id=' + jqgrid_actividad_id;

                    j("#group_actividades").html("<b>" + json_actividad_select.actividad.toUpperCase() + "</b>")

                    j("#jqgrid_m_t").setGridParam({ url: jqgrid_actividades_url });

                    var colums_indicgrid = [
   		                    { name: 'id', index: 'id', width: 55 },
   		                    { name: 'actividad', index: 'actividad', width: 90, editable: true, edittype: "select", editoptions: { value: json_actividad_select.id.toString() + ':' + json_actividad_select.actividad.toString()} },
                            { name: 'verbo', index: 'verbo', width: 50, editable: true, edittype: "select", editoptions: { value: j("#ban_options_verbos").val()} },
                            { name: 'meta', index: 'meta', width: 50, editable: true },
                            { name: 'unidad', index: 'unidad', width: 50, editable: true, edittype: "select", editoptions: { value: j("#ban_options_unidades").val()} },
                            { name: 'descripcion', index: 'descripcion', width: 50, edittype: "textarea", editable: true },
                            { name: 'ssp', index: 'ssp', width: 50, editable: true, edittype: "checkbox", editoptions: { value: "Si:No"} },
                            { name: 'fechainicial', index: 'fechainicial', width: 50, editable: true },
                            { name: 'fechafinal', index: 'fechafinal', width: 50, editable: true },
                            { name: 'indicador', index: 'indicador', width: 60, edittype: "textarea", editable: false },
   		                    { name: 'medios', index: 'medios', width: 60, edittype: "textarea", editable: true },
                            { name: 'supuestos', index: 'supuestos', width: 60, edittype: "textarea", editable: true },
                            { name: 'tiporedaccion', index: 'tiporedaccion', hidden: true, width: 0, editable: true, edittype: "select", hidden: false, editoptions: { value: "entre:Entre;hasta:Hasta"} }
   	                ];


                    j('#jqgrid_m_t').setGridParam({ colModel: colums_indicgrid });

                    j("#jqgrid_m_t").trigger('reloadGrid');

                },
                mytype: "POST",
                postData: { tabla: "act", proyecto_id: function () { return j("#ban_proyecto_id").val(); } },
                viewrecords: true,
                sortorder: "desc",
                editurl: "ajaxBancoProyectos.aspx",
                caption: "Marco Lógico"
            });


            j("#jqgrid_m_t").jqGrid({
                datatype: "json",
                colNames: ['No.', 'Actividad', 'Verbo', 'Meta', 'Unidad', 'Descripción', 'SSP', 'Fecha Inicio', 'Fecha Fin', 'Indicador', 'Medios de Verificación', 'Supuestos', 'Tipo Redacción'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 55 },
   		                    { name: 'actividad', index: 'actividad', width: 90, editable: true, edittype: "select", editoptions: { value: j("#col_actividades").val()} },
                            { name: 'verbo', index: 'verbo', width: 50, editable: true, edittype: "select", editoptions: { value: j("#ban_options_verbos").val()} },
                            { name: 'meta', index: 'meta', width: 50, editable: true },
                            { name: 'unidad', index: 'unidad', width: 50, editable: true, edittype: "select", editoptions: { value: j("#ban_options_unidades").val()} },
                            { name: 'descripcion', index: 'descripcion', width: 50, edittype: "textarea", editable: true },
                            { name: 'ssp', index: 'ssp', width: 50, editable: true, edittype: "checkbox", editoptions: { value: "Si:No"} },
                            { name: 'fechainicial', index: 'fechainicial', width: 50, editable: true },
                            { name: 'fechafinal', index: 'fechafinal', width: 50, editable: true },
                            { name: 'indicador', index: 'indicador', width: 60, edittype: "textarea", editable: false },
   		                    { name: 'medios', index: 'medios', width: 60, edittype: "textarea", editable: true },
                            { name: 'supuestos', index: 'supuestos', width: 60, edittype: "textarea", editable: true },
                            { name: 'tiporedaccion', index: 'tiporedaccion', hidden: true, width: 0, editable: true, edittype: "select", hidden: false, editoptions: { value: "entre:Entre;hasta:Hasta"} }
   	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#jqgrid_m_l_d',
                sortname: 'id',
                mytype: "POST",
                onSelectRow: function (ids) {
                    var json_indicador_select = j("#jqgrid_m_t").jqGrid('getRowData', ids);
                    jqgrid_indicador_id = json_indicador_select.id;

                    jqgrid_indicadores_url = 'ajaxBancoProyectos.aspx?modulo=ind_val&indicador_id=' + jqgrid_indicador_id;

                    //j("#group_activ").html("<b>" + json_actividad_select.actividad.toUpperCase() + "</b>")

                    j("#jqgrid_val_ind_t").setGridParam({ url: jqgrid_indicadores_url });

                    var colums_indicgrid = [
   		                    { name: 'id', index: 'id', width: 30 },
   		                    { name: 'indicador', index: 'indicador', width: 90, editable: true, edittype: "select", editoptions: { value: json_indicador_select.id.toString() + ":" + json_indicador_select.indicador.toString()} },
                            { name: 'meta', index: 'meta', width: 90, editable: true },
   		                    { name: 'fecha', index: 'fecha', width: 90, editable: true },
                            { name: 'ejecutado', index: 'ejecutado', width: 90, editable: true },
                            { name: 'url', index: 'url', width: 90, editable: false, formatter: 'link', formatoptions: { target: '_blank'} }
   	                ];

                    j('#fecha').datepicker({ dateFormat: 'dd/mm/yy' });

                    clearInterval(interval);

                    interval = setInterval("j('#fecha').datepicker('destroy'); j('#fecha').datepicker({dateFormat: 'dd/mm/yy', minDate: '" + json_indicador_select.fechainicial + "', maxDate: '" + json_indicador_select.fechafinal + "' });", 1000);

                    j('#jqgrid_val_ind_t').setGridParam({ colModel: colums_indicgrid });

                    j("#jqgrid_val_ind_t").trigger('reloadGrid');

                },
                postData: { tabla: "ind", proyecto_id: function () { return j("#ban_proyecto_id").val(); } },
                viewrecords: true,
                sortorder: "desc",
                editurl: "ajaxBancoProyectos.aspx",
                caption: "Marco Lógico"
            });
            //            j("#jqgrid_m_t").jqGrid('navGrid', "#jqgrid_m_l_d", { edit: true, add: true, del: false });
            //            j("#jqgrid_m_t").jqGrid('inlineNav', "#jqgrid_m_l_d");

            j("#jqgrid_val_ind_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=ind_val',
                datatype: "json",
                colNames: ['No.', 'Indicador', 'Meta', 'Fecha', 'Ejecutado', 'Monitoreo'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 30 },
   		                    { name: 'indicador', index: 'indicador', width: 90, editable: true, edittype: "select", editoptions: { value: j("#col_indicadores").val()} },
                            { name: 'meta', index: 'meta', width: 90, editable: true },
   		                    { name: 'fecha', index: 'fecha', width: 90, editable: true },
                            { name: 'ejecutado', index: 'ejecutado', width: 90, editable: true },
                            { name: 'url', index: 'url', width: 90, editable: false, formatter: 'link', formatoptions: { target: '_blank'} }
   	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#jqgrid_val_ind_d',
                sortname: 'id',
                mytype: "POST",
                postData: { tabla: "ind_val_t", proyecto_id: function () { return j("#ban_proyecto_id").val(); } },
                viewrecords: true,
                sortorder: "desc",
                editurl: "ajaxBancoProyectos.aspx",
                caption: "Marco Lógico"
            });
            j("#jqgrid_val_ind_t").jqGrid('navGrid', "#jqgrid_val_ind_d", { edit: true, add: true, del: false });
            j("#jqgrid_val_ind_t").jqGrid('inlineNav', "#jqgrid_val_ind_d");

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <h3>
        Agrupación de Subprocesos por Proceso</h3>
    <table id="jqgrid_subp_t">
    </table>
    <div id="jqgrid_subp_d">
    </div>
    <h3 style="color: #10852B;">
        Agrupación de Actividades por SubProcesos</h3>
    <p id="group_subprocesos" style="color: #10852B;">
    </p>
    <table id="jqgrid_act_t">
    </table>
    <div id="jqgrid_act_d">
    </div>
    <br />
    <br />
    <h3 style="color: #DE6F2A;">
        Agrupación de Indicadores por Actividades</h3>
    <p id="group_actividades" style="color: #DE6F2A;">
    </p>
    <table id="jqgrid_m_t">
    </table>
    <div id="jqgrid_m_l_d">
    </div>
    <h3>
        Registro de valores para indicadores.</h3>
    <table id="jqgrid_val_ind_t">
    </table>
    <div id="jqgrid_val_ind_d">
    </div>
    <input type="hidden" runat="server" name="ban_proyecto_id" id="ban_proyecto_id" value="" />
    <input type="hidden" runat="server" name="col_procesos" id="col_procesos" value="" />
    <input type="hidden" runat="server" name="col_sub_procesos" id="col_sub_procesos"
        value="" />
    <input type="hidden" runat="server" name="col_actividades" id="col_actividades" value="" />
    <input type="hidden" runat="server" name="col_indicadores" id="col_indicadores" value="" />
    <input type="hidden" name="options_verbos" value="" runat="server" id="ban_options_verbos" />
    <input type="hidden" name="options_unidades" value="" runat="server" id="ban_options_unidades" />
    <input type="hidden" runat="server" name="htmlprocesos" id="htmlprocesos" value="" />
    </form>
</body>
</html>
