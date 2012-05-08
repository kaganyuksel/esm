<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="List.aspx.cs" Inherits="ESM.List" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager"
    TagPrefix="asp" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 98%; margin: 0 auto;">
        <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
            <DataControls>
                <asp:DataControlReference ControlID="GridView1" />
            </DataControls>
        </asp:DynamicDataManager>
        <br />
        <br />
        <h1 style="color: #005EA7;">
            <%= table.DisplayName.Replace('_',' ') %></h1>
        <br />
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="DD">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                        HeaderText="Lista de errores de validación" CssClass="DDValidator" />
                    <asp:DynamicValidator runat="server" ID="GridViewValidator" ControlToValidate="GridView1"
                        Display="None" CssClass="DDValidator" />
                    <asp:QueryableFilterRepeater runat="server" ID="FilterRepeater" Visible="False">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("DisplayName") %>' OnPreRender="Label_PreRender" />
                            <asp:DynamicFilter runat="server" ID="DynamicFilter" OnFilterChanged="DynamicFilter_FilterChanged" />
                            <br />
                        </ItemTemplate>
                    </asp:QueryableFilterRepeater>
                    <br />
                </div>
                <style type="text/css">
                    .gridview
                    {
                        width: 100%;
                        border: 1px solid #dddddd;
                    }
                    .gridview a
                    {
                        text-decoration: none;
                        color: #005EA7;
                        cursor: pointer;
                    }
                    .gridview th
                    {
                        text-align: center;
                    }
                    .gridview td
                    {
                        text-align: left;
                        width: 100px;
                    }
                </style>
                <div style="width: 100%; overflow-x: scroll;">
                    <asp:GridView ID="GridView1" runat="server" DataSourceID="GridDataSource" EnablePersistedSelection="true"
                        AllowPaging="True" CssClass="gridview" AllowSorting="True" CellPadding="10">
                        <AlternatingRowStyle CssClass="trblanca" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:DynamicHyperLink runat="server" Action="Edit" Text="Editar" />&nbsp;<asp:LinkButton
                                        runat="server" CommandName="Delete" Text="Eliminar" OnClientClick='return confirm("¿Está seguro de que desea eliminar este elemento?");' />&nbsp;<asp:DynamicHyperLink
                                            runat="server" Text="Detalles" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="trheader" />
                        <PagerStyle CssClass="DDFooter" />
                        <PagerTemplate>
                            <asp:GridViewPager runat="server" />
                        </PagerTemplate>
                        <EmptyDataTemplate>
                            Actualmente no hay elementos en esta tabla.
                        </EmptyDataTemplate>
                        <RowStyle CssClass="trgris" />
                    </asp:GridView>
                </div>
                <asp:LinqDataSource ID="GridDataSource" runat="server" EnableDelete="true" />
                <asp:QueryExtender TargetControlID="GridDataSource" ID="GridQueryExtender" runat="server">
                    <asp:DynamicFilterExpression ControlID="FilterRepeater" />
                </asp:QueryExtender>
                <br />
                <style type="text/css">
                    .insert a
                    {
                        color: #005EA7;
                        font-size: 14px;
                        text-decoration: none;
                        cursor: pointer;
                    }
                </style>
                <div class="insert">
                    <asp:DynamicHyperLink ID="InsertHyperLink" runat="server" Action="Insert"><img runat="server" src="~/DynamicData/Content/Images/plus.gif" alt="Insertar nuevo elemento" />Insertar nuevo elemento</asp:DynamicHyperLink>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
