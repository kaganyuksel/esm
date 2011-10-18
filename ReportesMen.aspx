<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ReportesMen.aspx.cs" Inherits="ESM.ReportesMen" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager"
    TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        a
        {
            text-decoration: none;
            color: #005EA7;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div style="width: 95%; margin: 0 auto;">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <img src="Icons/Stats.png" alt="Reportes" />
                </td>
                <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                    <h1 style="color: #0b72bc;">
                        Formulario de reportes MEN</h1>
                    Consulta de información consolidada para los módulos existentes.
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div style="width: 95%; margin: 0 auto; border-right: 1pc solid #999999;">
        <div style="width: 20%; float: left;">
            *Selección de reporte a visualizar:
            <br />
            <br />
            -
            <asp:LinkButton ID="lknAgendaSE" Text="Agenda SE" runat="server" OnClick="lknAgendaSE_Click" />
            <br />
            -
            <asp:LinkButton ID="lknAgendaEE" Text="Agenda EE" runat="server" OnClick="lknAgendaEE_Click" />
            <br />
            -
            <asp:LinkButton ID="lknDiliSE" Text="Diligenciamiento SE" runat="server" 
                onclick="lknDiliSE_Click" />
        </div>
        <div style="width: 80%; float: right;">
            <asp:Label Text="" runat="server" ID="lbltotal" /><asp:LinkButton Text="Exportar a Excel"
                runat="server" OnClick="Unnamed1_Click" />
            <asp:GridView runat="server" ID="gvAgendaEE" AutoGenerateColumns="False" Width="100%"
                AllowSorting="True" AllowPaging="true" Font-Size="0.5em" OnPageIndexChanging="gvAgendaEE_PageIndexChanging">
                <AlternatingRowStyle CssClass="trblanca" />
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre SE" />
                    <asp:BoundField DataField="Consultor" HeaderText="Consultor" />
                    <asp:BoundField DataField="CodigoDane" HeaderText="Codigo Dane" />
                    <asp:BoundField DataField="Establecimiento" HeaderText="Nombre EE" />
                    <asp:BoundField DataField="Municipio" HeaderText="Departamento/Municipio" />
                    <asp:BoundField DataField="FechaInicio" HeaderText="Fecha de Visita" />
                </Columns>
                <HeaderStyle CssClass="trheader" />
                <RowStyle CssClass="trgris" />
                <EmptyDataTemplate>
                    Actualmente no hay elementos en esta tabla.
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:GridView runat="server" ID="gvAgendaSE" AutoGenerateColumns="False" Width="100%"
                AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gvAgendaSE_PageIndexChanging"
                Font-Size="14px">
                <AlternatingRowStyle CssClass="trblanca" />
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Consultor" HeaderText="Consultor" />
                    <asp:BoundField DataField="DepartamentoMunicipio" HeaderText="Departamento/Municipio" />
                    <asp:BoundField DataField="FechaInicio" HeaderText="Fecha de Visita" />
                </Columns>
                <HeaderStyle CssClass="trheader" />
                <RowStyle CssClass="trgris" />
                <EmptyDataTemplate>
                    Actualmente no hay elementos en esta tabla.
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:GridView runat="server" ID="gvdilise" AutoGenerateColumns="False" Width="100%"
                AllowSorting="True" AllowPaging="True" Font-Size="14px">
                <AlternatingRowStyle CssClass="trblanca" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label Visible="false" Text='<%# Eval("IdSecretaria") %>' runat="server" ID="lblidse" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre SE" />
                    <asp:BoundField DataField="Consultor" HeaderText="Consultor" />
                    <asp:BoundField DataField="FechaInicio" HeaderText="Fecha de Visita" />
                    <asp:TemplateField HeaderText="Estado Acta">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Estado") %>' runat="server" ID="lblactaestado" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FechaDiligenciamiento" HeaderText="Fecha de diligenciamiento" />
                    <asp:BoundField DataField="CantAsist" HeaderText="Asistentes Acta" />
                    <asp:TemplateField HeaderText="Acta Cargada">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("ActaDocumento") %>' runat="server" ID="lblActaDocumento" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="trheader" />
                <RowStyle CssClass="trgris" />
                <EmptyDataTemplate>
                    Actualmente no hay elementos en esta tabla.
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
    <div style="clear: both;">
    </div>
    <br />
    <br />
</asp:Content>
