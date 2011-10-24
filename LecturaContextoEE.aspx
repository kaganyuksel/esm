<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LecturaContextoEE.aspx.cs" Inherits="ESM.LecturaContextoEE" MaintainScrollPositionOnPostback="true" %>

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
        </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".numerico").change(function () {
                var valor = $(this).val();
                if (isNaN(valor)) {
                    alert("El valor ingresado no es del tipo numerico.");
                    $(this).val(0);
                }
            });

            $(".sisclass").change(function () {
                var suma = 0;
                var valor = 0;
                $(".sisclass").each(function (i) {
                    if (isNaN($(this).val)) {

                        valor = $(this).val();
                        suma = parseInt(suma) + parseInt(valor);
                    }
                });
                if (suma > 100)
                    $("#primer_porcentaje").css("color", "red");
                else
                    $("#primer_porcentaje").css("color", "green");
                $("#primer_porcentaje").html(suma + "%");
            });
            $(".classsisdos").change(function () {
                var suma = 0;
                var valor = 0;
                $(".classsisdos").each(function (i) {
                    if (isNaN($(this).val)) {

                        valor = $(this).val();
                        suma = parseInt(suma) + parseInt(valor);
                    }
                });
                if (suma > 100)
                    $("#segundo_porcentaje").css("color", "red");
                else
                    $("#segundo_porcentaje").css("color", "green");
                $("#segundo_porcentaje").html(suma + "%");
            });
        });

        
    </script>
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
                1. Seleccione el establecimiento educativo a evaluar.
            </h3>
            <br />
            <p id="filtrosp" runat="server">
                Búsqueda de Establecimiento Educativo:
                <asp:TextBox runat="server" ID="txtFiltro" />
                <%--<input type="button" name="btnFiltro" value="Buscar" onclick="buscar();" />--%>
                <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CausesValidation="false"
                    UseSubmitBehavior="true" />
            </p>
            <asp:GridView ID="gvResultados" runat="server" AllowPaging="True" AllowSorting="True"
                CssClass="gvResultados" RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6"
                AutoGenerateColumns="False" Width="100%" 
                OnSelectedIndexChanged="gvResultados_SelectedIndexChanged" 
                onpageindexchanging="gvResultados_PageIndexChanging">
                <HeaderStyle CssClass="trheader" />
                <Columns>
                    <asp:CommandField SelectText="<img id='imgEvaluar'  height='24px' src='/Icons/Stationery.png' alt='Evaluar' />"
                        ShowSelectButton="True" ControlStyle-CssClass="a" >
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
                EntityTypeName="" TableName="Establecimiento_Educativos">
            </asp:LinqDataSource>
        </div>
        <br />
        <div id="ModMediciones" runat="server">
            <h4 id="titulomediciones" runat="server" visible="false" style="color: #0b72bc;">
                Listado de Mediciones Realizadas</h4>
            <asp:GridView ID="gvMediciones" runat="server" Width="100%" Visible="False" AutoGenerateColumns="False"
                OnSelectedIndexChanged="gvMediciones_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="<img  width='24px' src='/Icons/Calender.png' alt='Seleccionar Medicion'>" />
                    <asp:BoundField DataField="IdMedicion" HeaderText="Identificador de Medición" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha de Medición" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button Text="Registrar Nueva Medición" runat="server" ID="btnRegistrar" OnClick="btnRegistrar_Click"
                Visible="False" />
        </div>
        <br />
        <asp:Panel ID="LC" runat="server" Visible="false">
            <table border="0" cellspacing="0" style="border: 0; width: 100%; -moz-border-radius: 3px;
                -webkit-border-radius: 3px; border-radius: 3px; /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 1px #000000; -webkit-box-shadow: 0px 0px 1px #000000; box-shadow: 0px 0px 1px #000000;
                /*ie 7 and 8 do not support blur property of shadows*/">
                <tr class="trheader">
                    <td class="style1" colspan="5">
                        LECTURA DE CONTEXTO ESTABLECIMIENTO EDUCATIVO (EE)
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="5">
                        El propósito de este instrumento es tener la respuesta a algunas preguntas generales
                        de acciones adelantadas en la INSTITUCIÓN EDUCATIVA en torno a las competencias
                        ciudadanas, de tal manera que den un contexto particular que permita orientar de
                        mejor manera las acciones por ejecutar del programa de competencias ciudadanas del
                        MEN en esta Institución. Se recomienda además leer el instrumento de lectura de
                        contexto de la Secretaría de Educación, para tener mayor información de referencia.
                    </td>
                </tr>
                <tr class="trheader" style="height: 40px; vertical-align: middle; line-height: 40px;">
                    <td class="style1" colspan="5">
                        0. INFORMACIÓN PREVIA A LA VISITA
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="5">
                        PRUEBAS SABER EE: Resultados comparación 2003/2005 CIUDADANAS<br />
                        Consultar en el siguiente link:<br />
                        Link :<a href="http://www.icfes.gov.co/saber59/">http://www.icfes.gov.co/saber59/</a><br />
                    </td>
                </tr>
                <tr class="trheader">
                    <td class="style1" colspan="5">
                        1. INFORMACIÓN DEL ESTABLECIMIENTO EDUCATIVO
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="5">
                        1.1 Información básica del Establecimiento:
                    </td>
                </tr>
                <tr class="trblanca">
                    <td style="width: 30%">
                        A.) Nombre:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtNombre" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td>
                        B.) Código DANE:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtCodigo" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trblanca">
                    <td style="width: 20%;">
                        C.) Direccion sede principal:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtDireccion" runat="server" Width="99%"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        Teléfonos:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTelefonos" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td>
                        D.) Numero de sedes para EE:
                    </td>
                    <td colspan="4">
                        <input onkeypress="return mis_datos(event)" type="text" id="txtSedes" runat="server"
                            class="numerico" style="width: 100%;" value="0" />
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        E.) Correo electrónico
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtCorreoElectronico" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td>
                        F.) Jornadas que ofrece el EE
                    </td>
                    <td colspan="4">
                        <asp:CheckBoxList ID="cblistjornadas" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>Completa</asp:ListItem>
                            <asp:ListItem>Mañana</asp:ListItem>
                            <asp:ListItem>Tarde</asp:ListItem>
                            <asp:ListItem>Noche</asp:ListItem>
                            <asp:ListItem>Fin de Semana</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="5">
                        1.2 Ubicación y localización física de la institución (Sede Principal)
                    </td>
                </tr>
                <tr class="trgris">
                    <td>
                        A.) Municipio
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtMunicipio" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        B.) Zona
                    </td>
                    <td colspan="4">
                        <asp:RadioButton ID="rbtnRural" runat="server" GroupName="grouprural" Text="Rural" />
                        <asp:RadioButton ID="rbtnUrbana" runat="server" GroupName="grouprural" Text="Urbana" />
                    </td>
                </tr>
                <tr class="trgris">
                    <td>
                        C.) Tipo
                    </td>
                    <td colspan="4">
                        <asp:CheckBoxList ID="cblistTipo" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>Cabecera municipal</asp:ListItem>
                            <asp:ListItem>Corregimiento</asp:ListItem>
                            <asp:ListItem>Inspección</asp:ListItem>
                            <asp:ListItem>Caserio</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr class="trheader">
                    <td colspan="5" style="text-align: center;">
                        2. INFORMACIÓN DE ESTUDIANTES
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="5">
                        2.1 Indique el número de estudiantes matriculados (todas las sedes y jornadas) en
                        2011
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        Estudiantes Matriculados 2011:
                    </td>
                    <td colspan="4">
                        Total:
                        <asp:TextBox ID="txt21" runat="server" CssClass="numerico" Width="10%">0</asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="5">
                        2.2 Indique a continuación el porcentaje estimado de estudiantes matriculados en
                        2011 según estrato socioeconómico.
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        Estrato 1:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtE1" CssClass="sisclass numerico" runat="server" Width="10%">0</asp:TextBox>
                        &nbsp;% de estudiantes
                    </td>
                </tr>
                <tr class="trgris">
                    <td>
                        Estrato 2:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtE2" CssClass="sisclass numerico" runat="server" Width="10%">0</asp:TextBox>
                        &nbsp;% de estudiantes
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        Estrato 3:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtE3" CssClass="sisclass numerico" runat="server" Width="10%">0</asp:TextBox>
                        &nbsp;% de estudiantes
                    </td>
                </tr>
                <tr class="trgris">
                    <td>
                        Estrato 4:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtE4" CssClass="sisclass numerico" runat="server" Width="10%">0</asp:TextBox>
                        &nbsp;% de estudiantes
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        Estrato 5:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtE5" CssClass="sisclass numerico" runat="server" Width="10%">0</asp:TextBox>
                        &nbsp;% de estudiantes
                    </td>
                </tr>
                <tr class="trgris">
                    <td>
                        Estrato 6:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtE6" CssClass="sisclass numerico" runat="server" Width="10%">0</asp:TextBox>
                        &nbsp;% de estudiantes
                    </td>
                </tr>
                <tr class="trgris">
                    <td>
                        &nbsp;
                    </td>
                    <td colspan="4">
                        <label id="primer_porcentaje">
                        </label>
                        &nbsp;
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="5">
                        2.3 Indique a continuación el porcentaje estimado de estudiantes matriculados en
                        2011 según niveles SISBEN.
                    </td>
                </tr>
                <tr class="trgris">
                    <td>
                        Sisben 1:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtS1" CssClass="classsisdos numerico" runat="server" Width="10%">0</asp:TextBox>
                        &nbsp;% de estudiantes
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        Sisben 2:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtS2" CssClass="classsisdos numerico" runat="server" Width="10%">0</asp:TextBox>
                        &nbsp;% de estudiantes
                    </td>
                </tr>
                <tr class="trgris">
                    <td>
                        Sisben 3:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtS3" CssClass="classsisdos numerico" runat="server" Width="10%">0</asp:TextBox>
                        &nbsp;% de estudiantes
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        Sisben 4:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtS4" CssClass="classsisdos numerico" runat="server" Width="10%">0</asp:TextBox>
                        &nbsp;% de estudiantes
                    </td>
                </tr>
                <tr class="trgris">
                    <td>
                        No Sabe:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtNoSabe" CssClass="classsisdos numerico" runat="server" Width="10%">0</asp:TextBox>
                        &nbsp;% de estudiantes
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        No Tiene:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtNotiene" CssClass="classsisdos numerico" runat="server" Width="10%">0</asp:TextBox>
                        &nbsp;% de estudiantes
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        &nbsp;
                    </td>
                    <td colspan="4">
                        <label id="segundo_porcentaje">
                        </label>
                        &nbsp;
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="5">
                        2.4 ¿El EE atiende población víctima del conflicto?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="5" class="style1">
                        <asp:RadioButton ID="rbtnSi24" runat="server" AutoPostBack="True" GroupName="group24"
                            OnCheckedChanged="RadioButton1_CheckedChanged" Text="Si" />
                        <asp:RadioButton ID="rbtnNo24" runat="server" AutoPostBack="True" GroupName="group24"
                            OnCheckedChanged="RadioButton2_CheckedChanged" Text="No" Checked="True" />
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="5">
                        2.5 En la siguiente tabla escriba el número de estudiantes matriculados por nivel
                        educativo victimas del conflicto matriculados en 2011.
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        A.) En situación de desplazamiento
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txt25_1" runat="server" CssClass="numerico" Enabled="False" Width="15%">0</asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td>
                        B.) Desvinculados
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txt25_2" runat="server" Enabled="False" CssClass="numerico" Width="15%">0</asp:TextBox>
                    </td>
                </tr>
                <tr class="trblanca">
                    <td>
                        C.) Hijos de adultos desmovilizados
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txt25_3" runat="server" Enabled="False" CssClass="numerico" Width="15%">0</asp:TextBox>
                    </td>
                </tr>
                <tr class="trheader">
                    <td colspan="5" style="text-align: center;">
                        3. INICIATIVAS DEL ESTABLECIMIENTO EN FORMACIÓN CIUDADANA
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="5">
                        3.1 ¿Cuáles son los principales problemas que enfrentan el EE en temas de convivencia
                        escolar?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="5">
                        <asp:TextBox ID="txt31" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="5">
                        3.2 ¿En el EE, cómo se manejan situaciones relacionadas con problematicas de convivencia
                        escolar (discriminación, matoneo, otros)?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="5">
                        <asp:TextBox ID="txt32" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="5">
                        3.3 ¿Qué tan importante cree ustes que es el tema formación ciudadana en su establecimiento
                        educativo?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="5">
                        <asp:TextBox ID="txt33" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="5">
                        3.4 Ha oído algo sobre CC?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="5" style="text-align: center;">
                        <asp:RadioButton ID="rbtnSi34" runat="server" AutoPostBack="True" GroupName="Group34"
                            OnCheckedChanged="RadioButton3_CheckedChanged" Text="Si" />
                        <asp:RadioButton ID="rbtnNo34" runat="server" AutoPostBack="True" GroupName="Group34"
                            OnCheckedChanged="RadioButton4_CheckedChanged" Text="No" Checked="True" />
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="5">
                        3.4.1 ¿Qué sabe de este tema ?(Competencias Ciudadanas)
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="5">
                        <asp:TextBox ID="txt341" runat="server" Enabled="False" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="5">
                        3.5 ¿Tiene el EE un programa o proyecto de formación ciudadana?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style1" colspan="5">
                        <asp:RadioButton ID="rbtnSi35" runat="server" AutoPostBack="True" GroupName="Group35"
                            Text="Si" OnCheckedChanged="rbtnSi35_CheckedChanged" />
                        <asp:RadioButton ID="rbtnNo35" runat="server" AutoPostBack="True" GroupName="Group35"
                            Text="No" Checked="True" OnCheckedChanged="rbtnNo35_CheckedChanged" />
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        3.5.1 Nombre del programa o proyecto de formación ciudadana
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="5">
                        <asp:TextBox ID="txt351" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        3.5.2 ¿Cuál es el objetivo de este programa/iniciativa?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="5">
                        <asp:TextBox ID="txt352" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        3.5.3 ¿A qué población/es está dirigido este programa/iniciativa?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="5">
                        <asp:TextBox ID="txt353" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td colspan="5">
                        3.5.4¿Cómo lo hace? (horario, actores, espacio fisico, registro documental)
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="5">
                        <asp:TextBox ID="txt354" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        3.5.5 ¿Quiénes lideran el desarrollo de esta iniciativa? (docentes, directivos,
                        estudiantes, otros)
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="5">
                        <asp:TextBox ID="txt355" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        3.5.6 ¿En qué áreas académicas se desarrolla esta iniciativa?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="5">
                        <asp:TextBox ID="txt356" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        3.5.7 ¿Cómo realiza el seguimiento, monitoreo y evaluación este programa/iniciativa?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="5">
                        <asp:TextBox ID="txt357" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        3.6 ¿Cuáles son las formas de participación de estudiantes, docentes, padres de
                        familia, personal, administrativo y de apoyo en el gobierno del EE?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="5">
                        <asp:TextBox ID="txt36" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        3.7 ¿Cuál es la experiencia con el Manual de Convivencia? ¿Quiénes lo construyeron?
                        ¿Cómo lo construyeron?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="5">
                        <asp:TextBox ID="txt37" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        3.8 ¿El PEI del EE incluye de manera explícita el tema de ciudadanía?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style1" colspan="5">
                        &nbsp;
                        <asp:RadioButton ID="rbtnSi38" runat="server" AutoPostBack="True" GroupName="Group38"
                            Text="Si" Enabled="False" OnCheckedChanged="rbtnSi38_CheckedChanged" />
                        <asp:RadioButton ID="rbtnNo38" runat="server" AutoPostBack="True" GroupName="Group38"
                            Text="No" Checked="True" Enabled="False" OnCheckedChanged="rbtnNo38_CheckedChanged" />
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        &nbsp; 3.8.1 Explique como lo incluye
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="5">
                        <asp:TextBox ID="txt381" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        3.9 ¿El PAM del EE incluye de manera explícita el tema de ciudadanía?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style1" colspan="5">
                        <asp:RadioButton ID="rbtnSi39" runat="server" AutoPostBack="True" GroupName="Group39"
                            Text="Si" Enabled="False" OnCheckedChanged="rbtnSi39_CheckedChanged" />
                        <asp:RadioButton ID="rbtnNo39" runat="server" AutoPostBack="True" GroupName="Group39"
                            Text="No" Checked="True" Enabled="False" OnCheckedChanged="rbtnNo39_CheckedChanged" />
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        &nbsp; 3.9.1 Explique como lo incluye
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="5">
                        &nbsp;<asp:TextBox ID="txt391" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trheader">
                    <td class="style1" colspan="5">
                        4. INFRAESTRUCTURA INFORMÁTICA
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        4.1 ¿La EE tiene conexión a Internet?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style1" colspan="5">
                        <asp:RadioButton ID="rbtnSi41" runat="server" AutoPostBack="True" GroupName="Group41"
                            Text="Si" OnCheckedChanged="rbtnSi41_CheckedChanged" />
                        <asp:RadioButton ID="rbtnNo41" runat="server" AutoPostBack="True" GroupName="Group41"
                            Text="No" Checked="True" OnCheckedChanged="rbtnNo41_CheckedChanged" />
                        <asp:RadioButton ID="rbtnAlgunnas41" runat="server" AutoPostBack="True" GroupName="Group41"
                            Text="Algunas de las sedes" OnCheckedChanged="rbtnAlgunnas41_CheckedChanged" />
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        &nbsp; 4.2 Indique el número de computadores en uso con acceso a Internet en la
                        EE:
                        <asp:TextBox ID="txt42" runat="server" CssClass="numerico" Width="10%" Enabled="False">0</asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        &nbsp; 4.3 ¿La EE cuenta con un sitio Web?
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style1" colspan="5">
                        &nbsp;
                        <asp:RadioButton ID="rbtnSi43" runat="server" AutoPostBack="True" GroupName="Group43"
                            Text="Si" OnCheckedChanged="rbtnSi43_CheckedChanged" />
                        <asp:RadioButton ID="rbtnNo43" runat="server" AutoPostBack="True" GroupName="Group43"
                            Text="No" Checked="True" OnCheckedChanged="rbtnNo43_CheckedChanged" />
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        &nbsp; 4.3.1 Escriba la direccion web de este sitio:
                    </td>
                </tr>
                <tr class="trblanca">
                    <td class="style2" colspan="5">
                        <asp:TextBox ID="txt431" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trheader">
                    <td class="style1" colspan="5">
                        5. INFORMACIÓN DE DOCENTES
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="5">
                        5.1 ¿En su EE hay docentes formados en competencias ciudadanas?
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style1" colspan="5">
                        <asp:RadioButton ID="rbtnSi51" runat="server" AutoPostBack="True" GroupName="Group43"
                            Text="Si" OnCheckedChanged="rbtnSi51_CheckedChanged" />
                        <asp:RadioButton ID="rbtnNo51" runat="server" AutoPostBack="True" GroupName="Group43"
                            Text="No" Checked="True" OnCheckedChanged="rbtnNo51_CheckedChanged" />
                    </td>
                </tr>
                <tr class="trblanca">
                    <td colspan="5">
                        5.1.1 Cantidad de docentes formados en competencias ciudadanas
                        <asp:TextBox ID="txt511" runat="server" CssClass="numerico" Width="10%" Enabled="False">0</asp:TextBox>
                    </td>
                </tr>
                <tr class="trheader">
                    <td class="style1" colspan="5">
                        OBSERVACIONES
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        &nbsp;<asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trgris">
                    <td class="style2" colspan="5">
                        <asp:Button ID="btnAlmacenar" runat="server" OnClick="btnAlmacenar_Click" Text="Almacenar" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <br />
    <br />
    <br />
</asp:Content>
