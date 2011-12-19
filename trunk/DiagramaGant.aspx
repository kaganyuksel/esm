<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DiagramaGant.aspx.cs" Inherits="ESM.DiagramaGant"
    Culture="es-CO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Style/jquery-ui-1.8.15.custom.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <link href="Style/jsgantt.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jsgantt.js" type="text/javascript"></script>
    <script type="text/javascript">
        $.datepicker.setDefaults({
            dateFormat: 'dd/mm/yy', currentText: 'Ahora', closeText: 'X', autoSize: true,
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'], dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'],
            firstDay: 1, monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'], nextText: 'Siguiente', prevText: 'Anterior',
            showAnim: 'slide', yearRange: '1990:2020'
        });
        $(function () {
            //time picker
            $('.fechaini').datepicker({
                dateFormat: 'dd/mm/yy', currentText: 'Ahora', closeText: 'Cerrar',
                onSelect: function (selectedDate) { save_dates(1, selectedDate, $(this).attr("alt"), this); }, showButtonPanel: false
            });
            //time picker
            $('.fechafin').datepicker({
                dateFormat: 'dd/mm/yy', currentText: 'Ahora', closeText: 'Cerrar',
                onSelect: function (selectedDate) { save_dates(2, selectedDate, $(this).attr("alt"), this); }, showButtonPanel: false
            });
        });

        function load_date_ini(selectedDatefunction, Vid) {
            $('.fechaini').datepicker("destroy");
            $('.fechaini').datepicker({
                dateFormat: 'dd/mm/yy', currentText: 'Ahora', closeText: 'Cerrar',
                maxDate: selectedDatefunction,
                onSelect: function (selectedDate) { save_dates(1, selectedDate, $(this).attr("alt"), this, Vid) }, showButtonPanel: false
            });
        }

        function load_date_fin(selectedDatefunction, Vid) {
            $('.fechafin').datepicker("destroy");
            $('.fechafin').datepicker({
                dateFormat: 'dd/mm/yy', currentText: 'Ahora', closeText: 'Cerrar',
                minDate: selectedDatefunction,
                onSelect: function (selectedDate) { save_dates(2, selectedDate, $(this).attr("alt"), this, Vid) }, showButtonPanel: false
            });
        }

        function tempDate(valor, fechaEva, tipo, Vid) {
            if (tipo == 2)
                load_date_fin($("#" + fechaEva).val(), Vid);
            else
                load_date_ini($("#" + fechaEva).val(), Vid)

            $("#HFTempDate").val($(valor).val());
        }

        function save_dates(tipo, valor, id, objeto, Vid) {
            var valorIni = valor;
            valor = valor.replace("/", "-").replace("/", "-");
            var fechaini;
            var fechafin;
            $.ajax({
                url: "save_dates.aspx?id=" + id + "&tipo=" + tipo + "&valor=" + valor,
                async: false,
                success: function (result) {
                    if (result == "Fecha actualizada correctamente") {
                        alert(result);
                        if (tipo == 1) {
                            fechaini = valorIni;
                            fechafin = $("#fecha_fin_id_" + Vid).val();
                        } else {
                            fechaini = $("#fecha_ini_id_" + Vid).val();
                            fechafin = valorIni;
                        }
                        retorna_ancho(fechaini, fechafin, Vid, tipo);
                    } else {
                        alert(result);
                        $(objeto).datetimepicker("setDate", $("#HFTempDate").val());
                    }
                },
                error: function (result) {
                    alert("Error " + result.status + ' ' + result.statusText);
                }
            });
        }

        function retorna_ancho(fechaini, fechafin, Vid, tipo) {
            var vColWidth = 0;
            var vColUnit = 0;
            var vTaskRight;
            var vTaskLeft;
            var ancho;
            var izq;
            var vMinDate = new Date();
            var vFormat = $("input[@name='radFormat']:checked").val();
            //
            if (vFormat == 'day') {
                vColWidth = 18;
                vColUnit = 1;
            }
            else if (vFormat == 'week') {
                vColWidth = 37;
                vColUnit = 7;
            }
            else if (vFormat == 'month') {
                vColWidth = 37;
                vColUnit = 30;
            }
            else if (vFormat == 'quarter') {
                vColWidth = 60;
                vColUnit = 90;
            }
            else if (vFormat == 'hour') {
                vColWidth = 18;
                vColUnit = 1;
            }
            else if (vFormat == 'minute') {
                vColWidth = 18;
                vColUnit = 1;
            }
            fechaini = JSGantt.parseDateStr(fechaini, 'dd/mm/yyyy');
            fechafin = JSGantt.parseDateStr(fechafin, 'dd/mm/yyyy');
            vMinDate = JSGantt.getMinDate(vTaskListGlobal, vFormat);

            //
            if (vFormat == 'minute') {
                vTaskRight = (Date.parse(fechafin) - Date.parse(fechaini)) / (60 * 1000) + 1 / vColUnit;
                vTaskLeft = Math.ceil((Date.parse(fechaini) - Date.parse(vMinDate)) / (60 * 1000));
            }
            else if (vFormat == 'hour') {
                vTaskRight = (Date.parse(fechafin) - Date.parse(fechaini)) / (60 * 60 * 1000) + 1 / vColUnit;
                vTaskLeft = (Date.parse(fechaini) - Date.parse(vMinDate)) / (60 * 60 * 1000);
            }
            else {
                vTaskRight = (Date.parse(fechafin) - Date.parse(fechaini)) / (24 * 60 * 60 * 1000) + 1 / vColUnit;
                vTaskLeft = Math.ceil((Date.parse(fechaini) - Date.parse(vMinDate)) / (24 * 60 * 60 * 1000));
            }

            vDayWidth = (vColWidth / vColUnit) + (1 / vColUnit);

            //left
            if (tipo == 1) {
                izq = Math.ceil(vTaskLeft * (vDayWidth) + 1);
                $("#bardiv_" + Vid).css("left", izq + "px");
            }
            //right
            ancho = Math.ceil((vTaskRight) * (vDayWidth) - 1);
            $("#bardiv_" + Vid).css("width", ancho + "px");
            $("#taskbar_" + Vid).css("width", ancho + "px");
            return true
        }
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".pp_pic_holder", top.document).css("top", "0px");

        });
    
    </script>
    <style type="text/css">
        .fechaini, .fechafin
        {
            cursor: pointer;
            width: 80px;
            font-size: 10px;
            text-align: center;
            border: 0px;
            background-color: Transparent;
            height: 17px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <h2>
        <table>
            <tr>
                <td>
                    <img alt="Plan Operativo" src="Icons/Calender.png" width="48px" />
                </td>
                <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                    <h1 id="titulo_diagrama" runat="server" style="color: #0b72bc; width: 50%;">
                        Cronograma General</h1>
                </td>
            </tr>
        </table>
        &nbsp;</h2>
    <asp:HiddenField ID="HFTempDate" runat="server" />
    <div style="position: relative;" class="gantt" id="GanttChartDIV">
    </div>
    </form>
</body>
</html>
