﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jqgrid_marco_logico.aspx.cs"
    Inherits="ESM.jqgrid_marco_logico" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/Style/jqgrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="Style/jquery-ui-1.8.15.custom.css" rel="stylesheet" type="text/css" />
    <link href="Style/bancoproyectos.css" rel="stylesheet" type="text/css" />
    <link href="Style/mastercustom.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script src="/Scripts/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/jqgrid/grid.locale-es.js" type="text/javascript"></script>
    <script src="/Scripts/jqgrid/js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script type="text/javascript">
        var j = jQuery.noConflict();
        j(document).ready(function () {

            //            j("#htmlprocesos").val(j("#gbox_jqgrid_subp_t").html());

            setInterval("j('#fecha, #fechainicial, #fechafinal').datepicker({dateFormat: 'dd/mm/yy'}); j('#htmlprocesos').val(j('#gbox_jqgrid_subp_t').html()); j('#htmlprocesos').val(j('#htmlprocesos').val().replace(/</g, '1')); j('#htmlprocesos').val(j('#htmlprocesos').val().replace(/>/g, '2'));", 1000);

            j.extend(j.jgrid.edit, { width: "500" });

            j("#jqgrid_subp_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=subprocesos',
                datatype: "json",
                colNames: ['No.', 'Proceso', 'Subproceso', 'Indicador', 'Medios', 'Supuestos'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 30 },
   		                    { name: 'proceso', index: 'proceso', width: 90, editable: true, edittype: "select", editoptions: { value: j("#col_procesos").val()} },
                            { name: 'subproceso', index: 'subproceso', width: 90, editable: true },
   		                    { name: 'indicador', index: 'indicador', width: 90, editable: true },
                            { name: 'medios', index: 'medios', width: 90, editable: true },
                            { name: 'supuestos', index: 'supuestos', width: 90, editable: true }
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
                caption: "Marco Lógico"
            });
            j("#jqgrid_subp_t").jqGrid('navGrid', "#jqgrid_subp_d", { edit: true, add: true, del: false });
            j("#jqgrid_subp_t").jqGrid('inlineNav', "#jqgrid_subp_d");

            j("#jqgrid_act_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=actividades',
                datatype: "json",
                colNames: ['No.', 'SubProcesos', 'Actividades', 'Fecha', 'Presupuesto'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 30 },
   		                    { name: 'subproceso', index: 'subproceso', width: 100, editable: true, edittype: "select", editoptions: { value: j("#col_sub_procesos").val()} },
                            { name: 'actividad', index: 'actividad', width: 100, editable: true },
   		                    { name: 'fecha', index: 'fecha', width: 150, editable: true },
                            { name: 'presupuesto', index: 'presupuesto', width: 100, editable: true, formatter: 'number', formatoptions: { decimalSeparator: ".", thousandsSeparator: " ", decimalPlaces: 2, defaultValue: '0'} }
   	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#jqgrid_act_d',
                sortname: 'id',
                onSelectRow: function (id) {
                    if (id && id !== lastsel3) {
                        j('#rowed6').jqGrid('editRow', id, true, pickdates);
                    }
                },
                mytype: "POST",
                postData: { tabla: "act", proyecto_id: function () { return j("#ban_proyecto_id").val(); } },
                viewrecords: true,
                sortorder: "desc",
                editurl: "ajaxBancoProyectos.aspx",
                caption: "Marco Lógico"
            });
            j("#jqgrid_act_t").jqGrid('navGrid', "#jqgrid_act_d", { edit: true, add: true, del: false });
            j("#jqgrid_act_t").jqGrid('inlineNav', "#jqgrid_act_d");


            j("#jqgrid_m_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=indicador',
                datatype: "json",
                colNames: ['No.', 'Actividad', 'Verbo', 'Meta', 'Unidad', 'Descripción', 'SSP', 'Fecha Inicio', 'Fecha Fin', 'Indicador', 'Medios de Verificación', 'Supuestos', 'Tipo Redacción'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 55 },
   		                    { name: 'actividad', index: 'actividad', width: 90, editable: true, edittype: "select", editoptions: { value: j("#col_actividades").val()} },
                            { name: 'verbo', index: 'verbo', width: 50, editable: true, edittype: "select", editoptions: { value: j("#ban_options_verbos").val()} },
                            { name: 'meta', index: 'meta', width: 50, editable: true },
                            { name: 'unidad', index: 'unidad', width: 50, editable: true, edittype: "select", editoptions: { value: j("#ban_options_unidades").val()} },
                            { name: 'descripcion', index: 'descripcion', width: 50, editable: true },
                            { name: 'ssp', index: 'ssp', width: 50, editable: true, edittype: "checkbox", editoptions: { value: "Si:No"} },
                            { name: 'fechainicial', index: 'fechainicial', width: 50, editable: true },
                            { name: 'fechafinal', index: 'fechafinal', width: 50, editable: true },
                            { name: 'indicador', index: 'indicador', width: 60, editable: false },
   		                    { name: 'medios', index: 'medios', width: 60, editable: true },
                            { name: 'supuestos', index: 'supuestos', width: 60, editable: true },
                            { name: 'tiporedaccion', index: 'tiporedaccion', hidden: true, width: 0, editable: true, edittype: "select", hidden: false, editoptions: { value: "entre:Entre;hasta:Hasta"} }
   	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#jqgrid_m_l_d',
                sortname: 'id',
                //                grouping: true,
                //                groupingView: {
                //                    groupField: ['actividad'],
                //                    groupColumnShow: [false],
                //                    groupText: ['<b>{0} - {1} Item(s)</b>']
                //                },
                mytype: "POST",
                postData: { tabla: "ind", proyecto_id: function () { return j("#ban_proyecto_id").val(); } },
                viewrecords: true,
                sortorder: "desc",
                editurl: "ajaxBancoProyectos.aspx",
                caption: "Marco Lógico"
            });
            j("#jqgrid_m_t").jqGrid('navGrid', "#jqgrid_m_l_d", { edit: true, add: true, del: false });
            j("#jqgrid_m_t").jqGrid('inlineNav', "#jqgrid_m_l_d");

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <p>
        <asp:ImageButton ID="btnExportar" ImageUrl="~/Icons/print.png" runat="server" 
            onclick="btnExportar_Click" /> Exportar
        Marco Logico</p>
    <h3>
        Agrupación de Subprocesos por Proceso</h3>
    <table id="jqgrid_subp_t">
    </table>
    <div id="jqgrid_subp_d">
    </div>
    <h3 style="color: #10852B;">
        Agrupación de Actividades por SubProcesos</h3>
    <table id="jqgrid_act_t">
    </table>
    <div id="jqgrid_act_d">
    </div>
    <br />
    <br />
    <h3 style="color: #DE6F2A;">
        Agrupación de Indicadores por Actividades</h3>
    <table id="jqgrid_m_t">
    </table>
    <div id="jqgrid_m_l_d">
    </div>
    <input type="hidden" runat="server" name="ban_proyecto_id" id="ban_proyecto_id" value="" />
    <input type="hidden" runat="server" name="col_procesos" id="col_procesos" value="" />
    <input type="hidden" runat="server" name="col_sub_procesos" id="col_sub_procesos"
        value="" />
    <input type="hidden" runat="server" name="col_actividades" id="col_actividades" value="" />
    <input type="hidden" name="options_verbos" value="" runat="server" id="ban_options_verbos" />
    <input type="hidden" name="options_unidades" value="" runat="server" id="ban_options_unidades" />
    <input type="hidden" runat="server" name="htmlprocesos" id="htmlprocesos" value="" />
    </form>
</body>
</html>