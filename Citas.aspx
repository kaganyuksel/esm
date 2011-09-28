<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Citas.aspx.cs" Inherits="ESM.Citas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>--%>
    <link href="Scripts/full_calendar/fullcalendar.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/full_calendar/fullcalendar.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            alert($("#ContentPlaceHolder1_idconsultor").val());
            $("#calendar").fullCalendar({

                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month'
                },
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNameShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'],
                buttonText: {
                    today: 'hoy',
                    month: 'mes',
                    week: 'semana',
                    day: 'día'
                },
                allDaySlot: false,
                allDayText: 'Todo el día',
                theme: true,
                defaultView: 'month',
                editable: true,
                height: 600,
                events: {
                    url: '/json.aspx?id=' + $("#ContentPlaceHolder1_idconsultor").val(),
                    cache: true
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 80%; margin: 0 auto;">
        <asp:HiddenField runat="server" ID="idconsultor" />
        <br />
        <br />
        <div>
            <h1 style="color: #005EA7;">
                <img src="Icons/Calender.png" alt="Citas" />Agenda para Consultor</h1>
            <p style="font-size: 13px; margin: 0 48px;">
                Visualiza la asignación de visitas a Establecimientos Educativos y/o Secretarías
                de Educación
            </p>
        </div>
        <br />
        <br />
        <div id='calendar'>
        </div>
    </div>
</asp:Content>
