<%@ Page Title="" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="ESM.WebForm1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        $(function () {
            $("#ContentPlaceHolder1_txtUsuario").focusout(function () {
                if ($("#ContentPlaceHolder1_txtUsuario").val() == "")
                { alert("El campo Nombre de Usuario no puede ser vacio."); }
            });
            $("#ContentPlaceHolder1_txtContrasena").focusout(function () {
                if ($("#ContentPlaceHolder1_txtContrasena").val() == "")
                { alert("El campo Contraseña no puede ser vacio."); }
            });
        });
        

    </script>
    <script type="text/javascript">
        $(function () {
            $("input:submit", ".demo").button({

                icons: {
                    primary: "ui-icon-locked"
                },
                text: true

            });

        });
    </script>
    <script type="text/javascript">

        $(function () {

            $("#dialog:ui-dialog").dialog("destroy");

            $("#dialog-message").dialog({
                modal: true,
                buttons: {
                    Ok: function () {
                        $(this).dialog("close");
                    }
                }
            });
        });

        
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:Panel ID="pnlSesion" runat="server" DefaultButton="btnIniciarSesion">
        <div class="demo">
            <table style="margin: 60px auto; font-size: 13px; width: 700px; height: 200px; text-align: center;
                border: 1px solid #dddddd; -moz-border-radius: 3px; -webkit-border-radius: 3px;
                border-radius: 3px; /*ie 7 and 8 do not support border radius*/">
                <tbody>
                    <tr>
                        <td>
                            <div style="line-height: 25px; line-height: 25px; font-family: Helvetica, Arial, sans-serif;
                                height: 30px; font-size: 16px; color: #005ea7;" class="ui-widget-header">
                                Iniciar Sesión
                            </div>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Nombre de Usuario
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtUsuario" runat="server" ToolTip="Nombre de Usuario"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contraseña
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:Button ID="btnRecordar" runat="server" Text="Recordar Contraseña" TabIndex="1"
                                Visible="false" />
                            <asp:Button ID="btnIniciarSesion" runat="server" Text="Iniciar Sesión" OnClick="btnIniciarSesion_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <div id="formulario" runat="server">
            </div>
        </div>
    </asp:Panel>
</asp:Content>
