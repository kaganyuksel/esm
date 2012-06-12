<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jqgrid_causas_efectos.aspx.cs"
    Inherits="ESM.jqgrid_causas_efectos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/Style/jqgrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="Style/jquery-ui-1.8.15.custom.css" rel="stylesheet" type="text/css" />
    <link href="Style/bancoproyectos.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script src="/Scripts/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/jqgrid/grid.locale-es.js" type="text/javascript"></script>
    <script src="/Scripts/jqgrid/js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script type="text/javascript">
        var j = jQuery.noConflict();
        j(document).ready(function () {
            j.extend(j.jgrid.edit, { width: "600", afterComplete: function (response, postdata, formid) { alert('El proceso finalizó correctamente.'); } });

            j("#jqgrid_c_e_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=causas_efectos',
                datatype: "json",
                colNames: ['No.', 'Causa', 'Efecto', 'Beneficio', 'Causa Indirecta', 'Efecto Indirecto', 'Objetivo'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 55 },
   		                    { name: 'causa', index: 'causa', edittype: "textarea", width: 160, editable: true },
   		                    { name: 'efecto', index: 'efecto', edittype: "textarea", width: 160, editable: true },
                            { name: 'beneficio', index: 'beneficio', edittype: "textarea", width: 160, editable: true, hidden: true },
                            { name: 'causaindirecta', index: 'causaindirecta', edittype: "textarea", width: 100, editable: true },
                            { name: 'efectoindirecto', index: 'efectoindirecto', edittype: "textarea", width: 100, editable: true },
                            { name: 'objetivo', index: 'objetivo', width: 160, edittype: "textarea", editable: true, hidden: true }
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
                caption: "CAUSAS -- EFECTOS"
            });
            j("#jqgrid_c_e_t").jqGrid('navGrid', "#jqgrid_c_e_d", { edit: true, add: true, del: false });
            j("#jqgrid_c_e_t").jqGrid('inlineNav', "#jqgrid_c_e_d");

            j("#jqgrid_o_b_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=causas_efectos',
                datatype: "json",
                colNames: ['No.', 'Causa', 'Efecto', 'Beneficio', 'Causa Indirecta', 'Efecto Indirecto', 'Objetivo'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 55 },
   		                    { name: 'causa', index: 'causa', width: 160, edittype: "textarea", editable: false },
   		                    { name: 'efecto', index: 'efecto', width: 160, edittype: "textarea", editable: false },
                            { name: 'beneficio', index: 'beneficio', width: 160, edittype: "textarea", editable: true },
                            { name: 'causaindirecta', index: 'causaindirecta', edittype: "textarea", width: 160, editable: false, hidden: true },
                            { name: 'efectoindirecto', index: 'efectoindirecto', edittype: "textarea", width: 160, editable: false, hidden: true },
                            { name: 'objetivo', index: 'objetivo', width: 160, edittype: "textarea", editable: true }
   	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#jqgrid_o_b_d',
                sortname: 'id',
                mytype: "POST",
                postData: { tabla: "c_e", proyecto_id: function () { return j("#ban_proyecto_id").val(); } },
                viewrecords: true,
                sortorder: "desc",
                editurl: "ajaxBancoProyectos.aspx",
                caption: "OBJETIVOS -- BENEFICIOS"
            });
            j("#jqgrid_o_b_t").jqGrid('navGrid', "#jqgrid_o_b_d", { edit: true, add: false, del: false });
            j("#jqgrid_o_b_t").jqGrid('inlineNav', "#jqgrid_o_b_d");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mod_causas_efectos" visible="false" runat="server">
        <table id="jqgrid_c_e_t">
        </table>
        <div id="jqgrid_c_e_d">
        </div>
    </div>
    <div id="mod_objetivos_beneficios" visible="false" runat="server">
        <table id="jqgrid_o_b_t">
        </table>
        <div id="jqgrid_o_b_d">
        </div>
    </div>
    <input type="hidden" runat="server" name="ban_proyecto_id" id="ban_proyecto_id" value="" />
    </form>
</body>
</html>
