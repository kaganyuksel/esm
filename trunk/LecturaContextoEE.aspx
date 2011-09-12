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
                AutoGenerateColumns="false" Width="100%" 
                onselectedindexchanged="gvResultados_SelectedIndexChanged">
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
        <asp:Button Text="Registrar Nueva Medición" runat="server" ID="btnRegistrar" Visible="False" />
        <br />
        <asp:Panel ID="LC" runat="server" Visible="false">
            <h1 style="color: #005EA7;">
                <img src="/Icons/Tutorial.png" alt="Lectura Contexto EE" />Lectura de Contexto para
                el Establecimiento Educativo.</h1>
            <br />
            <br />
            <table style="width: 100%;">
                <tr>
                    <td class="style1" colspan="6">
                        LECTURA DE CONTEXTO<br />
                        ESTABLECIMIENTO EDUCATIVO (EE)
                    </td>
                </tr>
                <tr>
                    <td class="style3" colspan="6">
                        El propósito de este instrumento es tener la respuesta a algunas preguntas generales
                        de acciones adelantadas en la INSTITUCIÓN EDUCATIVA en torno a las competencias
                        ciudadanas, de tal manera que den un contexto particular que permita orientar de
                        mejor manera las acciones por ejecutar del programa de competencias ciudadanas del
                        MEN en esta Institución. Se recomienda además leer el instrumento de lectura de
                        contexto de la Secretaría de Educación, para tener mayor información de referencia.
                    </td>
                </tr>
                <tr style="height: 40px; vertical-align: middle; line-height: 40px;">
                    <td class="style1" colspan="6" style="background: #226E22; color: #ffffff;">
                        0. INFORMACIÓN PREVIA A LA VISITA
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="3">
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
                    <td class="style1" colspan="6" style="background: #226E22; color: #ffffff;">
                        1. INFORMACIÓN DEL ESTABLECIMIENTO EDUCATIVO
                    </td>
                </tr>
                <tr>
                    <td>
                        1.1 Información básica del Establecimiento:
                    </td>
                    <td>
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
                        a.Nombre:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
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
                        b. Código DANE:
                    </td>
                    <td>
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
                    <td>
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
                    <td>
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
                    <td>
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
                    <td>
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
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        1.2 Ubicación y localización física de la institución (Sede Principal)
                    </td>
                </tr>
                <tr>
                    <td>
                        a. Municipio
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
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
                        b.Zona
                    </td>
                    <td>
                        <asp:CheckBoxList ID="cblistZona" runat="server">
                            <asp:ListItem>Urbana</asp:ListItem>
                            <asp:ListItem>Rural</asp:ListItem>
                        </asp:CheckBoxList>
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
                    <td class="style1" colspan="6" style="background: #226E22; color: #ffffff;">
                        2. INFORMACIÓN DE ESTUDIANTES
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        2.1 Indique el número de estudiantes matriculados en 2011 según niveles SISBEN
                    </td>
                </tr>
                <tr>
                    <td>
                        Estudiantes Matriculados 2011:
                    </td>
                    <td>
                        Total:
                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
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
                    <td colspan="6">
                        2.2 Indique a continuación el porcentaje estimado de estudiantes matriculados en
                        2011 según SISBEN.
                    </td>
                </tr>
                <tr>
                    <td>
                        Estrato 1:
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        % de estudiantes
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
                        Estrato 2:
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        % de estudiantes
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
                        Estrato 3:
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        % de estudiantes
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
                        Estrato 4:
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        % de estudiantes
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
                        Estrato 5:
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        % de estudiantes
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
                        Estrato 6:
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        % de estudiantes
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
                    <td>
                        100%
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
                    <td colspan="6">
                        2.3 Indique a continuación el porcentaje estimado de estudiantes matriculados en
                        2011 según niveles SISBEN.
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Sisben 1:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                        % de estudiantes
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Sisben 2:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                        % de estudiantes
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Sisben 3:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                        % de estudiantes
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Sisben 4:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
                        % de estudiantes
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        No Sabe:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
                        % de estudiantes
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        No Tiene:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                        % de estudiantes
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        2.4 ¿El EE atiende población víctima del conflicto?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="6">
                        <asp:RadioButton ID="RadioButton1" runat="server" Text="Si" />
                        <asp:RadioButton ID="RadioButton2" runat="server" Text="No" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        2.5 En la siguiente tabla escriba el número de estudiantes matriculados por nivel
                        educativo victimas del conflicto matriculados en 2011
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="3">
                        En situación de desplazamiento
                    </td>
                    <td class="style1" colspan="3">
                        <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="3">
                        Desvinculados
                    </td>
                    <td class="style1" colspan="3">
                        <asp:TextBox ID="TextBox24" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="3">
                        Hijos de adultos desmovilizados
                    </td>
                    <td class="style1" colspan="3">
                        <asp:TextBox ID="TextBox25" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 40px; vertical-align: middle; line-height: 40px;">
                    <td class="style1" colspan="6" style="background: #226E22; color: #ffffff;">
                        2. INICIATIVAS DEL ESTABLECIMIENTO EN FORMACIÓN CIUDADANA
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        2.1 ¿Cuáles son los principales problemas que enfrentan el EE en temas de convivencia
                        escolar?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="6">
                        <asp:TextBox ID="TextBox26" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        2.2 ¿En el EE, cómo se manejan situaciones relacionadas con problematicas de convivencia
                        escolar (discriminación, matoneo, otros)?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="6">
                        <asp:TextBox ID="TextBox27" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        2.3 ¿Qué tan importante cree ustes que es el tema formación ciudadana en su establecimiento
                        educativo?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="6">
                        <asp:TextBox ID="TextBox28" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        2.4 Ha oído algo sobre CC?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="6">
                        <asp:RadioButton ID="RadioButton3" runat="server" Text="Si" />
                        <asp:RadioButton ID="RadioButton4" runat="server" Text="No" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        2.4.1 ¿Qué sabe de este tema ?(Competencias Ciudadanas)
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        <asp:TextBox ID="TextBox29" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        2.5 ¿Tiene el EE un programa o proyecto de formación ciudadana?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="6">
                        <asp:RadioButton ID="RadioButton5" runat="server" Text="Si" />
                        <asp:RadioButton ID="RadioButton6" runat="server" Text="No" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        2.5.1 Nombre del programa o proyecto de formación ciudadana
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        <asp:TextBox ID="TextBox30" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        2.5.2 ¿Cuál es el objetivo de este programa/iniciativa?
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        <asp:TextBox ID="TextBox31" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        2.5.3 ¿A qué población/es está dirigido este programa/iniciativa?
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        <asp:TextBox ID="TextBox32" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        2.5.4¿Cómo lo hace? (horario, actores, espacio fisico, registro documental)
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        <asp:TextBox ID="TextBox33" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        2.5.5 ¿Quiénes lideran el desarrollo de esta iniciativa? (docentes, directivos,
                        estudiantes, otros)
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        <asp:TextBox ID="TextBox34" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        2.5.6 ¿En qué áreas académicas se desarrolla esta iniciativa?
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        <asp:TextBox ID="TextBox35" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        2.5.7 ¿Cómo realiza el seguimiento, monitoreo y evaluación este programa/iniciativa?
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        <asp:TextBox ID="TextBox36" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        &nbsp; 2.6 ¿Cuáles son las formas de participación de estudiantes, docentes, padres
                        de familia, personal, administrativo y de apoyo en el gobierno del EE?
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        &nbsp;
                        <asp:TextBox ID="TextBox37" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        &nbsp; 2.7 ¿Cuál es la experiencia con el Manual de Convivencia? ¿Quiénes lo construyeron?
                        ¿Cómo lo construyeron?
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        <asp:TextBox ID="TextBox38" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        &nbsp; 2.8 ¿El PEI del EE incluye de manera explícita el tema de ciudadanía?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="6">
                        &nbsp;
                        <asp:RadioButton ID="RadioButton7" runat="server" Text="Si" />
                        <asp:RadioButton ID="RadioButton8" runat="server" Text="No" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        &nbsp; 2.8.1 Explique como lo incluye
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        &nbsp;
                        <asp:TextBox ID="TextBox39" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        &nbsp; 2.9 ¿El PAM del EE incluye de manera explícita el tema de ciudadanía?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="6">
                        &nbsp;
                        <asp:RadioButton ID="RadioButton9" runat="server" Text="Si" />
                        <asp:RadioButton ID="RadioButton10" runat="server" Text="No" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        &nbsp; 2.9.1 Explique como lo incluye
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        &nbsp;
                        <asp:TextBox ID="TextBox40" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 40px; vertical-align: middle; line-height: 40px;">
                    <td class="style1" colspan="6" style="background: #226E22; color: #ffffff;">
                        &nbsp; 3. INFRAESTRUCTURA INFORMÁTICA
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        &nbsp; 3.1 ¿La EE tiene conexión a Internet?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="6">
                        &nbsp;
                        <asp:RadioButton ID="RadioButton11" runat="server" Text="Si" />
                        <asp:RadioButton ID="RadioButton12" runat="server" Text="No" />
                        <asp:RadioButton ID="RadioButton13" runat="server" Text="Algunas de las sedes" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        &nbsp; 3.2 Indique el número de computadores en uso con acceso a Internet en la
                        IE:<asp:TextBox ID="TextBox41" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        &nbsp; 3.3 ¿La EE cuenta con un sitio Web?
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="6">
                        &nbsp;
                        <asp:RadioButton ID="RadioButton14" runat="server" Text="Si" />
                        <asp:RadioButton ID="RadioButton15" runat="server" Text="No" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        &nbsp; 3.3.1 Escriba la direccion web de este sitio:
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        <asp:TextBox ID="TextBox42" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="6">
                        &nbsp; OBSERVACIONES
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        &nbsp;<asp:TextBox ID="TextBox43" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="6">
                        <asp:Button ID="btnAlmacenar" runat="server" Text="Almacenar" 
                            onclick="btnAlmacenar_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
