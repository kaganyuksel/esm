<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LecturaContextoEE.aspx.cs" Inherits="ESM.LecturaContextoEE" %>

<%@ Register Src="~/DynamicData/Content/GridViewPager.ascx" TagName="GridViewPager"
    TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
        .style2
        {
            text-align: left;
        }
        .style3
        {
            text-align: justify;
            color: #999999;
            font-size: small;
        }
        .style4
        {
            text-align: left;
            color: #800000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo" style="width: 90%; margin: 0 auto;">
        <br />
        <div>
            <table>
                <tr>
                    <td>
                        <img src="/Icons/Edit.png" alt="Evaluacion" />
                    </td>
                    <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                        <h1 style="color: #0b72bc;">
                            Formulario de Lectura de Contexto para Establecimientos Educativos</h1>
                        Administración de información para el formato Lectura de Contexto.
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
                <%--<input type="button" name="btnFiltro" value="Buscar" onclick="buscar();" />--%>
                <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CausesValidation="false"
                    UseSubmitBehavior="true" />
            </p>
            <asp:GridView ID="gvResultados" runat="server" AllowPaging="True" AllowSorting="True"
                CssClass="gvResultados" RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6"
                AutoGenerateColumns="false" Width="100%" OnSelectedIndexChanged="gvResultados_SelectedIndexChanged">
                <HeaderStyle CssClass="trheader" />
                <Columns>
                    <asp:CommandField SelectText="<img id='imgEvaluar'  height='24px' src='/Icons/Stationery.png' alt='Evaluar' />"
                        ShowSelectButton="True" ControlStyle-CssClass="a" />
                    <asp:BoundField DataField="CodigoDane" HeaderText="Cod. DANE" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Municipio" HeaderText="Municipio" />
                    <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                    <asp:TemplateField Visible="false" SortExpression="IDIE" HeaderText="IDIE">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("IdIE") %>' runat="server" ID="IDIE"></asp:Label>
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
        <br />
        <div id="ModMediciones" runat="server">
            <h4 id="titulomediciones" runat="server" visible="false" style="color: #0b72bc;">
                Listado de Mediciones Realizadas</h4>
            <asp:GridView ID="gvMediciones" runat="server" Width="100%" Visible="False" 
                AutoGenerateColumns="false" 
                onselectedindexchanged="gvMediciones_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="<img  width='24px' src='/Icons/Calender.png' alt='Seleccionar Medicion'>" />
                    <asp:BoundField DataField="IdMedicion" HeaderText="Identificador de Medicion" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha de Medicion" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button Text="Registrar Nueva Medición" runat="server" ID="btnRegistrar" OnClick="btnRegistrar_Click"
                Visible="False" />
        </div>
        <br />
        <asp:Panel ID="LC" runat="server" Visible="false">
            <h1 style="color: #005EA7;">
                <img src="/Icons/Tutorial.png" alt="Lectura Contexto EE" />Lectura de Contexto para
                el Establecimiento Educativo.</h1>
            <br />
            <br />
            <table style="width: 100%;">
                <tr>
                    <td class="style1" colspan="7">
                        LECTURA DE CONTEXTO<br />
                        ESTABLECIMIENTO EDUCATIVO (EE)
                    </td>
                </tr>
                <tr>
                    <td class="style3" colspan="7">
                        El propósito de este instrumento es tener la respuesta a algunas preguntas generales
                        de acciones adelantadas en la INSTITUCIÓN EDUCATIVA en torno a las competencias
                        ciudadanas, de tal manera que den un contexto particular que permita orientar de
                        mejor manera las acciones por ejecutar del programa de competencias ciudadanas del
                        MEN en esta Institución. Se recomienda además leer el instrumento de lectura de
                        contexto de la Secretaría de Educación, para tener mayor información de referencia.
                    </td>
                </tr>
                <tr style="height: 40px; vertical-align: middle; line-height: 40px;">
                    <td class="style1" colspan="7" style="background: #226E22; color: #ffffff;">
                        0. INFORMACIÓN PREVIA A LA VISITA
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="4">
                        PRUEBAS SABER EE: Resultados comparación 2003/2005 CIUDADANAS<br />
                        Consultar en el siguiente link:<br />
                        Link :<a href="http://www.icfes.gov.co/saber59/">http://www.icfes.gov.co/saber59/</a><br />
                        <br />
                    </td>
                    <td class="style4" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr style="height: 40px; vertical-align: middle; line-height: 40px;">
                    <td class="style1" colspan="7" style="background: #226E22; color: #ffffff;">
                        1. INFORMACIÓN DEL ESTABLECIMIENTO EDUCATIVO
                    </td>
                </tr>
                <tr>
                    <td>
                        1.1 Información básica del Establecimiento:
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        a.Nombre:
                        <asp:TextBox ID="txtNombre" runat="server" Width="100%"></asp:TextBox>
                        &nbsp; &nbsp; &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        b. Código DANE:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        c. Direccion sede principal:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        d.Teléfonos:
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtTelefonos" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        e. Correo electronico
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtCorreoElectronico" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        f. Jornadas que ofrece el EE
                    </td>
                    <td colspan="2">
                        <asp:CheckBoxList ID="cblistjornadas" runat="server">
                            <asp:ListItem>Completa</asp:ListItem>
                            <asp:ListItem>Mañana</asp:ListItem>
                            <asp:ListItem>Tarde</asp:ListItem>
                            <asp:ListItem>Noche</asp:ListItem>
                            <asp:ListItem>Fin de Semana</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    <td colspan="2">
                        d. Numero de sedes para EE:
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtSedes" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        1.2 Ubicación y localización física de la institución (Sede Principal)
                    </td>
                </tr>
                <tr>
                    <td>
                        a. Municipio
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtMunicipio" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        b.Zona
                        <asp:RadioButton ID="rbtnRural" runat="server" Text="Rural" />
                        <asp:RadioButton ID="rbtnUrbana" runat="server" Text="Urbana" />
                    </td>
                    <td colspan="2">
                        c.Tipo
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:CheckBoxList ID="cblistTipo" runat="server">
                            <asp:ListItem>Cabecera municipal</asp:ListItem>
                            <asp:ListItem>Corregimiento</asp:ListItem>
                            <asp:ListItem>Inspección</asp:ListItem>
                            <asp:ListItem>Caserio</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr style="height: 40px; vertical-align: middle; line-height: 40px;">
                    <td class="style1" colspan="7" style="background: #226E22; color: #ffffff;">
                        2. INFORMACIÓN DE ESTUDIANTES
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        2.1 Indique el número de estudiantes matriculados en 2011 según niveles SISBEN
                    </td>
                </tr>
                <tr>
                    <td>
                        Estudiantes Matriculados 2011:
                    </td>
                    <td colspan="2">
                        Total:
                        <asp:TextBox ID="txt21" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        2.2 Indique a continuación el porcentaje estimado de estudiantes matriculados en
                        2011 según SISBEN.
                    </td>
                </tr>
                <tr>
                    <td>
                        Estrato 1:
                    </td>
                    <td colspan="6">
                        <asp:TextBox ID="txtE1" runat="server"></asp:TextBox>
                        % de estudiantes &nbsp; &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Estrato 2:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtE2" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="3">
                        % de estudiantes &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Estrato 3:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtE3" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        % de estudiantes
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Estrato 4:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtE4" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        % de estudiantes
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Estrato 5:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtE5" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        % de estudiantes
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Estrato 6:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtE6" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        % de estudiantes
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td colspan="2">
                        100%
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        2.3 Indique a continuación el porcentaje estimado de estudiantes matriculados en
                        2011 según niveles SISBEN.
                    </td>
                </tr>
                <tr>
                    <td>
                        Sisben 1:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtS1" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="4">
                        % de estudiantes
                    </td>
                </tr>
                <tr>
                    <td>
                        Sisben 2:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtS2" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="4">
                        % de estudiantes
                    </td>
                </tr>
                <tr>
                    <td>
                        Sisben 3:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtS3" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="4">
                        % de estudiantes
                    </td>
                </tr>
                <tr>
                    <td>
                        Sisben 4:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtS4" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="4">
                        % de estudiantes
                    </td>
                </tr>
                <tr>
                    <td>
                        No Sabe:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtNoSabe" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="4">
                        % de estudiantes
                    </td>
                </tr>
                <tr>
                    <td>
                        No Tiene:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtNotiene" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="4">
                        % de estudiantes
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        100%
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        2.4 ¿El EE atiende población víctima del conflicto?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="7">
                        <asp:RadioButton ID="rbtnSi24" runat="server" Text="Si" AutoPostBack="True" GroupName="group24"
                            OnCheckedChanged="RadioButton1_CheckedChanged" />
                        <asp:RadioButton ID="rbtnNo24" runat="server" Text="No" AutoPostBack="True" GroupName="group24"
                            OnCheckedChanged="RadioButton2_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        2.5 En la siguiente tabla escriba el número de estudiantes matriculados por nivel
                        educativo victimas del conflicto matriculados en 2011
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="4">
                        En situación de desplazamiento
                    </td>
                    <td class="style1" colspan="3">
                        <asp:TextBox ID="txt25_1" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="4">
                        Desvinculados
                    </td>
                    <td class="style1" colspan="3">
                        <asp:TextBox ID="txt25_2" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="4">
                        Hijos de adultos desmovilizados
                    </td>
                    <td class="style1" colspan="3">
                        <asp:TextBox ID="txt25_3" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 40px; vertical-align: middle; line-height: 40px;">
                    <td class="style1" colspan="7" style="background: #226E22; color: #ffffff;">
                        3. INICIATIVAS DEL ESTABLECIMIENTO EN FORMACIÓN CIUDADANA
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        3.1 ¿Cuáles son los principales problemas que enfrentan el EE en temas de convivencia
                        escolar?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="7">
                        <asp:TextBox ID="txt31" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        3.2 ¿En el EE, cómo se manejan situaciones relacionadas con problematicas de convivencia
                        escolar (discriminación, matoneo, otros)?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="7">
                        <asp:TextBox ID="txt32" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        3.3 ¿Qué tan importante cree ustes que es el tema formación ciudadana en su establecimiento
                        educativo?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="7">
                        <asp:TextBox ID="txt33" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        3.4 Ha oído algo sobre CC?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="7">
                        <asp:RadioButton ID="rbtnSi34" runat="server" Text="Si" AutoPostBack="True" GroupName="Group34"
                            OnCheckedChanged="RadioButton3_CheckedChanged" />
                        <asp:RadioButton ID="rbtnNo34" runat="server" Text="No" AutoPostBack="True" GroupName="Group34"
                            OnCheckedChanged="RadioButton4_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        3.4.1 ¿Qué sabe de este tema ?(Competencias Ciudadanas)
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:TextBox ID="txt341" runat="server" TextMode="MultiLine" Width="100%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        3.5 ¿Tiene el EE un programa o proyecto de formación ciudadana?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="7">
                        <asp:RadioButton ID="rbtnSi35" runat="server" Text="Si" AutoPostBack="True" GroupName="Group35" />
                        <asp:RadioButton ID="rbtnNo35" runat="server" Text="No" AutoPostBack="True" GroupName="Group35" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        3.5.1 Nombre del programa o proyecto de formación ciudadana
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:TextBox ID="txt351" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        3.5.2 ¿Cuál es el objetivo de este programa/iniciativa?
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:TextBox ID="txt352" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        3.5.3 ¿A qué población/es está dirigido este programa/iniciativa?
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:TextBox ID="txt353" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        3.5.4¿Cómo lo hace? (horario, actores, espacio fisico, registro documental)
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:TextBox ID="txt354" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        3.5.5 ¿Quiénes lideran el desarrollo de esta iniciativa? (docentes, directivos,
                        estudiantes, otros)
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:TextBox ID="txt355" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        3.5.6 ¿En qué áreas académicas se desarrolla esta iniciativa?
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:TextBox ID="txt356" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        3.5.7 ¿Cómo realiza el seguimiento, monitoreo y evaluación este programa/iniciativa?
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:TextBox ID="txt357" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        &nbsp; 3.6 ¿Cuáles son las formas de participación de estudiantes, docentes, padres
                        de familia, personal, administrativo y de apoyo en el gobierno del EE?
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        &nbsp;
                        <asp:TextBox ID="txt36" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        &nbsp; 3.7 ¿Cuál es la experiencia con el Manual de Convivencia? ¿Quiénes lo construyeron?
                        ¿Cómo lo construyeron?
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:TextBox ID="txt37" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        &nbsp; 3.8 ¿El PEI del EE incluye de manera explícita el tema de ciudadanía?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="7">
                        &nbsp;
                        <asp:RadioButton ID="rbtnSi38" runat="server" Text="Si" AutoPostBack="True" GroupName="Group38" />
                        <asp:RadioButton ID="rbtnNo38" runat="server" Text="No" AutoPostBack="True" GroupName="Group38" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        &nbsp; 3.8.1 Explique como lo incluye
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        &nbsp;
                        <asp:TextBox ID="txt381" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        &nbsp; 3.9 ¿El PAM del EE incluye de manera explícita el tema de ciudadanía?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="7">
                        &nbsp;
                        <asp:RadioButton ID="rbtnSi39" runat="server" Text="Si" AutoPostBack="True" GroupName="Group39" />
                        <asp:RadioButton ID="rbtnNo39" runat="server" Text="No" AutoPostBack="True" GroupName="Group39" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        &nbsp; 3.9.1 Explique como lo incluye
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        &nbsp;
                        <asp:TextBox ID="txt391" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 40px; vertical-align: middle; line-height: 40px;">
                    <td class="style1" colspan="7" style="background: #226E22; color: #ffffff;">
                        &nbsp; 4. INFRAESTRUCTURA INFORMÁTICA
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        &nbsp; 4.1 ¿La EE tiene conexión a Internet?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="7">
                        &nbsp;
                        <asp:RadioButton ID="rbtnSi41" runat="server" Text="Si" AutoPostBack="True" GroupName="Group41" />
                        <asp:RadioButton ID="rbtnNo41" runat="server" Text="No" AutoPostBack="True" GroupName="Group41" />
                        <asp:RadioButton ID="rbtnAlgunnas41" runat="server" Text="Algunas de las sedes" AutoPostBack="True"
                            GroupName="Group41" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        &nbsp; 4.2 Indique el número de computadores en uso con acceso a Internet en la
                        IE:<asp:TextBox ID="txt42" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        &nbsp; 4.3 ¿La EE cuenta con un sitio Web?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="7">
                        &nbsp;
                        <asp:RadioButton ID="rbtnSi43" runat="server" Text="Si" AutoPostBack="True" GroupName="Group43" />
                        <asp:RadioButton ID="rbtnNo43" runat="server" Text="No" AutoPostBack="True" GroupName="Group43" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        &nbsp; 4.3.1 Escriba la direccion web de este sitio:
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:TextBox ID="txt431" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="7">
                        &nbsp; OBSERVACIONES
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        &nbsp;<asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="7">
                        <asp:Button ID="btnAlmacenar" runat="server" Text="Almacenar" OnClick="btnAlmacenar_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
