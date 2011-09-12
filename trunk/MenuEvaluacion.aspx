<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MenuEvaluacion.aspx.cs" Inherits="ESM.MenuEvaluacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 90%; margin: 0 auto;">
        <style>
            .menuevaluacion
            {
                font-size: 12px;
            }
            .menuevaluacion a
            {
                text-decoration: none;
                color: #005EA7;
                font-size: 14px;
            }
            .menuevaluacion h1
            {
                color: #005EA7;
                font-size: 19px;
            }
        </style>
        <br />
        <br />
        <br />
        <br />
        <table class="menuevaluacion" style="width: 80%; margin: 0 auto; border: 0px; margin: 0 auto;">
            <tr>
                <td colspan="4" style="text-align: center;">
                    <h1>
                        <img height="48px" width="48px" src="Icons/Portfolio.png" alt="Secretaria de Educacion" />
                        Secretarías de Educación</h1>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <a href="/LecturaContextoSE.aspx">
                        <img width="48px" height="48px" src="/Icons/Stationery.png" alt="Establecimiento Educativo" /></a>
                </td>
                <td>
                    <h4>
                        <a href="/LecturaContextoSE.aspx">Lectura de Contexto</a></h4>
                    Administra la información para el formato Lectura de Contexto SE.
                </td>
                <td style="width: 48px; height: 48px;" runat="server" visible="false">
                    <a href="/Evaluacion.aspx?idrama=1">
                        <img width="48px" height="48px" src="/Icons/Edit.png" alt="Establecimiento Educativo" /></a>
                </td>
                <td runat="server" visible="false">
                    <h4>
                        <a href="/Evaluacion.aspx?idrama=1">Evaluacion</a></h4>
                    Ejecuta el proceso de evaluación para las Secretarias de Educacion.
                </td>
                <td>
                    <a href="#">
                        <img src="/Icons/Paste.png" height="48px" alt="Acta de Visita" /></a>
                </td>
                <td>
                    <h4>
                        <a href="#" onclick="alert('En construción.');">Acta de Visita</a></h4>
                    Administra la información para el formato acta de visita de la SE.
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <table class="menuevaluacion" style="width: 80%; margin: 0 auto;">
            <tr>
                <td colspan="4" style="text-align: center;">
                    <h1>
                        <img src="/Icons/Tutorial.png" alt="EE" />
                        Establecimientos Educativos</h1>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <a href="/Evaluacion.aspx?idrama=2">
                        <img src="/Icons/Edit.png" alt="Evaluacion" /></a>
                </td>
                <td>
                    <h4>
                        <a href="/Evaluacion.aspx?idrama=2">Evaluacion</a></h4>
                    Realiza el proceso de evaluación para los Establecimientos Educativos.
                </td>
                <td>
                    <a href="/LecturaContextoEE.aspx">
                        <img src="/Icons/Stationery.png" height="48px" alt="Lectura Contexto" /></a>
                </td>
                <td>
                    <h4>
                        <a href="/LecturaContextoEE.aspx">Lectura de Contexto</a></h4>
                    Administra la Información para el formato Lectura de Contexto EE.
                </td>
            </tr>
            <tr>
                <td>
                    <a href="#">
                        <img src="/Icons/Paste.png" height="48px" alt="Lectura Contexto" /></a>
                </td>
                <td>
                    <h4>
                        <a href="#" onclick="alert('En construción.');">Acta de Visita</a></h4>
                    Administra la Información para el formato acta de visita del EE.
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
