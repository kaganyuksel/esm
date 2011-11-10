<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ArbolProblemas.aspx.cs" Inherits="ESM.ArbolProblemas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Pretty/css/prettyPhoto.css" rel="stylesheet" charset="utf-8" media="screen"
        type="text/css" />
    <script src="/Pretty/js/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>
    <style type="text/css">
        body
        {
            overflow-x: hidden;
        }
        .leftone
        {
            margin-left: 5em;
        }
        
        .lefttwo
        {
            margin-left: 10em;
        }
        .efectos input[type="text"]
        {
            border: 2px solid #CCCCCC;
            border-radius: 3px 3px 3px 3px;
            width: 50%;
            padding-left: 10px;
            color: #4D4D4D;
        }
        .efectos input[type="text"]:focus
        {
            /*#005EA7*/
            border: 2px solid #00A9B5;
            border-radius: 3px 3px 3px 3px;
        }
        .efectos input[type="text"]:hover
        {
            border: 2px solid #00A9B5;
            border-radius: 3px 3px 3px 3px;
        }
        .efectos h2
        {
            color: #00A9B5;
        }
        .causas input[type="text"]
        {
            border: 2px solid #CCCCCC;
            border-radius: 3px 3px 3px 3px;
            width: 50%;
            padding-left: 10px;
            color: #4D4D4D;
        }
        .causas input[type="text"]:focus
        {
            /*#005EA7*/
            border: 2px solid #357D28;
            border-radius: 3px 3px 3px 3px;
        }
        .causas input[type="text"]:hover
        {
            border: 2px solid #357D28;
            border-radius: 3px 3px 3px 3px;
        }
        .causas h2
        {
            color: #357D28;
        }
        .problema textarea
        {
            border: 2px solid #CCCCCC;
            border-radius: 3px 3px 3px 3px;
            width: 50%;
            padding-left: 10px;
            padding-top: 5px;
            color: #4D4D4D;
            font-family: Tahoma;
            font-size: 1em;
            width: 80%;
            height: 5em;
        }
        .problema textarea:hover
        {
            border: 2px solid #005EA7;
            border-radius: 3px 3px 3px 3px;
        }
        .problema input[type="text"]
        {
            border: 2px solid #CCCCCC;
            border-radius: 3px 3px 3px 3px;
            width: 50%;
            padding-left: 10px;
            color: #4D4D4D;
        }
        .problema input[type="text"]:focus
        {
            border: 2px solid #CCCCCC;
            border-radius: 3px 3px 3px 3px;
            width: 50%;
            padding-left: 10px;
            color: #4D4D4D;
        }
        .problema textarea:focus
        {
            border: 2px solid #005EA7;
            border-radius: 3px 3px 3px 3px;
        }
        .problema h2
        {
            color: #005EA7;
        }
        .mover
        {
            -moz-transition: margin 0.25s ease-in-out;
            -webkit-transition: margin 0.25s ease-in-out;
            -o-transition: margin 0.25s ease-in-out;
            transition: margin 0.25s ease-in-out;
        }
        .past
        {
            margin-left: -50%;
        }
        .future
        {
            margin-left: 50%;
        }
        .finalidad
        {
            margin-left: 150px;
        }
    </style>
    <script type="text/javascript">
        
	    $(function() {
		    $( ".accordion" ).accordion({
			    autoHeight: false,
			    navigation: true,
                collapsible: true,
                animated: 'bounceslide'
                 
		    });
            
	    });

        function AlmacenarResultado(idresultado, causa, resultado) {
            $.ajax({
                url: "ajax.aspx?idResultado="+ idresultado +"&causa="+ $("#"+causa).val() + "&resultado=" + $("#"+resultado).val()+"&resultados=true",
                async: false,
                succes: function (result) {
                    alert(result);
                },
                error: function (result) {
                    alert("Error:" + result.status + " Estatus: " + result.statusText);
                }
            });
        }

        function AlmacenarActividad(idresultado, actividad, presupuesto) {
            $.ajax({
                url: "ajax.aspx?idResultado="+ idresultado +"&actividad="+ $("#"+actividad).val() + "&presupuesto=" + $("#"+presupuesto).val()+"&actividades=true",
                async: false,
                succes: function (result) {
                    alert(result);
                },
                error: function (result) {
                    alert("Error:" + result.status + " Estatus: " + result.statusText);
                }
            });
        }
     
        $(document).ready(function () {
            $("#adetalles").click(function () {
                var idproyecto = $("#hidproyecto").val();
                $.prettyPhoto.open("/detallesmarcologico.aspx?idproyecto=" + idproyecto + "&iframe=true&width=100%&height=100%");
            });


            $("a.pretty").prettyPhoto({
                ie6_fallback: true,
                modal: true,
                social_tools: false,
            });

            $("#ContentPlaceHolder1_cbovervos").val();

            $("#ContentPlaceHolder1_txtFechaIndicador").datepicker({ dateFormat: "yy/mm/dd" });

            var problema = $("#ContentPlaceHolder1_txtproblema").val();

            if (problema.trim().length != 0) {
                $("#ContentPlaceHolder1_lknAlmacenarP").attr("disabled", true);
            }

            $("#ContentPlaceHolder1_txtEfecto1").val("");
            $("#ContentPlaceHolder1_txtCausa1").val("");

            $(".speech").each(function () {
                $(this).attr("onwebkitspeechchange", "textarea_change(this)");
            });


            var result = $("#ContentPlaceHolder1_alerthq").val();
            if (result == 1) {
                $("#a_succes").trigger("click");
                $("#ContentPlaceHolder1_alerthq").val("-1");
            }
            if (result == 0) {
                $("#a_error").trigger("click");
                $("#ContentPlaceHolder1_alerthq").val("-1");
            }

            $("#ContentPlaceHolder1_txtCausa1").change(function () {

                if ($(this).val().trim().length > 9)
                    $("#ContentPlaceHolder1_txtEfecto1").attr("disabled", false);
                else {
                    $("#ContentPlaceHolder1_txtEfecto1").val("");
                    $("#ContentPlaceHolder1_txtEfecto1").attr("disabled", true);
                }
            });

            
        });

        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo" style="width: 90%; margin: 0 auto;">
        <div id="slides" style="display: block; width: 4000px; clear: both; overflow: hidden;">
            <div id="izquierda" style="width: 50%; float: left;" class="demo mover">
                <div style="width: 1024px;">
                    <br />
                    <br />
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <img src="/Icons/network.png" width="64px" alt="Evaluacion" />
                                </td>
                                <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                                    <h1 style="color: #0b72bc;">
                                        Árbol de Problemas</h1>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <br />
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
                            Proyectos Existentes:</h2>
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
                        <h1>
                            <img width="24px" src="/Icons/System.png" alt="Administración" />
                            Nuevo Proyecto</h1>
                    </div>
                    <div id="divCargado" runat="server" visible="false">
                        <h1>
                            <img width="24px" src="/Icons/System.png" alt="Administración" />
                            Administración del Proyecto</h1>
                    </div>
                    <br />
                    <div class="problema" runat="server" id="divproblema" visible="false">
                        <h2>
                            * Problema Central</h2>
                        <br />
                        <asp:TextBox ID="txtproblema" runat="server" TextMode="MultiLine" placeholder="1. Descripcion del Problema" />
                        <asp:LinkButton Text="<img src='/Icons/save-icon.png' width='24px' alt='save project' />"
                            runat="server" ID="lknAlmacenarP" OnClick="lknAlmacenarP_Click" />
                        <%--<input class="speech" id="probleman" style="width: 15px; border: 0;" />--%>
                    </div>
                    <div class="efectos" runat="server" id="divefectos" visible="false">
                        <h2>
                            * Causas y Efectos</h2>
                        <br />
                        <span>
                            <asp:TextBox ID="txtCausa1" runat="server" placeholder="2. Descripcion de la causa"
                                TextMode="MultiLine" Width="40%" />
                        </span>
                        <br />
                        <span>
                            <asp:TextBox ID="txtEfecto1" runat="server" placeholder="3. Descripcion del efecto"
                                TextMode="MultiLine" Width="40%" Enabled="False" />
                        </span>
                        <asp:LinkButton Text='<img src="/Icons/save-icon.png" width="24px" alt="save efect" />'
                            runat="server" ID="lknAlmacenarE" OnClick="lknAlmacenarE_Click" />
                        <br />
                        <br />
                        <asp:GridView ID="gvEfectos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="Id" DataSourceID="sqlefectos" PageSize="15" Width="80%" AllowSorting="True">
                            <AlternatingRowStyle CssClass="trblanca" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Icons/Bin_Full.png" DeleteText=""
                                    EditImageUrl="~/Icons/Stationery.png" EditText="" ShowDeleteButton="True" ShowEditButton="True">
                                    <ControlStyle Width="24px" />
                                </asp:CommandField>
                                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True"
                                    SortExpression="Id" />
                                <asp:BoundField DataField="Efecto" HeaderText="Efecto" SortExpression="Efecto" />
                                <asp:BoundField DataField="Causa" HeaderText="Causa" SortExpression="Causa" />
                            </Columns>
                            <HeaderStyle CssClass="trheader" />
                            <RowStyle CssClass="trgris" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="sqlefectos" runat="server" ConflictDetection="CompareAllValues"
                            ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>" DeleteCommand="DELETE FROM [Causas_Efectos] WHERE [Id] = @original_Id AND (([Efecto] = @original_Efecto) OR ([Efecto] IS NULL AND @original_Efecto IS NULL)) AND (([Causa] = @original_Causa) OR ([Causa] IS NULL AND @original_Causa IS NULL))"
                            InsertCommand="INSERT INTO [Causas_Efectos] ([Efecto], [Causa]) VALUES (@Efecto, @Causa)"
                            OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [Id], [Efecto], [Causa] FROM [Causas_Efectos] WHERE ([Proyecto_id] = @Proyecto_id)"
                            UpdateCommand="UPDATE [Causas_Efectos] SET [Efecto] = @Efecto, [Causa] = @Causa WHERE [Id] = @original_Id AND (([Efecto] = @original_Efecto) OR ([Efecto] IS NULL AND @original_Efecto IS NULL)) AND (([Causa] = @original_Causa) OR ([Causa] IS NULL AND @original_Causa IS NULL))">
                            <DeleteParameters>
                                <asp:Parameter Name="original_Id" Type="Int32" />
                                <asp:Parameter Name="original_Efecto" Type="String" />
                                <asp:Parameter Name="original_Causa" Type="String" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="Efecto" Type="String" />
                                <asp:Parameter Name="Causa" Type="String" />
                            </InsertParameters>
                            <SelectParameters>
                                <asp:SessionParameter DefaultValue="0" Name="Proyecto_id" SessionField="idproyecto"
                                    Type="Int32" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="Efecto" Type="String" />
                                <asp:Parameter Name="Causa" Type="String" />
                                <asp:Parameter Name="original_Id" Type="Int32" />
                                <asp:Parameter Name="original_Efecto" Type="String" />
                                <asp:Parameter Name="original_Causa" Type="String" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </div>
                    <br />
                    <br />
                </div>
            </div>
            <div id="derecha" style="width: 1024px; float: left;" class="demo mover">
                <br />
                <br />
                <div>
                    <table>
                        <tr>
                            <td>
                                <img src="/Icons/network.png" width="64px" alt="Evaluacion" />
                            </td>
                            <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                                <h1 style="color: #0b72bc;">
                                    Árbol de Objetivos</h1>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="problema" style="border: 2px solid #ccc; -moz-border-radius: 2px; -webkit-border-radius: 2px;
                        border-radius: 2px; /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 3px #000000; -webkit-box-shadow: 0px 0px 3px #000000; box-shadow: 0px 0px 3px #000000;
                        /*ie 7 and 8 do not support blur property of shadows*/ color: #005EA7; font-size: 1em;
                        height: 30px; line-height: 30px; padding-left: 10px;">
                        Finalidad:
                        <asp:TextBox ID="txtfinalidad" CssClass="finalidad" runat="server" />
                    </div>
                    <br />
                    <div class="problema" style="border: 2px solid #ccc; -moz-border-radius: 2px; -webkit-border-radius: 2px;
                        border-radius: 2px; /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 3px #000000; -webkit-box-shadow: 0px 0px 3px #000000; box-shadow: 0px 0px 3px #000000;
                        /*ie 7 and 8 do not support blur property of shadows*/ color: #005EA7; font-size: 1em;
                        padding-left: 10px;">
                        <h1>
                            Proposito</h1>
                        <asp:TextBox ID="txtProposito" runat="server" />
                        <asp:LinkButton ID="lknAlmacenarProposito" Text="<img Width='24px' src='/Icons/save-icon.png' alt='Almacenar Proposito' />"
                            runat="server" onclick="lknAlmacenarProposito_Click" />
                        <a id="adetalles" href="#">
                            <img src="/Icons/details.png" width="24px" alt="Detalles" /></a>
                        <br />
                        <br />
                        <br />
                        <asp:Panel ID="presultados" runat="server">
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <input type="button" value="Siguiente" onclick="$('#izquierda').addClass('past');"
            class="ui-button ui-widget ui-state-default ui-corner-all" role="button" aria-disabled="false" />
        <input type="button" value="Volver" onclick="$('#izquierda').removeClass('past'); "
            class="ui-button ui-widget ui-state-default ui-corner-all" role="button" aria-disabled="false" />
    </div>
    <input type="hidden" runat="server" id="alerthq" value="-1" />
    <input type="hidden" runat="server" id="hidproyecto" value="-1" />
</asp:Content>
