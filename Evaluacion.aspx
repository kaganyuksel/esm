<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Evaluacion.aspx.cs" Inherits="ESM.Evaluacion.Evaluacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager"
    TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Pretty/css/prettyPhoto.css" rel="stylesheet" charset="utf-8" media="screen"
        type="text/css" />
    <script src="/Pretty/js/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#dialog:ui-dialog").dialog("destroy");

            $("#dtimer").dialog({
                height: 140,
                modal: true,
                autoOpen: false,
                buttons: {
                    Ok: function () {
                        $(this).dialog("close");
                    }
                }
            });

        });

        $(function () {
            $(".checkclass").buttonset();
        });
        $(function () {
            $("#accordionie").accordion({
                autoHeight: true,
                collapsible: true,
                navigation: true
            });

        });

        $(function () {
            $("#ContentPlaceHolder1_rbtnEstudiante").change(function () {
                alert("cambio");
                if ($("#ContentPlaceHolder1_lblActorEvaluado").val() == "Estudiantes") {
                    $("#ContentPlaceHolder1_trActor").css("background", "#c4c4c4");
                }
            });
        });
        function cambiarcolor() {
            $("#ContentPlaceHolder1_trActor").css("background", "#c4c4c4");
        }

        $(function () {
            $("#tabs").tabs().find(".ui-tabs-nav").sortable({ axis: "x" });
        });

    </script>
    <script type="text/javascript">

        $(document).ready(function () {

            $(".sesiones").change(function () {
                var text = $(this).val();

                if (!isNaN(text)) {
                    $(this).val() = "";
                }

            });
        });

        $(document).ready(function () {
            $("#contenteval").scrollTop(600);
        });
        
    </script>
    <style type="text/css">
        #format
        {
            margin-top: 2em;
        }
        
        .evaluacion
        {
            -moz-border-radius: 3px 3px 0px 0px;
            -webkit-border-radius: 3px 3px 0px 0px;
            border-radius: 3px 3px 0px 0px; /*IE 7 AND 8 DO NOT SUPPORT BORDER RADIUS*/
            border: 1px solid #dddddd;
            width: 100%;
        }
        .evaluacion td, th, tr
        {
            text-align: left;
        }
        .gvResultados
        {
            -moz-border-radius: 3px 3px 0px 0px;
            -webkit-border-radius: 3px 3px 0px 0px;
            border-radius: 3px 3px 0px 0px; /*IE 7 AND 8 DO NOT SUPPORT BORDER RADIUS*/
            border: 1px solid #dddddd;
            width: 100%;
        }
        
        .gvResultados tr, td, th
        {
            text-align: center;
        }
        .a
        {
            color: #000000;
            text-decoration: none;
        }
        .evaluacion
        {
            width: 100%;
            margin: 0 auto;
            text-align: left;
        }
        .evaluacionth
        {
            text-align: left;
            padding: 3px;
        }
        .flotante
        {
            display: scroll;
            position: fixed;
            bottom: 320px;
            right: 0px;
            margin-right: 3%;
            z-index: 99;
        }
    </style>
    <script type="text/javascript">
        function buscar() {
            $.ajax({
                url: "Evaluacion.aspx?text=" + $("#ContentPlaceHolder1_txtFiltro").val(),
                async: false,
                succes: function () {
                    alert("Actualizado Ajax");
                },
                error: function (result) {
                    alert("Error:" + result.status + " Estatus: " + result.statusText);
                }
            });

            $('#results').fadeOut('slow').load('reload.php').fadeIn("slow");
        }

        $("a.pretty").prettyPhoto({
            ie6_fallback: true,
            modal: true,
            social_tools: false,
            

        });

        function sesionenable(padre){
            var id = $(padre).parent().attr("alt")
            if ($(".sesion_" + id).attr("disabled")) {
                $(".sesion_" + id).attr("disabled", false);
            }
        }
        function sesiondisabled(padre){
            var id = $(padre).parent().attr("alt")
            if (!$(".sesion_" + id).is(":disabled")) {
                $(".sesion_" + id).attr("disabled", "disabled");
                $(".sesion_" + id).val(" ");
            }
        }

         $("#tabs").tabs({
                    remote: true, cache: true,
                    show: function (event, ui) {
                        var sel = $('#tabs').tabs('option', 'selected');
                        sel = sel+1;
                        $("#hidLastTab").val(sel);
                    },
                    selected: $("#hidLastTab").val()
                });
    </script>
    <style type="text/css">
        .demo h1, h3, h4
        {
            color: #005EA7;
        }
        .gvTopEval
        {
            line-height: 30px;
            border: 1px solid #dddddd;
        }
        .gvTopEval input[type=image]
        {
            line-height: 30px;
            height: 30px;
        }
        .gvMediciones
        {
            border: 1px solid #dddddd;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="contenteval" class="demo" style="width: 90%; margin: 50px auto;">
        <section>
        
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <img src="/Icons/Edit.png" alt="Evaluacion" />
                    </td>
                    <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                        <h1 style="color: #0b72bc;">
                            Formulario de Evaluación</h1>
                        Administra la configuración para la evaluación a presentar.
                    </td>
                </tr>
            </table>
        </section>
        <br />
        <br />
        <input type="hidden" id="hidLastTab" value="0" />
        <asp:UpdatePanel ID="udpnlFiltro" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="objtimer" />
                <asp:PostBackTrigger ControlID="gvResultados" />
            </Triggers>
            <ContentTemplate>
                <asp:Timer ID="objtimer" runat="server" Interval="120000" OnTick="objtimer_Tick">
                </asp:Timer>
                <div id="modSEseleccion" runat="server" visible="false">
                    <h3>
                        * Seleccione la Secretaría de Educacion a Evaluar.
                    </h3>
                    <br />
                    <asp:GridView ID="gvSE" runat="server" AllowPaging="True" AllowSorting="True" CssClass="gvResultados"
                        RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6" OnSelectedIndexChanged="gvSE_SelectedIndexChanged"
                        DataSourceID="lqdsSE" OnPageIndexChanging="gvSE_PageIndexChanging">
                        <HeaderStyle CssClass="trheader" />
                        <Columns>
                            <asp:TemplateField Visible="false" SortExpression="IDSE" HeaderText="IDSE">
                                <ItemTemplate>
                                    <asp:Label Text='<%# Eval("IdSecretaria") %>' runat="server" ID="IDIE"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField SelectText="<img  height='24px' src='/Icons/Stationery.png' alt='Evaluar' />"
                                ShowSelectButton="True" ControlStyle-CssClass="a" />
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
                    <asp:LinqDataSource ID="lqdsSE" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
                        EntityTypeName="" Select="new (IdSecretaria, Nombre, Direccion, Telefono, LecturaContextoSE)"
                        TableName="Secretaria_Educacion">
                    </asp:LinqDataSource>
                </div>
                <div id="modEESeleccion" runat="server" visible="false">
                    <h3>
                        1. Seleccione el Establecimiento Educativo a evaluar.
                    </h3>
                    <br />
                    <p id="filtrosp" runat="server">
                        Búsqueda de Establecimiento Educativo:
                        <asp:TextBox runat="server" ID="txtFiltro" />
                        <%--<input type="button" name="btnFiltro" value="Buscar" onclick="buscar();" />--%>
                        <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CausesValidation="false"
                            UseSubmitBehavior="true" OnClick="Unnamed2_Click" />
                    </p>
                    <asp:GridView ID="gvResultados" runat="server" AllowPaging="True" AllowSorting="True"
                        CssClass="gvResultados" RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6"
                        AutoGenerateColumns="false" OnSelectedIndexChanged="gvResultados_SelectedIndexChanged"
                        OnPageIndexChanging="gvResultados_PageIndexChanging">
                        <AlternatingRowStyle CssClass="trblanca" />
                        <Columns>
                            <asp:CommandField SelectText="<img id='imgEvaluar'  height='24px' src='/Icons/Stationery.png' alt='Evaluar' />"
                                ShowSelectButton="True" ControlStyle-CssClass="a" >
                            <ControlStyle CssClass="a" />
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
                        <HeaderStyle CssClass="trheader"></HeaderStyle>
                        <PagerStyle CssClass="DDFooter" />
                        <PagerTemplate>
                            <asp:GridViewPager ID="GridViewPager1" runat="server" />
                        </PagerTemplate>
                        <EmptyDataTemplate>
                            Actualmente no hay elementos en esta tabla.
                        </EmptyDataTemplate>
                        <RowStyle CssClass="trgris"></RowStyle>
                    </asp:GridView>
                    <asp:LinqDataSource ID="ldsies" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
                        EntityTypeName="" TableName="Establecimiento_Educativos">
                    </asp:LinqDataSource>
                </div>
                <div id="ModMediciones" runat="server" visible="false">
                    <h4>
                        * Datos básicos del establecimiento educativo.</h4>
                    <br />
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border: 1px solid #dddddd;">
                        <tr class="trheader">
                            <td colspan="2">
                                Información de Evaluación
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
                    <asp:Button Text="Realizar Nueva Medición" Visible="true" runat="server" ID="btnMedicion"
                        OnClick="btnMedicion_Click" />
                    <a id="anchor" href="#"></a>
                </div>
                <div id="ModTopEval" runat="server" visible="false">
                    <h3>
                        * Listado de las últimas evaluaciones realizadas para el establecimiento educativo.</h3>
                    <br />
                    <asp:GridView ID="gvTopEval" CssClass="gvTopEval" runat="server" AutoGenerateColumns="false"
                        Width="100%" OnSelectedIndexChanged="gvTopEval_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ButtonType="Link" SelectText="<img  width='24px' src='/Icons/Paste.png' alt='Seleccionar Medicion'>"
                                ShowSelectButton="True" />
                            <asp:BoundField DataField="No_Evaluacion" HeaderText="No. Evaluación" />
                            <asp:BoundField DataField="No_Actor" HeaderText="No. Actor" />
                            <asp:BoundField DataField="Actor" HeaderText="Actor" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" />
                            <asp:BoundField DataField="Medicion" HeaderText="Medición" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="ModDocumentos" runat="server" visible="false">
                    <h3>
                        * Carga documentos de verificación</h3>
                    <a id="adocumentos" class="pretty" runat="server" style="cursor: pointer; text-decoration: none;
                        color: inherit;">Cargar Documentos
                        <img height="24px" src="Icons/Up.png" alt="Cargar" /></a>
                </div>
                <div id="ModEvaluacion" runat="server" visible="false">
                    <br />
                    <h3>
                        * Evaluación.
                    </h3>
                    <br />
                    <div>
                        <style type="text/css">
                            .aditionals
                            {
                                text-align: center;
                                margin: 0 auto;
                            }
                        </style>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border: 1px solid #dddddd;">
                            <tr class="trheaderSecretaria" id="trActor" runat="server">
                                <td id="ModAc" runat="server" class="tdactor" colspan="2" style="color: #ffffff;
                                    background: #005EA7; font-weight: bold; font-size: 18px;">
                                    <asp:Panel ID="Actorespnl" runat="server">
                                        Evaluando a:
                                        <asp:DropDownList ID="cboActores" runat="server" AutoPostBack="True" DataTextField="Actor"
                                            DataValueField="IdActor" Font-Size="18px" OnSelectedIndexChanged="cboActores_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label Text="Error" ID="lblerrorAc" runat="server" Visible="false" />
                                        <asp:LinqDataSource ID="ldsActores" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
                                            EntityTypeName="" Select="new (IdActor, Actor)" TableName="Actores" Where="IdRama != @IdRama">
                                            <WhereParameters>
                                                <asp:Parameter DefaultValue="1" Name="IdRama" Type="Int32" />
                                            </WhereParameters>
                                        </asp:LinqDataSource>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <div id="tabs">
                            <ul>
                                <li><a href="#tabs-1">Gestión institucional</a></li>
                                <li><a href="#tabs-2">Instancias de participación</a></li>
                                <li><a href="#tabs-3">Aula de clase</a></li>
                                <li><a href="#tabs-4">Proyecto Pedagógico</a></li>
                                <li><a href="#tabs-5">Tiempo Libre</a></li>
                            </ul>
                            <div id="tabs-1">
                                <asp:GridView ID="gvAmb1" CssClass="evaluacion" runat="server" AutoGenerateColumns="false"
                                    OnRowDataBound="gvAmb1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblidorden" runat="server" Text='<%# Eval("IdOrden") %>' Visible="true"></asp:Label>
                                                <asp:Label ID="lblIdPregunta" runat="server" Text='<%# Eval("IdPregunta") %>' Visible="false"></asp:Label>
                                                <asp:RadioButton ID="rbtnSi" onclick='sesionenable(this)' class="radiosi" alt='<%# Eval("IdPregunta") %>'
                                                    GroupName="gpregunta" Text="Si" runat="server" />
                                                <asp:RadioButton ID="rbtnNo" onclick="sesiondisabled(this)" GroupName="gpregunta"
                                                    alt='<%# Eval("IdPregunta") %>' Text="No" runat="server" />
                                                <asp:RadioButton Visible="false" ID="rbtnNoAplica" class="radiono" GroupName="gpregunta"
                                                    Text="No Aplica" runat="server" />
                                                <asp:CheckBox ID="chxPendiente" Visible="false" Text="Pendiente" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Pregunta1" HeaderStyle-Width="60%" HeaderText="Pregunta" />
                                        <asp:TemplateField HeaderText="Sesiones">
                                            <ItemStyle CssClass="aditionals" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsesion" CssClass="sesiones" runat="server" Width="30px" Height="30px"
                                                    Visible="false" MaxLength="3"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label Text="" ID="lblLP" Font-Size="14px" runat="server" Visible="false" /></b>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ayuda">
                                            <ItemTemplate>
                                                <a id="lknAyuda" href="#" style="width: 30px;" class="pretty" runat="server">
                                                    <img style="line-height: 30px;" src="../Icons/1314381320_help_48.png" height="24px"
                                                        alt="Ayuda" /></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div id="tabs-2">
                                <asp:GridView ID="gvAmb2" CssClass="evaluacion" runat="server" AutoGenerateColumns="false"
                                    OnRowDataBound="gvAmb2_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblidorden" runat="server" Text='<%# Eval("IdOrden") %>' Visible="true"></asp:Label>
                                                <asp:Label ID="lblIdPregunta" runat="server" Text='<%# Eval("IdPregunta") %>' Visible="false"></asp:Label>
                                                <asp:RadioButton ID="rbtnSi" onclick='sesionenable(this)' class="radiosi" alt='<%# Eval("IdPregunta") %>'
                                                    GroupName="gpregunta" Text="Si" runat="server" />
                                                <asp:RadioButton ID="rbtnNo" onclick="sesiondisabled(this)" GroupName="gpregunta"
                                                    alt='<%# Eval("IdPregunta") %>' Text="No" runat="server" />
                                                <asp:RadioButton Visible="false" ID="rbtnNoAplica" class="radiono" GroupName="gpregunta"
                                                    Text="No Aplica" runat="server" />
                                                <asp:CheckBox ID="chxPendiente" Visible="false" Text="Pendiente" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Pregunta1" HeaderStyle-Width="60%" HeaderText="Pregunta" />
                                        <asp:TemplateField HeaderText="Sesiones">
                                            <ItemStyle CssClass="aditionals" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsesion" CssClass="sesiones" runat="server" Width="30px" Height="30px"
                                                    Visible="false" MaxLength="3"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label Text="" ID="lblLP" Font-Size="14px" runat="server" Visible="false" /></b>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ayuda">
                                            <ItemTemplate>
                                                <a id="lknAyuda" href="#" style="width: 30px;" class="pretty" runat="server">
                                                    <img style="line-height: 30px;" src="../Icons/1314381320_help_48.png" height="24px"
                                                        alt="Ayuda" /></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div id="tabs-3">
                                <asp:GridView ID="gvAmb3" CssClass="evaluacion" runat="server" AutoGenerateColumns="false"
                                    OnRowDataBound="gvAmb3_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblidorden" runat="server" Text='<%# Eval("IdOrden") %>' Visible="true"></asp:Label>
                                                <asp:Label ID="lblIdPregunta" runat="server" Text='<%# Eval("IdPregunta") %>' Visible="false"></asp:Label>
                                                <asp:RadioButton ID="rbtnSi" onclick='sesionenable(this)' class="radiosi" alt='<%# Eval("IdPregunta") %>'
                                                    GroupName="gpregunta" Text="Si" runat="server" />
                                                <asp:RadioButton ID="rbtnNo" onclick="sesiondisabled(this)" GroupName="gpregunta"
                                                    alt='<%# Eval("IdPregunta") %>' Text="No" runat="server" />
                                                <asp:RadioButton Visible="false" ID="rbtnNoAplica" class="radiono" GroupName="gpregunta"
                                                    Text="No Aplica" runat="server" />
                                                <asp:CheckBox ID="chxPendiente" Visible="false" Text="Pendiente" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Pregunta1" HeaderStyle-Width="60%" HeaderText="Pregunta" />
                                        <asp:TemplateField HeaderText="Sesiones">
                                            <ItemStyle CssClass="aditionals" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsesion" CssClass="sesiones" runat="server" Width="30px" Height="30px"
                                                    Visible="false" MaxLength="3"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label Text="" ID="lblLP" Font-Size="14px" runat="server" Visible="false" /></b>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ayuda">
                                            <ItemTemplate>
                                                <a id="lknAyuda" href="#" style="width: 30px;" class="pretty" runat="server">
                                                    <img style="line-height: 30px;" src="../Icons/1314381320_help_48.png" height="24px"
                                                        alt="Ayuda" /></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div id="tabs-4">
                                <asp:GridView ID="gvAmb4" CssClass="evaluacion" runat="server" AutoGenerateColumns="false"
                                    OnRowDataBound="gvAmb4_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblidorden" runat="server" Text='<%# Eval("IdOrden") %>' Visible="true"></asp:Label>
                                                <asp:Label ID="lblIdPregunta" runat="server" Text='<%# Eval("IdPregunta") %>' Visible="false"></asp:Label>
                                                <asp:RadioButton ID="rbtnSi" onclick='sesionenable(this)' class="radiosi" alt='<%# Eval("IdPregunta") %>'
                                                    GroupName="gpregunta" Text="Si" runat="server" />
                                                <asp:RadioButton ID="rbtnNo" onclick="sesiondisabled(this)" GroupName="gpregunta"
                                                    alt='<%# Eval("IdPregunta") %>' Text="No" runat="server" />
                                                <asp:RadioButton Visible="false" ID="rbtnNoAplica" class="radiono" GroupName="gpregunta"
                                                    Text="No Aplica" runat="server" />
                                                <asp:CheckBox ID="chxPendiente" Visible="false" Text="Pendiente" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Pregunta1" HeaderStyle-Width="60%" HeaderText="Pregunta" />
                                        <asp:TemplateField HeaderText="Sesiones">
                                            <ItemStyle CssClass="aditionals" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsesion" CssClass="sesiones" runat="server" Width="30px" Height="30px"
                                                    Visible="false" MaxLength="3"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label Text="" ID="lblLP" Font-Size="14px" runat="server" Visible="false" /></b>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ayuda">
                                            <ItemTemplate>
                                                <a id="lknAyuda" href="#" style="width: 30px;" class="pretty" runat="server">
                                                    <img style="line-height: 30px;" src="../Icons/1314381320_help_48.png" height="24px"
                                                        alt="Ayuda" /></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div id="tabs-5">
                                <asp:GridView ID="gvAmb5" CssClass="evaluacion" runat="server" AutoGenerateColumns="false"
                                    OnRowDataBound="gvAmb5_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblidorden" runat="server" Text='<%# Eval("IdOrden") %>' Visible="true"></asp:Label>
                                                <asp:Label ID="lblIdPregunta" runat="server" Text='<%# Eval("IdPregunta") %>' Visible="false"></asp:Label>
                                                <asp:RadioButton ID="rbtnSi" onclick='sesionenable(this)' class="radiosi" alt='<%# Eval("IdPregunta") %>'
                                                    GroupName="gpregunta" Text="Si" runat="server" />
                                                <asp:RadioButton ID="rbtnNo" onclick="sesiondisabled(this)" GroupName="gpregunta"
                                                    alt='<%# Eval("IdPregunta") %>' Text="No" runat="server" />
                                                <asp:RadioButton Visible="false" ID="rbtnNoAplica" class="radiono" GroupName="gpregunta"
                                                    Text="No Aplica" runat="server" />
                                                <br />
                                                <asp:CheckBox ID="chxPendiente" Visible="false" Text="Pendiente" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Pregunta1" HeaderStyle-Width="60%" HeaderText="Pregunta" />
                                        <asp:TemplateField HeaderText="Sesiones">
                                            <ItemStyle CssClass="aditionals" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsesion" CssClass="sesiones" runat="server" Width="30px" Height="30px"
                                                    Visible="false" MaxLength="3"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label Text="" ID="lblLP" Font-Size="14px" runat="server" Visible="false" /></b>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ayuda">
                                            <ItemTemplate>
                                                <a id="lknAyuda" href="#" style="width: 30px;" class="pretty" runat="server">
                                                    <img style="line-height: 30px;" src="../Icons/1314381320_help_48.png" height="24px"
                                                        alt="Ayuda" /></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                    </div>
                    <asp:Button Text="Guardar" runat="server" ID="btnalmacenarparcial" OnClick="btnalmacenarparcial_Click"
                        Visible="false" />
                    <asp:Button Text="Guardar y Bloquear" runat="server" ID="btnDefinitiva" Visible="false"
                        OnClick="btnDefinitiva_Click" />
                    <asp:Button Text="Volver" Visible="false" runat="server" ID="btnVolverEE" OnClick="btnVolver_Click" />
                    <style type="text/css">
                        .labelok
                        {
                            margin: 0 auto;
                        }
                    </style>
                    <p id="informacionuno" runat="server" visible="false" style="width: 300px; color: #8C8C8C;
                        font-size: 12px; text-align: justify;">
                        <b>Para tener en cuenta:</b> El botón "Guardar" almacena la información en estado
                        "Parcial" para editar si es necesario hacerlo. El botón "Guardar y Bloquear" almacena
                        la evaluación en estado "Cerrada" y no permite realizar cambios.
                    </p>
                </div>
                <div id="divmensaje" runat="server" style="height: 35px; top: 0; position: fixed;
                    background: green; color: #ffffff; width: 300px; margin: 0 30%; font-size: 12px;
                    text-align: center; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;
                    /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 1px #000000; -webkit-box-shadow: 0px 0px 1px #000000; box-shadow: 0px 0px 1px #000000;
                    /*ie 7 and 8 do not support blur property of shadows*/
opacity: 0.95; -ms-filter: progid:DXImageTransform.Microsoft.Alpha(Opacity=95); /*-ms-filter must come before filter*/
filter: alpha(opacity=95); /*inner elements must not break this elements boundaries*/
/*all filters must be placed together*/" visible="false">
                    <label class="labelok" runat="server" id="lbloki">
                    </label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdateProgress ID="udpgss" runat="server" AssociatedUpdatePanelID="udpnlFiltro"
        DisplayAfter="0">
        <ProgressTemplate>
            <div style="z-index: 301; -moz-border-radius: 4px; -webkit-border-radius: 4px; border-radius: 4px;
                /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 2px #000000; -webkit-box-shadow: 0px 0px 2px #000000; box-shadow: 0px 0px 2px #000000;
                /*ie 7 and 8 do not support blur property of shadows*/
/*inner elements must not break this elements boundaries*/
/*all filters must be placed together*/
 width: 20%; height: 40px; position: fixed; top: 0; background: #ffffff; margin: 0 40%;">
                <table style="width: 100%;">
                    <tr style="background: #ffffff;">
                        <td>
                            <img src="/Icons/progres.gif" alt="progress" />
                        </td>
                        <td style="text-align: left; vertical-align: middle;">
                            Cargando...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
