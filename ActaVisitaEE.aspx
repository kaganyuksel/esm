<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ActaVisitaEE.aspx.cs" Inherits="ESM.ActaVisitaEE" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager"
    TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        $("input:submit", ".demo").button();

        $("a.pretty").prettyPhoto({
            ie6_fallback: true,
            modal: true,
            social_tools: false
        });
        $(document).ready(function () {
            $(".numerico").change(function () {
                var valor = $(this).val();
                if (isNaN(valor)) {
                    alert("El valor ingresado no es del tipo numerico.");
                    $(this).val(0);
                }
            });

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
                            Formato Acta de Visita para Establecimientos Educativos</h1>
                        Administra la información para el formato acta de visita.
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <div id="modEESeleccion" runat="server" visible="false">
            <h3>
                1. Seleccione el Establecimiento Educativo a evaluar.
            </h3>
            <br />
            <p id="filtrosp" runat="server">
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
                AutoGenerateColumns="false" OnSelectedIndexChanged="gvResultados_SelectedIndexChanged"
                OnPageIndexChanging="gvResultados_PageIndexChanging">
                <HeaderStyle CssClass="th"></HeaderStyle>
                <AlternatingRowStyle CssClass="trblanca" />
                <Columns>
                    <asp:CommandField SelectText="<img id='imgEvaluar'  height='24px' src='/Icons/Stationery.png' alt='Evaluar' />"
                        ShowSelectButton="True" ControlStyle-CssClass="a">
                        <ControlStyle CssClass="a"></ControlStyle>
                    </asp:CommandField>
                    <asp:BoundField DataField="CodigoDane" HeaderText="Cod. DANE" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Municipio" HeaderText="Municipio" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:TemplateField Visible="false" SortExpression="IDIE" HeaderText="IDIE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("IdIE") %>' runat="server" ID="IDIE"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    Actualmente no hay elementos en esta tabla.
                </EmptyDataTemplate>
                <HeaderStyle CssClass="trheader" />
                <PagerStyle CssClass="DDFooter" />
                <PagerTemplate>
                    <asp:GridViewPager ID="GridViewPager1" runat="server" />
                </PagerTemplate>
                <RowStyle CssClass="trgris"></RowStyle>
            </asp:GridView>
            <asp:LinqDataSource ID="ldsies" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
                EntityTypeName="" TableName="Establecimiento_Educativo" Select="new (IdIE, CodigoDane, Nombre, Telefono, Municipio, Rector)">
            </asp:LinqDataSource>
        </div>
        <div id="ModMediciones" runat="server" visible="false">
            <h4>
                * Datos Basicos del Establecimiento Educativo.</h4>
            <style type="text/css">
                .gvMediciones
                {
                    border: 1px solid #dddddd;
                }
            </style>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border: 1px solid #dddddd;">
                <tr class="trheader">
                    <td colspan="2">
                        Información de Acta de Visita EE
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="tds">
                        Codigo DANE:<b><asp:Label Text="Codigo Dane" ID="lblCodIe" runat="server" Font-Size="14px" /></b>
                    </td>
                    <td class="tds">
                        Nombre:<b><asp:Label ID="lblIE" Text="Institucion Educativa" Font-Size="14px" runat="server"></asp:Label></b>
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="2" class="tds">
                        Municipio: <b>
                            <asp:Label ID="lblMunicipio" Text="Municipio" Font-Size="14px" runat="server" /></b>
                    </td>
                    <%--<td>
                                Actor Evaluado:
                                <asp:Label ID="lblActorEvaluado" Text="Secretaria de Educación" runat="server" />
                            </td>--%>
                </tr>
            </table>
            <asp:GridView ID="gvMediciones" runat="server" Width="100%" OnSelectedIndexChanged="gvMediciones_SelectedIndexChanged"
                CssClass="gvMediciones">
                <Columns>
                    <asp:CommandField ButtonType="Link" SelectText="<img  width='24px' src='/Icons/Calender.png' alt='Seleccionar Medicion'>"
                        ShowSelectButton="True" />
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
                .style3
                {
                    font-size: medium;
                }
            </style>
            <table id="acta" style="max-width: 100%; -moz-border-radius: 3px; -webkit-border-radius: 3px;
                border-radius: 3px; /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 1px #000000; -webkit-box-shadow: 0px 0px 1px #000000; box-shadow: 0px 0px 1px #000000;
                font-size: 13px; /*ie 7 and 8 do not support blur property of shadows*/
 width: 100%;" cellpadding="0" cellspacing="0">
                <tr class="trheader">
                    <td style="text-align: center;" colspan="7">
                        ACTA DE VISITA
                    </td>
                </tr>
                <tr class="trheader">
                    <td style="text-align: center;" colspan="7">
                        1. INFORMACIÓN DEL ESTABLECIMIENTO EDUCTIVO
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="7">
                        1.1 Información básica del Establecimiento :
                    </td>
                </tr>
                <tr class="trblanca">
                    <td style="width: 150px;">
                        a. Nombre EE:
                    </td>
                    <td colspan="6">
                        <asp:TextBox ID="txtNombreEE" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td style="width: 150px;">
                        b. Código DANE:
                    </td>
                    <td colspan="6">
                        <asp:TextBox ID="txtCodigoDANE" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trblanca">
                    <td style="width: 150px;">
                        Dirección Sede Principal:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtDireccion" runat="server" Width="100%" Style="margin-bottom: 0px"></asp:TextBox>
                    </td>
                    <td style="width: 140px;">
                        Teléfonos:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtTelefonoEE" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td style="width: 150px;">
                        Fecha Inicial:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtFechaInicial" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td style="width: 140px;">
                        Fecha Final:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtFechaFinal" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trheader">
                    <td style="text-align: center;" colspan="7">
                        2. PERSONAS QUE SUMINISTRAN LA INFORMACIÓN
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="7" dir="ltr">
                        <div class="style1">
                            <span class="style3">Seleccion de actor</span>:
                            <asp:DropDownList ID="cboActores" runat="server" DataTextField="Actor" DataValueField="IdActor"
                                AutoPostBack="true" Font-Size="16px" OnSelectedIndexChanged="cboActores_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <asp:LinqDataSource ID="lqdsActores" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
                            EntityTypeName="" Select="new (IdActor, Actor)" TableName="Actores" Where="IdRama == @IdRama &amp;&amp; IdActor != @IdActor">
                            <WhereParameters>
                                <asp:Parameter DefaultValue="2" Name="IdRama" Type="Int32" />
                                <asp:Parameter DefaultValue="2" Name="IdActor" Type="Int32" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="2">
                        Nombre:
                    </td>
                    <td colspan="2" style="width: 40%;">
                        <asp:TextBox ID="txtNombre" runat="server" Width="100%" Enabled="False"></asp:TextBox>
                        <br />
                    </td>
                    <td style="width: 10%">
                        Teléfono
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtTelefono" runat="server" Width="100%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="2">
                        Correo Electrónico:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtCorreo" runat="server" Width="100%" Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label Visible="False" Text="Áreas de Enseñanza:" ID="lblAEnsenansa" runat="server"></asp:Label>
                        <asp:TextBox Visible="false" ID="txtGensenanza" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="3">
                        <asp:Label Visible="false" Text="Grados de Enseñanza:" ID="lblGradosEnsenanza" runat="server"></asp:Label>
                        <asp:TextBox Visible="false" ID="txtGradosEnsenanza" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label Text="Grados que cursan los hijos:" runat="server" Visible="False" ID="lblgh" />
                        <asp:TextBox Visible="false" ID="txtGradoHijos" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="3">
                        <asp:Label Visible="false" ID="lblNivelEducativo" runat="server" Text="Nivel Educativo"
                            AssociatedControlID="cboNivelesEducativos"></asp:Label>
                        <asp:DropDownList Visible="false" ID="cboNivelesEducativos" runat="server" DataSourceID="lqsdNivelEducacion"
                            DataTextField="NivelEducativo" DataValueField="IdNivelEducativo" Font-Size="14px">
                        </asp:DropDownList>
                        <asp:LinqDataSource ID="lqsdNivelEducacion" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
                            EntityTypeName="" Select="new (IdNivelEducativo, NivelEducativo)" TableName="NivelesEducativos">
                        </asp:LinqDataSource>
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <asp:Label Visible="false" ID="lblGrado" runat="server" Text="Grado:"></asp:Label>
                        <asp:TextBox Visible="false" ID="txtGrado" runat="server" CssClass="numerico">0</asp:TextBox>
                        <asp:Label Visible="false" ID="lblCargo" runat="server" Text="Cargo"></asp:Label>
                        <asp:TextBox Visible="false" ID="txtCargo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="7">
                        <asp:Button ID="btnAlmacenar" Text="Almacenar" runat="server" OnClick="btnAlmacenar_Click" />
                    </td>
                </tr>
                <tr class="trheader">
                    <td style="text-align: center;" colspan="7">
                        * CARGAR DOCUMENTOS
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="7" style="text-align: center;">
                        <a id="documentos" runat="server" href="#" class="pretty">Cargar Documentos</a>
                    </td>
                </tr>
                <tr class="trheader">
                    <td style="text-align: center;" colspan="7">
                        <h3>
                            Directivos:</h3>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:GridView runat="server" ID="gvDirectivos" Width="100%" AllowSorting="True" AutoGenerateColumns="false">
                            <AlternatingRowStyle CssClass="trblanca" />
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                                <asp:BoundField DataField="CorreoElectronico" HeaderText="Correo Electrónico" />
                                <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
                            </Columns>
                            <HeaderStyle CssClass="trheader" />
                            <RowStyle CssClass="trgris" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr class="trheader">
                    <td style="text-align: center;" colspan="7">
                        <h3>
                            Estudiantes:</h3>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:GridView runat="server" ID="gvEstudiantes" Width="100%" AutoGenerateColumns="false">
                            <AlternatingRowStyle CssClass="trblanca" />
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                                <asp:BoundField DataField="CorreoElectronico" HeaderText="Correo Electrónico" />
                                <asp:BoundField DataField="Grado" HeaderText="Grado" />
                            </Columns>
                            <HeaderStyle CssClass="trheader" />
                            <RowStyle CssClass="trgris" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr class="trheader">
                    <td style="text-align: center;" colspan="7">
                        <h3>
                            Educadores:</h3>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:GridView runat="server" ID="gvEducadores" Width="100%" AutoGenerateColumns="false">
                            <AlternatingRowStyle CssClass="trblanca" />
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                                <asp:BoundField DataField="CorreoElectronico" HeaderText="Correo Electrónico" />
                                <asp:BoundField DataField="AreasEnseñansa" HeaderText="Areas Enseñanza" />
                                <asp:BoundField DataField="GradosEnseñansa" HeaderText="Grados Enseñanza" />
                            </Columns>
                            <HeaderStyle CssClass="trheader" />
                            <RowStyle CssClass="trgris" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr class="trheader">
                    <td style="text-align: center;" colspan="7">
                        <h3>
                            Padres de Familia:</h3>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:GridView runat="server" ID="gvPadresFamilia" Width="100%" AutoGenerateColumns="false">
                            <AlternatingRowStyle CssClass="trblanca" />
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                                <asp:BoundField DataField="CorreoElectronico" HeaderText="Correo Electrónico" />
                                <asp:BoundField DataField="GradoHijos" HeaderText="Grado Hijos" />
                                <asp:BoundField DataField="NivelEducativo" HeaderText="Nivel Educativo" />
                            </Columns>
                            <HeaderStyle CssClass="trheader" />
                            <RowStyle CssClass="trgris" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr class="trheader">
                    <td style="text-align: center;" colspan="7">
                        OBSERVACIONES
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="7">
                        <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        <asp:Button Text="Almacenar Acta" ID="btnAlmacenarActa" runat="server" OnClick="btnAlmacenarActa_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
    </div>
</asp:Content>
