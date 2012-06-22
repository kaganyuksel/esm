<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteGeneralIndicadores.aspx.cs"
    Inherits="ESM.ReporteGeneralIndicadores" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="width: 100%; height: 1000px;">
        <rsweb:ReportViewer ID="rptIndicadores" Width="100%" Height="1000px" runat="server">
        </rsweb:ReportViewer>
        <br />
        <asp:SqlDataSource ID="ldsRegistroProyecto" runat="server" 
            ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>" 
            SelectCommand="SELECT * FROM [Registro_Proyectos] WHERE ([proyecto_id] = @proyecto_id)">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="proyecto_id" 
                    QueryStringField="proyecto_id" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:SqlDataSource ID="ldsIndicadores" runat="server" 
            ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>" 
            SelectCommand="SELECT * FROM [Indicadores]"></asp:SqlDataSource>
        <br />
        <asp:SqlDataSource ID="ldsmetas" runat="server" 
            ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>" 
            SelectCommand="SELECT * FROM [Indicadores_Metas]"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
