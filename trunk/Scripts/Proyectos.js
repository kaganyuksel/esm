$(function () {
    $("#accordion").accordion({
        autoHeight: false,
        navigation: true,
        collapsible: true
    });

    $("#accordion").accordion("activate", 0)
    
});

$(function () {
    $('#menu_arbol a').stop().animate({ 'marginright': '-165px' }, 1000);

    $('#menu_arbol > li').hover(
                    function () {
                        $('a', $(this)).stop().animate({ 'marginright': '-2px' }, 200);
                    },
                    function () {
                        $('a', $(this)).stop().animate({ 'marginright': '-165px' }, 200);
                    }
                );
});
$.datepicker.setDefaults({
    dateFormat: 'dd/mm/yy', currentText: 'Ahora', closeText: 'X', autoSize: true,
    dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
    dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'], dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'],
    firstDay: 1, monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'], nextText: 'Siguiente', prevText: 'Anterior',
    showAnim: 'slide', yearRange: '1990:2020'
});
$(function () {
    $('.fechaini').datepicker({
        dateFormat: 'dd/mm/yy', currentText: 'Ahora', closeText: 'Cerrar',
        onSelect: function (selectedDate) { save_dates(1, selectedDate, $(this).attr("alt"), this); }, showButtonPanel: false
    });
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

$(function () {
    $(".accordion").accordion({
        autoHeight: false,
        navigation: true,
        collapsible: true,
        animated: 'bounceslide'

    });

});

function AlmacenarProceso(idproceso, causa, proceso) {
    $.ajax({
        url: "ajax.aspx?idproceso=" + idproceso + "&causa=" + causa + "&proceso=" + $("#" + proceso).val() + "&procesos=true",
        async: false,
        success: function (result) {
            $(".success").html("Proceso almacenado correctamente.");
            $("#a_succes").trigger("click");
        },
        error: function (result) {
            alert("Error:" + result.status + " Estatus: " + result.statusText);
        }
    });

    $("#ContentPlaceHolder1_Bandera").val("1");
}

function AlmacenarSubProceso(idproceso, subproceso) {
    $.ajax({
        url: "ajax.aspx?idproceso=" + idproceso + "&subproceso=" + $("#" + subproceso).val() + "&subprocesos=true",
        async: false,
        success: function (result) {

            $("#col_subprocesos_" + idproceso).hide(500, function () {
                $("#col_subprocesos_" + idproceso).html(result);
            });

            $("#col_subprocesos_" + idproceso).show(500);

            $("#" + subproceso).val("");

            $(".success").html("Subproceso almacenado correctamente.");
            $("#a_succes").trigger("click");
        },
        error: function (result) {
            alert("Error:" + result.status + " Estatus: " + result.statusText);
        }
    });

    $("#ContentPlaceHolder1_Bandera").val("1");
}

function AlmacenarEstrategia(idsubproceso, estrategia) {
    $.ajax({
        url: "ajax.aspx?subproceso_id=" + idsubproceso + "&estrategia=" + $("#" + estrategia).val() + "&estrategias=true",
        async: false,
        success: function (result) {

            $("#col_estrategias_" + idsubproceso).hide(500, function () {
                $("#col_estrategias_" + idsubproceso).html(result);
            });

            $("#col_estrategias_" + idsubproceso).show(500);

            $("#" + estrategia).val("");

            $(".success").html("Estrategia almacenada correctamente.");
            $("#a_succes").trigger("click");
        },
        error: function (result) {
            alert("Error:" + result.status + " Estatus: " + result.statusText);
        }
    });

    $("#ContentPlaceHolder1_Bandera").val("1");
}

function ActualizarEstrategia(idestrategia, estrategia) {

    $.ajax({
        url: "ajax.aspx?idestrategia=" + idestrategia + "&estrategia=" + $("#" + estrategia).val() + "&estrategia_up=true",
        async: false,
        success: function (result) {


            $(".success").html("Estrategia actualizada correctamente.");
            $("#a_succes").trigger("click");
        },
        error: function (result) {
            alert("Error:" + result.status + " Estatus: " + result.statusText);
        }
    });

    $("#ContentPlaceHolder1_Bandera").val("1");
}


function ActualizarSubProceso(idsubproceso, subproceso) {

    $.ajax({
        url: "ajax.aspx?idsubproceso=" + idsubproceso + "&subproceso=" + $("#" + subproceso).val() + "&subprocesos_up=true",
        async: false,
        success: function (result) {
            $(".success").html("Subproceso actualizado correctamente.");
            $("#a_succes").trigger("click");
        },
        error: function (result) {
            alert("Error:" + result.status + " Estatus: " + result.statusText);
        }
    });

    $("#ContentPlaceHolder1_Bandera").val("1");
}

function AlmacenarActividad(idresultado, actividad, presupuesto) {
    $.ajax({
        url: "ajax.aspx?idResultado=" + idresultado + "&actividad=" + $("#" + actividad).val() + "&presupuesto=" + $("#" + presupuesto).val() + "&actividades=true",
        async: false,
        success: function (result) {

            $("#col_actividades_" + idresultado).hide(500, function () {
                $("#col_actividades_" + idresultado).html(result);
            });

            $("#col_actividades_" + idresultado).show(500);

            $("#" + actividad).val("");
            $("#" + presupuesto).val("0");

            $(".success").html("Actividad almacenada correctamente.");
            $("#a_succes").trigger("click");


        },
        error: function (result) {
            $(".error").html("Upps... ocurrio un error inesperado");
            $("#a_error").trigger("click");
        }
    });

    $("#ContentPlaceHolder1_Bandera").val("1");
}

function ActualizarActividad(idactividad, actividad, presupuesto) {
    $.ajax({
        url: "ajax.aspx?idactividad=" + idactividad + "&actividad=" + $("#" + actividad).val() + "&presupuesto=" + $("#" + presupuesto).val() + "&actividadesu=true",
        async: false,
        success: function (result) {
            $(".success").html("Actividad actualizada correctamente.");
            $("#a_succes").trigger("click");
        },
        error: function (result) {
            alert("Error:" + result.status + " Estatus: " + result.statusText);
        }
    });

    $("#ContentPlaceHolder1_Bandera").val("1");
}
function ActivateAcordion() {
    $(".accordion").accordion({ active: 0 });
    $("#expandir").focus();
}

$(document).ready(function () {

    $("#accordion").accordion();
    $(".problema textarea").css("width", "80%");
    $('span[title]').qtip({ style: { name: 'blue', tip: true} });

    $(this).scroll(function () {

        var distancia = window.scrollY
        if (distancia >= 110) {
            $("#menu_proyecto").css("top", "0px");
        }
        else if (distancia == 0) {
            $("#menu_proyecto").css("top", "");
        }


    });

    $("#ContentPlaceHolder1_mycolor").blur(function () {

        alert($("#ContentPlaceHolder1_mycolor").val());

        $("#ContentPlaceHolder1_txtCausa1").css("background", $("#ContentPlaceHolder1_mycolor").val());
        $("#ContentPlaceHolder1_txtEfecto1").css("background", $("#ContentPlaceHolder1_mycolor").val());

    });

    $(".reload").click(function () {
        $("#ContentPlaceHolder1_Bandera").val("1");
    });

    var problema_text = $("#ContentPlaceHolder1_txtproblema").val().toString();

    if ($.trim(problema_text).length == 0) {
        $("#ContentPlaceHolder1_lknAlmacenarE").css("display", "none");
        $("#ContentPlaceHolder1_txtCausa1").attr("disabled", true);
        $("#ContentPlaceHolder1_txtEfecto1").attr("disabled", true);
    }
    else {
        $("#ContentPlaceHolder1_lknAlmacenarE").css("display", "block");
        $("#ContentPlaceHolder1_txtCausa1").attr("disabled", false);
        $("#ContentPlaceHolder1_txtEfecto1").attr("disabled", false);
    }

    $(".accordion").accordion({ active: 2 });

    if ($("#ContentPlaceHolder1_Bandera").val() == "1") {

        $('#izquierda').addClass('past');

    }
    var idproyecto = $("#ContentPlaceHolder1_hidproyecto").val();
    $(".adetalles").click(function () {

        $.prettyPhoto.open("/detallesmarcologico.aspx?idproyecto=" + idproyecto + "&iframe=true&width=100%&height=100%");
    });
    $(".Cronograma_Proyecto").click(function () {
        $.prettyPhoto.open("/DiagramaGant.aspx?&iframe=true&width=100%&height=100%");
    });
    $(".Visualizar_Matriz").click(function () {

        $.prettyPhoto.open("/ReportMarcoLogico.aspx?idproyecto=" + idproyecto + "&marcologico=true&iframe=true&width=100%&height=100%");
    });
    $("#PlanOperativo_a").click(function () {

        $.prettyPhoto.open("/ReportMarcoLogico.aspx?idproyecto=" + idproyecto + "&planoperativo=true&iframe=true&width=100%&height=100%");

    });

    $(".pretty").prettyPhoto({
        ie6_fallback: true,
        modal: true,
        social_tools: false
    });

    $("#ContentPlaceHolder1_cbovervos").val();

    $("#ContentPlaceHolder1_txtFechaIndicador").datepicker({ dateFormat: "yy/mm/dd" });

    var problema = $("#ContentPlaceHolder1_txtproblema").val();

    if (problema.trim().length != 0) {
        $("#ContentPlaceHolder1_lknAlmacenarP").attr("disabled", true);
    }

    $("#ContentPlaceHolder1_txtEfecto1").val("");
    $("#ContentPlaceHolder1_txtEfecto1").attr("disabled", true);
    $("#ContentPlaceHolder1_txtCausa1").val("");

    //    $(".speech").each(function () {
    //        $(this).attr("onwebkitspeechchange", "textarea_change(this)");
    //    });


    var result = $("#ContentPlaceHolder1_alerthq").val();
    if (result == 1) {
        $("#a_succes").trigger("click");
        $("#ContentPlaceHolder1_alerthq").val("-1");
    }
    if (result == 0) {
        $("#a_error").trigger("click");
        $("#ContentPlaceHolder1_alerthq").val("-1");
    }

    $("#ContentPlaceHolder1_txtCausa1").change(function () {

        if ($(this).val().trim().length > 9)
            $("#ContentPlaceHolder1_txtEfecto1").attr("disabled", false);
        else {
            $("#ContentPlaceHolder1_txtEfecto1").val("");
            $("#ContentPlaceHolder1_txtEfecto1").attr("disabled", true);
        }
    });

});

function SlideSiguiente() {
    $(".presente").prev().removeClass("past");

    $(".presente").css("margin-left", "-25%");

    $(".presente").addClass("past");
    $(".presente").removeClass("presente");
    $(".presente").removeClass("futuro");

    $(".futuro").addClass("presente");
    $(".futuro").removeClass("futuro");
    $(".futuro").removeClass("past");

    $(".presente+div:first").addClass("futuro")
    $(".presente+div:first").removeClass("presente");
    $(".presente+div:first").removeClass("past");

    if ($(".presente").attr("id") == "derecha") {
        $("#li_marco_logico").css("border", "dashed 2px #fff");
        $("#li_arbol_problemas").css("border", "none");
        $("#li_plan_operativo").css("border", "none");
        $("#li_cronograma").css("border", "none");

        $("#li_Subprocesos").css("background", "#007cb6");
        $("#li_Subprocesos").css("border", "none");

        $("#li_plan_operativo").css("background", "#007cb6");
        $("#li_marco_logico").css("background", "#004464");
        $("#li_arbol_problemas").css("background", "#007cb6");
        $("#li_cronograma").css("background", "#007cb6");
        $("#li_procesos").css("background", "#007cb6");
        $("#li_procesos").css("border", "none");
        //$('#ayuda').fadeOut('fast');
        //$("#ayuda").html("<img src='/Icons/marco_logico.png' style='float:left;' width='48px'/> <h3 style='float:right; line-height:48px; color:#007CB6;'>Estrategias</h3> <p style=' margin-top:50px; margin-left: 5px; text-align:justify; clear:both;'><br/>El primero es la fonación y el segundo la articulación. <br/><br/> La fonación consiste en la producción de voz por las cuerdas vocales y la articulación consiste en la producción de puntos y modos de articulación para los fonemas de la lengua en que se expresa el hablante.</p>");
        //$('#ayuda').fadeIn('slow');
    }
    else if ($(".presente").attr("id") == "izquierda") {
        $("#li_arbol_problemas").css("border", "dashed 2px #fff");
        $("#li_marco_logico").css("border", "none");
        $("#li_plan_operativo").css("border", "none");
        $("#li_cronograma").css("border", "none");

        $("#li_Subprocesos").css("background", "#007cb6");
        $("#li_Subprocesos").css("border", "none");

        $("#li_plan_operativo").css("background", "#007cb6");
        $("#li_marco_logico").css("background", "#007cb6");
        $("#li_arbol_problemas").css("background", "#004464");
        $("#li_cronograma").css("background", "#007cb6");
        $("#li_procesos").css("background", "#007cb6");
        $("#li_procesos").css("border", "none");

        //$('#ayuda').fadeOut('fast');
        //$("#ayuda").html("<img src='/Icons/marco_logico.png' style='float:left;' width='48px'/> <h3 style='float:right; line-height:48px; color:#007CB6;'>Estrategias</h3> <p style=' margin-top:50px; margin-left: 5px; text-align:justify; clear:both;'><br/>El primero es la fonación y el segundo la articulación. <br/><br/> La fonación consiste en la producción de voz por las cuerdas vocales y la articulación consiste en la producción de puntos y modos de articulación para los fonemas de la lengua en que se expresa el hablante.</p>");
        //$('#ayuda').fadeIn('slow');
    }
    else if ($(".presente").attr("id") == "derechaSiguiente") {
        $("#li_arbol_problemas").css("border", "none");
        $("#li_marco_logico").css("border", "none");
        $("#li_plan_operativo").css("border", "dashed 2px #fff");
        $("#li_cronograma").css("border", "none");

        $("#li_Subprocesos").css("background", "#007cb6");
        $("#li_Subprocesos").css("border", "none");

        $("#li_plan_operativo").css("background", "#004464");
        $("#li_marco_logico").css("background", "#007cb6");
        $("#li_arbol_problemas").css("background", "#007cb6");
        $("#li_cronograma").css("background", "#007cb6");
        $("#li_procesos").css("background", "#007cb6");
        $("#li_procesos").css("border", "none");

        //$('#ayuda').fadeOut('fast');
        //$("#ayuda").html("<img src='/Icons/plan_operativo.png' style='float:left;' width='48px'/> <h3 style='float:right; line-height:48px; color:#007CB6;'>Actividades</h3> <p style=' margin-top:50px; margin-left: 5px; text-align:justify; clear:both;'><br/>El primero es la fonación y el segundo la articulación. <br/><br/> La fonación consiste en la producción de voz por las cuerdas vocales y la articulación consiste en la producción de puntos y modos de articulación para los fonemas de la lengua en que se expresa el hablante.</p>");
        //$('#ayuda').fadeIn('slow');
    }
    else if ($(".presente").attr("id") == "Cronograma") {
        $("#li_arbol_problemas").css("border", "none");
        $("#li_marco_logico").css("border", "none");
        $("#li_plan_operativo").css("border", "none");

        $("#li_procesos").css("background", "#007cb6");
        $("#li_procesos").css("border", "none");

        $("#li_Subprocesos").css("background", "#007cb6");
        $("#li_Subprocesos").css("border", "none");

        $("#li_plan_operativo").css("background", "#007cb6");
        $("#li_marco_logico").css("background", "#007cb6");
        $("#li_arbol_problemas").css("background", "#007cb6");
        $("#li_cronograma").css("background", "#004464");

        $("#li_cronograma").css("border", "dashed 2px #fff");

        $(".presente").css("display", "block");

        //$('#ayuda').fadeOut('fast');
        //$("#ayuda").html("<img src='/Icons/calender.png' style='float:left;' width='48px'/> <h3 style='float:right; line-height:48px; color:#007CB6;'>Cronograma</h3> <p style=' margin-top:50px; margin-left: 5px; text-align:justify; clear:both;'><br/>El primero es la fonación y el segundo la articulación. <br/><br/> La fonación consiste en la producción de voz por las cuerdas vocales y la articulación consiste en la producción de puntos y modos de articulación para los fonemas de la lengua en que se expresa el hablante.</p>");
        //$('#ayuda').fadeIn('slow');

        $("#btnsiguiente").attr("disabled", "true");
    }
    else if ($(".presente").attr("id") == "Mod_Procesos") {
        $("#li_arbol_problemas").css("border", "none");
        $("#li_marco_logico").css("border", "none");
        $("#li_plan_operativo").css("border", "none");

        $("#li_plan_operativo").css("background", "#007cb6");
        $("#li_marco_logico").css("background", "#007cb6");
        $("#li_arbol_problemas").css("background", "#007cb6");
        $("#li_cronograma").css("background", "#007cb6");
        $("#li_cronograma").css("border", "none");

        $("#li_Subprocesos").css("background", "#007cb6");
        $("#li_Subprocesos").css("border", "none");

        $("#li_procesos").css("background", "#004464");
        $("#li_procesos").css("border", "dashed 2px #fff");

        //$('#ayuda').fadeOut('fast');
        //$("#ayuda").html("<img src='/Icons/marco_logico.png' style='float:left;' width='48px'/> <h3 style='float:right; line-height:48px; color:#007CB6;'>Procesos</h3> <p style=' margin-top:50px; margin-left: 5px; text-align:justify; clear:both;'><br/>El primero es la fonación y el segundo la articulación. <br/><br/> La fonación consiste en la producción de voz por las cuerdas vocales y la articulación consiste en la producción de puntos y modos de articulación para los fonemas de la lengua en que se expresa el hablante.</p>");
        //$('#ayuda').fadeIn('slow');
        $("#btnvolver").removeAttr("Disabled");
    }
    else if ($(".presente").attr("id") == "Mod_Subprocesos") {
        $("#li_arbol_problemas").css("border", "none");
        $("#li_marco_logico").css("border", "none");
        $("#li_plan_operativo").css("border", "none");

        $("#li_plan_operativo").css("background", "#007cb6");
        $("#li_marco_logico").css("background", "#007cb6");
        $("#li_arbol_problemas").css("background", "#007cb6");
        $("#li_cronograma").css("background", "#007cb6");
        $("#li_cronograma").css("border", "none");

        $("#li_procesos").css("background", "#007cb6");
        $("#li_procesos").css("border", "none");

        $("#li_Subprocesos").css("background", "#004464");
        $("#li_Subprocesos").css("border", "dashed 2px #fff");

        //$('#ayuda').fadeOut('fast');

        //$('#ayuda').html("<img src='/Icons/marco_logico.png' style='float:left;' width='48px'/> <h3 style='float:right; line-height:48px; color:#007CB6;'>Subprocesos</h3> <p style=' margin-top:50px; margin-left: 5px; text-align:justify; clear:both;'><br/>El primero es la fonación y el segundo la articulación. <br/><br/> La fonación consiste en la producción de voz por las cuerdas vocales y la articulación consiste en la producción de puntos y modos de articulación para los fonemas de la lengua en que se expresa el hablante.</p>");

        //$('#ayuda').fadeIn('slow');
    }
}

function SlideVolver() {

    $(".presente+div:first").removeClass("futuro");

    $(".past").css("margin-left", "0px");

    $(".presente").addClass("futuro");
    $(".presente").removeClass("presente");
    $(".presente").removeClass("past");


    $(".past").addClass("presente");
    $(".past").removeClass("past");
    $(".past").removeClass("futuro");


    $(".presente").prev().addClass("past");
    $(".presente").prev().removeClass("presente");
    $(".presente").prev().removeClass("futuro");

    if ($(".presente").attr("id") == "derecha") {
        $("#li_marco_logico").css("border", "dashed 2px #fff");
        $("#li_arbol_problemas").css("border", "none");
        $("#li_plan_operativo").css("border", "none");
        $("#li_cronograma").css("border", "none");

        $("#li_Subprocesos").css("background", "#007cb6");
        $("#li_Subprocesos").css("border", "none");

        $("#li_plan_operativo").css("background", "#007cb6");
        $("#li_marco_logico").css("background", "#004464");
        $("#li_arbol_problemas").css("background", "#007cb6");
        $("#li_cronograma").css("background", "#007cb6");
        $("#li_procesos").css("background", "#007cb6");
        $("#li_procesos").css("border", "none");
        //$('#ayuda').fadeOut('fast');
        //$("#ayuda").html("<img src='/Icons/marco_logico.png' style='float:left;' width='48px'/> <h3 style='float:right; line-height:48px; color:#007CB6;'>Estrategias</h3> <p style=' margin-top:50px; margin-left: 5px; text-align:justify; clear:both;'><br/>El primero es la fonación y el segundo la articulación. <br/><br/> La fonación consiste en la producción de voz por las cuerdas vocales y la articulación consiste en la producción de puntos y modos de articulación para los fonemas de la lengua en que se expresa el hablante.</p>");
        //$('#ayuda').fadeIn('slow');
    }
    else if ($(".presente").attr("id") == "izquierda") {
        $("#li_arbol_problemas").css("border", "dashed 2px #fff");
        $("#li_marco_logico").css("border", "none");
        $("#li_plan_operativo").css("border", "none");
        $("#li_cronograma").css("border", "none");

        $("#li_Subprocesos").css("background", "#007cb6");
        $("#li_Subprocesos").css("border", "none");

        $("#li_plan_operativo").css("background", "#007cb6");
        $("#li_marco_logico").css("background", "#007cb6");
        $("#li_arbol_problemas").css("background", "#004464");
        $("#li_cronograma").css("background", "#007cb6");
        $("#li_procesos").css("background", "#007cb6");
        $("#li_procesos").css("border", "none");
        //$('#ayuda').fadeOut('fast');
        //$("#ayuda").html("<img src='/Icons/network.png' style='float:left;' width='48px'/> <h5 style='float:right; line-height:48px; color:#007CB6; text-align:justify;'>Árbol de Problemas</h5> <p style=' margin-top:50px; margin-left: 5px; text-align:justify; clear:both;'><br/>El primero es la fonación y el segundo la articulación. <br/><br/> La fonación consiste en la producción de voz por las cuerdas vocales y la articulación consiste en la producción de puntos y modos de articulación para los fonemas de la lengua en que se expresa el hablante.</p>");
        //$('#ayuda').fadeIn('slow');

        $("#btnvolver").attr("Disabled", "true");
    }
    else if ($(".presente").attr("id") == "derechaSiguiente") {
        $("#li_arbol_problemas").css("border", "none");
        $("#li_marco_logico").css("border", "none");
        $("#li_plan_operativo").css("border", "dashed 2px #fff");
        $("#li_cronograma").css("border", "none");

        $("#li_Subprocesos").css("background", "#007cb6");
        $("#li_Subprocesos").css("border", "none");

        $("#li_plan_operativo").css("background", "#004464");
        $("#li_marco_logico").css("background", "#007cb6");
        $("#li_arbol_problemas").css("background", "#007cb6");
        $("#li_cronograma").css("background", "#007cb6");
        $("#li_procesos").css("background", "#007cb6");
        $("#li_procesos").css("border", "none");

        //$('#ayuda').fadeOut('fast');
        //$("#ayuda").html("<img src='/Icons/plan_operativo.png' style='float:left;' width='48px'/> <h3 style='float:right; line-height:48px; color:#007CB6;'>Actividades</h3> <p style=' margin-top:50px; margin-left: 5px; text-align:justify; clear:both;'><br/>El primero es la fonación y el segundo la articulación. <br/><br/> La fonación consiste en la producción de voz por las cuerdas vocales y la articulación consiste en la producción de puntos y modos de articulación para los fonemas de la lengua en que se expresa el hablante.</p>");
        //$('#ayuda').fadeIn('slow');

        $("#btnsiguiente").removeAttr("Disabled");
    }
    else if ($(".presente").attr("id") == "Cronograma") {
        $("#li_arbol_problemas").css("border", "none");
        $("#li_marco_logico").css("border", "none");
        $("#li_plan_operativo").css("border", "none");

        $("#li_procesos").css("background", "#007cb6");
        $("#li_procesos").css("border", "none");

        $("#li_Subprocesos").css("background", "#007cb6");
        $("#li_Subprocesos").css("border", "none");

        $("#li_plan_operativo").css("background", "#007cb6");
        $("#li_marco_logico").css("background", "#007cb6");
        $("#li_arbol_problemas").css("background", "#007cb6");
        $("#li_cronograma").css("background", "#004464");

        $("#li_cronograma").css("border", "dashed 2px #fff");

        $(".presente").css("display", "block");

        //$('#ayuda').fadeOut('fast');
        //$("#ayuda").html("<img src='/Icons/calender.png' style='float:left;' width='48px'/> <h3 style='float:right; line-height:48px; color:#007CB6;'>Cronograma</h3> <p style=' margin-top:50px; margin-left: 5px; text-align:justify; clear:both;'><br/>El primero es la fonación y el segundo la articulación. <br/><br/> La fonación consiste en la producción de voz por las cuerdas vocales y la articulación consiste en la producción de puntos y modos de articulación para los fonemas de la lengua en que se expresa el hablante.</p>");
        //$('#ayuda').fadeIn('slow');

    }
    else if ($(".presente").attr("id") == "Mod_Procesos") {
        $("#li_arbol_problemas").css("border", "none");
        $("#li_marco_logico").css("border", "none");
        $("#li_plan_operativo").css("border", "none");

        $("#li_plan_operativo").css("background", "#007cb6");
        $("#li_marco_logico").css("background", "#007cb6");
        $("#li_arbol_problemas").css("background", "#007cb6");
        $("#li_cronograma").css("background", "#007cb6");
        $("#li_cronograma").css("border", "none");

        $("#li_Subprocesos").css("background", "#007cb6");
        $("#li_Subprocesos").css("border", "none");

        $("#li_procesos").css("background", "#004464");
        $("#li_procesos").css("border", "dashed 2px #fff");

        //$('#ayuda').fadeOut('fast');
        //$("#ayuda").html("<img src='/Icons/marco_logico.png' style='float:left;' width='48px'/> <h3 style='float:right; line-height:48px; color:#007CB6;'>Procesos</h3> <p style=' margin-top:50px; margin-left: 5px; text-align:justify; clear:both;'><br/>El primero es la fonación y el segundo la articulación. <br/><br/> La fonación consiste en la producción de voz por las cuerdas vocales y la articulación consiste en la producción de puntos y modos de articulación para los fonemas de la lengua en que se expresa el hablante.</p>");
        //$('#ayuda').fadeIn('slow');
    }
    else if ($(".presente").attr("id") == "Mod_Subprocesos") {
        $("#li_arbol_problemas").css("border", "none");
        $("#li_marco_logico").css("border", "none");
        $("#li_plan_operativo").css("border", "none");

        $("#li_plan_operativo").css("background", "#007cb6");
        $("#li_marco_logico").css("background", "#007cb6");
        $("#li_arbol_problemas").css("background", "#007cb6");
        $("#li_cronograma").css("background", "#007cb6");
        $("#li_cronograma").css("border", "none");

        $("#li_procesos").css("background", "#007cb6");
        $("#li_procesos").css("border", "none");

        $("#li_Subprocesos").css("background", "#004464");
        $("#li_Subprocesos").css("border", "dashed 2px #fff");

        //$('#ayuda').fadeOut('fast');
        //$("#ayuda").html("<img src='/Icons/marco_logico.png' style='float:left;' width='48px'/> <h3 style='float:right; line-height:48px; color:#007CB6;'>Subprocesos</h3> <p style=' margin-top:50px; margin-left: 5px; text-align:justify; clear:both;'><br/>El primero es la fonación y el segundo la articulación. <br/><br/> La fonación consiste en la producción de voz por las cuerdas vocales y la articulación consiste en la producción de puntos y modos de articulación para los fonemas de la lengua en que se expresa el hablante.</p>");
        //$('#ayuda').fadeIn('slow');
    }
}