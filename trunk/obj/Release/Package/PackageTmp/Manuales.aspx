<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Manuales.aspx.cs" Inherits="ESM.Manuales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div style="width: 80%; margin: 0 auto;">
        <table class="menuevaluacion" style="width: 50%; margin: 0 auto; border: 0px; margin: 0 auto;">
            <tr>
                <td style="text-align: left; vertical-align: middle;">
                    <h1>
                        <img height="48px" width="48px" src="Icons/Help.png" alt="Documentación" />
                        Documentación</h1>
                </td>
                <td style="padding-left: 30px;">
                    <br />
                    <img src="Icons/Address_Book.png" width="24px" alt="docs" />
                    <a href="/Documentacion/ESM_lectura_de_contexto _EE_v05.xls" target="_blank">Lectura
                        Contexto SE</a>
                    <br />
                    <img src="Icons/Address_Book.png" width="24px" alt="docs" />
                    <a href="/Documentacion/ESM_lectura_de_contexto _SE_v05.xls" target="_blank">Lectura
                        Contexto EE</a>
                    <br />
                    <img src="Icons/Address_Book.png" width="24px" alt="docs" />
                    <a href="/Documentacion/Evaluación_PDF_v01.rar" target="_blank">Formatos Evaluación
                        EE</a>
                    <br />
                    <img src="Icons/Address_Book.png" width="24px" alt="docs" />
                    <a href="/Documentacion/Sistematización_v03_20111024.pdf" target="_blank">Formato y
                        acta de visita para Sistematización</a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
