﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ArbolProblemas.aspx.cs" Inherits="ESM.ArbolProblemas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Pretty/css/prettyPhoto.css" rel="stylesheet" charset="utf-8" media="screen"
        type="text/css" />
    <script src="/Pretty/js/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>
    <link href="Style/MarcoLogico.css" rel="stylesheet" type="text/css" />
    <link href="Style/jsgantt.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jsgantt.js" type="text/javascript"></script>
    <style type="text/css">
        .txtareacausa
        {
            border: 2px solid #357D28;
            border-radius: 3px 3px 3px 3px;
            width: 50%;
            padding-left: 10px;
            color: #4D4D4D;
        }
        .txtareacausa:focus
        {
            border: 2px solid #357D28;
            border-radius: 3px 3px 3px 3px;
            width: 50%;
            padding-left: 10px;
            color: #4D4D4D;
        }
        .txtareaefecto
        {
            border: 2px solid #00A9B5;
            border-radius: 3px 3px 3px 3px;
            width: 50%;
            padding-left: 10px;
            color: #4D4D4D;
        }
        .txtareaefecto:focus
        {
            border: 2px solid #00A9B5;
            border-radius: 3px 3px 3px 3px;
            width: 50%;
            padding-left: 10px;
            color: #4D4D4D;
        }
        .btnleft
        {
            top: 50%;
            position: absolute;
            left: 3.5%;
            width: 50px;
            height: 50px;
            float: left;
            background-repeat: no-repeat;
            background-position: center;
            background-image: url('/Icons/back.png');
            border: 0;
            background-color: transparent;
        }
        .btnleft:hover
        {
            background-image: url('/Icons/backhover.png');
        }
        .btnright
        {
            top: 50%;
            position: absolute;
            right: 3.5%;
            width: 4em;
            float: right;
            background-repeat: no-repeat;
            background-position: center;
            background-image: url('/Icons/next.png');
            border: 0;
            background-color: transparent;
        }
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
            margin-left: -25%;
        }
        .presente
        {
        }
        .futuro
        {
            margin-left: 25%;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            var d = 300;
            $('#navigation a').each(function () {
                $(this).stop().animate({
                    'marginBottom': '-80px'
                }, d += 150);
            });

            $('#navigation > li').hover(
                function () {
                    $('a', $(this)).stop().animate({
                        'marginBottom': '-2px'
                    }, 200);
                },
                function () {
                    $('a', $(this)).stop().animate({
                        'marginBottom': '-80px'
                    }, 200);
                }
            );
        });
    </script>
    <script type="text/javascript">
        $.datepicker.setDefaults({
            dateFormat: 'dd/mm/yy', currentText: 'Ahora', closeText: 'X', autoSize: true,
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'], dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'],
            firstDay: 1, monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'], nextText: 'Siguiente', prevText: 'Anterior',
            showAnim: 'slide', yearRange: '1990:2020'
        });
        $(function () {
            //time picker
            $('.fechaini').datepicker({
                dateFormat: 'dd/mm/yy', currentText: 'Ahora', closeText: 'Cerrar',
                onSelect: function (selectedDate) { save_dates(1, selectedDate, $(this).attr("alt"), this); }, showButtonPanel: false
            });
            //time picker
            $('.fechafin').datepicker({
                dateFormat: 'dd/mm/yy', currentText: 'Ahora', closeText: 'Cerrar',
                onSelect: function (selectedDate) { save_dates(2, selectedDate, $(this).attr("alt"), this); }, showButtonPanel: false
            });
        });

        function load_date_ini(selectedDatefunction, Vid) {
            $('.fechaini').datepicker("destroy");
            $('.fechaini').datepicker({
                dateFormat: 'dd/mm/yy', currentText: 'Ahora', closeText: 'Cerrar',
                maxDate: selectedDatefunction,
                onSelect: function (selectedDate) { save_dates(1, selectedDate, $(this).attr("alt"), this, Vid) }, showButtonPanel: false
            });
        }

        function load_date_fin(selectedDatefunction, Vid) {
            $('.fechafin').datepicker("destroy");
            $('.fechafin').datepicker({
                dateFormat: 'dd/mm/yy', currentText: 'Ahora', closeText: 'Cerrar',
                minDate: selectedDatefunction,
                onSelect: function (selectedDate) { save_dates(2, selectedDate, $(this).attr("alt"), this, Vid) }, showButtonPanel: false
            });
        }

        function tempDate(valor, fechaEva, tipo, Vid) {
            if (tipo == 2)
                load_date_fin($("#" + fechaEva).val(), Vid);
            else
                load_date_ini($("#" + fechaEva).val(), Vid)

            $("#HFTempDate").val($(valor).val());
        }

        function save_dates(tipo, valor, id, objeto, Vid) {
            var valorIni = valor;
            valor = valor.replace("/", "-").replace("/", "-");
            var fechaini;
            var fechafin;
            $.ajax({
                url: "save_dates.aspx?id=" + id + "&tipo=" + tipo + "&valor=" + valor,
                async: false,
                success: function (result) {
                    if (result == "Fecha actualizada correctamente") {
                        alert(result);
                        if (tipo == 1) {
                            fechaini = valorIni;
                            fechafin = $("#fecha_fin_id_" + Vid).val();
                        } else {
                            fechaini = $("#fecha_ini_id_" + Vid).val();
                            fechafin = valorIni;
                        }
                        retorna_ancho(fechaini, fechafin, Vid, tipo);
                    } else {
                        alert(result);
                        $(objeto).datetimepicker("setDate", $("#HFTempDate").val());
                    }
                },
                error: function (result) {
                    alert("Error " + result.status + ' ' + result.statusText);
                }
            });
        }

        function retorna_ancho(fechaini, fechafin, Vid, tipo) {
            var vColWidth = 0;
            var vColUnit = 0;
            var vTaskRight;
            var vTaskLeft;
            var ancho;
            var izq;
            var vMinDate = new Date();
            var vFormat = $("input[@name='radFormat']:checked").val();
            //
            if (vFormat == 'day') {
                vColWidth = 18;
                vColUnit = 1;
            }
            else if (vFormat == 'week') {
                vColWidth = 37;
                vColUnit = 7;
            }
            else if (vFormat == 'month') {
                vColWidth = 37;
                vColUnit = 30;
            }
            else if (vFormat == 'quarter') {
                vColWidth = 60;
                vColUnit = 90;
            }
            else if (vFormat == 'hour') {
                vColWidth = 18;
                vColUnit = 1;
            }
            else if (vFormat == 'minute') {
                vColWidth = 18;
                vColUnit = 1;
            }
            fechaini = JSGantt.parseDateStr(fechaini, 'dd/mm/yyyy');
            fechafin = JSGantt.parseDateStr(fechafin, 'dd/mm/yyyy');
            vMinDate = JSGantt.getMinDate(vTaskListGlobal, vFormat);

            //
            if (vFormat == 'minute') {
                vTaskRight = (Date.parse(fechafin) - Date.parse(fechaini)) / (60 * 1000) + 1 / vColUnit;
                vTaskLeft = Math.ceil((Date.parse(fechaini) - Date.parse(vMinDate)) / (60 * 1000));
            }
            else if (vFormat == 'hour') {
                vTaskRight = (Date.parse(fechafin) - Date.parse(fechaini)) / (60 * 60 * 1000) + 1 / vColUnit;
                vTaskLeft = (Date.parse(fechaini) - Date.parse(vMinDate)) / (60 * 60 * 1000);
            }
            else {
                vTaskRight = (Date.parse(fechafin) - Date.parse(fechaini)) / (24 * 60 * 60 * 1000) + 1 / vColUnit;
                vTaskLeft = Math.ceil((Date.parse(fechaini) - Date.parse(vMinDate)) / (24 * 60 * 60 * 1000));
            }

            vDayWidth = (vColWidth / vColUnit) + (1 / vColUnit);

            //left
            if (tipo == 1) {
                izq = Math.ceil(vTaskLeft * (vDayWidth) + 1);
                $("#bardiv_" + Vid).css("left", izq + "px");
            }
            //right
            ancho = Math.ceil((vTaskRight) * (vDayWidth) - 1);
            $("#bardiv_" + Vid).css("width", ancho + "px");
            $("#taskbar_" + Vid).css("width", ancho + "px");
            return true
        }
    </script>
    <style type="text/css">
        .fechaini, .fechafin
        {
            cursor: pointer;
            width: 80px;
            font-size: 10px;
            text-align: center;
            border: 0px;
            background-color: Transparent;
            height: 17px;
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

        function ActualizarActividad(idactividad, actividad, presupuesto) {
            $.ajax({
                url: "ajax.aspx?idactividad="+ idactividad +"&actividad="+ $("#"+actividad).val() + "&presupuesto=" + $("#"+presupuesto).val()+"&actividadesu=true",
                async: false,
                succes: function (result) {
                    alert(result);
                },
                error: function (result) {
                    alert("Error:" + result.status + " Estatus: " + result.statusText);
                }
            });

            $("#ContentPlaceHolder1_Bandera").val("1");
        }

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

            $("#ContentPlaceHolder1_Bandera").val("1");
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

            $("#ContentPlaceHolder1_Bandera").val("1");
        }
        
        function ActivateAcordion()
        {
            $( ".accordion" ).accordion({ active: 0 });
            $("#expandir").focus();
        }

        $(document).ready(function () {
            
            $( ".accordion" ).accordion({ active: 2 });

            if($("#ContentPlaceHolder1_Bandera").val() == "1"){
            
            $('#izquierda').addClass('past');

            }

            $("#adetalles").click(function () {
                var idproyecto = $("#hidproyecto").val();
                $.prettyPhoto.open("/detallesmarcologico.aspx?idproyecto=" + idproyecto + "&iframe=true&width=100%&height=100%");
            });
            $("#Cronograma_Proyecto").click(function () {
                $.prettyPhoto.open("/DiagramaGant.aspx?&iframe=true&width=100%&height=100%");
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


        function SlideSiguiente()
        {
            $(".presente").prev().removeClass("past");

            $(".presente").css("margin-left","-25%");
            
            $(".presente").addClass("past");
            $(".presente").removeClass("presente");
            $(".presente").removeClass("futuro");

            $(".futuro").addClass("presente");
            $(".futuro").removeClass("futuro");
            $(".futuro").removeClass("past");

            $(".presente+div:first").addClass("futuro")
            $(".presente+div:first").removeClass("presente");
            $(".presente+div:first").removeClass("past");

            


        }

        function SlideVolver()
        {
            
            $(".presente+div:first").removeClass("futuro");

//            alert($(".presente").prev().html());
            $(".past").css("margin-left","0px");
            
            $(".presente").addClass("futuro");
            $(".presente").removeClass("presente");
            $(".presente").removeClass("past");
            

            $(".past").addClass("presente");
            $(".past").removeClass("past");
            $(".past").removeClass("futuro");

            
            $(".presente").prev().addClass("past");
            $(".presente").prev().removeClass("presente");
            $(".presente").prev().removeClass("futuro");

            

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<div style="width: 60%; margin-bottom: 0; margin-right: auto;">
        <ul id="navigation">
            <li class="home"><a href=""><span>Arbol Problemas</span></a></li>
            <li class="about"><a href=""><span>Marco Logico</span></a></li>
            <li class="search"><a href=""><span>Plan Operativo</span></a></li>
            <li class="photos"><a href=""><span>Cronograma</span></a></li>
            <%--<li class="rssfeed"><a href=""><span>Rss Feed</span></a></li>
            <li class="podcasts"><a href=""><span>Podcasts</span></a></li>
            <li class="contact"><a href=""><span>Contact</span></a></li>
    </ul> </div>--%>
    <div class="demo" style="width: 90%; margin: 0 auto;">
        <div id="slides" style="display: block; width: 6000px; clear: both; overflow: hidden;">
            <div id="izquierda" style="width: 25%; float: left;" class="demo mover presente">
                <div style="width: 1024px;">
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
                        <h2 style="color: #0b72bc;">
                            * Causas y Efectos</h2>
                        <br />
                        <span>Causa:
                            <asp:TextBox ID="txtCausa1" runat="server" class="txtareacausa" placeholder="2. Descripcion de la causa"
                                TextMode="MultiLine" Width="40%" />
                        </span>
                        <br />
                        <span>Efecto:
                            <asp:TextBox ID="txtEfecto1" runat="server" placeholder="3. Descripcion del efecto"
                                TextMode="MultiLine" Width="40%" Enabled="False" CssClass="txtareaefecto" />
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
                                <asp:BoundField DataField="Causa" HeaderText="Causa" SortExpression="Causa" />
                                <asp:BoundField DataField="Efecto" HeaderText="Efecto" SortExpression="Efecto" />
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
            <div id="derecha" style="width: 25%; margin: 0 auto; float: left;" class="demo mover futuro">
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
                        height: 30px; line-height: 30px; padding-left: 10px; font-size: 1.5em; width: 50%;">
                        Finalidad
                        <asp:TextBox ID="txtfinalidad" runat="server" />
                        <asp:LinkButton ID="lknAlmacenarFinalidad" Text="<img Width='24px' src='/Icons/save-icon.png' alt='Almacenar Proposito' />"
                            runat="server" OnClick="lknAlmacenarFinalidad_Click" />
                    </div>
                    <br />
                    <div class="problema" style="border: 2px solid #ccc; -moz-border-radius: 2px; -webkit-border-radius: 2px;
                        border-radius: 2px; /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 3px #000000; -webkit-box-shadow: 0px 0px 3px #000000; box-shadow: 0px 0px 3px #000000;
                        /*ie 7 and 8 do not support blur property of shadows*/ color: #005EA7; font-size: 1em;
                        padding-left: 10px; width: 50%;">
                        <h1>
                            Propósito</h1>
                        <asp:TextBox ID="txtProposito" runat="server" />
                        <asp:LinkButton ID="lknAlmacenarProposito" Text="<img Width='24px' src='/Icons/save-icon.png' alt='Almacenar Proposito' />"
                            runat="server" OnClick="lknAlmacenarProposito_Click" />
                        <a id="adetalles" href="#">
                            <img src="/Icons/details.png" width="24px" alt="Detalles" /></a> <a id="Cronograma_Proyecto"
                                href="#">
                                <img src="/Icons/Calender.png" width="24px" alt="Cronograma" /></a>
                        <input type="button" id="expandir" onclick="ActivateAcordion();" value="Expandir Actividades"
                            role="button" aria-disabled="false" class="ui-button ui-widget ui-state-default ui-corner-all" />
                        <br />
                        <asp:Panel ID="presultados" runat="server">
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div id="derechaSiguiente" style="width: 25%; margin: 0 auto; float: left;" class="demo mover">
                <br />
                <br />
                <div>
                    <table>
                        <tr>
                            <td>
                                <img src="/Icons/network.png" width="64px" alt="Plan Operativo" />
                            </td>
                            <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                                <h1 style="color: #0b72bc;">
                                    Plan Operativo</h1>
                            </td>
                        </tr>
                    </table>
                    <div class="problema" style="border: 2px solid #ccc; -moz-border-radius: 2px; -webkit-border-radius: 2px;
                        border-radius: 2px; /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 3px #000000; -webkit-box-shadow: 0px 0px 3px #000000; box-shadow: 0px 0px 3px #000000;
                        /*ie 7 and 8 do not support blur property of shadows*/ color: #005EA7; font-size: 1em;
                        padding-left: 10px; width: 50%;">
                        <h1>
                            Plan Operativo</h1>
                        <a id="a1" href="#">
                            <img src="/Icons/details.png" width="24px" alt="Detalles" /></a> <a id="A2" href="#">
                                <img src="/Icons/Calender.png" width="24px" alt="Cronograma" /></a>
                        <br />
                        <asp:Panel ID="pnlActividades" runat="server">
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div id="Cronograma" style="width: 25%; margin: 0 auto; float: left;" class="demo mover">
                <br />
                <br />
                <div>
                    <table>
                        <tr>
                            <td>
                                <img src="/Icons/network.png" width="64px" alt="Plan Operativo" />
                            </td>
                            <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                                <h1 style="color: #0b72bc; width: 50%;">
                                    Cronograma</h1>
                            </td>
                        </tr>
                    </table>
                    <div class="problema" style="border: 2px solid #ccc; -moz-border-radius: 2px; -webkit-border-radius: 2px;
                        border-radius: 2px; /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 3px #000000; -webkit-box-shadow: 0px 0px 3px #000000; box-shadow: 0px 0px 3px #000000;
                        /*ie 7 and 8 do not support blur property of shadows*/ color: #005EA7; font-size: 1em;
                        padding-left: 10px; width: 80%;">
                        <h1>
                            Cronograma general de actividades</h1>
                        <br />
                        <asp:HiddenField ID="HFTempDate" runat="server" />
                        <div style="position: relative;" class="gantt" id="GanttChartDIV">
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
        </div>
        <input type="button" onclick="SlideSiguiente();" class="btnright ui-button ui-widget ui-state-default ui-corner-all"
            role="button" aria-disabled="false" />
        <input type="button" onclick="SlideVolver();" class="btnleft ui-button ui-widget ui-state-default ui-corner-all"
            role="button" aria-disabled="false" />
    </div>
    <input type="hidden" runat="server" id="alerthq" value="-1" />
    <input type="hidden" runat="server" id="hidproyecto" value="-1" />
    <input type="hidden" runat="server" id="Bandera" value="-1" />
</asp:Content>