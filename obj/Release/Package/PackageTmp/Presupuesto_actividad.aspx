<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Presupuesto_actividad.aspx.cs"
    Inherits="ESM.Presupuesto_actividad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Presupuesto Actividad</title>
    <link href="mastercustom.css" rel="stylesheet" type="text/css" />
    <link href="Style/jquery-ui-1.8.15.custom.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtFecha").datepicker();

            $(".numerico").change(function () {

//                if (isNaN($(this).val()))
//                    $(this).val("0");
//                else if ($(this).val().length == 0)
//                    $(this).val("0");
            });
        });
    </script>
    <style type="text/css">
        body
        {
            background: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 80%; margin: 0 auto;">
        <h2 style="margin-top: 50px; color: #005EA7">
            <img src="/Icons/Stationery.png" width="48px" alt="Valores" />Metas intermedias
            para presupuesto de actividad</h2>
        Administra valores ejecutados de acuerdo a las metas propuestas.<br />
        <br />
        <div style="clear: both; width: 100%;">
            <textarea name="name" id="txtactividad" runat="server" style="width: 50%;" value="" /><a
                id="a_Reporte" runat="server" target="_blank"><img src="/Icons/details.png" width="24px" /></a>
            <br />
            <br />
            <div style="clear: both;">
                <ol style="display: inline;">
                    <li style="float: left;"><span style="display: block;">Fecha</span>
                        <asp:TextBox ID="txtFecha" placeholder="Fecha de Medición" runat="server"></asp:TextBox></li>
                    <li style="float: left;"><span style="display: block;">Meta</span>
                        <asp:TextBox ID="txtmeta_presupuesto" runat="server" CssClass="numerico" placeholder="Meta propuesta"
                            Text="0"></asp:TextBox></li>
                    <li id="li_valor_meta" visible="false" runat="server" style="float: left;"><span
                        style="display: block;">Ejecutado</span>
                        <asp:TextBox ID="txtValor" runat="server" CssClass="numerico" placeholder="Valor"
                            Text="0"></asp:TextBox></li>
                    <li style="float: left;"><span style="display: block;">Almacenar</span>
                        <asp:LinkButton ID="lknAlmacenar" Text="<img src='/Icons/save-icon.png' width='24px' alt='Almacenar'/>"
                            runat="server" OnClick="lknAlmacenar_Click" /></li>
                </ol>
                <input type="hidden" id="idmeta" runat="server" name="name" value="" />
            </div>
            <br />
            <br />
            <asp:GridView ID="gvMediciones_Indicador" Style="margin-left: 0px; clear: both" runat="server"
                Width="50%" OnSelectedIndexChanged="gvMediciones_Indicador_SelectedIndexChanged">
                <AlternatingRowStyle CssClass="trblanca" />
                <Columns>
                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Icons/Stationery.png" ShowSelectButton="True">
                        <ControlStyle Width="24px" />
                    </asp:CommandField>
                </Columns>
                <HeaderStyle CssClass="trheader" />
                <RowStyle CssClass="trgris" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
