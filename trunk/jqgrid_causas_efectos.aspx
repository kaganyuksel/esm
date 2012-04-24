<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jqgrid_causas_efectos.aspx.cs"
    Inherits="ESM.jqgrid_causas_efectos" %>

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
            j("#jqgrid_c_e_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=causas_efectos',
                datatype: "json",
                colNames: ['No.', 'Causa', 'Efecto', 'Beneficio'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 55 },
   		                    { name: 'causa', index: 'causa', width: 90, editable: true },
   		                    { name: 'efecto', index: 'efecto', width: 100, editable: true },
                            { name: 'beneficio', index: 'beneficio', width: 100, editable: true }
   	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#jqgrid_c_e_d',
                sortname: 'id',
                mytype: "POST",
                postData: { tabla: "c_e", proyecto_id: function () { return j("#ban_proyecto_id").val(); } },
                viewrecords: true,
                sortorder: "desc",
                editurl: "ajaxBancoProyectos.aspx",
                caption: "Causas Efectos"
            });
            j("#jqgrid_c_e_t").jqGrid('navGrid', "#jqgrid_c_e_d", { edit: true, add: true, del: false });
            j("#jqgrid_c_e_t").jqGrid('inlineNav', "#jqgrid_c_e_d");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="jqgrid_c_e_d">
    </div>
    <table id="jqgrid_c_e_t">
    </table>
    <input type="hidden" runat="server" name="ban_proyecto_id" id="ban_proyecto_id" value="" />
    </form>
</body>
</html>
