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
    <link href="mastercustom.css" rel="stylesheet" type="text/css" />
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


                }
            });

            $("#sortable1, #sortable2").disableSelection();
            $("#sortable3, #sortable4").disableSelection();
        });

        function Medios() {

            alert("Detalles");


        };

        $(document).ready(function () {




            $("#txtFechaIndicador").datepicker({ dateFormat: "yy/mm/dd" });
            $("#txtFechaFinal").datepicker({ dateFormat: "yy/mm/dd" });


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
        <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final:" Visible="False"></asp:Label>
        <asp:TextBox ID="txtFechaFinal" runat="server" Visible="False"></asp:TextBox>
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
        <asp:GridView ID="gvIndicadores_Actividad" runat="server" 
            AutoGenerateColumns="False" DataKeyNames="Id"
            DataSourceID="sqlActividadesIndicadores" Width="80%" Visible="False">
            <AlternatingRowStyle CssClass="trblanca" />
            <Columns>
                <asp:CommandField ButtonType="Image" CancelImageUrl="~/Icons/Close.png" CancelText=""
                    DeleteImageUrl="~/Icons/Bin_Full.png" EditImageUrl="~/Icons/Stationery.png" InsertImageUrl="~/Icons/save-icon.png"
                    ShowDeleteButton="True" ShowEditButton="True" UpdateImageUrl="~/Icons/save-icon.png">
                    <ControlStyle Width="24px" />
                </asp:CommandField>
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True"
                    SortExpression="Id" Visible="False" />
                <asp:BoundField DataField="Actividad_id" HeaderText="Identificador" SortExpression="Actividad_id" />
                <asp:BoundField DataField="Indicador" HeaderText="Indicador" SortExpression="Indicador" />
                <asp:BoundField DataField="unidad_id" HeaderText="unidad_id" SortExpression="unidad_id"
                    Visible="False" />
                <asp:BoundField DataField="verbo_id" HeaderText="verbo_id" SortExpression="verbo_id"
                    Visible="False" />
                <asp:BoundField DataField="fecha_indicador_inicial" HeaderText="Fecha Inicial" SortExpression="fecha_indicador_inicial" />
                <asp:BoundField DataField="fecha_indicador_final" HeaderText="Fecha Final" SortExpression="fecha_indicador_final" />
                <asp:BoundField DataField="Fecha_Creacion" HeaderText="Fecha de Creación" SortExpression="Fecha_Creacion" />
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
                <asp:QueryStringParameter DefaultValue="0" Name="Actividad_id" 
                    QueryStringField="idactividad" Type="Int32" />
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
        <br />
        <div>
            <asp:BulletedList ID="sortable1" CssClass="droptrue" Width="150px" Height="200px"
                runat="server" DataTextField="Medio_de_verificacion" DataValueField="Id" AppendDataBoundItems="True"
                ViewStateMode="Enabled">
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
        <input type="hidden" runat="server" id="supuestosinput" value="" />
        <input type="hidden" runat="server" id="mediosinput" value="" />
    </div>
    </form>
</body>
</html>
