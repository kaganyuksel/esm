﻿<%@ Page Language="C#" MasterPageFile="Site.master" CodeBehind="Default.aspx.cs"
    Inherits="ESM._Default" %>

<asp:Content ID="headContent" runat="Server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        $(document).ready(function () {

            $("#citastext").click(function () {

                var url = $("#ContentPlaceHolder1_citas").attr("href");
                window.location = url;
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content1" runat="Server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />
    <br />
    <br />
    <asp:GridView ID="Menu1" runat="server" AutoGenerateColumns="false" Visible="false"
        CssClass="DDGridView" RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6">
        <Columns>
            <asp:TemplateField HeaderText="Nombre de la tabla" SortExpression="TableName">
                <ItemTemplate>
                    <asp:DynamicHyperLink ID="HyperLink1" runat="server"><%# Eval("DisplayName") %></asp:DynamicHyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <table class="menuprincipalbyconfiguracion" runat="server" id="AdministracionUsuario"
        visible="false" cellspacing="10">
        <tr class="th">
            <td>
                <img alt="Modulo de Acceso a ESM" src="Icons/Security.png" />
            </td>
            <td colspan="3">
                <h1>
                    Modulo de Acceso a ESM</h1>
            </td>
        </tr>
        <tr>
            <td>
                <a href="/Roles/list.aspx">
                    <img src="Icons/Lock.png" alt="Perfiles de Usuario" /></a>
            </td>
            <td class="td">
                <h3 onclick="window.location='/Roles/list.aspx'">
                    Perfiles de Usuario</h3>
                <br />
                Administra la configuracion de acceso a la aplciación.
            </td>
            <td>
                <a href="/Usuarios/list.aspx">
                    <img src="Icons/Key.png" alt="Perfiles de Usuario" /></a>
            </td>
            <td class="td">
                <h3 onclick="window.location='/Usuarios/list.aspx'">
                    Usuarios</h3>
                <br />
                Administra la ifnromación los usuarios que de acuerdo a un perfil de usuario accederan
                al sistema.
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table class="menuprincipalbyconfiguracion" id="AdministracionConfiguracion" runat="server"
        visible="false" cellspacing="10">
        <tr class="th">
            <td>
                <img src="Icons/Stationery.png" alt="Modulo de Administración y Configuración">
            </td>
            <td colspan="3">
                <h1>
                    Modulo de Administración y Configuración</h1>
            </td>
        </tr>
        <tr>
            <td>
                <a href="/Establecimiento_Educativo/list.aspx">
                    <img src="/Icons/Tutorial.png" alt="Institución Educativa" /></a>
            </td>
            <td class="td">
                <h3 onclick="window.location='/Establecimiento_Educativo/list.aspx'">
                    Establecimiento Educativo</h3>
                <br />
                Almacena la información de los establecimientos educativos administrando cada una
                de las caracteristicas des la mismas.
            </td>
            <td>
                <a href="/Consultores/list.aspx">
                    <img src="/Icons/Paste.png" alt="Consultores" /></a>
            </td>
            <td class="td">
                <h3 onclick="window.location='/Consultores/list.aspx'">
                    Consultores</h3>
                <br />
                Almacena la información de los profesionales que realizaran la vista y evaluaran
                a los Establecimientos Educativos.
            </td>
        </tr>
        <tr>
            <td>
                <a href="/Ambientes/list.aspx">
                    <img src="/Icons/tag.png" alt="Ambientes" /></a>
            </td>
            <td class="td">
                <h3 onclick="window.location='/Ambientes/list.aspx'">
                    Ambientes</h3>
                <br />
                Administra la información de los Ambientes predeterminados o adicionales que se
                quieran evaluar.
            </td>
            <td>
                <a href="/Procesos/list.aspx">
                    <img src="/Icons/System.png" alt="Procesos" /></a>
            </td>
            <td class="td">
                <h3 onclick="window.location='/Procesos/list.aspx'">
                    Procesos</h3>
                <br />
                Asigna y administra la información de los procesos que heredan de un ambiente especifico.
            </td>
        </tr>
        <tr>
            <td>
                <a href="/Componentes/list.aspx">
                    <img src="/Icons/Template.png" alt="Actores" /></a>
            </td>
            <td class="td">
                <h3 onclick="window.location='/Componentes/list.aspx'">
                    Componentes</h3>
                <br />
                Asigna y administra la información de los componentes que heredan de un proceso
                y ambiente especifico.
            </td>
            <td>
                <a href="/Procesos/list.aspx">
                    <img src="/Icons/Write.png" alt="Preguntas" /></a>
            </td>
            <td class="td">
                <h3 onclick="window.location='/Preguntas/list.aspx'">
                    Crear Preguntas</h3>
                <br />
                Administra la información de las preguntas que hacen parte del formulario de evaluación,
                teniendo en cuenta los actores y privilegiados.
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table id="ModEval" runat="server" class="menuprincipalbyconfiguracion" cellspacing="10">
        <tr class="th">
            <td>
                <img src="/Icons/Edit.png" alt="Modulo de Administración y Configuración">
            </td>
            <td colspan="3">
                <h1>
                    Modulo de Evaluación</h1>
            </td>
        </tr>
        <tr>
            <td>
                <a id="citas" runat="server" href="/Citas.aspx">
                    <img src="Icons/Calender.png" alt="Evaluar" /></a>
            </td>
            <td class="td">
                <h3 id="citastext">
                    Citas
                </h3>
                <br />
                Verifica las citas que existen a cada uno de los Establecimientos Educativos y Secretarías
                de Educación.
            </td>
            <td>
                <a href="/MenuEvaluacion.aspx">
                    <img height="48px" src="/Icons/Certificate.png" alt="Evaluar" /></a>
            </td>
            <td class="td">
                <h3 onclick="window.location='/MenuEvaluacion.aspx'">
                    Realizar Evaluación</h3>
                <br />
                Comienza con el proceso de evaluación para determinar en estado del actor frente
                a los ambientes, procesos, componentes, y preguntas.
            </td>
        </tr>
        <tr id="consolidado" runat="server" visible="false">
            <td>
                <a href="/Consolidado.aspx">
                    <img src="/Icons/Stats.png" alt="Consolidado" /></a>
            </td>
            <td class="td">
                <h3 onclick="window.location='/Consolidado.aspx'">
                    Consolidado</h3>
                <br />
                Visualiza un consolidado que evalua los resultados obtenidos de cada una de las
                evaluaciones realizadas por cada uno de los actores.
            </td>
        </tr>
    </table>
    <table id="MEN" runat="server" visible="false" class="menuprincipalbyconfiguracion"
        cellspacing="10">
        <tr>
            <td style="width: 48px;">
                <a href="/AyudaPreguntas.aspx">
                    <img height="48px" src="Icons/Help.png" alt="AyudaPreguntas" /></a>
            </td>
            <td>
                <h3 onclick="window.location='/AyudaPreguntas.aspx'">
                    Ayuda Preguntas</h3>
                <br />
                Visualiza las preguntas almacendas por componente y asigna una descripción para
                las mismas.
            </td>
            <td class="td">
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
</asp:Content>
