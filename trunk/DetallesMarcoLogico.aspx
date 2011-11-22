<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetallesMarcoLogico.aspx.cs"
    Inherits="ESM.DetallesMarcoLogico" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html>
<head runat="server">
    <title>Detalles</title>
    <link href="Style/jquery-ui-1.8.15.custom.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <%--<script src="/Scripts/jquery-1.6.3-vsdoc.js" type="text/javascript"></script>--%>
    <script src="Scripts/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <link href="mastercustom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .marcaagua
        {
            border: 0;
            background-color: transparent;
            width: 100%;
            text-align: center;
            vertical-align: middle;
        }
        .marcaagua span
        {
            color: #848282;
            text-align: center;
        }
        select
        {
            min-width: 100px;
        }
        .select
        {
            color: #1e1e1e;
            font-size: 14px;
            position: relative; /*float: left;*/
            min-width: 100px;
            width: 5em;
            height: 16px;
            padding: 5px;
            margin: 6px 0 6px 0;
            border-top: solid #e4e4e4 1px;
            border-right: none;
            border-bottom: solid #999999 1px;
            border-left: solid #e4e4e4 1px;
            background: #f9f9f9;
            background: -moz-linear-gradient(100% 100% 90deg, #dedede, #f9f9f9);
            background: -webkit-gradient(linear, left bottom, left top, from(#dedede), to(#f9f9f9));
        }
        .select .label
        {
            font-size: 12px;
            padding: 0;
            margin: 0;
        }
        .select .label img
        {
            vertical-align: middle;
            overflow: hidden;
            margin: 0;
            padding: 0 5px 0 2px;
        }
        .select .button
        {
            color: #ffffff;
            font-size: 14px;
            text-align: center;
            position: absolute;
            right: -1px;
            top: -1px;
            width: 16px;
            height: 16px;
            padding: 5px;
            margin: 0;
            cursor: pointer;
            border-top: solid #6199D7 1px;
            border-right: solid #3278C4 1px;
            border-bottom: solid #002F65 1px;
            border-left: solid #224F80 1px;
            background: #3278c4;
            background: -moz-linear-gradient(100% 100% 90deg, #224F80, #3278C4);
            background: -webkit-gradient(linear, left bottom, left top, from(#224F80), to(#3278C4));
        }
        .select .button img
        {
            rotation: 0deg;
            -moz-transform: rotate(0deg);
            -webkit-transition: -webkit-transform 0.15s linear;
            -webkit-transform: rotate(0deg);
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=0);
        }
        .select .button.active img
        {
            rotation: 90deg;
            -moz-transform: rotate(90deg);
            -webkit-transform: rotate(90deg);
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=1);
        }
        .select .list
        {
            position: absolute;
            top: 28px;
            left: -1px;
            z-index: 1000;
            border-right: solid #e4e4e4 1px;
            border-bottom: solid #999999 1px;
            border-left: solid #e4e4e4 1px;
        }
        .select .optgroup
        {
            color: #1e1e1e;
            font-weight: bold;
            font-size: 12px;
            padding: 5px;
            margin: 0;
            background: #f2f2f2;
            background: -moz-linear-gradient(100% 100% 90deg, #dfdfdf, #f2f2f2);
            background: -webkit-gradient(linear, left bottom, left top, from(#dfdfdf), to(#f2f2f2));
        }
        .select .optgroup img
        {
            vertical-align: middle;
            overflow: hidden;
            margin: 0;
            padding: 0 5px 0 0;
        }
        .select .option
        {
            color: #1e1e1e;
            font-size: 12px;
            padding: 5px;
            cursor: pointer;
            min-width: 100px;
            border-bottom: dotted #cccccc 1px;
            background: #f9f9f9;
            background: -moz-linear-gradient(100% 100% 90deg, #f9f9f9, #ffffff);
            background: -webkit-gradient(linear, left bottom, left top, from(#f9f9f9), to(#ffffff));
        }
        .select .option img
        {
            vertical-align: middle;
            overflow: hidden;
            margin: 0;
            padding: 0 5px 0 0;
        }
        .select .option.bottom
        {
            border-bottom: none;
        }
        .select .option:hover
        {
            background: #ffffff;
        }
        .select input.button
        {
            color: #ffffff;
            font-size: 14px;
            height: 32px;
            padding: 0 5px 0 5px;
            margin: 6px;
            cursor: pointer;
            border-top: solid #6199D7 1px;
            border-right: solid #224F80 1px;
            border-bottom: solid #002F65 1px;
            border-left: solid #224F80 1px;
            background: -moz-linear-gradient(100% 100% 90deg, #224F80, #3278C4);
            background: -webkit-gradient(linear, left bottom, left top, from(#224F80), to(#3278C4));
            text-shadow: #1e1e1e 1px 1px 1px;
        }
    </style>
    <style type="text/css">
        body
        {
            background: #fff;
        }
        .detalles
        {
            border: 1px dashed #ccc;
            padding: 10 10 10 10;
        }
        
        #sortable1, #sortable2, #sortable3, #sortable4
        {
            list-style-type: none;
            margin: 0;
            padding: 0;
            float: left;
            margin-right: 10px;
            background: #eee;
            padding: 5px;
            width: 143px;
            border: 2px dashed #ccc;
        }
        #sortable1 li, #sortable2 li, #sortable3 li, #sortable4 li
        {
            margin: 5px;
            padding: 5px;
            font-size: 1em;
            width: 120px;
        }
        .detalles input[type="text"]
        {
            border: 2px solid #CCCCCC;
            border-radius: 3px 3px 3px 3px; /*width: 50%;*/
            padding-left: 10px;
            color: #4D4D4D;
        }
        .detalles input[type="text"]:hover
        {
            border: 2px solid #00A9B5;
            border-radius: 3px 3px 3px 3px;
        }
        .droptrue
        {
            min-height: 200px;
        }
    </style>
    <script type="text/javascript">

        var medios = null;

        $(function () {
            $("ul.droptrue").sortable({
                connectWith: "ul"

            });

            $("ul.dropfalse").sortable({
                connectWith: "ul",
                update: function (event, ui) {
                    var id = $(this).attr("id");
                    if (id == "sortable2") {
                        $("#mediosinput").val("");
                        $("#" + id + ">li").each(function () {
                            $("#mediosinput").val($("#mediosinput").val() + $(this).html() + ",");
                        });
                    }
                    else if (id == "sortable4") {
                        $("#supuestosinput").val("");
                        $("#" + id + ">li").each(function () {
                            $("#supuestosinput").val($("#supuestosinput").val() + $(this).html() + ",");
                        });
                    }
                },
                
            });

            $("ul.droptwotrue").sortable({
                connectWith: "ul"

            });

            $("ul.droptwofalse").sortable({
                connectWith: "ul",
                update: function (event, ui) {
                    var id = $(this).attr("id");
                    if (id == "sortable2") {
                        $("#mediosinput").val("");
                        $("#" + id + ">li").each(function () {
                            $("#mediosinput").val($("#mediosinput").val() + $(this).html() + ",");
                        });
                    }
                    else if (id == "sortable4") {
                        $("#supuestosinput").val("");
                        $("#" + id + ">li").each(function () {
                            $("#supuestosinput").val($("#supuestosinput").val() + $(this).html() + ",");
                        });
                    }


                }
            });

            $("#sortable1, #sortable2").disableSelection();
            $("#sortable3, #sortable4").disableSelection();
        });

        function Medios() {

            alert("Detalles");


        };

        var openSelect = null;
        // Init
        $(function () {
            $("select.select").each(function () {
                var i = $(this);
                var list = [];
                var label = "";
                i.children().each(function () {
                    var tagName = String($(this)[0].tagName).toUpperCase();
                    if (tagName == "OPTION") {
                        list.push($(this));
                    } else if (tagName == "OPTGROUP") {
                        var subList = { label: $(this).attr("label"), img: $(this).attr("img"), children: $(this).children() };
                        list.push(subList);
                    }
                });

                var sName = $(this).attr("name");
                var html = "<div class=\"select span-5\" >";
                html += "<input type=\"hidden\" name=\"" + sName + "\" value=\"" + list[0].val() + "\" />";
                html += "<div class=\"label\" ></div>";
                html += "<div class=\"button\" ><img src=\"img/arrow.png\" width=\"16\" height=\"16\" /></div>";
                html += "<div class=\"list hide\" >";
                for (var node in list) {
                    if (list[node].label != undefined) {
                        var img = "";
                        if (list[node].img != undefined) {
                            img = "<img src=\"" + list[node].img + "\" />";
                        }
                        html += "<div class=\"optgroup span-5 last\" >" + img + list[node].label + "</div>";
                        list[node].children.each(function () {
                            var img = "";
                            if ($(this).attr("img")) {
                                img = "<img src=\"" + $(this).attr("img") + "\" />";
                            }
                            var selected = "";
                            if ($(this).attr("selected")) {
                                selected = "selected=\"selected\"";
                                label = img + $(this).html();
                            }
                            html += "<div class=\"option span-5 last\" " + selected + " val=\"" + $(this).val() + "\">" + img + $(this).html() + "</div>";
                        });
                    } else {
                        var img = "";
                        if (list[node].attr("img") != undefined) {
                            img = "<img src=\"" + list[node].attr("img") + "\" />";
                        }
                        var selected = "";
                        if (list[node].attr("selected")) {
                            selected = "selected=\"selected\"";
                            label = img + list[node].html();
                        }
                        html += "<div class=\"option span-5 last\" " + selected + " val=\"" + list[node].val() + "\">" + img + list[node].html() + "</div>";
                    }
                }
                html += "</div>";
                html += "</div>";
                html = $(html);
                i.replaceWith(html);
                html.data("state", "close");
                var list = html.children(".list");
                // Try to get selected
                if (!label) {
                    label = list.children().first().html();
                }
                list.children(".option").last().addClass("bottom");
                html.children(".label").html(label);
                list.children(".option").click(function () {
                    html.children("input[name='" + sName + "']").val($(this).attr("val"));
                    html.children(".label").html($(this).html());
                });
                html.click(function (event) {
                    if ($(this).data("state") == "close") {
                        $(this).children(".button").addClass("active");
                        list.slideDown(125, function () {
                            html.data("state", "open");
                        });
                    }
                });
                $(document).click(function (event) {
                    if (html.data("state") == "open") {
                        $(html).children(".button").removeClass("active");
                        list.hide();
                        html.data("state", "close");
                    }
                });
            });
        });

        $(document).ready(function () {

//            $("#ItemMedioMarca").live(function(){
//                alert($("#sortable1>li:first").html());
//                $("#sortable1>li:first").trigger("click");
//                
//            });

            $(".numerico").change(function(){
            
                if(isNaN($(this).val())){
                    $(this).val("0");
                }
        
            });

            $("#sortable2").css("height",$("#sortable1").css("height"));
            $("#sortable4").css("height",$("#sortable3").css("height"));

            $("#txtFechaIndicador").datepicker({ dateFormat: "yy/mm/dd" });
            $("#txtFechaFinal").datepicker({ dateFormat: "yy/mm/dd" });


            $(".droptrue>li").addClass("ui-state-default");
            $(".dropfalse>li").addClass("ui-state-highlight");
            $(".droptwotrue>li").addClass("ui-state-default");
            $(".droptwofalse>li").addClass("ui-state-highlight");

            $(".marcaagua").each(function(){
            
                $(this).removeClass("ui-state-highlight");
            
            });

            $("#previsualizar").click(function () {
                var indicador = "Al " + $("#txtFechaIndicador").val() + " " + $("#cboverbos option:selected").html() + " " + $("#Meta").val() + " " + $("#cboUnidades option:selected").html() + " " + $("#txtdescripcionI").val()
                $("#txtindicadorg").val(indicador);

            });
        });
        
    </script>
</head>
<body>
    <form id="form1" class="detalles" style="" runat="server">
    <table>
        <tr>
            <td>
                <img src="/Icons/details.png" width="48px" alt="Evaluacion" />
            </td>
            <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                <h1 style="color: #0b72bc;">
                    Detalles</h1>
                Administra los componentes tales como indicadores, medios de verificación y supuestos
            </td>
        </tr>
    </table>
    <div style="margin-left: 30px; font-size: 13px;">
        <br />
        <b style="color: #0b72bc;">Parametros para generar indicadores </b>
        <br />
        <div class="demo" style="width: 100%;">
            Verbo:<asp:DropDownList ID="cboverbos" runat="server" DataSourceID="sqldtverbos"
                DataTextField="Verbo" DataValueField="Id">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sqldtverbos" runat="server" ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>"
                SelectCommand="SELECT [Id], [Verbo] FROM [Verbos]"></asp:SqlDataSource>
            Fecha:<asp:TextBox ID="txtFechaIndicador" runat="server" />
            <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final:" Visible="False"></asp:Label>
            <asp:TextBox ID="txtFechaFinal" runat="server" Visible="False"></asp:TextBox>
            Meta:<asp:TextBox ID="Meta" Width="24px" MaxLength="3" runat="server">0</asp:TextBox>
            Unidades:<asp:DropDownList ID="cboUnidades" runat="server" DataSourceID="sqldtUnidades"
                DataTextField="Unidad" DataValueField="Id">
            </asp:DropDownList>
            &nbsp;<asp:CheckBox ID="chxSSP" runat="server" Text="SSP" TextAlign="Left" Visible="False" />
            <asp:SqlDataSource ID="sqldtUnidades" runat="server" ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>"
                SelectCommand="SELECT [Id], [Unidad] FROM [Unidades]"></asp:SqlDataSource>
            Descripcion:<asp:TextBox ID="txtdescripcionI" runat="server" />
            <asp:ImageButton ID="previsualizar" runat="server" ImageUrl="~/Icons/Search.png"
                Width="24px" />
            <br />
            <br />
            <b style="color: #0b72bc;">Previsualización de indicador</b>
            <br />
            <asp:TextBox ID="txtindicadorg" Width="80%" runat="server"></asp:TextBox>
            <asp:LinkButton Text="&lt;img src='/Icons/save-icon.png' width='24px' alt='Agregar' /&gt;"
                runat="server" ID="lknAgregarIndicador" OnClick="lknAgregarIndicador_Click" />
            <br />
            <asp:GridView ID="gvresultados" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                DataSourceID="sqlResultados" Visible="False" Width="80%">
                <AlternatingRowStyle CssClass="trblanca" />
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True"
                        SortExpression="Id" Visible="False" />
                    <asp:BoundField DataField="Proyecto_id" HeaderText="Proyecto_id" SortExpression="Proyecto_id"
                        Visible="False" />
                    <asp:BoundField DataField="Causa" HeaderText="Causa" SortExpression="Causa" Visible="False" />
                    <asp:BoundField DataField="Efecto" HeaderText="Efecto" SortExpression="Efecto" />
                    <asp:BoundField DataField="Resultado" HeaderText="Resultado" SortExpression="Resultado" />
                    <asp:BoundField DataField="Resultado_Detalle" HeaderText="Resultado_Detalle" SortExpression="Resultado_Detalle"
                        Visible="False" />
                    <asp:BoundField DataField="Indicador_Resultado" HeaderText="Indicador" SortExpression="Indicador_Resultado" />
                    <asp:BoundField DataField="Presupuesto" HeaderText="Presupuesto" SortExpression="Presupuesto"
                        Visible="False" />
                </Columns>
                <HeaderStyle CssClass="trheader" />
                <RowStyle CssClass="trgris" />
            </asp:GridView>
            <asp:SqlDataSource ID="sqlResultados" runat="server" ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>"
                DeleteCommand="DELETE FROM [Causas_Efectos] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Causas_Efectos] ([Proyecto_id], [Causa], [Efecto], [Resultado], [Resultado_Detalle], [Indicador_Resultado], [Presupuesto]) VALUES (@Proyecto_id, @Causa, @Efecto, @Resultado, @Resultado_Detalle, @Indicador_Resultado, @Presupuesto)"
                SelectCommand="SELECT * FROM [Causas_Efectos] WHERE ([Id] = @Id)" UpdateCommand="UPDATE [Causas_Efectos] SET [Proyecto_id] = @Proyecto_id, [Causa] = @Causa, [Efecto] = @Efecto, [Resultado] = @Resultado, [Resultado_Detalle] = @Resultado_Detalle, [Indicador_Resultado] = @Indicador_Resultado, [Presupuesto] = @Presupuesto WHERE [Id] = @Id">
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Proyecto_id" Type="Int32" />
                    <asp:Parameter Name="Causa" Type="String" />
                    <asp:Parameter Name="Efecto" Type="String" />
                    <asp:Parameter Name="Resultado" Type="String" />
                    <asp:Parameter Name="Resultado_Detalle" Type="String" />
                    <asp:Parameter Name="Indicador_Resultado" Type="String" />
                    <asp:Parameter Name="Presupuesto" Type="Int32" />
                </InsertParameters>
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="Id" QueryStringField="idresultado"
                        Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Proyecto_id" Type="Int32" />
                    <asp:Parameter Name="Causa" Type="String" />
                    <asp:Parameter Name="Efecto" Type="String" />
                    <asp:Parameter Name="Resultado" Type="String" />
                    <asp:Parameter Name="Resultado_Detalle" Type="String" />
                    <asp:Parameter Name="Indicador_Resultado" Type="String" />
                    <asp:Parameter Name="Presupuesto" Type="Int32" />
                    <asp:Parameter Name="Id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:GridView ID="gvproyecto" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                DataSourceID="sqlProyecto" Visible="False" Width="80%">
                <AlternatingRowStyle CssClass="trblanca" />
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True"
                        SortExpression="Id" Visible="False" />
                    <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" SortExpression="Proyecto"
                        Visible="False" />
                    <asp:BoundField DataField="Problema" HeaderText="Problema" SortExpression="Problema"
                        Visible="False" />
                    <asp:BoundField DataField="Finalidad" HeaderText="Finalidad" SortExpression="Finalidad" />
                    <asp:BoundField DataField="Proposito" HeaderText="Proposito" SortExpression="Proposito" />
                    <asp:BoundField DataField="Fecha_Creacion" HeaderText="Fecha_Creacion" SortExpression="Fecha_Creacion"
                        Visible="False" />
                    <asp:BoundField DataField="Indicador" HeaderText="Indicador" SortExpression="Indicador" />
                    <asp:BoundField DataField="Presupuesto" HeaderText="Presupuesto" SortExpression="Presupuesto"
                        Visible="False" />
                </Columns>
                <HeaderStyle CssClass="trheader" />
                <RowStyle CssClass="trgris" />
            </asp:GridView>
            <asp:SqlDataSource ID="sqlProyecto" runat="server" ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>"
                DeleteCommand="DELETE FROM [Proyectos] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Proyectos] ([Proyecto], [Problema], [Finalidad], [Proposito], [Fecha_Creacion], [Indicador], [Presupuesto]) VALUES (@Proyecto, @Problema, @Finalidad, @Proposito, @Fecha_Creacion, @Indicador, @Presupuesto)"
                SelectCommand="SELECT * FROM [Proyectos] WHERE ([Id] = @Id)" UpdateCommand="UPDATE [Proyectos] SET [Proyecto] = @Proyecto, [Problema] = @Problema, [Finalidad] = @Finalidad, [Proposito] = @Proposito, [Fecha_Creacion] = @Fecha_Creacion, [Indicador] = @Indicador, [Presupuesto] = @Presupuesto WHERE [Id] = @Id">
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Proyecto" Type="String" />
                    <asp:Parameter Name="Problema" Type="String" />
                    <asp:Parameter Name="Finalidad" Type="String" />
                    <asp:Parameter Name="Proposito" Type="String" />
                    <asp:Parameter DbType="Date" Name="Fecha_Creacion" />
                    <asp:Parameter Name="Indicador" Type="String" />
                    <asp:Parameter Name="Presupuesto" Type="Decimal" />
                </InsertParameters>
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="0" Name="Id" SessionField="idproyecto" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Proyecto" Type="String" />
                    <asp:Parameter Name="Problema" Type="String" />
                    <asp:Parameter Name="Finalidad" Type="String" />
                    <asp:Parameter Name="Proposito" Type="String" />
                    <asp:Parameter DbType="Date" Name="Fecha_Creacion" />
                    <asp:Parameter Name="Indicador" Type="String" />
                    <asp:Parameter Name="Presupuesto" Type="Decimal" />
                    <asp:Parameter Name="Id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:GridView ID="gvIndicadores_Actividad" runat="server" AutoGenerateColumns="False"
                DataKeyNames="Id" DataSourceID="sqlActividadesIndicadores" Width="80%" Visible="False"
                OnSelectedIndexChanged="gvIndicadores_Actividad_SelectedIndexChanged">
                <AlternatingRowStyle CssClass="trblanca" />
                <Columns>
                    <asp:CommandField ButtonType="Image" CancelImageUrl="~/Icons/Close.png" CancelText=""
                        DeleteImageUrl="~/Icons/Bin_Full.png" EditImageUrl="~/Icons/Stationery.png" InsertImageUrl="~/Icons/save-icon.png"
                        ShowDeleteButton="True" UpdateImageUrl="~/Icons/save-icon.png" SelectImageUrl="~/Icons/Paste.png"
                        ShowSelectButton="True">
                        <ControlStyle Width="24px" />
                    </asp:CommandField>
                    <asp:BoundField DataField="Id" HeaderText="Identificador" InsertVisible="False" ReadOnly="True"
                        SortExpression="Id" />
                    <asp:BoundField DataField="Actividad_id" HeaderText="Identificador" 
                        SortExpression="Actividad_id" Visible="False" />
                    <asp:BoundField DataField="Indicador" HeaderText="Indicador" SortExpression="Indicador" />
                    <asp:BoundField DataField="unidad_id" HeaderText="unidad_id" SortExpression="unidad_id"
                        Visible="False" />
                    <asp:BoundField DataField="verbo_id" HeaderText="verbo_id" SortExpression="verbo_id"
                        Visible="False" />
                    <asp:BoundField DataField="fecha_indicador_inicial" HeaderText="Fecha Inicial" SortExpression="fecha_indicador_inicial" />
                    <asp:BoundField DataField="fecha_indicador_final" HeaderText="Fecha Final" SortExpression="fecha_indicador_final" />
                    <asp:BoundField DataField="Fecha_Creacion" HeaderText="Fecha de Creación" SortExpression="Fecha_Creacion" />
                    <asp:CheckBoxField DataField="SSP" HeaderText="SSP" SortExpression="SSP" />
                </Columns>
                <HeaderStyle CssClass="trheader" />
                <RowStyle CssClass="trgris" />
            </asp:GridView>
            <asp:SqlDataSource ID="sqlActividadesIndicadores" runat="server" ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>"
                DeleteCommand="DELETE FROM [Indicadores] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Indicadores] ([Actividad_id], [Indicador], [unidad_id], [verbo_id], [fecha_indicador_inicial], [fecha_indicador_final], [Fecha_Creacion]) VALUES (@Actividad_id, @Indicador, @unidad_id, @verbo_id, @fecha_indicador_inicial, @fecha_indicador_final, @Fecha_Creacion)"
                SelectCommand="SELECT * FROM [Indicadores] WHERE ([Actividad_id] = @Actividad_id)"
                UpdateCommand="UPDATE [Indicadores] SET [Actividad_id] = @Actividad_id, [Indicador] = @Indicador, [unidad_id] = @unidad_id, [verbo_id] = @verbo_id, [fecha_indicador_inicial] = @fecha_indicador_inicial, [fecha_indicador_final] = @fecha_indicador_final, [Fecha_Creacion] = @Fecha_Creacion WHERE [Id] = @Id">
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Actividad_id" Type="Int32" />
                    <asp:Parameter Name="Indicador" Type="String" />
                    <asp:Parameter Name="unidad_id" Type="Int32" />
                    <asp:Parameter Name="verbo_id" Type="Int32" />
                    <asp:Parameter DbType="Date" Name="fecha_indicador_inicial" />
                    <asp:Parameter DbType="Date" Name="fecha_indicador_final" />
                    <asp:Parameter DbType="Date" Name="Fecha_Creacion" />
                </InsertParameters>
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="Actividad_id" QueryStringField="idactividad"
                        Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Actividad_id" Type="Int32" />
                    <asp:Parameter Name="Indicador" Type="String" />
                    <asp:Parameter Name="unidad_id" Type="Int32" />
                    <asp:Parameter Name="verbo_id" Type="Int32" />
                    <asp:Parameter DbType="Date" Name="fecha_indicador_inicial" />
                    <asp:Parameter DbType="Date" Name="fecha_indicador_final" />
                    <asp:Parameter DbType="Date" Name="Fecha_Creacion" />
                    <asp:Parameter Name="Id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <br />
            <div style="clear: both;">
                <div style="float: left;">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="2">
                                <h3 style="color: #0b72bc;">
                                    Medios de Verificación</h3>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:BulletedList ID="sortable1" CssClass="droptrue" Width="150px" runat="server"
                                    DataTextField="Medio_de_verificacion" DataValueField="Id" AppendDataBoundItems="True"
                                    ViewStateMode="Enabled">
                                </asp:BulletedList>
                            </td>
                            <td style="vertical-align: top;">
                                <asp:BulletedList ID="sortable2" Width="150px" CssClass="dropfalse" runat="server">
                                    <asp:ListItem ID="ItemMedioMarca" Text="Medios de Verificación" class="marcaagua"
                                        Enabled="false" />
                                </asp:BulletedList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <asp:LinqDataSource ID="lqMediosVerificacion" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
                                    EntityTypeName="" Select="new (Medio_de_verificacion, Id)" TableName="Medios_de_verificacions">
                                </asp:LinqDataSource>
                                Nuevo Medio de Verificación:<br />
                                <asp:TextBox ID="txtmedio" runat="server" />
                                <asp:ImageButton ID="btnAlmacenaMedio" Width="24px" ImageUrl="/Icons/save-icon.png"
                                    runat="server" OnClick="btnAlmacenaMedio_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div style="float: left;">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2">
                            <h3 style="color: #0b72bc;">
                                Supuestos
                            </h3>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <asp:BulletedList ID="sortable3" CssClass="droptwotrue" Width="150px" runat="server"
                                DataSourceID="lqsupuestos" DataTextField="supuesto1" DataValueField="Id">
                            </asp:BulletedList>
                        </td>
                        <td style="vertical-align: top;">
                            <asp:BulletedList ID="sortable4" Width="150px" CssClass="droptwofalse" runat="server">
                                <asp:ListItem Text="Supuestos Asignados" class="marcaagua" Enabled="false" />
                            </asp:BulletedList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:LinqDataSource ID="lqsupuestos" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
                                EntityTypeName="" Select="new (supuesto1, Id)" TableName="Supuestos">
                            </asp:LinqDataSource>
                            Nuevo Supuesto:<br />
                            <asp:TextBox ID="txtsupuesto" runat="server" />
                            <asp:ImageButton ID="btnAlmacenaSupuesto" Width="24px" ImageUrl="/Icons/save-icon.png"
                                runat="server" OnClick="btnAlmacenaSupuesto_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <input type="hidden" runat="server" id="supuestosinput" value="" />
            <input type="hidden" runat="server" id="mediosinput" value="" />
            <input type="hidden" runat="server" value="-1" id="actualizaActividad" />
            <div style="clear: both; width: 100%; height: 10px;">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
