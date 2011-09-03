<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuloDocumentos.aspx.cs"
    Inherits="ESM.ModuloDocumentos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/jquery-ui-1.8.15.custom.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ValidarDocumento(documento) {
            var result = null;
            $(".classdocument").each(function (i) {

                var item = $(this).html();

                if (documento == item) {
                    result = confirm("Ya existe un documento para " + documento + " , Desea Reemplazarlo?");
                    return result;
                }
            });
            return result;
        }
        $(document).ready(function () {
            $("#btnUpPEIS").click(function () {

                return ValidarDocumento("PEI");

            });
        });
        $(document).ready(function () {
            $("#btnUpPMI").click(function () {

                return ValidarDocumento("PMI");

            });
        });
        $(document).ready(function () {
            $("#btnUpMC").click(function () {

                return ValidarDocumento("Manual de Convivencia");

            });
        });
        $(document).ready(function () {
            $("#btnUpPS").click(function () {

                return ValidarDocumento("Plan de Estudio");

            });
        });
        $(document).ready(function () {
            $("#btnUpActas").click(function () {

                return ValidarDocumento("Actas");

            });
        });
        $(document).ready(function () {
            $("#btnUpPro").click(function () {

                return ValidarDocumento("Proyectos");

            });
        });
        $(document).ready(function () {
            $("#btnUpCon").click(function () {

                return ValidarDocumento("Convenios");

            });
        });
        $(document).ready(function () {
            $("#btnUpPCC").click(function () {

                return ValidarDocumento("Programa de Competencias Ciudadanas");

            });
        });

        $(function () {
            $("input:submit", ".demo").button({

                icons: {
                    primary: "ui-icon-locked"
                },
                text: true

            });

        });
        $(function () {
            $("input:file", ".demo").button({

                icons: {
                    primary: "ui-icon-locked"
                },
                text: true

            });

        });
    </script>
    <style type="text/css">
        .files
        {
            width: 400px;
        }
    </style>
</head>
<body style="background: #dddddd; font-family: 'Lucida Grande' , 'Lucida Sans Unicode' , Helvetica, Arial, Verdana, sans-serif;">
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <div class="demo" style="width: 98%; margin: 0 auto;">
            <style type="text/css">
                #archivos td
                {
                    padding: 5px 5px 5px 5px;
                    font-size: 12px;
                }
            </style>
            <table id="archivos" style="width: 100%; -moz-border-radius: 2px; -webkit-border-radius: 2px;
                border-radius: 2px; /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 4px #000000; -webkit-box-shadow: 0px 0px 4px #000000; box-shadow: 0px 0px 4px #000000;
                /*ie 7 and 8 do not support blur property of shadows*/
background: #ffffff;" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="3" style="-moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;
                        background: #DEDEDE; color: #ffffff; height: 32px;">
                        <h1 style="font-weight: none; color: #005EA7; font-family: 'Lucida Grande' , 'Lucida Sans Unicode' , Helvetica, Arial, Verdana, sans-serif;">
                            <img height="48px" src="Icons/Upload.png" alt="Almacen de Documentos" />
                            Modulo para carga de documentos.</h1>
                    </td>
                </tr>
                <tr>
                    <td>
                        PEI:
                    </td>
                    <td>
                        <asp:FileUpload ID="FPEI" runat="server" CssClass="files" />
                    </td>
                    <td>
                        <asp:Button ID="btnUpPEIS" Text="Cargar" runat="server" OnClick="btnUpPEIS_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        PMI:
                    </td>
                    <td>
                        <asp:FileUpload ID="FPMI" runat="server" CssClass="files" />
                    </td>
                    <td>
                        <asp:Button ID="btnUpPMI" Text="Cargar" runat="server" OnClick="btnUpPEIS_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Manual de Convivencia:
                    </td>
                    <td>
                        <asp:FileUpload ID="FMC" runat="server" CssClass="files" />
                    </td>
                    <td>
                        <asp:Button ID="btnUpMC" Text="Cargar" runat="server" OnClick="btnUpPEIS_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Plan de Estudio:
                    </td>
                    <td>
                        <asp:FileUpload ID="FPS" runat="server" CssClass="files" />
                    </td>
                    <td>
                        <asp:Button ID="btnUpPS" Text="Cargar" runat="server" OnClick="btnUpPEIS_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Actas:
                    </td>
                    <td>
                        <asp:FileUpload ID="FActas" runat="server" CssClass="files" />
                    </td>
                    <td>
                        <asp:Button ID="btnUpActas" Text="Cargar" runat="server" OnClick="btnUpPEIS_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Proyectos:
                    </td>
                    <td>
                        <asp:FileUpload ID="FProyecto" runat="server" CssClass="files" />
                    </td>
                    <td>
                        <asp:Button ID="btnUpPro" Text="Cargar" runat="server" OnClick="btnUpPEIS_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Convenios:
                    </td>
                    <td>
                        <asp:FileUpload ID="FConvenio" runat="server" CssClass="files" />
                    </td>
                    <td>
                        <asp:Button ID="btnUpCon" Text="Cargar" runat="server" OnClick="btnUpPEIS_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Programa de Competencias Ciudadanas:
                    </td>
                    <td>
                        <asp:FileUpload ID="FPCC" runat="server" CssClass="files" />
                    </td>
                    <td>
                        <asp:Button ID="btnUpPCC" Text="Cargar" runat="server" OnClick="btnUpPEIS_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center;">
                        <br />
                        <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Green" Font-Bold="true"
                            Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <style>
                .gvDocumentos
                {
                    border: 1px solid #dddddd;
                    -moz-border-radius: 1px;
                    -webkit-border-radius: 1px;
                    border-radius: 1px; /*IE 7 AND 8 DO NOT SUPPORT BORDER RADIUS*/
                    -moz-box-shadow: 0px 0px 2px #000000;
                    -webkit-box-shadow: 0px 0px 2px #000000;
                    box-shadow: 0px 0px 2px #000000; /*IE 7 AND 8 DO NOT SUPPORT BLUR PROPERTY OF SHADOWS*/
                }
                .header
                {
                    background: #d8d8d8;
                    color: #005EA7;
                    height: 40px;
                }
                .td
                {
                    border: 1px solid #dddddd;
                    -moz-border-radius: 1px;
                    -webkit-border-radius: 1px;
                    border-radius: 1px; /*IE 7 AND 8 DO NOT SUPPORT BORDER RADIUS*/
                    -moz-box-shadow: 0px 0px 2px #000000;
                    -webkit-box-shadow: 0px 0px 2px #000000;
                    box-shadow: 0px 0px 2px #000000; /*IE 7 AND 8 DO NOT SUPPORT BLUR PROPERTY OF SHADOWS*/
                    height: 30px;
                    color: #005EA7;
                    background: #ffffff;
                    text-align: center;
                }
            </style>
            <asp:GridView ID="gvDocumentos" CssClass="gvDocumentos" runat="server" AutoGenerateColumns="False"
                Width="100%">
                <HeaderStyle CssClass="header" />
                <RowStyle CssClass="td" />
                <Columns>
                    <asp:BoundField DataField="Documento" HeaderText="Documento">
                        <ItemStyle CssClass="classdocument" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Actualizado" HeaderText="Actualizado" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href='/Files/<%# Eval("Ruta") %>' target="_blank">Visualizar</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
