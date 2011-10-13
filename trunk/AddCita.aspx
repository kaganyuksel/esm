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
        <h2 style="text-align: center; color: #005EA7;">
            <img src="/Icons/calender.png" width="48px" height="48px" alt="icon-citas" />
            Asignación de Citas
        </h2>
        <br />
        <h3 style="color: #005EA7;">
            * Selección de establecimiento:
        </h3>
        <br />
        <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" GroupName="grupocita"
            OnCheckedChanged="RadioButton1_CheckedChanged" Text="Secretaría Educación" Checked="True" />
        <asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="True" GroupName="grupocita"
            OnCheckedChanged="RadioButton2_CheckedChanged" Text="Establecimiento Educativo" />
        <asp:GridView runat="server" AutoGenerateColumns="False" DataSourceID="lqSecreatarias"
            ID="gvresultadosSE" Width="100%" AllowPaging="True" AllowSorting="True" Font-Size="13px"
            OnPageIndexChanging="gvresultadosSE_PageIndexChanging" OnSelectedIndexChanged="gvresultadosSE_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" Height="40px" />
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre Secretaría" ReadOnly="True"
                    SortExpression="Nombre" />
                <asp:BoundField DataField="DepMun" HeaderText="Departamento/Municipio" ReadOnly="True"
                    SortExpression="DepMun" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" ReadOnly="True" SortExpression="Direccion" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" ReadOnly="True" SortExpression="Telefono" />
                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Icons/Calender.png" ShowSelectButton="True">
                    <ControlStyle Width="24px" />
                </asp:CommandField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblidse" Text='<%# Eval("IdSecretaria") %>' Visible="false" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="trheader" />
            <PagerStyle CssClass="DDFooter" />
            <PagerTemplate>
                <asp:GridViewPager ID="GridViewPager1" runat="server" />
            </PagerTemplate>
            <EmptyDataTemplate>
                Actualmente no hay elementos en esta tabla.
            </EmptyDataTemplate>
            <RowStyle BackColor="#EDEDED" Height="40px" />
        </asp:GridView>
        <asp:LinqDataSource ID="lqSecreatarias" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
            EntityTypeName="" Select="new (Nombre, DepMun, Direccion, Telefono, IdSecretaria)"
            TableName="Secretaria_Educacions" Where="IdConsultor == @IdConsultor">
            <WhereParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="IdConsultor" QueryStringField="idc"
                    Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
        <asp:GridView runat="server" AutoGenerateColumns="False" DataSourceID="lqIES" ID="gvresultadosIE"
            Width="100%" AllowPaging="True" AllowSorting="True" Font-Size="13px" OnPageIndexChanged="gvresultadosIE_PageIndexChanged"
            OnPageIndexChanging="gvresultadosIE_PageIndexChanging" Visible="False" OnSelectedIndexChanged="gvresultadosIE_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" Height="40px" />
            <Columns>
                <asp:BoundField DataField="CodigoDane" HeaderText="Codigo Dane" ReadOnly="True" SortExpression="CodigoDane" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" SortExpression="Nombre" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" ReadOnly="True" SortExpression="Direccion" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" ReadOnly="True" SortExpression="Telefono" />
                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Icons/Calender.png" ShowSelectButton="True">
                    <ControlStyle Width="24px" />
                </asp:CommandField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("IdIE") %>' ID="lblidie" Visible="false" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="trheader" />
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
            EntityTypeName="" Select="new (CodigoDane, Nombre, Direccion, Telefono, IdIE)"
            TableName="Establecimiento_Educativos" Where="Secretaria_Educacion.IdConsultor == @Secretaria_Educacion">
            <WhereParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="Secretaria_Educacion" QueryStringField="idc"
                    Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
    <br />
    <br />
    <br />
    <div style="width: 100%; text-align: center;">
        <asp:Label Visible="false" Text="" runat="server" ID="lblmensaje" Font-Size="18px" />
    </div>
    </form>
</body>
</html>
