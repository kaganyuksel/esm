﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="AyudaPreguntas.aspx.cs" Inherits="ESM.Preguntas.AyudaPreguntas" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Preguntas-Información Adicional</title>
    <script type="text/javascript">

        function habilitar(idcampo) {


            if ($(".text_" + idcampo).attr("disabled")) {

                $(".text_" + idcampo).attr("disabled", false);
            }
            else {
                $(".text_" + idcampo).attr("disabled", true);
            }

        }
            

        
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div style="width: 90%; margin: 0 40px;">
        <br />
        <br />
        <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: middle;
            height: 76px; text-align: left; font-size: 13px;">
            <tr>
                <td style="vertical-align: middle; width: 55px;">
                    <img src="/Icons/Template.png" height="48px" alt="Seleccion" />
                </td>
                <td style="vertical-align: middle;">
                    <h1 style="color: #005EA7; width: 100%; line-height: 48px;">
                        Asignacion de Descripcion para Preguntas</h1>
                    Asigna la descripción y seleccion de medios de verificación.
                </td>
            </tr>
        </table>
        <br />
        <br />
        <h3 style="color: #005EA7;">
            1. Seleccion de Componente</h3>
        <asp:TreeView ID="tvayuda" runat="server" AutoGenerateDataBindings="False" ImageSet="Arrows"
            OnSelectedNodeChanged="tvayuda_SelectedNodeChanged">
            <HoverNodeStyle Font-Underline="True" ForeColor="#005EA7" />
            <Nodes>
                <asp:TreeNode Text="Principal" Value="Principal"></asp:TreeNode>
            </Nodes>
            <NodeStyle Font-Names="Tahoma" Font-Size="12px" ForeColor="#005EA7" HorizontalPadding="3px"
                NodeSpacing="0px" VerticalPadding="0px" />
            <ParentNodeStyle Font-Bold="False" />
            <SelectedNodeStyle Font-Underline="True" ForeColor="#005EA7" HorizontalPadding="0px"
                VerticalPadding="0px" />
        </asp:TreeView>
    </div>
    <style type="text/css">
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
    <div class="demo" style="width: 95%; margin: 0 auto; clear: both;">
        <%--<div class="demo" style="clear: both;"> --%>
        <asp:Button ID="btndescPreguntas" CssClass="flotante" runat="server" Text="ok" OnClick="btndescPreguntas_Click"
            Visible="false" />
        <%--</div>--%>
        <br />
        <br />
        <h3 id="titulo1" runat="server" visible="false" style="color: #005EA7;">
            2. Preguntas para el componente
            <asp:Label Text="" ID="lblcomponente" runat="server" /></h3>
        <br />
        <style>
            .txtmiltilinepregunta
            {
                margin-left: 2px;
                margin-right: 2px;
                width: 49%;
                margin-top: 2px;
                margin-bottom: 2px;
                height: 86px;
            }
            .txtmultiline
            {
                margin-left: 2px;
                margin-right: 2px;
                width: 45%;
                margin-top: 2px;
                margin-bottom: 2px;
                height: 86px;
            }
            .gvPreguntas
            {
                border: 1px solid #dddddd;
                -moz-border-radius: 2px;
                -webkit-border-radius: 2px;
                border-radius: 2px; /*IE 7 AND 8 DO NOT SUPPORT BORDER RADIUS*/
                -moz-box-shadow: 0px 0px 2px #000000;
                -webkit-box-shadow: 0px 0px 2px #000000;
                box-shadow: 0px 0px 2px #000000; /*IE 7 AND 8 DO NOT SUPPORT BLUR PROPERTY OF SHADOWS*/
            }
            .gvPreguntas td
            {
                vertical-align: middle;
            }
            .gvPreguntasList
            {
                vertical-align: top;
            }
            .gvPreguntas h4
            {
                color: #005EA7;
            }
        </style>
        <asp:GridView ID="gvPreguntas" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvPreguntas_RowDataBound"
            Width="97%" CssClass="gvPreguntas">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="lblIdPregunta" runat="server" Text='<%# Eval("IdPregunta") %>'></asp:Label>
                        <img src="../Icons/Edit.png" height="24px" style="cursor: pointer;" onclick="habilitar(<%# Eval("IdPregunta") %>);"
                            alt="Editar" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pregunta">
                    <ItemTemplate>
                        <h4>
                            Pregunta y Descripción</h4>
                        <asp:TextBox CssClass="txtmiltilinepregunta" TextMode="MultiLine" ID="txtPregunta"
                            runat="server" Text='<%# Eval("Pregunta1") %>'></asp:TextBox>
                        <asp:TextBox CssClass="txtmultiline" TextMode="MultiLine" ID="txtDescPre" runat="server"></asp:TextBox>
                        <h4>
                            Actores</h4>
                        <asp:CheckBoxList ID="listActores" runat="server" Font-Size="12px" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Profesional de Campo" />
                            <asp:ListItem Text="Estudiantes" />
                            <asp:ListItem Text="Educadores" />
                            <asp:ListItem Text="Directivos" />
                            <asp:ListItem Text="Padres" />
                        </asp:CheckBoxList>
                        <h4>
                            Medios de Verificación</h4>
                        <asp:CheckBoxList ID="listMedios" Font-Size="12px" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Text="Lectura" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Participación" Value="2"></asp:ListItem>
                        </asp:CheckBoxList>
                        <h4>
                            Documentos de Consulta</h4>
                        <asp:CheckBoxList ID="listDocuments" Font-Size="12px" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="PEI" Value="1"></asp:ListItem>
                            <asp:ListItem Text="PMI" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Manual de Convivencia" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Planes de Estudio" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Documentos de proyectos pedagógicos" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Otros" Value="6"></asp:ListItem>
                        </asp:CheckBoxList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <br />
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>