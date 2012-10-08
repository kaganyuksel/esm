<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Organigrama.aspx.cs" Inherits="ESM.Organigama" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Organigramas - ESM</title>
    <style runat="server" id="style_tables" type="text/css">
        div, h1, table, tr, td
        {
            page-break-after: always;
        }
        td
        {
            width: auto;
            height: auto;
        }
    </style>
</head>
<body>
    <input type="button" name="volverESM" value="Volver" onclick="window.close();"/>
    <form id="form1" runat="server">
    <div style="page-break-after: always;">
        <h1 id="titulo_problemas" runat="server">
            ÁRBOL DE PROBLEMAS</h1>
        <div id="org" runat="server">
        </div>
    </div>
    <div style="page-break-after: always;">
        <h1 id="titulo_objetivos" runat="server">
            ÁRBOL DE OBJETIVOS</h1>
        <div id="org_Objetivos" runat="server">
        </div>
    </div>
    <div style="page-break-after: always;">
        <h1 id="titulo_plan_accion" runat="server">
            PLAN DE ACCIÓN</h1>
        <div id="planaccion" runat="server">
        </div>
    </div>
    </form>
</body>
</html>
