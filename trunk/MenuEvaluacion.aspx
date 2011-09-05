<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MenuEvaluacion.aspx.cs" Inherits="ESM.MenuEvaluacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 90%; margin: 0 auto;">
        <br />
        <br />
        <style>
            .menuevaluacion a
            {
                text-decoration: none;
                color: #000000;
            }
        </style>
        <table class="menuevaluacion" style="border: 0px; width: 100%; margin: 0 auto;">
            <tr>
                <td style="width: 48px; height: 48px;">
                    <img height="48px" width="48px" src="Icons/SE.png" alt="Secretaria de Educacion" />
                </td>
                <td>
                    <h3>
                        <a>Secretaria de Educacion</a></h3>
                </td>
                <td style="width: 48px; height: 48px;">
                    <img width="48px" height="48px" src="/Icons/EE.png" alt="Establecimiento Educativo" />
                </td>
                <td>
                    <h3>
                        <a>Instituciones Educativas</a>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <h4>
                        <a href="/Evaluacion.aspx?idrama=1">Evaluacion</a></h4>
                    <h4>
                        <a href="/LecturaContextoSE.aspx">Lectura de Contexto</a></h4>
                </td>
                <td>
                </td>
                <td>
                    <h4>
                        <a href="/Evaluacion.aspx?idrama=2">Evaluacion</a></h4>
                    <h4>
                        <a href="#">Lectura de Contexto</a></h4>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
