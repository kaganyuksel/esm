<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jqgrid_ejecucion.aspx.cs"
    Inherits="ESM.jqgrid_ejecucion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="/Style/jqgrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="Style/jquery-ui-1.8.15.custom.css" rel="stylesheet" type="text/css" />
    <link href="Style/bancoproyectos.css" rel="stylesheet" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="/Scripts/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/jqgrid/grid.locale-es.js" type="text/javascript"></script>
    <script src="/Scripts/jqgrid/js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script type="text/javascript">
        var j = jQuery.noConflict();
        j(document).ready(function () {

            if (document.body.clientWidth > 1024) {
                j.extend(j.jgrid.edit, { width: "500", afterComplete: function (response, postdata, formid) { parent.UpdateArbolProblemas(j('#ban_proyecto_id').val(), true); alert('El proceso finalizó correctamente.'); } });
            }
            else {
                j.extend(j.jgrid.edit, { width: "250", afterComplete: function (response, postdata, formid) { parent.UpdateArbolProblemas(j('#ban_proyecto_id').val(), true); alert('El proceso finalizó correctamente.'); } });
            }
            

            j("#jqgrid_act_ind_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=actividades_indicadores',
                datatype: "json",
                colNames: ['No.', 'Actividad', 'Indicador'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 100 },
   		                    { name: 'actividad', index: 'actividad', width: 200, editable: true },
                            { name: 'indicador', index: 'indicador', width: 400, editable: true },

   	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#jqgrid_act_ind_d',
                sortname: 'id',
                grouping: true,
                groupingView: {
                    groupField: ['actividad'],
                    groupColumnShow: [false],
                    groupText: ['<b>{0} - {1} Item(s)</b>']
                },
                mytype: "POST",
                postData: { tabla: "none", proyecto_id: function () { return j("#ban_proyecto_id").val(); } },
                viewrecords: true,
                sortorder: "desc",
                caption: "Marco Lógico"
            });
            j("#jqgrid_act_ind_t").jqGrid('navGrid', "#jqgrid_act_ind_d", { edit: false, add: false, del: false });
            j("#jqgrid_act_ind_t").jqGrid('inlineNav', "#jqgrid_act_ind_d");

            j("#jqgrid_plan_oper_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=plan_operativo',
                datatype: "json",
                colNames: ['No.', 'Proceso', 'Subproceso'],
                colModel: [
               		                    { name: 'id', index: 'id', width: 100 },
               		                    { name: 'proceso', index: 'proceso', width: 200, editable: false },
                                        { name: 'subproceso', index: 'subproceso', width: 400, editable: false }
               	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#jqgrid_plan_oper_d',
                sortname: 'id',
                height: 300,
                grouping: true,
                groupingView: {
                    groupField: ['proceso'],
                    groupColumnShow: [false],
                    groupText: ['<b>{0} - {1} Item(s)</b>']
                },
                mytype: "POST",
                postData: { tabla: "none", proyecto_id: function () { return j("#ban_proyecto_id").val(); } },
                viewrecords: true,
                sortorder: "desc",
                editurl: "ajaxBancoProyectos.aspx",
                caption: "Marco Lógico"
            });
            j("#jqgrid_plan_oper_t").jqGrid('navGrid', "#jqgrid_plan_oper_d", { edit: false, add: false, del: false });
            j("#jqgrid_plan_oper_t").jqGrid('inlineNav', "#jqgrid_plan_oper_d");


            j("#jqgrid_sub_act_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=sub_act_group',
                datatype: "json",
                colNames: ['No.', 'Subproceso', 'Actividad'],
                colModel: [
               		                    { name: 'id', index: 'id', width: 100 },
               		                    { name: 'subproceso', index: 'subproceso', width: 200, editable: true, edittype: "select", editoptions: { value: j("#col_actividades").val()} },
                                        { name: 'actividad', index: 'actividad', width: 400, editable: true, edittype: "select", editoptions: { value: j("#ban_options_verbos").val()} }
               	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#jqgrid_sub_act_d',
                sortname: 'id',
                grouping: true,
                groupingView: {
                    groupField: ['subproceso'],
                    groupColumnShow: [false],
                    groupText: ['<b>{0} - {1} Item(s)</b>']
                },
                mytype: "POST",
                postData: { tabla: "ind", proyecto_id: function () { return j("#ban_proyecto_id").val(); } },
                viewrecords: true,
                sortorder: "desc",
                editurl: "ajaxBancoProyectos.aspx",
                caption: "Marco Lógico"
            });
            j("#jqgrid_sub_act_t").jqGrid('navGrid', "#jqgrid_sub_act_d", { edit: true, add: true, del: false });
            j("#jqgrid_sub_act_t").jqGrid('inlineNav', "#jqgrid_sub_act_d");

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <h3>
        Relación Procesos - Subprocesos</h3>
    <table id="jqgrid_plan_oper_t">
    </table>
    <div id="jqgrid_plan_oper_d">
    </div>
    <br />
    <br />
    <h3>
        Relación Subprocesos - Actividades</h3>
    <table id="jqgrid_sub_act_t">
    </table>
    <div id="jqgrid_sub_act_d">
    </div>
    <br />
    <br />
    <h3>
        Relación Actividades - Indicadores</h3>
    <table id="jqgrid_act_ind_t">
    </table>
    <div id="jqgrid_act_ind_d">
    </div>
    <input type="hidden" runat="server" name="ban_proyecto_id" id="ban_proyecto_id" value="" />
    <input type="hidden" runat="server" name="col_procesos" id="col_procesos" value="" />
    <input type="hidden" runat="server" name="col_sub_procesos" id="col_sub_procesos"
        value="" />
    <input type="hidden" runat="server" name="col_actividades" id="col_actividades" value="" />
    <input type="hidden" name="options_verbos" value="" runat="server" id="ban_options_verbos" />
    <input type="hidden" name="options_unidades" value="" runat="server" id="ban_options_unidades" />
    </form>
</body>
</html>
