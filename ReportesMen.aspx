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
        /*.tables
        {
            <%---moz-border-radius: 2px;
            -webkit-border-radius: 2px;
            border-radius: 2px; /*IE 7 AND 8 DO NOT SUPPORT BORDER RADIUS
            -moz-box-shadow: 0px 0px 2px #000000;
            -webkit-box-shadow: 0px 0px 2px #000000;
            box-shadow: 0px 0px 2px #000000; /*IE 7 AND 8 DO NOT SUPPORT BLUR PROPERTY OF SHADOWS--%>
        }*/
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
                        Formulario de reportes consolidados.</h1>
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
            <br />
            &nbsp;<asp:LinkButton ID="lknDiliRes" Text="Diligenciamiento EE Resumido" runat="server"
                OnClick="lknDiliRes_Click" Visible="False" />
        </div>
        <div style="width: 80%; float: right;">
            <asp:Label Text="" runat="server" ID="lbltotal" />
            <asp:LinkButton Text="Exportar a Excel" runat="server" OnClick="ExportExcel_Click"
                ID="lknExportExcel" />
            <table id="infoadicionalAgendaEE" class="tables" runat="server" visible="false" border="0"
                cellpadding="0" width="100%" cellspacing="0">
                <tr class="trheader" style="padding: 5 5 5 5;">
                    <td>
                        Información para Agenda EE
                    </td>
                </tr>
                <tr>
                    <td class="trgris">
                        Total EE:
                        <asp:Label Text="" runat="server" ID="lbltotalees" />
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        Visitados:
                        <asp:Label Text="" runat="server" ID="lblvisitados" />%
                    </td>
                </tr>
                <tr class="trgris">
                    <td>
                        Por visitar:
                        <asp:Label Text="" runat="server" ID="lblporvisitar" />%
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        Por visitar agendados:
                        <asp:Label Text="" runat="server" ID="lblporvisitaragendados" />%
                    </td>
                </tr>
            </table>
            <asp:GridView runat="server" ID="gvAgendaEE" AutoGenerateColumns="False" Width="100%"
                AllowSorting="True" AllowPaging="true" CssClass="tables" Font-Size="0.5em" OnPageIndexChanging="gvAgendaEE_PageIndexChanging">
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
                Font-Size="14px" CssClass="tables">
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
            <div style="overflow-x: scroll;" id="divse" runat="server" visible="false">
                <asp:GridView runat="server" ID="gvdilise" Width="1024px" AutoGenerateColumns="False"
                    AllowSorting="True" AllowPaging="True" Font-Size="14px" OnPageIndexChanging="gvdilise_PageIndexChanging"
                    CssClass="tables">
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
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado Acta">
                            <ItemTemplate>
                                <asp:Label Text='Sin Diligenciar' runat="server" ID="lblactaestado" />
                            </ItemTemplate>
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
            <asp:Panel ID="pnlgveedili" runat="server" ScrollBars="Both" Visible="false">
                <asp:GridView runat="server" ID="gvDiliEE" AutoGenerateColumns="False" 
                    AllowSorting="True" Width="4000px" Font-Size="14px" 
                    OnPageIndexChanging="gvDiliEE_PageIndexChanging" AllowPaging="True" 
                    Visible="False">
                    <AlternatingRowStyle CssClass="trblanca" />
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre SE" HtmlEncode="False"></asp:BoundField>
                        <asp:BoundField DataField="Consultor" HeaderText="Consultor" HtmlEncode="False">
                        </asp:BoundField>
                        <asp:BoundField DataField="CodigoDane" HeaderText="Codigo DANE"></asp:BoundField>
                        <asp:BoundField DataField="EENombre" HeaderText="Nombre EE" HtmlEncode="False"></asp:BoundField>
                        <asp:BoundField DataField="Municipio" HeaderText="Departamento/Municipio" HtmlEncode="False">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Fecha Visita">
                            <ItemTemplate>
                                <asp:Label Visible="true" Text='No Asignada' runat="server" ID="lblcita" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PEI">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblpei" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PMI">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblpmi" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manual de Convivencia">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblmaco" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Plan de Estudios">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblplan" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Proyecto Pedagogico">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblproy" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Otros">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblotros" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acta Cargada">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblactaeecargada" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Directivos Acta">
                            <ItemTemplate>
                                <asp:Label Text="0" runat="server" ID="lblcantdir" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estudiantes Acta">
                            <ItemTemplate>
                                <asp:Label Text="0" runat="server" ID="lblcantest" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Educadores Acta">
                            <ItemTemplate>
                                <asp:Label Text="0" runat="server" ID="lblcantedu" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Padres Acta">
                            <ItemTemplate>
                                <asp:Label Text="0" runat="server" ID="lblcantpad" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Profesionales Acta">
                            <ItemTemplate>
                                <asp:Label Text="0" runat="server" ID="lblcantpro" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lectura Contexto">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblestadoactaee" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Observaciones">
                            <ItemTemplate>
                                <asp:Label Text="Ninguna" runat="server" ID="lblobservaciones" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estudiante">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevalest" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Padres">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevalpad" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Profesional">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevalprof" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Directivos">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevaldir" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Educador">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevaledu" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label Visible="false" Text='<%# Eval("idie") %>' runat="server" ID="lblidie" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="trheader" />
                    <RowStyle CssClass="trgris" />
                    <EmptyDataTemplate>
                        Actualmente no hay elementos en esta tabla.
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:GridView runat="server" ID="gvcopyDiliEE" AutoGenerateColumns="False" AllowSorting="True"
                    Visible="False" AllowPaging="false">
                    <AlternatingRowStyle CssClass="trblanca" />
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre SE" HtmlEncode="False"></asp:BoundField>
                        <asp:BoundField DataField="Consultor" HeaderText="Consultor" HtmlEncode="False">
                        </asp:BoundField>
                        <asp:BoundField DataField="CodigoDane" HeaderText="Codigo DANE"></asp:BoundField>
                        <asp:BoundField DataField="EENombre" HeaderText="Nombre EE" HtmlEncode="False"></asp:BoundField>
                        <asp:BoundField DataField="Municipio" HeaderText="Departamento/Municipio" HtmlEncode="False">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Fecha Visita">
                            <ItemTemplate>
                                <asp:Label Visible="true" Text='No Asignada' runat="server" ID="lblcita" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PEI">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblpei" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PMI">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblpmi" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manual de Convivencia">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblmaco" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Plan de Estudios">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblplan" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Proyecto Pedagogico">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblproy" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Otros">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblotros" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acta Cargada">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblactaeecargada" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Directivos Acta">
                            <ItemTemplate>
                                <asp:Label Text="0" runat="server" ID="lblcantdir" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estudiantes Acta">
                            <ItemTemplate>
                                <asp:Label Text="0" runat="server" ID="lblcantest" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Educadores Acta">
                            <ItemTemplate>
                                <asp:Label Text="0" runat="server" ID="lblcantedu" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Padres Acta">
                            <ItemTemplate>
                                <asp:Label Text="0" runat="server" ID="lblcantpad" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Profesionales Acta">
                            <ItemTemplate>
                                <asp:Label Text="0" runat="server" ID="lblcantpro" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lectura Contexto">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblestadoactaee" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Observaciones">
                            <ItemTemplate>
                                <asp:Label Text="Ninguna" runat="server" ID="lblobservaciones" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estudiante">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevalest" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Padres">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevalpad" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Profesional">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevalprof" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Directivos">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevaldir" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Educador">
                            <ItemTemplate>
                                <asp:Label Text="Sin Diligenciar" runat="server" ID="lblevaledu" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label Visible="false" Text='<%# Eval("idie") %>' runat="server" ID="lblidie" />
                            </ItemTemplate>
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
