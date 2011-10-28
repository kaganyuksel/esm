﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SistematizacionSE.aspx.cs" Inherits="ESM.SistematizacionSE" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager"
    TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        p.MsoNormal
        {
            margin-top: 0cm;
            margin-right: 0cm;
            margin-bottom: 10.0pt;
            margin-left: 0cm;
            line-height: 115%;
            font-size: 11.0pt;
            font-family: "Calibri" , "sans-serif";
        }
        .style1
        {
            text-align: justify;
            text-indent: -18.0pt;
            line-height: 115%;
            font-size: 11.0pt;
            font-family: Calibri, sans-serif;
            margin-left: 36.0pt;
            margin-right: 0cm;
            margin-top: 0cm;
            margin-bottom: .0001pt;
        }
        .style2
        {
            text-align: justify;
            text-indent: -18.0pt;
            line-height: 115%;
            font-size: 11.0pt;
            font-family: Calibri, sans-serif;
            margin-left: 36.0pt;
            margin-right: 0cm;
            margin-top: 0cm;
            margin-bottom: 10.0pt;
        }
        .style3
        {
            text-align: justify;
            line-height: 115%;
            font-size: 11.0pt;
            font-family: Calibri, sans-serif;
            margin-left: 36.0pt;
            margin-right: 0cm;
            margin-top: 0cm;
            margin-bottom: 10.0pt;
        }
        .style4
        {
            text-align: justify;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo" style="width: 85%; margin: 0 auto;">
        <br />
        <div>
            <table>
                <tr>
                    <td>
                        <img src="/Icons/Edit.png" alt="Evaluacion" />
                    </td>
                    <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                        <h1 style="color: #0b72bc;">
                            Instrumento de Sistematización para Secretaría de Educación</h1>
                        Componente Evaluación, Sistematización y Monitoreo
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <div id="ModseleccionSE" runat="server">
            <asp:GridView ID="gvSE" runat="server" AutoGenerateColumns="false" Width="100%" OnSelectedIndexChanged="gvSE_SelectedIndexChanged">
                <AlternatingRowStyle CssClass="trblanca" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="<img id='imgEvaluar'  height='24px' src='/Icons/Stationery.png' alt='Evaluar' />" />
                    <asp:BoundField DataField="IdSecretaria" HeaderText="Identificación" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre Secretaría" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                </Columns>
                <HeaderStyle CssClass="trheader" />
                <RowStyle CssClass="trgris" />
            </asp:GridView>
        </div>
        <br />
        <div id="ModMediciones" runat="server">
            <h4 id="titulomediciones" runat="server" visible="false" style="color: #0b72bc;">
                Listado de Mediciones Realizadasstado de Mediciones Realizadas</h4>
            <asp:GridView ID="gvMediciones" runat="server" Width="100%" Visible="False" AutoGenerateColumns="False"
                OnSelectedIndexChanged="gvMediciones_SelectedIndexChanged">
                <AlternatingRowStyle CssClass="trblanca" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="<img  width='24px' src='/Icons/Calender.png' alt='Seleccionar Medicion'>" />
                    <asp:BoundField DataField="IdMedicion" HeaderText="Identificador de Medición" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha de Medición" />
                </Columns>
                <HeaderStyle CssClass="trheader" />
                <RowStyle CssClass="trgris" />
            </asp:GridView>
            <br />
            <asp:Button Text="Registrar Nueva Medición" runat="server" ID="btnRegistrar" OnClick="btnRegistrar_Click"
                Visible="False" />
        </div>
        <br />
        <table id="LC" runat="server" visible="false" style="-moz-border-radius: 2px; -webkit-border-radius: 2px;
            border-radius: 2px; /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 1px #000000; -webkit-box-shadow: 0px 0px 1px #000000; box-shadow: 0px 0px 1px #000000;
            /*ie 7 and 8 do not support blur property of shadows*/">
            <tr class="trheader">
                <td>
                    <p align="center" class="MsoNormal" style="text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span>PROPUESTA SISTEMATIZACIÓN
                            <o:p></o:p>
                        </span></b>
                    </p>
                    <p align="center" class="MsoNormal" style="text-align: center">
                        <b style="mso-bidi-font-weight: normal"><span>Componente Evaluación, Sistematización
                            y Monitoreo<o:p></o:p></span></b></p>
                </td>
            </tr>
            <tr class="trblanca">
                <td>
                    <p class="style4">
                        <span>El proceso de sistematización busca recoger información del equipo nacional del
                            Programa de Competencias Ciudadanas y de los actores regionales de los EE y SE,
                            para identificar los logros, retos y lecciones aprendidas frente a las objetivos
                            y estrategias planteadas por cada componente durante el 2011. Además, busca recoger
                            información para conocer la mejor manera de articular los componentes a nivel de
                            SE y EE y a nivel nacional (internamente entre los componentes del Programa). Por
                            último busca, con base en el análisis de la información recogida, dar recomendaciones
                            generales para fortalecer el Programa y mejorar su ejecución durante el periodo
                            2012 – 2014.<o:p></o:p></span></p>
                    <p class="style4">
                        <o:p></o:p>
                    </p>
                    <p class="style4">
                        &nbsp;</p>
                    <p class="style4">
                        <span>Las preguntas que están dirigidas a rectores, docentes, funcionarios de la SE
                            y personas del equipo nacional son las siguientes:<o:p></o:p></span></p>
                    <p class="style4">
                        <o:p></o:p>
                    </p>
                    <p class="style4">
                        <o:p></o:p>
                    </p>
                </td>
            </tr>
            <tr class="trgris">
                <td>
                    <p class="style1" style="mso-add-space: auto; text-justify: inter-ideograph; mso-list: l0 level1 lfo1">
                        <![if !supportLists]><span>1)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><![endif]> <span>
                            Según el objetivo del componente&nbsp;para el período (enunciar el año o semestre),
                            ¿cree usted que se cumplió el objetivo?&nbsp;<o:p></o:p></span></p>
                    <p class="style3" style="mso-add-space: auto; text-justify: inter-ideograph;">
                        <span>Justifique la respuesta (Espacio para que escriba no menos de 5 renglones ni más
                            de 10)<o:p></o:p></span></p>
                </td>
            </tr>
            <tr class="trblanca">
                <td>
                    <asp:TextBox ID="txt1" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr class="trgris">
                <td>
                    <p class="style1" style="mso-add-space: auto; text-justify: inter-ideograph; mso-list: l0 level1 lfo1">
                        <![if !supportLists]><span>2)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><![endif]> <span>
                            ¿Qué acciones considera usted funcionaron del componente&nbsp;durante el período
                            (enunciar el año o semestre)?<o:p></o:p></span></p>
                    <p class="style3" style="mso-add-space: auto; text-justify: inter-ideograph;">
                        <span>Justifique para cada una de ellas porqué funcionaron. (Espacio para que escriba
                            no menos de 5 renglones ni más de 10)<o:p></o:p></span></p>
                </td>
            </tr>
            <tr class="trblanca">
                <td>
                    &nbsp;
                    <asp:TextBox ID="txt2" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr class="trgris">
                <td>
                    <p class="style1" style="mso-add-space: auto; text-justify: inter-ideograph; mso-list: l0 level1 lfo1">
                        <![if !supportLists]><span>3)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><![endif]> <span>
                            ¿Qué acciones considera usted que deberían cambiar del componente?<o:p></o:p></span></p>
                    <p class="style3" style="mso-add-space: auto; text-justify: inter-ideograph;">
                        <span>Justifique para cada una de ellas porqué funcionaron. (Espacio para que escriba
                            no menos de 5 renglones ni más de 10)<o:p></o:p></span></p>
                </td>
            </tr>
            <tr class="trblanca">
                <td>
                    <asp:TextBox ID="txt3" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr class="trgris">
                <td>
                    <p class="style1" style="mso-add-space: auto; text-justify: inter-ideograph; mso-list: l0 level1 lfo1">
                        <![if !supportLists]><span>4)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><![endif]> <span>
                            ¿Cómo considera usted que debería mejorar de la articulación de los componentes
                            del Programa de&nbsp;Competencias&nbsp;Ciudadanas, a nivel de SE y EE?<br clear="all" />
                            Proponga dos acciones con las cuales puede mejorarse la articulación entre los componente
                            del Programa de Competencias Ciudadanas con las Secretarías de Educación y los Establecimientos
                            Educativos.<o:p></o:p></span></p>
                    <p class="style3" style="mso-add-space: auto; text-justify: inter-ideograph;">
                        <span>(Puede ser una acción relacionada con las SE y otra con los EE)<o:p></o:p></span></p>
                </td>
            </tr>
            <tr class="trblanca">
                <td>
                    <asp:TextBox ID="txt4" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr class="trgris">
                <td>
                    <p class="style2" style="mso-add-space: auto; text-justify: inter-ideograph; mso-list: l0 level1 lfo1">
                        <![if !supportLists]><span>5)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><![endif]> <span>
                            ¿Cómo se imagina debe ser la articulación de su componente con respecto a los otros
                            componentes? (EQUIPO NACIONAL).<o:p></o:p></span></p>
                </td>
            </tr>
            <tr class="trblanca">
                <td>
                    <asp:TextBox ID="txt5" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr class="trgris">
                <td>
                    <p class="style2" style="mso-add-space: auto; text-justify: inter-ideograph; mso-list: l0 level1 lfo1">
                        <![if !supportLists]><span>6)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><![endif]> <span>
                            ¿Qué cree usted que fortalecería el Programa de Competencias Ciudadanas para el
                            2012?<o:p></o:p></span></p>
                </td>
            </tr>
            <tr class="trgris">
                <td>
                    <asp:TextBox ID="txt6" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr class="trblanca">
                <td>
                    <asp:Button Text="Almacenar" runat="server" ID="btnalmacenar" OnClick="btnalmacenar_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <br />
</asp:Content>
