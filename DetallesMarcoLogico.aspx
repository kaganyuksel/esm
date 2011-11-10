<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetallesMarcoLogico.aspx.cs"
    Inherits="ESM.DetallesMarcoLogico" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html>
<head runat="server">
    <title>Detalles</title>
    <link href="Style/jquery-ui-1.8.15.custom.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <style type="text/css">
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
    </style>
    <script type="text/javascript">
        $(function () {
            $("ul.droptrue").sortable({
                connectWith: "ul"
            });

            $("ul.dropfalse").sortable({
                connectWith: "ul"
                //                dropOnEmpty: false
            });

            $("#sortable1, #sortable2").disableSelection();
            $("#sortable3, #sortable4").disableSelection();
        });
        $(document).ready(function () {

            $("#txtFechaIndicador").datepicker({ dateFormat: "yy/mm/dd" });

            $(".droptrue>li").addClass("ui-state-default");
            $(".dropfalse>li").addClass("ui-state-highlight");


            $("#previsualizar").click(function () {
                var indicador = $("#txtFechaIndicador").val() + " " + $("#cboverbos option:selected").html() + " " + $("#Meta").val() + " " + $("#cboUnidades option:selected").html() + " " + $("#txtdescripcionI").val()
                $("#txtindicadorg").val(indicador);

            });
        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="demo" style="border: 1px solid #ccc; -moz-border-radius: 2px; -webkit-border-radius: 2px;
        border-radius: 2px; width: 100%;">
        Verbo:<asp:DropDownList ID="cboverbos" runat="server" DataSourceID="sqldtverbos"
            DataTextField="Verbo" DataValueField="Id">
        </asp:DropDownList>
        <asp:SqlDataSource ID="sqldtverbos" runat="server" ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>"
            SelectCommand="SELECT [Id], [Verbo] FROM [Verbos]"></asp:SqlDataSource>
        Fecha:<asp:TextBox ID="txtFechaIndicador" runat="server" />
        Meta:<asp:TextBox ID="Meta" Width="24px" MaxLength="3" runat="server" />
        Unidades:<asp:DropDownList ID="cboUnidades" runat="server" DataSourceID="sqldtUnidades"
            DataTextField="Unidad" DataValueField="Id">
        </asp:DropDownList>
        <asp:SqlDataSource ID="sqldtUnidades" runat="server" ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>"
            SelectCommand="SELECT [Id], [Unidad] FROM [Unidades]"></asp:SqlDataSource>
        Descripcion:<asp:TextBox ID="txtdescripcionI" runat="server" />
        <asp:ImageButton ID="previsualizar" runat="server" ImageUrl="~/Icons/Search.png"
            Width="24px" />
        <br />
        <asp:TextBox ID="txtindicadorg" Width="80%" runat="server"></asp:TextBox>
        <asp:LinkButton Text="<img src='/Icons/1314641093_plus_48.png' width='24px' alt='Agregar' />"
            runat="server" ID="lknAgregarIndicador" OnClick="lknAgregarIndicador_Click" />
        <br />
        <br />
        <div>
            <asp:BulletedList ID="sortable1" CssClass="droptrue" Width="150px" Height="200px"
                runat="server" DataSourceID="lqMediosVerificacion" DataTextField="Medio_de_verificacion"
                DataValueField="Id" AppendDataBoundItems="True">
            </asp:BulletedList>
            <asp:BulletedList ID="sortable2" Height="200px" Width="150px" CssClass="dropfalse"
                runat="server">
                <asp:ListItem Text="Supuestos Asignados" Enabled="false" />
            </asp:BulletedList>
            <asp:LinqDataSource ID="lqMediosVerificacion" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
                EntityTypeName="" Select="new (Medio_de_verificacion, Id)" TableName="Medios_de_verificacions">
            </asp:LinqDataSource>
        </div>
        <div>
            <asp:BulletedList ID="sortable3" CssClass="droptrue" Width="150px" Height="200px"
                runat="server" DataSourceID="lqsupuestos" DataTextField="supuesto1" DataValueField="Id">
            </asp:BulletedList>
            <asp:LinqDataSource ID="lqsupuestos" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
                EntityTypeName="" Select="new (supuesto1, Id)" TableName="Supuestos">
            </asp:LinqDataSource>
            <asp:BulletedList ID="sortable4" Height="200px" Width="150px" CssClass="dropfalse"
                runat="server">
                <asp:ListItem Text="Supuestos Asignados" Enabled="false" />
            </asp:BulletedList>
        </div>
    </div>
    </form>
</body>
</html>
