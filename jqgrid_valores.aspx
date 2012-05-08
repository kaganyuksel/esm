<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jqgrid_valores.aspx.cs"
    Inherits="ESM.jqgrid_valores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

            setInterval("j('#fecha').datepicker({dateFormat: 'dd/mm/yy', showAnim: 'bounce'});  j('#ejecutado').addClass('numerico');", 1000);

            j.extend(j.jgrid.edit, { width: "500" });

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
