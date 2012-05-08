<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reportactividades.aspx.cs"
    Inherits="ESM.reportactividades" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Button ID="btnGenerar" Visible="false" runat="server" OnClick="btnGenerar_Click"
            Text="Metas para Idicadores" />
        <asp:Button ID="btnEjecutado" Visible="false" Style="margin-left: 5px;" runat="server"
            OnClick="btnEjecutado_Click" Text="Ejecutado" />
        <rsweb:ReportViewer ID="rptActividades" runat="server" Width="100%" 
            Height="600px">
        </rsweb:ReportViewer>
        <asp:SqlDataSource ID="odsActividadesMetas" runat="server" ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>"
            SelectCommand="SELECT * FROM [actividades_presupuesto]" 
            ProviderName="<%$ ConnectionStrings:esmConnectionString2.ProviderName %>"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
