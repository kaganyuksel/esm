<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCita.aspx.cs" Inherits="ESM.AddCita" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager"
    TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/mastercustom.css" rel="stylesheet" type="text/css" />
    <link href="Site.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: #ffffff;">
    <form id="form1" runat="server">
    <div>
        <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" 
            GroupName="grupocita" oncheckedchanged="RadioButton1_CheckedChanged" 
            Text="Secretaría Educación" />
        <asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="True" 
            GroupName="grupocita" oncheckedchanged="RadioButton2_CheckedChanged" 
            Text="Establecimiento Educativo" />
    <asp:GridView runat="server" AutoGenerateColumns="False" 
            DataSourceID="lqSecreatarias" ID="gvresultadosSE"
            Width="100%" AllowPaging="True" AllowSorting="True" Font-Size="13px" 
            onpageindexchanging="gvresultadosSE_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" Height="40px" />
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre Secretaria" 
                    ReadOnly="True" SortExpression="Nombre" />
                <asp:BoundField DataField="DepMun" HeaderText="Departamento/Municipio" 
                    ReadOnly="True" SortExpression="DepMun" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" ReadOnly="True" SortExpression="Direccion" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" ReadOnly="True" SortExpression="Telefono" />
                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Icons/Calender.png" 
                    ShowSelectButton="True">
                <ControlStyle Width="24px" />
                </asp:CommandField>
            </Columns>
            <PagerStyle CssClass="DDFooter" />
            <PagerTemplate>
                <asp:GridViewPager ID="GridViewPager1" runat="server" />
            </PagerTemplate>
            <EmptyDataTemplate>
                Actualmente no hay elementos en esta tabla.
            </EmptyDataTemplate>
            <RowStyle BackColor="#EDEDED" Height="40px" />
        </asp:GridView>
        <asp:LinqDataSource ID="lqSecreatarias" runat="server" 
            ContextTypeName="ESM.Model.ESMBDDataContext" EntityTypeName="" 
            Select="new (Nombre, DepMun, Direccion, Telefono)" 
            TableName="Secretaria_Educacions" Where="IdConsultor == @IdConsultor">
            <WhereParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="IdConsultor" 
                    QueryStringField="idc" Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
        <asp:GridView runat="server" AutoGenerateColumns="False" DataSourceID="lqIES" ID="gvresultadosIE"
            Width="100%" AllowPaging="True" AllowSorting="True" Font-Size="13px" 
            onpageindexchanged="gvresultadosIE_PageIndexChanged" 
            onpageindexchanging="gvresultadosIE_PageIndexChanging" Visible="False">
            <AlternatingRowStyle BackColor="White" Height="40px" />
            <Columns>
                <asp:BoundField DataField="CodigoDane" HeaderText="Codigo Dane" ReadOnly="True" SortExpression="CodigoDane" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" SortExpression="Nombre" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" ReadOnly="True" SortExpression="Direccion" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" ReadOnly="True" SortExpression="Telefono" />
                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Icons/Calender.png" 
                    ShowSelectButton="True">
                <ControlStyle Width="24px" />
                </asp:CommandField>
            </Columns>
            <PagerStyle CssClass="DDFooter" />
            <PagerTemplate>
                <asp:GridViewPager ID="GridViewPager1" runat="server" />
            </PagerTemplate>
            <EmptyDataTemplate>
                Actualmente no hay elementos en esta tabla.
            </EmptyDataTemplate>
            <RowStyle BackColor="#EDEDED" Height="40px" />
        </asp:GridView>
        <asp:LinqDataSource ID="lqIES" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
            EntityTypeName="" Select="new (CodigoDane, Nombre, Direccion, Telefono)" TableName="Establecimiento_Educativos"
            Where="Secretaria_Educacion.IdConsultor == @Secretaria_Educacion">
            <WhereParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="Secretaria_Educacion" QueryStringField="idc"
                    Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
    </form>
</body>
</html>
