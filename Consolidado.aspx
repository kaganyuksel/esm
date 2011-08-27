<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Consolidado.aspx.cs" Inherits="ESM.Consolidado.Consolidado" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager"
    TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/AmCharts/flash/swfobject.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="demo" style="width: 95%; margin: 0 auto;">
                <br />
                <br />
                <h1 style="color: #005EA7;">
                    <img src="/Icons/Stats.png" alt="Consolidado" />
                    Consolidado de Resultados para proceso de Evaluacion.
                </h1>
                <br />
                <br />
                <div>
                    <p id="filtrosp" runat="server">
                        Busqueda de Institucion educativa:
                        <asp:TextBox runat="server" ID="txtFiltro" />
                        <%--<input type="button" name="btnFiltro" value="Buscar" onclick="buscar();" />--%>
                        <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CausesValidation="false"
                            UseSubmitBehavior="true" OnClick="Unnamed2_Click" />
                    </p>
                    <br />
                    <asp:GridView ID="gvResultados" runat="server" AllowPaging="True" AllowSorting="True"
                        CssClass="gvResultados" RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6"
                        AutoGenerateColumns="False" OnSelectedIndexChanged="gvResultados_SelectedIndexChanged"
                        Width="100%">
                        <HeaderStyle CssClass="ui-widget-header" />
                        <Columns>
                            <asp:BoundField DataField="CodigoDane" HeaderText="Codigo Dane" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre de la Institucion Educativa" />
                            <asp:BoundField DataField="Municipio" HeaderText="Municipio" />
                            <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                            <asp:BoundField DataField="Rector" HeaderText="Rector" />
                            <asp:TemplateField Visible="false" SortExpression="IDIE" HeaderText="IDIE">
                                <ItemTemplate>
                                    <asp:Label Text='<%# Eval("IdIE") %>' runat="server" ID="IDIE"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" SortExpression="IDCON" HeaderText="IDCON">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="IDCON" Text='<%# Eval("IdConsultor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField SelectText="Mediciones" ShowSelectButton="True" ControlStyle-CssClass="a" />
                        </Columns>
                        <HeaderStyle CssClass="th"></HeaderStyle>
                        <PagerStyle CssClass="DDFooter" />
                        <PagerTemplate>
                            <asp:GridViewPager ID="GridViewPager1" runat="server" />
                        </PagerTemplate>
                        <EmptyDataTemplate>
                            Actualmente no hay elementos en esta tabla.
                        </EmptyDataTemplate>
                        <RowStyle CssClass="td"></RowStyle>
                    </asp:GridView>
                    <asp:LinqDataSource ID="ldsies" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
                        EntityTypeName="" TableName="InstitucionEducativa">
                    </asp:LinqDataSource>
                    <asp:GridView runat="server" Width="100%" ID="gvMediciones" AutoGenerateColumns="False"
                        OnSelectedIndexChanged="gvMediciones_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="IdMedicion" HeaderText="No. Medición" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha de Medición" />
                            <asp:CommandField SelectText="Visualizar Consolidado" ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div style="100%">
                    <asp:TreeView ID="trvConsolidacion" runat="server" Visible="false" AutoGenerateDataBindings="False"
                        ImageSet="Arrows" OnSelectedNodeChanged="trvConsolidacion_SelectedNodeChanged">
                        <HoverNodeStyle Font-Underline="True" ForeColor="#005EA7" />
                        <Nodes>
                            <asp:TreeNode Text="Consolidación" Value="1">
                                <asp:TreeNode Text="Ambientes" Value="2"></asp:TreeNode>
                            </asp:TreeNode>
                        </Nodes>
                        <NodeStyle Font-Names="Tahoma" Font-Size="12px" ForeColor="#005EA7" HorizontalPadding="3px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                        <ParentNodeStyle Font-Bold="False" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#005EA7" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                    </asp:TreeView>
                </div>
                <div style="width: 100%; text-align: center;">
                    <div id="amcharts">
                    </div>
                    <div id="amProces">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
