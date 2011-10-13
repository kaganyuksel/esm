<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Citas.aspx.cs" Inherits="ESM.Citas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>--%>
    <link href="/Pretty/css/prettyPhoto.css" rel="stylesheet" charset="utf-8" media="screen"
        type="text/css" />
    <script src="/Pretty/js/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>
    <link href="Scripts/full_calendar/fullcalendar.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/full_calendar/fullcalendar.js" type="text/javascript"></script>
    <script type="text/javascript">
        
        var calendarv = null;

        $(document).ready(function () {
        
            var idConsultor = $("#ContentPlaceHolder1_idconsultor").val();
            function calendario()
            {
              calendarv = $("#calendar").fullCalendar({

                    header: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'month,agendaWeek,agendaDay'
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
                    selectable: true,
			        selectHelper: true,
			        select: function(start, end, allDay) {
                        start = $.fullCalendar.formatDate( start, "yyyy/MM/dd HH:mm:ss" );
                        end = $.fullCalendar.formatDate( end, "yyyy/MM/dd HH:mm:ss" );
                        $("#pretty").attr("href",'/AddCita.aspx?idc='+ idConsultor +'&start='+ start +'&end='+ end +'&iframe=true&width=100%&height=100%');
                        $("#pretty").trigger("click");

				        calendar.fullCalendar('renderEvent',
						    {
							    title: title,
							    start: start,
							    end: end,
							    allDay: allDay
						    },
						    true // make the event "stick"
					    );
				    
				        calendar.fullCalendar('unselect');
			        },
                    editable: true,
                    eventResize: function(event,dayDelta,minuteDelta,revertFunc) {
                    
                    

                        var fechainicial = $.fullCalendar.formatDate( event.start, "yyyy/MM/dd HH:mm:ss" )
                        var fechafinal = $.fullCalendar.formatDate( event.end, "yyyy/MM/dd HH:mm:ss" )

                        $.ajax({
                                url: '/AddCita.aspx?edit=true&idcita=' + event.id + '&start='+ fechainicial + '&end='+ fechafinal,
                                async: false,
                                succes: function () {
                                    alert("Actualizado Ajax");
                            },
                                error: function (result) {
                                    alert("Error:" + result.status + " Estatus: " + result.statusText);
                                }
                        });
                    },
                    eventDrop: function(event,dayDelta,minuteDelta,allDay,revertFunc) {

                        var fechainicial = $.fullCalendar.formatDate( event.start, "yyyy/MM/dd HH:mm:ss" )

                        $.ajax({
                                url: '/AddCita.aspx?edit=true&idcita=' + event.id + '&start='+ fechainicial + '&dias=' + dayDelta + '&minutos=' + minuteDelta + '&end=""',
                                async: false,
                                succes: function () {
                                    alert("Actualizado Ajax");
                            },
                                error: function (result) {
                                    alert("Error:" + result.status + " Estatus: " + result.statusText);
                                }
                        });
            

                    },
                    events: {
                        url: '/json.aspx?id=' + $("#ContentPlaceHolder1_idconsultor").val(),
                        cache: true
                    }
            });
        }
            $("#pretty").prettyPhoto({
                callback: function(){
                    calendarv.fullCalendar( 'destroy' );
                    calendario();
                }
            });
            
            calendario();
        });

        $("a.pretty").prettyPhoto({
            ie6_fallback: true,
            modal: true,
            social_tools: false,
            
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
    <a href="#" id="pretty"></a>
</asp:Content>
