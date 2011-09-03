﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LecturaContextoSE.aspx.cs" Inherits="ESM.LecturaContexto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        <table cellpadding="10" cellspacing="6" style="width: 100%; border: 1px solid #dddddd;">
            <tr>
                <td class="style1" colspan="4">
                    Lectura de Contexto<br />
                    SECRETARÍA DE EDUCACIÓN
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3" colspan="4">
                    El proposito de este documento es tener respuestas sobre las acciones adelantadas
                    en la Sectretaría de Educación en torno a las competencias ciudadanas. Con esta
                    información proporcionará un panorama a partir del cual se oriente de mejor manera
                    las acciones del programa de competencias ciudadanas del MEN en las secretarías.
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 40px; vertical-align: middle; line-height: 40px;">
                <td class="style1" colspan="4" style="background: #798F00; color: #ffffff;">
                    0. INFORMACIÓN PREVIA A LA VISTA
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong>Prueba Saber SE: Resultados comparación 2003/2005 ciudadanas</strong>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    Link actual <a href="http://www.icfes.gov.co/saber59/">http://www.icfes.gov.co/saber59/</a>
                </td>
            </tr>
            <tr style="height: 40px; vertical-align: middle; line-height: 40px;">
                <td class="style1" style="background: #00778F; color: #ffffff;" colspan="4">
                    1. INFORMACIÓN DE LA SECRETARÍA DE EDUCACIÓN
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    1.1 Datos de la Secretaria:
                </td>
            </tr>
            <tr>
                <td>
                    a. Nombre de la Secretaria: &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtNombreSE" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;<asp:RadioButton 
                        ID="rbtnDepartamentalSE" runat="server" Text="Departamental" />
                    &nbsp;<asp:RadioButton ID="rbtnMunicipalSE" runat="server" Text="Municipal" />
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
                    b. Direccion:
                </td>
                <td>
                    <asp:TextBox ID="txtDireccionSE" runat="server"></asp:TextBox>
                </td>
                <td>
                    d. Telefonos
                </td>
                <td>
                    <asp:TextBox ID="txtTelefonoIE" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    1.2 Datos Secretario(a) de Educación
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
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
                    <asp:TextBox ID="txtNombreSecre" runat="server"></asp:TextBox>
                </td>
                <td>
                    b. Correo Electronico:
                </td>
                <td>
                    <asp:TextBox ID="txtCorreoSecre" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    c. Telefonos de Contacto
                </td>
                <td>
                    <asp:TextBox ID="txtTelefonoSecre" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr style="background: #008F3B; color: #ffffff; height: 40px; line-height: 40px;">
                <td class="style1" colspan="4">
                    2. INICIATIVA DE LA SE EN LA FORMACIÓN CIUDADANA
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    ¿2.1 Cuáles son los principales problemas que enfrentan los EE en temas de convivencia
                    escolar?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="4">
                    <asp:TextBox ID="txt21" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    2.2 ¿Tiene la SE una politica, programa o proyecto de formacion ciudadana?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="4">
                    <asp:RadioButton ID="rbtn22Si" runat="server" Text="Si" GroupName="group22" />
                    <asp:RadioButton ID="rbtn22No" runat="server" Text="No" GroupName="group22" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    2.2.1 Nombre del programa(s) o proyecto(s) de formación ciudadana.
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txt221" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;2.2.2 Cuál es el objetivo de este programa Iniciativa?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txt222" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    2.2.3 ¿ A qué poblaciones esta dirigido este programa Iniciativa?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:CheckBoxList ID="cblist223" runat="server">
                        <asp:ListItem>Establecimiento Educativo</asp:ListItem>
                        <asp:ListItem>Estudiantes</asp:ListItem>
                        <asp:ListItem>Educadores</asp:ListItem>
                        <asp:ListItem>Directivos Docentes</asp:ListItem>
                        <asp:ListItem>Padres de Familia</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    Otro cuál?
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txt223Orto" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    2.2.4 Explique como implementa este programa iniciativa?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txt224" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    2.2.5 ¿Comó realiza el seguimiento, monitoreo y evaluación de este programa iniciativa?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txt225" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    2.3 ¿En el PAM de la SE incluye de manera explícita el tema de ciudadania?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="4">
                    <asp:RadioButton ID="rbtn23Si" runat="server" Text="Si" GroupName="group23" />
                    <asp:RadioButton ID="rbtn23No" runat="server" Text="No" GroupName="group23" />
                </td>
                <td class="style1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    2.3.1 Explique como lo incluye
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txt231" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="4">
                    3. Otras Iniciativas de Formación ciudadana
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    3.1 ¿Qué actores/organizaciones&nbsp; municipales y/o regionales públicos ?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txt31" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    3.2 ¿Cuáles son las acciones que estos actores/organizaciones municipales y/o regionales
                    <span class="style2">públicos</span> desarrollan para la información ciudadana?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txt32" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    3.3 ¿A quién van dirigidas estas acciones?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txt33" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    3.4 ¿Qué actores/organizaciones municipales y/o regionales privados desarrollan
                    acciones para la formación ciudadana?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txt34" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    3.5 ¿Cuáles son las acciones que estos actores/organizaciones municipales y/o regionales
                    privados desarrollan para la formación ciudadana?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txt35" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    3.6 ¿A quién van dirigidas estas acciones?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txt36" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="4">
                    4. Materiales de Formación ciudadana
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    4.1 ¿La SE cuenta con el material bibliográfico en temáticas relacionadas con formación
                    ciudadana para la consulta de los EE ?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="4">
                    <asp:RadioButton ID="rbtn41Si" runat="server" Text="Si" />
                    <asp:RadioButton ID="rbtn41No" runat="server" Text="No" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    4.1.1 Por favor enúncielos mencionando la fuente de consulta
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txt411" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="4">
                    5. Medios de Comunicación
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    5.1 ¿A través de qué medios de comunicación en el Municipio o en el Departamento
                    se promueven temas drelacionados con formación ciudadana ?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    Departamental/Municipal<br />
                    <asp:CheckBoxList ID="cblist51DedMun" runat="server">
                        <asp:ListItem>Radio</asp:ListItem>
                        <asp:ListItem>Prensa</asp:ListItem>
                        <asp:ListItem>Televisión</asp:ListItem>
                        <asp:ListItem>Internet</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td colspan="2">
                    Local<br />
                    <asp:CheckBoxList ID="cblist51Local" runat="server">
                        <asp:ListItem>Radio</asp:ListItem>
                        <asp:ListItem>Prensa</asp:ListItem>
                        <asp:ListItem>Televisión</asp:ListItem>
                        <asp:ListItem>Internet</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    5.2 ¿Cuales son las temáticas más comunes ?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txt52" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="4">
                    6. TICS
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    1.18 ¿Existen iniciativas activas de uso pedagógico de medios y TIC para la formación
                    de ciudadania ?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="4">
                    <asp:RadioButton ID="rbtn118Si" runat="server" Text="Si" />
                    <asp:RadioButton ID="rbtn118No" runat="server" Text="No" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    1.19 Qué iniciativas de uso pedagógico de medios y TIC para la formación de ciudadania
                    existen en la SE?
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txt119" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    Observaciones
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" 
                        Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btnAlmacenar" runat="server" Text="Almacenar" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
