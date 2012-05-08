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
                #archivos a
                {
                    text-decoration: none;
                    color: #005EA7;
                    font-size: 13px;
                }
                .style1
                {
                    font-size: 14px;
                    width: 150px;
                }
                .style2
                {
                    text-align: right;
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
                            <img height="48px" src="/Icons/Upload.png" alt="Almacen de Documentos" />
                            Módulo para carga de documentos.</h1>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        Para el proceso de carga se debe tener en cuenta que el documento sea del tipo:
                        pdf, rar, zip, doc, docx, y que el tamaño sea igual o inferior a 2MB.
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        PEI:
                    </td>
                    <td>
                        <asp:FileUpload ID="FPEI" runat="server" CssClass="files" Width="60%" />
                        <a href="#" target="_blank" visible="false" runat="server" id="lknPEI">Visualizar</a>
                    </td>
                    <td class="style2">
                        <asp:Button ID="btnUpPEIS" Text="Cargar" runat="server" OnClick="btnUpPEIS_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        PMI:
                    </td>
                    <td>
                        <asp:FileUpload ID="FPMI" runat="server" CssClass="files" Width="60%" />
                        <a visible="false" runat="server" id="lknPMI" target="_blank">Visualizar</a>
                    </td>
                    <td class="style2">
                        <asp:Button ID="btnUpPMI" Text="Cargar" runat="server" OnClick="btnUpPMI_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Manual de Convivencia:
                    </td>
                    <td>
                        <asp:FileUpload ID="FMC" runat="server" CssClass="files" Width="60%" />
                        <a target="_blank" href="#" visible="false" runat="server" id="lknMC">Visualizar</a>
                    </td>
                    <td class="style2">
                        <asp:Button ID="btnUpMC" Text="Cargar" runat="server" OnClick="btnUpMC_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Plan de Estudio:
                    </td>
                    <td>
                        <asp:FileUpload ID="FPS" runat="server" CssClass="files" Width="60%" />
                        <a href="#" target="_blank" visible="false" runat="server" id="lknPS">Visualizar</a>
                    </td>
                    <td class="style2">
                        <asp:Button ID="btnUpPS" Text="Cargar" runat="server" OnClick="btnUpPS_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Documentos de proyectos pedagógicos:
                    </td>
                    <td>
                        <asp:FileUpload ID="FDPP" runat="server" CssClass="files" Width="60%" />
                        <a href="#" target="_blank" visible="false" runat="server" id="lknDPP">Visualizar</a>
                    </td>
                    <td class="style2">
                        <asp:Button ID="btnUpDPP" Text="Cargar" runat="server" OnClick="btnUpPcc_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Otros:
                    </td>
                    <td>
                        <asp:FileUpload ID="FOtros" runat="server" CssClass="files" Width="60%" />
                        <a href="#" target="_blank" visible="false" runat="server" id="lknOtros">Visualizar</a>
                    </td>
                    <td class="style2">
                        <asp:Button ID="btnUpOtros" Text="Cargar" runat="server" OnClick="btnUpActas_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Acta de Visita EE:
                    </td>
                    <td>
                        <asp:FileUpload ID="FActaVisitaEE" runat="server" CssClass="files" Width="60%" />
                        <a href="#" target="_blank" visible="false" runat="server" id="lknActaVisitaEE">Visualizar</a>
                    </td>
                    <td class="style2">
                        <asp:Button ID="btnUpActaVisitaEE" Text="Cargar" runat="server" 
                            onclick="btnUpActaVisitaEE_Click" />
                    </td>
                </tr>

                <tr>
                    <td colspan="3" style="text-align: center;">
                        <br />
                        <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Green" Font-Bold="true"
                            Visible="false" Font-Size="20px"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
    </form>
</body>
</html>
