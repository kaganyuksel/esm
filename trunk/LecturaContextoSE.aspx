<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LecturaContextoSE.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="ESM.LecturaContexto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $(".numerico").change(function () {

                if (isNaN($(this).val()))
                    $(this).val("0");

            });

            $("#ContentPlaceHolder1_txtotrocual1").change(function () {

                var texto = $(this).val();
                if ($.trim(texto) != null && $.trim(texto) != "") {
                    $("#ContentPlaceHolder1_Cantidadotro1").attr("disabled", false);
                }
                else {
                    $("#ContentPlaceHolder1_Cantidadotro1").attr("disabled", true);
                }
            });

        });
    
    </script>
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
        .style2
        {
            text-decoration: underline;
        }
        .style3
        {
            text-align: justify;
            font-size: small;
            color: #666666;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div class="demo" style="width: 90%; margin: 0 auto;">
        <div>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <img src="/Icons/Edit.png" alt="Evaluacion" />
                    </td>
                    <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                        <h1 style="color: #0b72bc;">
                            Formulario de Lectura de Contexto para Secretaría de Educación</h1>
                        Administración de información para el formato Lectura de Contexto.
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <div id="ModseleccionSE" runat="server">
            <asp:GridView ID="gvSE" runat="server" AutoGenerateColumns="false" Width="100%" OnSelectedIndexChanged="gvSE_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="<img id='imgEvaluar'  height='24px' src='/Icons/Stationery.png' alt='Evaluar' />" />
                    <asp:BoundField DataField="IdSecretaria" HeaderText="Identificación" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre Secretaría" />
                    <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                </Columns>
            </asp:GridView>
        </div>
        <div id="ModMediciones" runat="server">
            <h4 id="titulomediciones" runat="server" visible="false" style="color: #0b72bc;">
                Listado de Mediciones Realizadas</h4>
            <asp:GridView ID="gvMediciones" AutoGenerateColumns="false" runat="server" Width="100%"
                OnSelectedIndexChanged="gvMediciones_SelectedIndexChanged" Visible="False">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="<img  width='24px' src='/Icons/Calender.png' alt='Seleccionar Medicion'>" />
                    <asp:BoundField DataField="Medicion" HeaderText="Medición" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button Text="Registrar Nueva Medición" runat="server" ID="btnRegistrar" OnClick="btnRegistrar_Click"
                Visible="False" />
        </div>
        <div id="ModLC" runat="server">
            <asp:Panel ID="LC" runat="server" Visible="false">
                <table id="lecturaContextoTable" runat="server" visible="false" cellpadding="0" cellspacing="0"
                    style="width: 100%; border: 1px solid #dddddd;">
                    <tr class="trheader">
                        <td class="style1" colspan="5">
                            LECTURA DE CONTEXTO SECRETARÍA DE EDUCACIÓN
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" colspan="5">
                            El proposito de este documento es tener respuestas sobre las acciones adelantadas
                            en la Sectretaría de Educación en torno a las competencias ciudadanas. Con esta
                            información proporcionará un panorama a partir del cual se oriente de mejor manera
                            las acciones del programa de competencias ciudadanas del MEN en las secretarías.
                        </td>
                    </tr>
                    <tr class="trheader" style="height: 40px; vertical-align: middle; line-height: 40px;">
                        <td class="style1" colspan="5">
                            0. INFORMACIÓN PREVIA A LA VISTA
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            <strong>Prueba Saber SE: Resultados comparación 2003/2005 ciudadanas</strong>
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            Link actual <a href="http://www.icfes.gov.co/saber59/">http://www.icfes.gov.co/saber59/</a>
                        </td>
                    </tr>
                    <tr class="trheader">
                        <td class="style1" colspan="5">
                            1. INFORMACIÓN DE LA SECRETARÍA DE EDUCACIÓN
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            1.1 Datos de la Secretaría:
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            A.) Nombre de la Secretaría:
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            <asp:TextBox ID="txtNombreSE" runat="server" Enabled="False" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td class="style1" colspan="5">
                            <asp:RadioButton ID="rbtnDepartamentalSE" runat="server" Enabled="False" Text="Departamental" />
                            <asp:RadioButton ID="rbtnMunicipalSE" runat="server" Enabled="False" Text="Municipal" />
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td style="width: 25%;">
                            B.) Dirección:
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtDireccionSE" runat="server" Enabled="False" Width="100%"></asp:TextBox>
                        </td>
                        <td style="width: 25%;">
                            C.) Teléfonos
                        </td>
                        <td colspan="2" style="width: 25%;">
                            <asp:TextBox ID="txtTelefonoSE" runat="server" Enabled="False" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            1.2 Datos Secretario(a) de Educación
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td>
                            A.)Nombre:
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombreSecre" runat="server" Enabled="False" Width="100%"></asp:TextBox>
                        </td>
                        <td>
                            B.) Correo Electrónico:
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtCorreoSecre" runat="server" Enabled="False" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td>
                            C.) Teléfonos de Contacto
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtTelefonoSecre" runat="server" Enabled="False" Width="50%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trheader">
                        <td class="style1" colspan="5">
                            2. INICIATIVA DE LA SE EN LA FORMACIÓN CIUDADANA
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            2.1¿Cuáles son los principales problemas que enfrentan los EE en temas de convivencia
                            escolar?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td class="style1" colspan="5">
                            <asp:TextBox ID="txt21" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            2.2 ¿Tiene la SE una política, programa o proyecto de formación ciudadana?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td class="style1" colspan="5">
                            <asp:RadioButton ID="rbtn22Si" runat="server" Text="Si" GroupName="group22" AutoPostBack="True"
                                OnCheckedChanged="rbtn22Si_CheckedChanged" />
                            <asp:RadioButton ID="rbtn22No" runat="server" Text="No" GroupName="group22" AutoPostBack="True"
                                OnCheckedChanged="rbtn22No_CheckedChanged" />
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            2.2.1 Nombre del programa(s) o proyecto(s) de formación ciudadana.
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txt221" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            2.2.2 ¿Cuál es el objetivo de este programa Iniciativa?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txt222" runat="server" TextMode="MultiLine" Width="100%" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            2.2.3 ¿ A qué poblaciones está dirigido este programa Iniciativa?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="2">
                            <asp:CheckBox Text="Establecimiento Educativo" runat="server" ID="chxEE" AutoPostBack="True"
                                OnCheckedChanged="chxEE_CheckedChanged" />
                        </td>
                        <td>
                            Cantidad:
                        </td>
                        <td colspan="2">
                            <asp:TextBox runat="server" ID="txtcantee" Width="25%" CssClass="numerico" Enabled="False">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="2">
                            <asp:CheckBox ID="chxest" runat="server" Text="Estudiantes" AutoPostBack="True" OnCheckedChanged="chxest_CheckedChanged" />
                        </td>
                        <td>
                            Cantidad:
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtcantest" runat="server" Width="25%" CssClass="numerico" Enabled="False">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="2">
                            <asp:CheckBox ID="chxEdu" runat="server" Text="Educadores" AutoPostBack="True" OnCheckedChanged="chxEdu_CheckedChanged" />
                        </td>
                        <td>
                            Cantidad:
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtcantedu" runat="server" Width="25%" CssClass="numerico" Enabled="False">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="2">
                            <asp:CheckBox ID="chxdirectivos" runat="server" Text="Directivos Docentes" AutoPostBack="True"
                                OnCheckedChanged="chxdirectivos_CheckedChanged" />
                        </td>
                        <td>
                            Cantidad:
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtcantdir" runat="server" Width="25%" CssClass="numerico" Enabled="False">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="2">
                            <asp:CheckBox ID="chxpad" runat="server" Text="Padres de Familia" AutoPostBack="True"
                                OnCheckedChanged="chxpad_CheckedChanged" />
                        </td>
                        <td>
                            Cantidad:
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtcantpad" runat="server" Width="25%" CssClass="numerico" Enabled="False">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td>
                            ¿Otro? Cual
                        </td>
                        <td>
                            <asp:TextBox ID="txtotrocual1" runat="server" Width="99%" />
                        </td>
                        <td>
                            Cantidad:
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="Cantidadotro1" runat="server" Width="25%" CssClass="numerico" Enabled="False">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td>
                            ¿Otro? Cual
                        </td>
                        <td>
                            <asp:TextBox ID="txtotrocual2" runat="server" Width="99%" />
                        </td>
                        <td>
                            Cantidad:
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="Cantidadotro2" runat="server" Width="25%" CssClass="numerico" Enabled="False">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td>
                            ¿Otro? Cual
                        </td>
                        <td>
                            <asp:TextBox ID="txtotrocual3" runat="server" Width="99%" />
                        </td>
                        <td>
                            Cantidad:
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="Cantidadotro3" runat="server" Width="25%" CssClass="numerico" Enabled="False">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td>
                            ¿Otro? Cual
                        </td>
                        <td>
                            <asp:TextBox ID="txtotrocual4" runat="server" Width="99%" />
                        </td>
                        <td>
                            Cantidad:
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="Cantidadotro4" runat="server" Width="25%" CssClass="numerico" Enabled="False">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td>
                            ¿Otro? Cual
                        </td>
                        <td>
                            <asp:TextBox ID="txtotrocual5" runat="server" Width="99%" />
                        </td>
                        <td>
                            Cantidad:
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="Cantidadotro5" runat="server" Width="25%" CssClass="numerico" Enabled="False">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            2.2.4 Explique cómo implementa este programa iniciativa
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txt224" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            2.2.5 ¿cómo realiza el seguimiento, monitoreo y evaluación de este programa iniciativa?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txt225" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            2.3 ¿En el PAM de la SE incluye de manera explícita el tema de ciudadania?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td class="style1" colspan="5">
                            <asp:RadioButton ID="rbtn23Si" runat="server" Text="Si" GroupName="group23" AutoPostBack="True"
                                OnCheckedChanged="rbtn23Si_CheckedChanged" />
                            <asp:RadioButton ID="rbtn23No" runat="server" Text="No" GroupName="group23" AutoPostBack="True"
                                OnCheckedChanged="rbtn23No_CheckedChanged" />
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            2.3.1 Explique cómo lo incluye
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txt231" Enabled="false" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trheader">
                        <td class="style1" colspan="5">
                            3. Otras Iniciativas de Formación ciudadana
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            3.1 ¿Qué actores/organizaciones municipales y/o regionales públicos ?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txt31" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            3.2 ¿Cuáles son las acciones que estos actores/organizaciones municipales y/o regionales
                            <span class="style2">públicos</span> desarrollan para la información ciudadana?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txt32" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            3.3 ¿A quién van dirigidas estas acciones?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txt33" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            3.4 ¿Qué actores/organizaciones municipales y/o regionales privados desarrollan
                            acciones para la formación ciudadana?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txt34" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            3.5 ¿Cuáles son las acciones que estos actores/organizaciones municipales y/o regionales
                            privados desarrollan para la formación ciudadana?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txt35" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            3.6 ¿A quién van dirigidas estas acciones?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txt36" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trheader">
                        <td class="style1" colspan="5">
                            4. Materiales de Formación ciudadana
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            4.1 ¿La SE cuenta con el material bibliográfico en temáticas relacionadas con formación
                            ciudadana para la consulta de los EE ?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td class="style1" colspan="5">
                            <asp:RadioButton ID="rbtn41Si" runat="server" Text="Si" AutoPostBack="True" OnCheckedChanged="rbtn41Si_CheckedChanged"
                                GroupName="group41" />
                            <asp:RadioButton ID="rbtn41No" runat="server" Text="No" AutoPostBack="True" OnCheckedChanged="rbtn41No_CheckedChanged"
                                GroupName="group41" />
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            4.1.1 Por favor enúncielos mencionando la fuente de consulta
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txt411" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trheader">
                        <td class="style1" colspan="5">
                            5. Medios de Comunicación
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            5.1 ¿A través de qué medios de comunicación en el Municipio o en el Departamento
                            se promueven temas drelacionados con formación ciudadana ?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            Departamental/Municipal:
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            <asp:CheckBoxList ID="cblist51DedMun" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                                <asp:ListItem>Radio</asp:ListItem>
                                <asp:ListItem>Prensa</asp:ListItem>
                                <asp:ListItem>Televisión</asp:ListItem>
                                <asp:ListItem>Internet</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            Local:
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            <asp:CheckBoxList ID="cblist51Local" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Radio</asp:ListItem>
                                <asp:ListItem>Prensa</asp:ListItem>
                                <asp:ListItem>Televisión</asp:ListItem>
                                <asp:ListItem>Internet</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            5.2 ¿Cuales son las temáticas más comunes ?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txt52" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trheader">
                        <td class="style1" colspan="5">
                            6. TICS
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            6.1 ¿Existen iniciativas activas de uso pedagógico de medios y TIC para la formación
                            de ciudadania ?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td class="style1" colspan="5">
                            <asp:RadioButton ID="rbtn118Si" runat="server" Text="Si" OnCheckedChanged="rbtn118Si_CheckedChanged"
                                AutoPostBack="True" GroupName="group118" />
                            <asp:RadioButton ID="rbtn118No" runat="server" Text="No" OnCheckedChanged="rbtn118No_CheckedChanged"
                                AutoPostBack="True" GroupName="group118" />
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            6.2 ¿Qué iniciativas de uso pedagógico de medios y TIC para la formación de ciudadania
                            existen en la SE?
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txt119" runat="server" TextMode="MultiLine" Width="99%" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trheader">
                        <td colspan="5">
                            OBSERVACIONES
                        </td>
                    </tr>
                    <tr class="trblanca">
                        <td colspan="5">
                            <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trgris">
                        <td colspan="5">
                            <asp:Button ID="btnAlmacenar" runat="server" Text="Almacenar" OnClick="btnAlmacenar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
    <br />
    <br />
</asp:Content>
