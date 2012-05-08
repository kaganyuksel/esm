var jproyecto = null;


function CargarProyecto(id, actualizar) {
    j.ajax({
        url: "ajaxBancoProyectos.aspx?proyecto_id=" + id + "&actualizarproyecto=" + actualizar,
        async: false,
        success: function (result) {
            jproyecto = result.split("<");
            jproyecto = jproyecto[0].trim();
            jproyecto = JSON.parse(jproyecto);

            //            j("#if_c_e").attr("src", "/jqgrid_causas_efectos.aspx?proyecto_id=" + j('#ContentPlaceHolder1_cmbproyectos option:selected').val());

            j("#ContentPlaceHolder1_txtnombreproyecto").val(jproyecto[0].Nombre);
            j("#ContentPlaceHolder1_txtproblema").val(jproyecto[0].Problema);
            j("#ContentPlaceHolder1_ban_proyecto_id").val(jproyecto[0].Id);

            alert("Id agregado");
        },
        error: function (result) {
            alert("Error " + result.status + ' ' + result.statusText);
        }
    });
}

function Actualizar() {
    j("#dialog_proyectos").css("display", "block");
    j("#dialog_proyectos").dialog({
        open: true,
        height: 300,
        modal: true,
        close: function () { j("#dialog_proyectos").css("display", "none"); }
    });
}

