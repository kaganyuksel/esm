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
            <asp:LinkButton ID="lknDiliSE" Text="Diligenciamiento SE" runat="server" OnClick="lknDiliSE_Click" />
            <br />
            -
            <asp:LinkButton ID="lknDiliEE" Text="Diligenciamiento EE" runat="server" OnClick="lknDiliEE_Click" />
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
                    <%--<asp:BoundField DataField="FechaInicio" HeaderText="Fecha de Visita" />--%>
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
            <div>
                <asp:GridView runat="server" ID="gvdilise" Width="100%" AutoGenerateColumns="False"
                    AllowSorting="True" AllowPaging="True" Font-Size="14px" OnPageIndexChanging="gvdilise_PageIndexChanging">
                    <AlternatingRowStyle CssClass="trblanca" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label Visible="false" Text='<%# Eval("IdSecretaria") %>' runat="server" ID="lblidse" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre SE" />
                        <asp:BoundField DataField="Consultor" HeaderText="Consultor" />
                        <asp:TemplateField HeaderText="Fecha Visita">
                            <ItemTemplate>
                                <asp:Label Text='No Asignada' runat="server" ID="lblfechavisita" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado Acta">
                            <ItemTemplate>
                                <asp:Label Text='Sin Diligenciar' runat="server" ID="lblactaestado" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acta Fecha">
                            <ItemTemplate>
                                <asp:Label Text='No Asignada' runat="server" ID="lblfechadili" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Asistentes Acta">
                            <ItemTemplate>
                                <asp:Label Text='0' runat="server" ID="lblCantAsocioados" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acta Cargada">
                            <ItemTemplate>
                                <asp:Label Text='No' runat="server" ID="lblActaDocumento" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lectura Contexto">
                            <ItemTemplate>
                                <asp:Label Text='Sin Diligenciar' runat="server" ID="lbllcestado" />
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
            <asp:Panel ID="pnlgveedili" runat="server" ScrollBars="Horizontal" Width="100%">
                <asp:GridView runat="server" ID="gvDiliEE" AutoGenerateColumns="False" AllowSorting="True"
                    AllowPaging="True" Font-Size="14px" OnPageIndexChanging="gvDiliEE_PageIndexChanging">
                    <AlternatingRowStyle CssClass="trblanca" />
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-Width="20%"></asp:BoundField>
                        <asp:BoundField DataField="Consultor" HeaderText="Consultor">
                            <HeaderStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CodigoDane" HeaderText="Codigo DANE">
                            <HeaderStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EENombre" HeaderText="Nombre EE">
                            <HeaderStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Municipio" HeaderText="Departamento/Municipio">
                            <HeaderStyle Width="20%" />
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="FechaInicio" HeaderText="Fecha Visita">
                            <HeaderStyle Width="20%" />
                        </asp:BoundField>--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label Visible="false" Text='<%# Eval("idie") %>' runat="server" ID="lblidie" />
                            </ItemTemplate>
                            <HeaderStyle Width="20%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estudiante">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevalest" />
                            </ItemTemplate>
                            <HeaderStyle Width="20%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Padres">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevalpad" />
                            </ItemTemplate>
                            <HeaderStyle Width="20%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Profesional">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevalprof" />
                            </ItemTemplate>
                            <HeaderStyle Width="20%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Directivos">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevaldir" />
                            </ItemTemplate>
                            <HeaderStyle Width="20%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Educador">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevaledu" />
                            </ItemTemplate>
                            <HeaderStyle Width="20%" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="trheader" />
                    <RowStyle CssClass="trgris" />
                    <EmptyDataTemplate>
                        Actualmente no hay elementos en esta tabla.
                    </EmptyDataTemplate>
                </asp:GridView>
            </asp:Panel>
        </div>
    </div>
    <div style="clear: both;">
    </div>
    <br />
    <br />
</asp:Content>
