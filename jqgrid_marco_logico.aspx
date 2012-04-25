﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jqgrid_marco_logico.aspx.cs"
    Inherits="ESM.jqgrid_marco_logico" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/Style/jqgrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="Style/jquery-ui-1.8.15.custom.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script src="/Scripts/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/jqgrid/grid.locale-es.js" type="text/javascript"></script>
    <script src="/Scripts/jqgrid/js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script type="text/javascript">
        var j = jQuery.noConflict();
        j(document).ready(function () {

            j("#jqgrid_subp_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=subprocesos',
                datatype: "json",
                colNames: ['No.', 'Proceso', 'Subproceso', 'Indicador', 'Medios', 'Supuestos'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 55 },
   		                    { name: 'proceso', index: 'proceso', width: 90, editable: true, edittype: "select", editoptions: { value: j("#col_procesos").val()} },
                            { name: 'subproceso', index: 'subproceso', width: 90, editable: true },
   		                    { name: 'indicador', index: 'indicador', width: 100, editable: true },
                            { name: 'medios', index: 'medios', width: 100, editable: true },
                            { name: 'supuestos', index: 'supuestos', width: 100, editable: true }
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
                caption: "Marco Logico"
            });
            j("#jqgrid_subp_t").jqGrid('navGrid', "#jqgrid_subp_d", { edit: true, add: true, del: false });
            j("#jqgrid_subp_t").jqGrid('inlineNav', "#jqgrid_subp_d");

            j("#jqgrid_act_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=actividades',
                datatype: "json",
                colNames: ['No.', 'SubProcesos', 'Actividades', 'Fecha', 'Presupuesto'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 55 },
   		                    { name: 'subproceso', index: 'subproceso', width: 90, editable: true, edittype: "select", editoptions: { value: j("#col_sub_procesos").val()} },
                            { name: 'actividad', index: 'actividad', width: 90, editable: true },
   		                    { name: 'fecha', index: 'fecha', width: 100, editable: true },
                            { name: 'presupuesto', index: 'presupuesto', width: 100, editable: true }
   	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#jqgrid_act_d',
                sortname: 'id',
                //                grouping: true,
                //                groupingView: {
                //                    groupField: ['subproceso'],
                //                    groupColumnShow: [false],
                //                    groupText: ['<b>{0} - {1} Item(s)</b>']
                //                },
                mytype: "POST",
                postData: { tabla: "act", proyecto_id: function () { return j("#ban_proyecto_id").val(); } },
                viewrecords: true,
                sortorder: "desc",
                editurl: "ajaxBancoProyectos.aspx",
                caption: "Marco Logico"
            });
            j("#jqgrid_act_t").jqGrid('navGrid', "#jqgrid_act_d", { edit: true, add: true, del: false });
            j("#jqgrid_act_t").jqGrid('inlineNav', "#jqgrid_act_d");


            j("#jqgrid_m_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=indicador',
                datatype: "json",
                colNames: ['No.', 'Actividad', 'Verbo', 'Meta', 'Unidad', 'SSP', 'Fecha Inicio', 'Fecha Fin', 'Indicador', 'Medios de Verificación', 'Supuestos'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 55 },
   		                    { name: 'actividad', index: 'actividad', width: 90, editable: true, edittype: "select", editoptions: { value: j("#col_sub_procesos").val()} },
                            { name: 'verbo', index: 'verbo', width: 90, editable: true, edittype: "select", editoptions: { value: j("#ban_options_verbos").val()} },
                            { name: 'meta', index: 'meta', width: 90, editable: true },
                            { name: 'unidad', index: 'unidad', width: 90, editable: true, edittype: "select", editoptions: { value: j("#ban_options_unidades").val()} },
                            { name: 'ssp', index: 'ssp', width: 90, editable: true },
                            { name: 'fechainicial', index: 'fechainicial', width: 90, editable: true },
                            { name: 'fechafinal', index: 'fechafinal', width: 90, editable: true },
                            { name: 'indicador', index: 'indicador', width: 90, editable: false },
   		                    { name: 'medios', index: 'medios', width: 100, editable: true },
                            { name: 'supuestos', index: 'supuestos', width: 100, editable: true }
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
                caption: "Marco Logico"
            });
            j("#jqgrid_m_t").jqGrid('navGrid', "#jqgrid_m_l_d", { edit: true, add: true, del: false });
            j("#jqgrid_m_t").jqGrid('inlineNav', "#jqgrid_m_l_d");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="jqgrid_subp_t">
    </table>
    <div id="jqgrid_subp_d">
    </div>
    <br />
    <br />
    <table id="jqgrid_act_t">
    </table>
    <div id="jqgrid_act_d">
    </div>
    <br />
    <br />
    <table id="jqgrid_m_t">
    </table>
    <div id="jqgrid_m_l_d">
    </div>
    <input type="hidden" runat="server" name="ban_proyecto_id" id="ban_proyecto_id" value="" />
    <input type="hidden" runat="server" name="col_procesos" id="col_procesos" value="" />
    <input type="hidden" runat="server" name="col_sub_procesos" id="col_sub_procesos"
        value="" />
    <input type="hidden" name="options_verbos" value="" runat="server" id="ban_options_verbos" />
    <input type="hidden" name="options_unidades" value="" runat="server" id="ban_options_unidades" />
    </form>
</body>
</html>
