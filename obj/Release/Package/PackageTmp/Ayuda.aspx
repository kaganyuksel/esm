<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ayuda.aspx.cs" Inherits="ESM.Ayuda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="-moz-border-radius: 3px; -webkit-border-radius: 3px; border-radius: 3px;
            /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 3px #000000; -webkit-box-shadow: 0px 0px 3px #000000; box-shadow: 0px 0px 3px #000000;
            /*ie 7 and 8 do not support blur property of shadows*/
width: 100%; text-align: center; font-size: 15px; font-family: 'Lucida Grande' , 'Lucida Sans Unicode' , Helvetica, Arial, Verdana, sans-serif;">
            <tr runat="server" visible="false" style="-moz-border-radius: 3px; -webkit-border-radius: 3px;
                border-radius: 3px; /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 3px #000000; -webkit-box-shadow: 0px 0px 3px #000000; box-shadow: 0px 0px 3px #000000;
                /*ie 7 and 8 do not support blur property of shadows*/
background: #005EA7; color: #ffffff; text-align: center; font-size: 14px; font-weight: bold;
                height: 40px; width: 100%">
                <td style="height: 50px;">
                    Descripción de la Pregunta Seleccionada
                </td>
            </tr>
            <tr runat="server" visible="false">
                <td>
                    <asp:Label ID="lblDescripcion" Text="text" runat="server" />
                </td>
            </tr>
            <tr style="height: 40px; width: 600px; background: #005EA7; color: #ffffff; text-align: center;
                font-size: 14px; font-weight: bold;">
                <td>
                    Documentos que a tener en cuenta.
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    <asp:CheckBoxList ID="clist" Enabled="false" runat="server" RepeatDirection="Vertical"
                        Font-Size="14px">
                        <asp:ListItem Text="PEI" Value="1"></asp:ListItem>
                        <asp:ListItem Text="PMI" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Manual de Convivencia" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Planes de Estudio" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Documentos de proyectos pedagógicos" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Otros" Value="6"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
