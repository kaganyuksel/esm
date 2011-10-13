<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ActaVisitaSE.aspx.cs" Inherits="ESM.ActaVisitaSE" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager"
    TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("input:submit", ".demo").button();
            $("a", ".demo").click(function () { return false; });
        });
    </script>
    <link href="/Pretty/css/prettyPhoto.css" rel="stylesheet" charset="utf-8" media="screen"
        type="text/css" />
    <script src="/Pretty/js/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
        .style2
        {
            text-align: left;
        }
    </style>
    <script type="text/javascript">
        $("a.pretty").prettyPhoto({
            ie6_fallback: true,
            modal: true,
            social_tools: false
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="demo" class="demo" style="width: 90%; margin: 0 auto;">
        <br />
        <br />
        <div style="">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <img src="/Icons/Edit.png" alt="Evaluacion" />
                    </td>
                    <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                        <h1 style="color: #0b72bc;">
                            Formato Acta de Visita para Secretaría de Educación</h1>
                        Administra la información para el formato acta de visita.
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <div id="modEESeleccion" runat="server" visible="false">
            <h3>
                1. Seleccione la Secretaría de Educación a evaluar.
            </h3>
            <br />
            <p id="filtrosp" runat="server" visible="false">
                Busqueda de Establecimiento Educativo:
                <asp:TextBox runat="server" ID="txtFiltro" />
                <%--<td>
                                Actor Evaluado:
                                <asp:Label ID="lblActorEvaluado" Text="Secretaria de Educación" runat="server" />
                            </td>--%>
                <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CausesValidation="false"
                    UseSubmitBehavior="true" OnClick="Unnamed2_Click" />
            </p>
            <asp:GridView ID="gvResultados" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                CssClass="gvResultados" RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6"
                AutoGenerateColumns="False" OnSelectedIndexChanged="gvResultados_SelectedIndexChanged">
                <HeaderStyle CssClass="trheader" />
                <Columns>
                    <asp:CommandField SelectText="" ShowSelectButton="True" ControlStyle-CssClass="a"
                        ButtonType="Image" SelectImageUrl="~/Icons/Stationery.png">
                        <ControlStyle CssClass="a" Height="24px" Width="24px"></ControlStyle>
                        <ItemStyle Height="24px" Width="24px" />
                    </asp:CommandField>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre SE" />
                    <asp:BoundField DataField="DepMun" HeaderText="Departamento/Municipio" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:TemplateField Visible="false" SortExpression="IDIE" HeaderText="IDIE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("IdSecretaria") %>' runat="server" ID="IDIE"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
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
                EntityTypeName="" TableName="Establecimiento_Educativo" Select="new (IdIE, CodigoDane, Nombre, Telefono, Municipio, Rector)">
            </asp:LinqDataSource>
        </div>
        <div id="ModMediciones" runat="server" visible="false">
            <h4>
                * Datos Basicos de la Secretaría de Educación.</h4>
            <style type="text/css">
                .gvMediciones
                {
                    border: 1px solid #dddddd;
                }
            </style>
            <br />
            <table border="0" class="demo" cellpadding="0" cellspacing="0" style="width: 100%;
                border: 1px solid #dddddd;">
                <tr class="trheader">
                    <td colspan="2">
                        Información de Acta de Visita SE
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="tds">
                        Nombre:<b><asp:Label ID="lblNombrese" runat="server" Font-Size="14px" /></b>
                    </td>
                    <td class="tds">
                        Teléfono:<b><asp:Label ID="lblTelefonose" Font-Size="14px" runat="server"></asp:Label></b>
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="2" class="tds">
                        Departamento/Municipio: <b>
                            <asp:Label ID="lblMunicipio" Text="Municipio" Font-Size="14px" runat="server" /></b>
                    </td>
                    <%--<td>
                                Actor Evaluado:
                                <asp:Label ID="lblActorEvaluado" Text="Secretaria de Educación" runat="server" />
                            </td>--%>
                </tr>
            </table>
            <asp:GridView ID="gvMediciones" runat="server" Width="100%" OnSelectedIndexChanged="gvMediciones_SelectedIndexChanged"
                CssClass="gvMediciones" AutoGenerateColumns="false">
                <Columns>
                    <asp:CommandField ButtonType="Image" SelectText="" ShowSelectButton="True" SelectImageUrl="~/Icons/Calender.png">
                        <ControlStyle Height="24px" Width="24px" />
                    </asp:CommandField>
                    <asp:BoundField DataField="IdMedicion" HeaderText="Medición No." />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha de Medición" />
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <br />
        <div id="ModActaVisitaEE" runat="server" visible="false">
            <style type="text/css">
                #acta label, input[type="submit"], input[type="text"]
                {
                    font-size: 13px;
                    text-align: center;
                }
                .tablehr
                {
                    height: 40px;
                    line-height: 40px;
                    background: #0063C7;
                    color: #ffffff;
                    text-align: center;
                }
                .demo
                {
                    font-size: 14px;
                }
            </style>
            <table id="acta" style="max-width: 100%; -moz-border-radius: 3px; -webkit-border-radius: 3px;
                border-radius: 3px; /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 1px #000000; -webkit-box-shadow: 0px 0px 1px #000000; box-shadow: 0px 0px 1px #000000;
                font-size: 13px; /*ie 7 and 8 do not support blur property of shadows*/
 width: 100%;" cellpadding="0" cellspacing="00">
                <tr class="trheader">
                    <td class="style1" colspan="7">
                        ACTA DE VISITA
                    </td>
                </tr>
                <tr class="trheader">
                    <td style="text-align: center;" colspan="7">
                        1. INFORMACIÓN DE LA SECRETARÍA DE EDUCACIÓN
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="7">
                        1.1 Información básica del Establecimiento :
                    </td>
                </tr>
                <tr class="trblanca">
                    <td style="width: 150px;">
                        a. Nombre SE:
                    </td>
                    <td colspan="6" style="width: 30%;">
                        <asp:TextBox ID="txtNombreEE" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td style="width: 20%;">
                        Dirección:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtDireccion" runat="server" Width="100%" Style="margin-bottom: 0px"></asp:TextBox>
                    </td>
                    <td style="width: auto;">
                        Teléfonos:
                    </td>
                    <td colspan="2" style="width: 30%;">
                        <asp:TextBox ID="txtTelefonoEE" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trblanca">
                    <td style="width: 20%;">
                        Fecha Inicial:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtFechaInicial" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td style="width: 140px;" colspan="2">
                        Fecha Final:
                    </td>
                    <td>
                        <asp:TextBox ID="txtFechaFinal" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trheader">
                    <td style="text-align: center;" colspan="7">
                        2. PERSONAS QUE SUMINISTRAN LA INFORMACIÓN
                    </td>
                </tr>
                <tr class="trgris">
                    <td style="width: 100px;">
                        Nombre:
                    </td>
                    <td colspan="2" style="width: 30%">
                        <asp:TextBox ID="txtNombre" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td style="width: 140px;">
                        Teléfono:
                        <br />
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtTelefono" runat="server" Width="100%"></asp:TextBox>
                        <br />
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="2" style="width: 30%">
                        Correo Electronico:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCorreo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td width="10%">
                        Cargo:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtCargo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="7">
                        <asp:Button ID="btnAlmacenar" Text="Almacenar Individuo" runat="server" OnClick="btnAlmacenar_Click" />
                    </td>
                </tr>
                <tr class="trheader">
                    <td style="text-align: center;" colspan="7">
                        * CARGAR DOCUMENTOS
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="7" style="text-align: center;">
                        <a id="documentos" runat="server" style="text-decoration: none; font-size: 14px;
                            cursor: pointer;" href="#" class="pretty">Cargar Documentos</a>
                    </td>
                </tr>
                <tr>
                    <td class="trblanca" colspan="7">
                        <h3>
                            Individuos:</h3>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:GridView runat="server" ID="gvIndividuos" Width="100%" AllowSorting="True" AutoGenerateColumns="true" />
                    </td>
                </tr>
                <tr class="trheader">
                    <td style="text-align: center;" colspan="7">
                        OBSERVACIONES
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="7">
                        &nbsp;
                        <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris" style="width: 100%;">
                    <td colspan="7">
                        <asp:Button Text="Almacenar Acta" ID="btnAlmacenarActa" runat="server" OnClick="btnAlmacenarActa_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
    </div>
</asp:Content>
