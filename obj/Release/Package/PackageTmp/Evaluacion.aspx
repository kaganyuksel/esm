<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Evaluacion.aspx.cs" Inherits="ESM.Evaluacion.Evaluacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager"
    TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Pretty/css/prettyPhoto.css" rel="stylesheet" charset="utf-8"
        media="screen" type="text/css" />
    <script src="/Pretty/js/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
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
    </script>
    <script type="text/javascript">

        function Ayuda(id) {
            alert(id);
        }
    
    </script>
    <style>
        #format
        {
            margin-top: 2em;
        }
    </style>
    <style>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .demo h1, h3, h4
        {
            color: #005EA7;
            font-weight: bold;
        }
    </style>
    <div class="demo" style="width: 90%; margin: 50px auto;">
        <section>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <img src="/Icons/Edit.png" alt="Evaluacion" />
                    </td>
                    <td style="vertical-align: middle; font-size: 13px;">
                        <h1 style="color: #0b72bc;">
                            Formulario de Evaluación</h1>
                        Administra la configuración para la evaluación a presentar.
                    </td>
                </tr>
            </table>
        </section>
        <br />
        <br />
        <asp:UpdatePanel ID="udpnlFiltro" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="gvResultados" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <h3 id="titulo1ie" runat="server">
                    1. Seleccione el Establecimiento Educativo a evaluar.
                </h3>
                <br />
                <div>
                    <p id="filtrosp" runat="server">
                        Busqueda de Establecimiento Educativo:
                        <asp:TextBox runat="server" ID="txtFiltro" />
                        <%--<input type="button" name="btnFiltro" value="Buscar" onclick="buscar();" />--%>
                        <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CausesValidation="false"
                            UseSubmitBehavior="true" OnClick="Unnamed2_Click" />
                    </p>
                    <asp:GridView ID="gvResultados" runat="server" AllowPaging="True" AllowSorting="True"
                        CssClass="gvResultados" RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6"
                        AutoGenerateColumns="False" OnSelectedIndexChanged="gvResultados_SelectedIndexChanged">
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
                            <asp:CommandField SelectText="Evaluar" ShowSelectButton="True" ControlStyle-CssClass="a" />
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
                </div>
                <style type="text/css">
                    #ContentPlaceHolder1_lbtnVolver
                    {
                        text-decoration: none;
                        color: #005EA7;
                    }
                </style>
                <div>
                    <style>
                        .volver
                        {
                            float: right;
                            margin-top: 0;
                        }
                    </style>
                    <h3 id="titulo3" runat="server" visible="false" style="width: 100%;">
                        2. Reliza el proceso de evaluación a el Establecimiento Educativo respectivo.
                        <asp:LinkButton Visible="false" Text="Volver" runat="server" ID="lbtnVolver" OnClick="lbtnVolver_Click" />
                    </h3>
                </div>
                <br />
                <div>
                    <h4 id="titulo22" runat="server" visible="false">
                        2.2 Listado de las ultimas evaluaciones realizadas para la Institucion Educativa.</h4>
                    <br />
                </div>
                <style type="text/css">
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
                </style>
                <asp:GridView ID="gvTopEval" CssClass="gvTopEval" runat="server" AutoGenerateColumns="False"
                    Width="100%" OnSelectedIndexChanged="gvTopEval_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="IdEvaluacion" HeaderText="Identificador de Evaluacion" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha de la Evaluación" />
                        <asp:BoundField DataField="IdMedicion" HeaderText="No. Identificación para Medición" />
                        <asp:CommandField ButtonType="Image" HeaderText="Cargar" SelectImageUrl="/Icons/Paste.png"
                            ShowSelectButton="True" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label Visible="false" Text='<%# Eval("IdActor") %>' runat="server" ID="lblidActor" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <h4 id="tituloeval" runat="server" visible="false">
                    2.3 Evaluación.</h4>
                <br />
                <div>
                    <table id="infoEval" runat="server" border="0" visible="false" cellpadding="0" cellspacing="0"
                        style="width: 100%; border: 1px solid #dddddd;">
                        <tr class="trheaderSecretaria" id="trActor" runat="server">
                            <td colspan="2" style="color: #ffffff; font-weight: bold; font-size: 18px;">
                                <asp:Panel ID="Actorespnl" runat="server">
                                    Evaluando a:
                                    <asp:DropDownList ID="cboActores" runat="server" AutoPostBack="True" DataTextField="Actor"
                                        DataValueField="IdActor" Font-Size="18px" OnSelectedIndexChanged="cboActores_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Label Text="Error" ID="lblerrorAc" runat="server" Visible="false" />
                                    <asp:LinqDataSource ID="ldsActores" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
                                        EntityTypeName="" Select="new (IdActor, Actor)" TableName="Actores">
                                    </asp:LinqDataSource>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr class="trheader">
                            <td colspan="2">
                                Información de Evaluación
                            </td>
                        </tr>
                        <tr class="trgris">
                            <td>
                                Codigo Dane:<asp:Label Text="Codigo Dane" ID="lblCodIe" runat="server" />
                            </td>
                            <td>
                                Institucion Educativa:<asp:Label ID="lblIE" Text="Institucion Educativa" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="trblanca">
                            <td>
                                Municipio:
                                <asp:Label ID="lblMunicipio" Text="Municipio" runat="server" />
                            </td>
                            <td>
                                Actor Evaluado:
                                <asp:Label ID="lblActorEvaluado" Text="Secretaria de Educación" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvEvaluacion" CssClass="evaluacion" runat="server" AutoGenerateColumns="False"
                        OnRowDataBound="gvEvaluacion_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Pregunta" HeaderText="Preguntas" HeaderStyle-CssClass="evaluacionth" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPregunta" runat="server" Text='<%# Eval("IdPregunta") %>' Visible="false"></asp:Label>
                                    <a id="lknAyuda" href="#" class="pretty" runat="server">
                                        <img style="line-height: 30px;" src="../Icons/1314381320_help_48.png" height="24px"
                                            alt="Ayuda" /></a>
                                    <asp:RadioButton ID="rbtnSi" GroupName="gpregunta" Text="Si" runat="server" />
                                    <asp:RadioButton ID="rbtnNo" GroupName="gpregunta" Text="No" runat="server" />
                                    <asp:RadioButton Visible="false" ID="rbtnNoAplica" GroupName="gpregunta" Text="No Aplica"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                </div>
                <asp:Button Text="Almacenar Evaluacion" runat="server" ID="btnalmacenarparcial" OnClick="btnalmacenarparcial_Click"
                    Visible="false" />
                <asp:Button Text="Actualizar Evaluacion" runat="server" ID="btnDefinitiva" Visible="false"
                    OnClick="btnDefinitiva_Click" />
                <style type="text/css">
                    .labelok
                    {
                        margin: 0 auto;
                    }
                </style>
                <div style="width: 100%; color: #3f9c0d; text-align: center; margin: 0 auto; font-weight: bold;">
                    <label class="labelok" runat="server" id="lbloki">
                    </label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
