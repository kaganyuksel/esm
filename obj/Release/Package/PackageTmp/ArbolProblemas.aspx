﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ArbolProblemas.aspx.cs" Inherits="ESM.ArbolProblemas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Pretty/css/prettyPhoto.css" rel="stylesheet" charset="utf-8" media="screen"
        type="text/css" />
    <script src="/Pretty/js/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>
    <link href="Style/MarcoLogico.css" rel="stylesheet" type="text/css" />
    <link href="Style/jsgantt.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jsgantt.js" type="text/javascript"></script>
    <link href="Style/menu_arbol.css" rel="stylesheet" type="text/css" />
    <link href="Style/Proyecto.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/iColorPicker.js" type="text/javascript"></script>
    <script src="Scripts/Proyectos.js" type="text/javascript"></script>
    <style type="text/css">
        .ayuda_marco
        {
            position: absolute;
            width: 20%;
            height: 700px;
            border: 3px solid #F7F7F7;
            background: #F2F2F2;
            top: 140px;
            right: 5%;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            border-radius: 4px; /*IE 7 AND 8 DO NOT SUPPORT BORDER RADIUS*/
            -moz-box-shadow: 0px 0px 3px #8b8b8b;
            -webkit-box-shadow: 0px 0px 3px #8b8b8b;
            box-shadow: 0px 0px 3px #8b8b8b; /*IE 7 AND 8 DO NOT SUPPORT BLUR PROPERTY OF SHADOWS*/
            overflow-y: scroll;
        }
        .ayuda_marco p
        {
            width: 100%;
        }
        #accordion
        {
            text-align: justify;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="menu_proyecto" style="margin-left: 10%; width: 90%; position: fixed; z-index: 8000;">
        <ul style="list-style-type: none; display: inline; top: 0; width: 800px;">
            <li style="width: 50px; float: left; vertical-align: middle;">
                <input id="btnvolver" type="button" onclick="SlideVolver();" class="btnleft ui-button ui-widget ui-state-default ui-corner-all"
                    role="button" aria-disabled="false" />
            </li>
            <li id="li_arbol_problemas" style="width: 150px; float: left; vertical-align: middle;
                border: dashed 2px #ccc; background: #004464; color: #fff; font-size: 0.8em;
                height: 30px; text-align: center; line-height: 30px; -moz-border-radius: 5px 0px 0px 5px;
                -webkit-border-radius: 5px 0px 0px 5px; border-radius: 5px 0px 0px 5px; /*ie 7 and 8 do not support border radius*/">
                Árbol Problemas</li>
            <li id="li_procesos" style="width: 100px; float: left; vertical-align: middle; background: #007cb6;
                color: #fff; font-size: 0.8em; height: 30px; text-align: center; line-height: 30px;">
                Procesos</li>
            <li id="li_Subprocesos" style="width: 100px; float: left; vertical-align: middle;
                background: #007cb6; color: #fff; font-size: 0.8em; height: 30px; text-align: center;
                line-height: 30px;">Subprocesos</li>
            <li id="li_marco_logico" style="width: 100px; float: left; vertical-align: middle;
                background: #007cb6; color: #fff; font-size: 0.8em; height: 30px; text-align: center;
                line-height: 30px;">Estrategias</li>
            <li id="li_plan_operativo" style="width: 100px; float: left; vertical-align: middle;
                background: #007cb6; color: #fff; font-size: 0.8em; height: 30px; text-align: center;
                line-height: 30px;">Actividades</li>
            <li id="li_cronograma" style="width: 100px; float: left; vertical-align: middle;
                background: #007cb6; color: #fff; font-size: 0.8em; height: 30px; text-align: center;
                line-height: 30px; -moz-border-radius: 0px 5px 5px 0px; -webkit-border-radius: 0px 5px 5px 0px;
                border-radius: 0px 5px 5px 0px; /*ie 7 and 8 do not support border radius*/">Cronograma</li>
            <li style="width: 50px; float: left; vertical-align: middle; height: 30px;">
                <input id="btnsiguiente" type="button" onclick="SlideSiguiente();" class="btnright ui-button ui-widget ui-state-default ui-corner-all"
                    role="button" aria-disabled="false" />
            </li>
        </ul>
    </div>
    <br />
    <br />
    <div class="demo" style="width: 90%; margin: 0 auto; clear: both;">
        <div id="slides" style="display: block; width: 7000px; clear: both; overflow: hidden;">
            <div id="izquierda" style="width: 25%; float: left;" class="demo mover presente">
                <div style="width: 50%;">
                    <asp:SqlDataSource ID="sqldtActividades" runat="server" ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>"
                        DeleteCommand="DELETE FROM [Actividades] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Actividades] ([Resultado_id], [Actividad], [Presupuesto]) VALUES (@Resultado_id, @Actividad, @Presupuesto)"
                        SelectCommand="SELECT * FROM [Actividades]" UpdateCommand="UPDATE [Actividades] SET [Resultado_id] = @Resultado_id, [Actividad] = @Actividad, [Presupuesto] = @Presupuesto WHERE [Id] = @Id">
                        <DeleteParameters>
                            <asp:Parameter Name="Id" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="Resultado_id" Type="Int32" />
                            <asp:Parameter Name="Actividad" Type="String" />
                            <asp:Parameter Name="Presupuesto" Type="Decimal" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Resultado_id" Type="Int32" />
                            <asp:Parameter Name="Actividad" Type="String" />
                            <asp:Parameter Name="Presupuesto" Type="Decimal" />
                            <asp:Parameter Name="Id" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <br />
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <img src="/Icons/network.png" width="48px" alt="Evaluacion" />
                                </td>
                                <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                                    <h1 style="color: #0b72bc;">
                                        Árbol de Problemas</h1>
                                    Paso No. 1 de 6
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div id="divseleccion" runat="server">
                        <h2 style="color: #005EA7;">
                            * Seleccione la opción que desea:</h2>
                        <br />
                        <asp:Button ID="btnnuevo" Text="Nuevo Proyecto" runat="server" OnClick="btnnuevo_Click" />
                        <asp:Button ID="btnCargar" Text="Cargar Proyecto" runat="server" OnClick="btnCargar_Click" />
                    </div>
                    <div class="problema" runat="server" id="divproyectos" visible="false">
                        <h2>
                            * Proyectos Existentes:</h2>
                        <br />
                        <asp:GridView ID="gvProyectos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            Width="80%" DataSourceID="lnqProyectos" OnSelectedIndexChanged="gvProyectos_SelectedIndexChanged">
                            <AlternatingRowStyle CssClass="trblanca" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" HeaderStyle-Width="24px" SelectImageUrl="~/Icons/Stationery.png"
                                    ShowSelectButton="True">
                                    <ControlStyle Width="24px" />
                                </asp:CommandField>
                                <asp:BoundField DataField="Id" HeaderStyle-Width="30px" HeaderText="Id" ReadOnly="True"
                                    SortExpression="Id" />
                                <asp:BoundField DataField="Problema" HeaderText="Problema" SortExpression="Problema"
                                    ReadOnly="True" />
                            </Columns>
                            <HeaderStyle CssClass="trheader" />
                            <RowStyle CssClass="trgris" />
                        </asp:GridView>
                        <asp:LinqDataSource ID="lnqProyectos" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
                            EntityTypeName="" Select="new (Id, Problema)" TableName="Proyectos">
                        </asp:LinqDataSource>
                    </div>
                    <div id="divNuevo" runat="server" visible="false">
                        <%--<h1>
                            <img width="24px" src="/Icons/System.png" alt="Administración" />
                            Nuevo Proyecto</h1>--%>
                    </div>
                    <div id="divCargado" runat="server" visible="false">
                        <%--<h1>
                            <img width="24px" src="/Icons/System.png" alt="Administración" />
                            Administración del Proyecto</h1>--%>
                    </div>
                    <br />
                    <div class="problema" runat="server" id="Mod_Name_Project" visible="false" style="border: dashed 2px #005EA7;
                        -moz-border-radius: 3px; -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px;">
                        <br />
                        <span style="font-size: 22px; color: #005EA7;">Nombre de Proyecto</span>
                        <br />
                        <asp:TextBox ID="txtname_project" runat="server" TextMode="MultiLine" placeholder="Nombre de Proyecto" />
                        <br />
                        <p style="font-style: italic;">
                            * Nombre de proyecto.</p>
                        <br />
                    </div>
                    <br />
                    <div class="problema" runat="server" id="divproblema" visible="false" style="border: dashed 2px #005EA7;
                        -moz-border-radius: 3px; -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px;">
                        <br />
                        <span style="font-size: 22px; color: #005EA7;">Problema Central </span><span style="color: #6E6E6E;"
                            class="style1">&gt; Proposito</span>
                        <br />
                        <asp:TextBox ID="txtproblema" runat="server" TextMode="MultiLine" placeholder="1. Descripcion del Problema" />
                        <asp:LinkButton Text="<img src='/Icons/save-icon.png' width='24px' alt='save project' />"
                            runat="server" ID="lknAlmacenarP" OnClick="lknAlmacenarP_Click" />
                        <br />
                        <p style="font-style: italic;">
                            * Descripción del problema central para el proyecto actual.</p>
                        <%--<input class="speech" id="probleman" style="width: 15px; border: 0;" />--%>
                        <br />
                    </div>
                    <br />
                    <div class="efectos" runat="server" id="divefectos" visible="false" style="border: dashed 2px #0091B2;
                        -moz-border-radius: 3px; -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px;">
                        <table border="0" cellpadding="0" cellspacing="0" width="80%">
                            <tr>
                                <td>
                                    <br />
                                    <h2 style="color: #0091B2;">
                                        Causas <span class="style1" style="color: #6E6E6E;">&gt; Resultados</span> y
                                    </h2>
                                    <h2 style="color: #0091B2;">
                                        Efectos <span class="style1" style="color: #6E6E6E;">&gt; Finalidad</span></h2>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Causa:
                                </td>
                                <td>
                                    Efecto:
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%;">
                                    <asp:TextBox ID="txtCausa1" runat="server" class="txtareacausa" placeholder="1. Descripcion de la causa"
                                        TextMode="MultiLine" Width="100%" />
                                </td>
                                <td style="width: 50%;">
                                    <asp:TextBox ID="txtEfecto1" runat="server" class="txtareaefecto" placeholder="2. Descripcion del efecto"
                                        TextMode="MultiLine" Width="100%" />
                                </td>
                                <td>
                                    <input class="iColorPicker" type="text" style="width: 60px;" id="mycolor" runat="server"
                                        value="#ffffff" />
                                </td>
                                <td style="text-align: center;">
                                    <asp:LinkButton Text='<img src="/Icons/save-icon.png" width="24px" alt="save efect" />'
                                        runat="server" ID="lknAlmacenarE" OnClick="lknAlmacenarE_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p style="font-style: italic;">
                                        * 1. Describa en un minimo de 3 palabras la causa que desea almacenar.</p>
                                </td>
                                <td>
                                    <p style="font-style: italic;">
                                        * 2. Describa el efecto corresponsiete a la causa anterior.</p>
                                </td>
                                <td>
                                    <p style="font-style: italic;">
                                        * 3. Seleccione un color.
                                    </p>
                                </td>
                                <td>
                                    <p style="font-style: italic;">
                                        * 4. Almacenar.</p>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="gvEfectos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="Id" DataSourceID="sqlefectos" PageSize="15" Width="80%" AllowSorting="True">
                            <AlternatingRowStyle CssClass="trblanca" />
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True"
                                    SortExpression="Id" />
                                <asp:BoundField DataField="Causa" HeaderText="Causa" SortExpression="Causa" />
                                <asp:BoundField DataField="Efecto" HeaderText="Efecto" SortExpression="Efecto" />
                                <asp:CommandField ButtonType="Image" CancelImageUrl="~/Icons/cancel.png" DeleteImageUrl="~/Icons/Bin_Full.png"
                                    EditImageUrl="~/Icons/Stationery.png" ShowDeleteButton="True" ShowEditButton="True"
                                    UpdateImageUrl="~/Icons/save-icon.png">
                                    <ControlStyle Width="24px" CssClass="reload" />
                                </asp:CommandField>
                            </Columns>
                            <HeaderStyle CssClass="trheader" />
                            <RowStyle CssClass="trgris" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="sqlefectos" runat="server" ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>"
                            DeleteCommand="DELETE FROM [Causas_Efectos] WHERE [Id] = @original_Id" InsertCommand="INSERT INTO [Causas_Efectos] ([Efecto], [Causa], [Color]) VALUES (@Efecto, @Causa, @Color)"
                            OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [Efecto], [Causa], [Color], [Id] FROM [Causas_Efectos] WHERE ([Proyecto_id] = @Proyecto_id)"
                            UpdateCommand="UPDATE [Causas_Efectos] SET [Efecto] = @Efecto, [Causa] = @Causa, [Color] = @Color WHERE [Id] = @original_Id">
                            <DeleteParameters>
                                <asp:Parameter Name="original_Id" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="Efecto" Type="String" />
                                <asp:Parameter Name="Causa" Type="String" />
                                <asp:Parameter Name="Color" Type="String" />
                            </InsertParameters>
                            <SelectParameters>
                                <asp:SessionParameter DefaultValue="0" Name="Proyecto_id" SessionField="idproyecto"
                                    Type="Int32" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="Efecto" Type="String" />
                                <asp:Parameter Name="Causa" Type="String" />
                                <asp:Parameter Name="Color" Type="String" />
                                <asp:Parameter Name="original_Id" Type="Int32" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                        <asp:Button Text="Volver" runat="server" ID="btnvolver" OnClick="btnvolver_Click" />
                    </div>
                    <br />
                    <br />
                </div>
            </div>
            <div id="Mod_Procesos" style="width: 25%; margin: 0 auto; float: left; color: #005EA7;"
                class="demo mover futuro">
                <br />
                <div>
                    <table>
                        <tr>
                            <td>
                                <img src="/Icons/marco_logico.png" width="48px" alt="Evaluacion" />
                            </td>
                            <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                                <h1 style="color: #0b72bc;">
                                    Procesos</h1>
                                Paso No. 2 de 6
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="problema" style="color: #005EA7; font-size: 1em; width: 50%; -moz-border-radius: 3px;
                        -webkit-border-radius: 3px; border-radius: 3px;">
                        <div class="problema" runat="server" style="border: dashed 2px #005EA7; -moz-border-radius: 3px;
                            -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px;">
                            <br />
                            <span style="font-size: 22px; color: #005EA7;">Nombre de Proyecto</span>
                            <br />
                            <asp:TextBox ID="txtname_project_pro" runat="server" TextMode="MultiLine" placeholder="Nombre de Proyecto" />
                            <br />
                            <p style="font-style: italic;">
                                * Nombre de proyecto.</p>
                            <br />
                        </div>
                        <br />
                        <div style="border: dashed 2px #005EA7; padding-left: 50px;">
                            <br />
                            <h1>
                                <span style="color: #6E6E6E;" class="style1">Problema Central &gt;</span> Propósito</h1>
                            <asp:TextBox ID="txtProposito" runat="server" TextMode="MultiLine" /><asp:LinkButton
                                ID="lknAlmacenarProposito" Text="<img Width='24px' src='/Icons/save-icon.png' alt='Almacenar Proposito' />"
                                runat="server" OnClick="lknAlmacenarProposito_Click" /><br />
                            <p style="font-style: italic; color: #000;">
                                * Descripción del propósito para el proyecto actual.</p>
                            <br />
                        </div>
                        <br />
                        <div class="problema" style="border: dashed 2px #00A9B5; -moz-border-radius: 3px;
                            -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px;">
                            <br />
                            <h1 style="color: #00A9B5;">
                                <span style="color: #6E6E6E;" class="style1">Efectos &gt;</span> Finalidad</h1>
                            <asp:TextBox ID="txtFinalidad" runat="server" TextMode="MultiLine" Width="80%" Style="margin: 0 auto;
                                border: solid 2px #00A9B5;" />
                            <asp:LinkButton ID="lknAlmacenarFinalidad" Text="<img Width='24px' src='/Icons/save-icon.png' alt='Almacenar Proposito' />"
                                runat="server" OnClick="lknAlmacenarFinalidad_Click" />
                            <br />
                            <p style="font-style: italic; color: #000;">
                                * Descripción de la finalidad para el proyecto actual.</p>
                            <br />
                            <a class="Visualizar_Matriz" style="color: #000; font-style: italic; text-decoration: none;"
                                href="#">
                                <img src="/Icons/Search.png" width="24px" alt="Matriz" />
                                Visualizar Matriz</a><br />
                            <br />
                            <a class="adetalles" style="color: #000; font-style: italic; text-decoration: none;"
                                href="#">
                                <img src="/Icons/details.png" width="24px" alt="Detalles" />
                                Administración de Indicadores, Medios de verificación y supuestos.</a><br />
                            <br />
                            <a class="Cronograma_Proyecto" style="color: #000; font-style: italic; text-decoration: none;"
                                href="#">
                                <img src="/Icons/Calender.png" width="24px" alt="Cronograma" />
                                Cronograma general para el proyecto actual.</a><br />
                            <br />
                        </div>
                        <br />
                        <asp:Panel ID="pnl_procesos" runat="server" Style="border: dashed 2px #357D28; -moz-border-radius: 3px;
                            -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px;">
                            <h2>
                                <br />
                                <span style="color: #6E6E6E;" class="style1">Causas &gt; </span>Procesos</h2>
                            <br />
                        </asp:Panel>
                    </div>
                    <br />
                </div>
                <br />
                <br />
                <br />
            </div>
            <div id="Mod_Subprocesos" style="width: 25%; margin: 0 auto; float: left; color: #005EA7;"
                class="demo mover">
                <br />
                <div>
                    <table>
                        <tr>
                            <td>
                                <img src="/Icons/marco_logico.png" width="48px" alt="Evaluacion" />
                            </td>
                            <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                                <h1 style="color: #0b72bc;">
                                    SubProcesos</h1>
                                Paso No. 3 de 6
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="problema" style="color: #005EA7; font-size: 1em; width: 50%; -moz-border-radius: 3px;
                        -webkit-border-radius: 3px; border-radius: 3px;">
                        <div class="problema" runat="server" style="border: dashed 2px #005EA7; -moz-border-radius: 3px;
                            -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px;">
                            <br />
                            <span style="font-size: 22px; color: #005EA7;">Nombre de Proyecto</span>
                            <br />
                            <asp:TextBox ID="txtname_project_sub" runat="server" TextMode="MultiLine" placeholder="Nombre de Proyecto" />
                            <br />
                            <p style="font-style: italic;">
                                * Nombre de proyecto.</p>
                            <br />
                        </div>
                        <br />
                        <div style="border: dashed 2px #005EA7; padding-left: 50px;">
                            <br />
                            <h1>
                                <span style="color: #6E6E6E;" class="style1">Problema Central &gt;</span> Propósito</h1>
                            <asp:TextBox ID="txtProposito_sub" runat="server" TextMode="MultiLine" /><br />
                            <p style="font-style: italic; color: #000;">
                                * Descripción del propósito para el proyecto actual.</p>
                            <br />
                        </div>
                        <br />
                        <div class="problema" style="border: dashed 2px #00A9B5; -moz-border-radius: 3px;
                            -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px;">
                            <br />
                            <h1 style="color: #00A9B5;">
                                <span style="color: #6E6E6E;" class="style1">Efectos &gt;</span> Finalidad</h1>
                            <asp:TextBox ID="txtFinalidad_Sub" runat="server" TextMode="MultiLine" Width="80%"
                                Style="margin: 0 auto; border: solid 2px #00A9B5;" />
                            <br />
                            <p style="font-style: italic; color: #000;">
                                * Descripción de la finalidad para el proyecto actual.</p>
                            <br />
                            <a class="Visualizar_Matriz" style="color: #000; font-style: italic; text-decoration: none;"
                                href="#" class="Visualizar_Matriz">
                                <img src="/Icons/Search.png" width="24px" alt="Matriz" />
                                Visualizar Matriz</a>
                            <br />
                            <br />
                            <a class="adetalles" style="color: #000; font-style: italic; text-decoration: none;"
                                href="#">
                                <img src="/Icons/details.png" width="24px" alt="Detalles" />
                                Administración de Indicadores, Medios de verificación y supuestos.</a><br />
                            <br />
                            <a class="Cronograma_Proyecto" style="color: #000; font-style: italic; text-decoration: none;"
                                href="#cronogramageneral">
                                <img src="/Icons/Calender.png" width="24px" alt="Cronograma" />
                                Cronograma general para el proyecto actual.</a><br />
                            <br />
                        </div>
                        <br />
                        <asp:Panel ID="pnl_Subprocesos" runat="server" Style="border: dashed 2px #357D28;
                            -moz-border-radius: 3px; -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px;">
                            <h2>
                                Subprocesos</h2>
                            <br />
                        </asp:Panel>
                    </div>
                    <br />
                </div>
                <br />
                <br />
                <br />
            </div>
            <div id="derecha" style="width: 25%; margin: 0 auto; float: left; color: #005EA7;"
                class="demo mover">
                <br />
                <div>
                    <table>
                        <tr>
                            <td>
                                <img src="/Icons/marco_logico.png" width="48px" alt="Evaluacion" />
                            </td>
                            <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                                <h1 style="color: #0b72bc;">
                                    Estrategias</h1>
                                Paso No. 4 de 6
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="problema" style="color: #005EA7; font-size: 1em; width: 50%; -moz-border-radius: 3px;
                        -webkit-border-radius: 3px; border-radius: 3px;">
                        <div class="problema" runat="server" style="border: dashed 2px #005EA7; -moz-border-radius: 3px;
                            -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px;">
                            <br />
                            <span style="font-size: 22px; color: #005EA7;">Nombre de Proyecto</span>
                            <br />
                            <asp:TextBox ID="txtname_project_estra" runat="server" TextMode="MultiLine" placeholder="Nombre de Proyecto" />
                            <br />
                            <p style="font-style: italic;">
                                * Nombre de proyecto.</p>
                            <br />
                        </div>
                        <br />
                        <div style="border: dashed 2px #005EA7; padding-left: 50px;">
                            <br />
                            <h1>
                                <span style="color: #6E6E6E;" class="style1">Problema Central &gt;</span> Propósito</h1>
                            <asp:TextBox ID="txtProposito_Estra" runat="server" TextMode="MultiLine" /><br />
                            <p style="font-style: italic; color: #000;">
                                * Descripción del propósito para el proyecto actual.</p>
                            <br />
                        </div>
                        <br />
                        <div class="problema" style="border: dashed 2px #00A9B5; -moz-border-radius: 3px;
                            -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px;">
                            <br />
                            <h1 style="color: #00A9B5;">
                                <span style="color: #6E6E6E;" class="style1">Efectos &gt;</span> Finalidad</h1>
                            <asp:TextBox ID="txtfinalidad_estra" runat="server" TextMode="MultiLine" Width="80%"
                                Style="margin: 0 auto; border: solid 2px #00A9B5;" />
                            <br />
                            <p style="font-style: italic; color: #000;">
                                * Descripción de la finalidad para el proyecto actual.</p>
                            <br />
                            <a id="Visualizar_Matriz" class="Visualizar_Matriz" style="color: #000; font-style: italic;
                                text-decoration: none;" href="#">
                                <img src="/Icons/Search.png" width="24px" alt="Matriz" />
                                Visualizar Matriz</a>
                            <br />
                            <br />
                            <a id="adetalles" style="color: #000; font-style: italic; text-decoration: none;"
                                href="#">
                                <img class="adetalles" src="/Icons/details.png" width="24px" alt="Detalles" />
                                Administración de Indicadores, Medios de verificación y supuestos.</a><br />
                            <br />
                            <a id="Cronograma_Proyecto" class="Cronograma_Proyecto" style="color: #000; font-style: italic;
                                text-decoration: none;" href="#cronogramageneral">
                                <img src="/Icons/Calender.png" width="24px" alt="Cronograma" />
                                Cronograma general para el proyecto actual.</a><br />
                            <br />
                        </div>
                        <br />
                        <asp:Panel ID="presultados" runat="server" Style="border: dashed 2px #357D28; -moz-border-radius: 3px;
                            -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px;">
                        </asp:Panel>
                    </div>
                    <br />
                </div>
                <br />
                <br />
                <br />
            </div>
            <div id="derechaSiguiente" style="width: 24%; margin: 0 auto; float: left;" class="demo mover">
                <br />
                <br />
                <div>
                    <table>
                        <tr>
                            <td>
                                <img src="/Icons/plan_operativo.png" width="48px" alt="Plan Operativo" />
                            </td>
                            <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                                <h1 style="color: #0b72bc;">
                                    Actividades</h1>
                                Paso No. 5 de 6
                            </td>
                        </tr>
                    </table>
                    <div class="problema" runat="server" style="border: dashed 2px #005EA7; -moz-border-radius: 3px;
                        -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px; width: 50%;">
                        <br />
                        <span style="font-size: 22px; color: #005EA7;">Nombre de Proyecto</span>
                        <br />
                        <asp:TextBox ID="txtnameproject_activ" runat="server" TextMode="MultiLine" placeholder="Nombre de Proyecto" />
                        <br />
                        <p style="font-style: italic;">
                            * Nombre de proyecto.</p>
                        <br />
                    </div>
                    <br />
                    <div class="problema" style="border: dashed 2px #005EA7; padding-left: 50px; width: 50%;">
                        <br />
                        <h1 style="color: #005EA7;">
                            <span style="color: #6E6E6E;" class="style1">Problema Central &gt;</span> Propósito</h1>
                        <asp:TextBox ID="txtproposito_po" runat="server" TextMode="MultiLine" /><br />
                        <p style="font-style: italic; color: #000;">
                            * Descripción del propósito para el proyecto actual.</p>
                        <br />
                    </div>
                    <br />
                    <div class="problema" style="border: dashed 2px #00A9B5; -moz-border-radius: 3px;
                        -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px; width: 50%;">
                        <br />
                        <h1 style="color: #00A9B5;">
                            <span style="color: #6E6E6E;" class="style1">Efectos &gt;</span> Finalidad</h1>
                        <asp:TextBox ID="txtfinalidad_po" runat="server" TextMode="MultiLine" Width="80%"
                            Style="margin: 0 auto; border: solid 2px #00A9B5;" />
                        <br />
                        <p style="font-style: italic; color: #000;">
                            * Descripción de la finalidad para el proyecto actual.</p>
                        <br />
                        <a id="PlanOperativo_a" style="color: #000; font-style: italic; text-decoration: none;"
                            href="#" class="Visualizar_Matriz">
                            <img src="/Icons/Search.png" width="24px" alt="Matriz" />
                            Visualizar Matriz</a>
                        <br />
                        <br />
                    </div>
                    <br />
                    <div class="problema" style="border: dashed 2px #357D28; -moz-border-radius: 3px;
                        -webkit-border-radius: 3px; border-radius: 3px; padding-left: 50px; width: 50%;">
                        <br />
                        <h1>
                            * Actividades</h1>
                        <br />
                        <p style="font-style: italic; color: #000;">
                            * En la parte inferior de cada uno de los resultados se encuentra un modulo
                            <br />
                            que contiene las actividades correspondientes para el mismo.
                            <br />
                            <br />
                            <input type="button" onclick="ActivateAcordion();" value="Expandir Actividades" role="button"
                                aria-disabled="false" class="ui-button ui-widget ui-state-default ui-corner-all" />
                            <b>Nota:</b> para visualizar todas las actividades al mismo tiempo basta con dar
                            click en el boton "Expandir Actividades".</p>
                        <br />
                        <br />
                        <asp:Panel ID="pnlActividades" runat="server">
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div id="Cronograma" style="width: 25%; float: left; display: none;" class="demo mover">
                <br />
                <br />
                <div>
                    <table>
                        <tr>
                            <td>
                                <img src="/Icons/Calender.png" width="48px" alt="Plan Operativo" />
                            </td>
                            <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                                <h1 style="color: #0b72bc; width: 50%;">
                                    Cronograma General</h1>
                                Paso No. 6 de 6
                            </td>
                        </tr>
                    </table>
                    <div class="problema" style="border: 2px solid #ccc; color: #005EA7; width: 50%;">
                        <br />
                        <asp:HiddenField ID="HFTempDate" runat="server" />
                        <div style="position: relative; width: 100%;" class="gantt" id="GanttChartDIV">
                        </div>
                    </div>
                    <%--<asp:LinkButton Text="Export to Excel" runat="server" ID="lknexport_gantt" OnClick="lknexport_gantt_Click" />--%>
                    <table id="t_gantt" visible="false" runat="server" border="0" cellpadding="0" cellspacing="0">
                    </table>
                </div>
            </div>
            <br />
            <br />
            <br />
        </div>
    </div>
    <div id="ayuda" class="ayuda_marco">
        <div id="accordion">
            <h3>
                <a href="#">Introducción</a></h3>
            <div>
                El proyecto es la unidad operacional de la planeación del desarrollo que vincula
                recursos, actividades y productos durante un periodo determinado y con una ubicación
                definida para la resolución de problemas o necesidades sentidas de la población.
                Un proyecto supone la búsqueda de una alternativa viable al planteamiento de un
                objetivo que está concebido para resolver un problema o necesidad y que para ello
                requiere la producción de bienes o servicios, de tal suerte que, una vez tomada
                la decisión de llevar a cabo un proyecto, sea necesaria la realización de una serie
                de actividades previstas que conllevarán al logro de los objetivos propuestos para
                el proyecto (www.dnp.gov.co) Para tal efecto y de conformidad con los ejercicios
                adelantados previamente con los grupos de trabajo del Programa de Competencias del
                Ministerio de Educación Nacional se describen los pasos adelantados para el cumplimento
                de los objetivos del programa así como de cada una de sus líneas de acción.
                <br />
                <br />
                <ol>
                    <li>IDENTIFICACIÓN </li>
                    <li>DISEÑO Y FORMULACIÓN</li>
                    <li>EJECUCIÓN Y SEGUIMIENTO</li>
                    <li>EVALUACIÓN POSTERIOR</li>
                </ol>
            </div>
            <h3>
                <a href="#">Ciclo del Proyecto</a></h3>
            <div>
                El proyecto es la unidad operacional de la planeación del desarrollo que vincula
                recursos, actividades y productos durante un periodo determinado y con una ubicación
                definida para la resolución de problemas o necesidades sentidas de la población.
                Un proyecto supone la búsqueda de una alternativa viable al planteamiento de un
                objetivo que está concebido para resolver un problema o necesidad y que para ello
                requiere la producción de bienes o servicios , de tal suerte que, una vez tomada
                la decisión de llevar a cabo un proyecto, sea necesaria la realización de una serie
                de actividades previstas que conllevarán al logro de los objetivos propuestos para
                el proyecto (www.dnp.gov.co)
                <br />
                <br />
                <a href="/Icons/cicloproyecto.png">
                    <img src="/Icons/cicloproyecto.png" width="100%" /></a>
            </div>
            <h3>
                <a href="#">Identificación</a></h3>
            <div>
                También conocida como pre inversión, comporta la primera etapa de formulación del
                proyecto y tiene por objetivo el acopio y preparación de información suficiente
                y pertinente (en ocasiones organizada en estudios técnicos, económicos, financieros,
                legales y de mercado) para determinar de forma preliminar la posibilidad real de
                resolver un problema o satisfacer una necesidad así como reducir la incertidumbre
                en el logro de los objetivos de dicha empresa. ANÁLISIS DE ACTORES PARTICIPANTES
                Consiste en identificar las personas, grupos, entidades o instituciones públicas
                o privadas que de alguna forma se relacionan con el proyecto. Debe incorporar los
                intereses, expectativas representaciones y demás de dichos actores y que pueden
                resultar de importancia para el proyecto: ¿Cómo elaborar el análisis de la participación?
                Identifique todos aquellos actores relacionados con el proyecto y que se pudieran
                verse beneficiados o incluso afectados por la ejecución del mismo. Puede categorizarlos
                según su nivel o ámbito (Nacional, regional, local etc.) Puede categorizarlos también
                según sean afectados, beneficiados, cooperantes, oponentes, o perjudicados. De las
                anteriores categorizaciones proceda a determinar cómo pueden ser incorporados en
                el desarrollo del proyecto. Matriz de actores participantes.
            </div>
            <h3>
                <a href="#">- Análisis de actores</a></h3>
            <div>
                Identificación del problema o necesidad: Como ya se había expuesto, un proyecto
                supone la búsqueda de una alternativa viable al planteamiento de un objetivo que
                está concebido para resolver un problema o necesidad y que para ello requiere la
                producción de bienes o servicios , de tal suerte que, una vez tomada la decisión
                de llevar a cabo un proyecto, sea necesaria la realización de una serie de actividades
                previstas que conllevarán al logro de los objetivos propuestos para el proyecto
                Es un conjunto de técnicas para:
                <br />
                <br />
                <ul>
                    <li>Analizar la situación en relación con un problema</li>
                    <li>Identificar los problemas principales de este contexto</li>
                    <li>Visualizar las relaciones de causa efecto en el árbol de problemas • Definir el
                        problema central de la situación</li>
                </ul>
                <br />
                <br />
                EL ÁRBOL DE PROBLEMAS Es una técnica que se emplea para identificar una situación
                problemática la cual se intenta solucionar mediante la intervención del proyecto
                utilizando una relación de tipo causa-efecto. El identificar de forma clara la situación
                problemática no siempre es un ejercicio fácil, suele pasar que, al identificar un
                problema emergen muchos otros asociados algunos de los cuáles se nos pueden presentar
                como causas, o efectos del mismo o incluso hacer dudar sobre si, el problema inicialmente
                considerado esta correctamente formulado.
                <br />
                <br />
                ¿Cómo realizar un árbol de problemas? Se recomienda realizar las siguientes tareas
                <ol>
                    <li>Convoque a los miembros de su grupo de trabajo o personas de la comunidad interesadas.</li>
                    <li>Describa de forma general el objetivo del proceso y subraye en la necesidad de identificar
                        de forma concertada el problema a resolver </li>
                    <li>Entregue a cada uno de ellos un paquete de tarjetas y solicite que escriban en ellas
                        los problemas de su comunidad, entidad y organización solicite guardar especial
                        cuidado de: a. Formular el problema como una situación negativa. b. Utilizar una
                        oración corta con palabras que sean, claras, simples y concretas. c. Identificar
                        únicamente los problemas existentes. Descarte los posibles o potenciales. </li>
                    <li>¿Cómo elaborar el árbol?: Dibuje el tronco de un árbol para representar su problema
                        central. Añada raíces y radículas para representar las causas directas e indirectas,
                        y ramas y ramitas para representar los efectos (o implicaciones) directos e indirectos
                        de su problema central (ver gráfico 03) </li>
                    <li>Con la ayuda de un facilitador, así como de todos los participantes ubique las tarjetas
                        comience según sean causas directas, indirectas, efectos directos o indirectos
                    </li>
                    <li>En este nivel puede hacer uso de una matriz de Vester para la calificación de los
                        diferentes causas</li>
                </ol>
            </div>
        </div>
    </div>
    <!-- / -->
    <input type="hidden" runat="server" id="alerthq" value="-1" />
    <input type="hidden" runat="server" id="hidproyecto" value="-1" />
    <input type="hidden" runat="server" id="Bandera" value="-1" />
</asp:Content>
