<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BancoProyectos.aspx.cs" Inherits="ESM.BancoProyectos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
    <link href="/Style/bancoproyectos.css" rel="stylesheet" type="text/css" />
    <link href="Style/jquery.jOrgChart.css" rel="stylesheet" type="text/css" />
    <link href="/Style/jqgrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="fancybox/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/turn.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.jOrgChart.js" type="text/javascript"></script>
    <script src="Scripts/jqgrid/grid.locale-es.js" type="text/javascript"></script>
    <script src="/Scripts/jqgrid/js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="/Scripts/bancoproyectos.js" type="text/javascript"></script>
    <script src="fancybox/jquery.fancybox-1.3.4.js" type="text/javascript"></script>
    <script src="fancybox/jquery.easing-1.3.pack.js" type="text/javascript"></script>
    <script type="text/javascript">
        var fls;
        var nuevo = false;
        var path_files = [];
        function addlist(event) {
            if (window.File) {
                fls = event.target.files;

                var lista = [];
                for (var i = 0, f; f = fls[i]; i++) {
                    var html_file = '<li><strong>' + f.name + '</strong> (' + f.type + ') -- ' + f.lastModifiedDate.toLocaleDateString() + '</li>';

                    lista.push(html_file);
                }

                var output_list = document.getElementById('ContentPlaceHolder1_list');

                output_list.innerHTML = '<ul>' + lista.join('') + '</ul>';
            }

            j('#ContentPlaceHolder1_btnuploadfile').trigger('click');

        }

        var obj_file = null;
        var page_number = null;
        var j = jQuery.noConflict();

        var if_marcologico;

        j(document).ready(function () {

            j("#close_message").click(function () {

                j("#forie").css("display", "none");

            });

            j(this).scroll(function () {

                var distancia = window.scrollY
                if (distancia >= 110) {
                    j("#nav_page").css("top", "0px");
                    j("#nav_page").css("position", "fixed");
                    j("#nav_page").css("margin-left", "35%");
                    j("#nav_page").css("z-index", "5000");
                }
                else if (distancia <= 110) {
                    j("#nav_page").css("top", "");
                    j("#nav_page").css("position", "");
                    j("#nav_page").css("margin-left", "");
                    j("#nav_page").css("z-index", "");
                }
            });

            obj_file = document.getElementById('ContentPlaceHolder1_files');
            obj_file.addEventListener('change', addlist, false);

            page_number = j("#magazine").turn('page');

            j("#magazine").css("margin-left", "-20%");

            j('#magazine').turn({
                gradients: true,
                acceleration: true,
                when: { turned: function () {
                    if (document.getElementById("ContentPlaceHolder1_ban_proyecto_id").value == " " && j('#magazine').turn('page') == 2 && !nuevo) {
                        j('#magazine').css('margin-left', 'auto');
                        alert('Por favor seleccione un proyecto para continuar');
                        j('#magazine').turn('page', 1);
                        j('#magazine').css('margin-left', '-20%');
                    } else if (j('#magazine').turn('page') == 2) {
                        j('#magazine').css('margin-left', 'auto');
                    }
                }
                }
            });

            j.extend(j.jgrid.edit, { width: "500", afterComplete: function (response, postdata, formid) { alert('El proceso finaliz� correctamente.'); } });

            if (j("#ContentPlaceHolder1_post_back").val() == "1") {
                j("#magazine").turn('next');
                j("#ContentPlaceHolder1_post_back").val("");
            }

            j("#jqgrid_table").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=fuentes_financiacion',
                datatype: "json",
                colNames: ['No.', 'Tipo Entidad', 'Entidad', 'Tipo Recurso'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 55 },
   		                    { name: 'tipoentidad', index: 'tipoentidad', width: 100, editable: true, edittype: "select", editoptions: { value: "Nacional:Nacional;Departamental:Departamental;Municipal:Municipal;Organismo Multilateral:Organismo Multilateral;Otro:Otro"} },
   		                    { name: 'entidad', index: 'entidad', width: 200, editable: true },
   		                    { name: 'tiporecurso', index: 'tiporecurso', width: 200, align: "right", editable: true }
   	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#jqgrid_div',
                sortname: 'id',
                mytype: "POST",
                postData: { tabla: "f_f", proyecto_id: function () { return j("#ContentPlaceHolder1_ban_proyecto_id").val(); } },
                viewrecords: true,
                sortorder: "desc",
                editurl: "ajaxBancoProyectos.aspx",
                caption: "Fuentes de Financiaci�n",
                afterComplete: function (response, postdata, formid) { alert("save"); }

            });
            j("#jqgrid_table").jqGrid('inlineNav', "#jqgrid_div");
            j("#jqgrid_table").navGrid('#jqgrid_div', { edit: true, add: true, del: true, search: false, view: true }, { width: 500, reloadAfterSubmit: true }, { reloadAfterSubmit: true, width: 500, closeAfterAdd: false }, { reloadAfterSubmit: true }, {});


            j("#jqgrid_matriz_identificacion_t").jqGrid({
                url: 'ajaxBancoProyectos.aspx?modulo=identificacion',
                datatype: "json",
                colNames: ['NO.', 'GRUPOS', 'INTERES', 'PROBLEMA RECIBIDO', 'RECURSOS Y MANDATOS'],
                colModel: [
   		                    { name: 'id', index: 'id', width: 55 },
   		                    { name: 'grupos', index: 'grupos', width: 120, editable: true },
   		                    { name: 'interes', index: 'interes', width: 120, editable: true },
   		                    { name: 'problemarecibido', index: 'problemarecibido', width: 120, align: "right", editable: true },
                            { name: 'recursosymandatos', index: 'recursosymandatos', width: 120, align: "right", editable: true }
   	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#jqgrid_matriz_identificacion_d',
                sortname: 'id',
                mytype: "POST",
                postData: { tabla: "i", proyecto_id: function () { return j("#ContentPlaceHolder1_ban_proyecto_id").val(); } },
                viewrecords: true,
                sortorder: "desc",
                editurl: "ajaxBancoProyectos.aspx",
                caption: "Fuentes de Financiaci�n"
            });
            j("#jqgrid_matriz_identificacion_t").jqGrid('navGrid', "#jqgrid_matriz_identificacion_d", { edit: true, add: true, del: false });
            j("#jqgrid_matriz_identificacion_t").jqGrid('inlineNav', "#jqgrid_matriz_identificacion_d");

            j('#accordion').accordion({ autoHeight: false,
                navigation: true,
                collapsible: true
            });

            j("#ContentPlaceHolder1_txtfechaelaboracion").datepicker({ showAnim: "bounce" });

            j("#btnalmacenarproyecto").click(function () {
                if (j("#ContentPlaceHolder1_txtnombreproyecto").val() == "" && j("#ContentPlaceHolder1_txtproblema").val() == "")
                    return false;
            });

            setTimeout('tooltip();', 5000);


            if (document.getElementById("ContentPlaceHolder1_ban_files").value == "1") {
                j("#magazine").css("margin-left", "auto");
                j("#magazine").turn("page", 16);

                document.getElementById("ContentPlaceHolder1_ban_files").value = " ";
            }
            else if (document.getElementById("ContentPlaceHolder1_ban_files").value == "2") {
                j("#magazine").css("margin-left", "auto");
                j("#magazine").turn("page", 16);
                alert("Para cargar un archivo se debe tener en cuenta que el tama�o sea menor o igual a 5MB");
                document.getElementById("ContentPlaceHolder1_ban_files").value = " ";
            }
            else if (document.getElementById("ContentPlaceHolder1_ban_files").value == "2") {
                j("#magazine").css("margin-left", "auto");
                j("#magazine").turn("page", 16);
                alert("Error al cargar el archivo seleccionado.");
                document.getElementById("ContentPlaceHolder1_ban_files").value = " ";
            }
            if (j("#ContentPlaceHolder1_ban_proyecto_id").val() != " " && j("#ContentPlaceHolder1_ban_proyecto_id").val() != "0") {
                setInterval("UpdateArbolProblemas(j('#ContentPlaceHolder1_ban_proyecto_id').val(), true);", 3000);
            }
        });

        setInterval('var numeric_text = j("#ContentPlaceHolder1_if_marco_logico").contents().find("#presupuesto"); j(numeric_text).change(function () { if(isNaN(j(this).val())){j(this).val("0");} });', 3000);

        function refreshMarcoLogico() {
            var id = j('#ContentPlaceHolder1_ban_proyecto_id').val();
            j.ajax({
                url: "ajaxBancoProyectos.aspx?proyecto_id=" + id + "&refreshMarcoLogico=true",
                async: false,
                success: function (result) {
                    j("#ContentPlaceHolder1_content_marcologico").html("");

                    j("#ContentPlaceHolder1_content_marcologico").html(result);
                },
                error: function (result) {
                    // TODO:JCMM: Mensaje de Error Controlado
                }
            });
        }

        function tooltip() {
            j(".node").each(function () {
                j(this).attr("title", j(this).html());
            });
            j(".node").each(function () {
                j(this).html(j(this).html().substring(0, 8));
            });

            j(".node").each(function () {
                j(this).qtip({ content: j(this).attr("title"), show: "mouseover", hide: "mouseout", style: { name: "dark" }
                });
            });
        }

        function UpdateArbolProblemas(id, actualizar) {
            j.ajax({
                url: "ajaxBancoProyectos.aspx?proyecto_id=" + id + "&actualizararbolproblemas=true",
                async: false,
                success: function (result) {
                    j("#ContentPlaceHolder1_org").html("");

                    j("#ContentPlaceHolder1_org").html(result);

                },
                error: function (result) {
                    alert("Error " + result.status + ' ' + result.statusText);
                }
            });

            j.ajax({
                url: "ajaxBancoProyectos.aspx?proyecto_id=" + id + "&actualizararbolobjetivos=true",
                async: false,
                success: function (result) {

                    j("#ContentPlaceHolder1_org_objetivos").html("");

                    j("#ContentPlaceHolder1_org_objetivos").html(result);

                },
                error: function (result) {
                    alert("Error " + result.status + ' ' + result.statusText);
                }
            });
            j.ajax({
                url: "ajaxBancoProyectos.aspx?proyecto_id=" + id + "&matrizactores=true",
                async: false,
                success: function (result) {

                    j("#ContentPlaceHolder1_matrizactores_div").html("");

                    j("#ContentPlaceHolder1_matrizactores_div").html(result);

                },
                error: function (result) {
                    alert("Error " + result.status + ' ' + result.statusText);
                }
            });
            j('#refreshOrganigrama').attr('href', '/Organigrama.aspx?proyecto_id=' + j('#ContentPlaceHolder1_ban_proyecto_id').val() + '&problemas=true');
            j('#refreshOrganigrama_obj').attr('href', '/Organigrama.aspx ?proyecto_id=' + j('#ContentPlaceHolder1_ban_proyecto_id').val() + '&objetivos=true');
            j('#planaccion').attr('href', '/Organigrama.aspx?proyecto_id=' + j('#ContentPlaceHolder1_ban_proyecto_id').val() + '&planaccion=true');
        }
        function deleteFile(file) {
            if (confirm('Esta seguro que desea eliminar el archivo.')) {
                j.ajax({
                    url: "ajaxBancoProyectos.aspx?deleteFile=" + file,
                    async: false,
                    success: function (result) {
                        j("#a_document_" + file).parent().remove();
                    },
                    error: function (result) {

                        alert("Error " + result.status + ' ' + result.statusText);
                    }
                });
            }
        }
    </script>
    <style type="text/css">
        #forie
        {
            vertical-align: middle;
            width: 93.6%;
            height: 30px;
            line-height: 30px;
            position: fixed;
            bottom: 0px;
            display: inline-block;
            z-index: 10000;
            font-size: 14px;
            font-family: 'Arial Narrow';
            background: #fff;
            border-top: 1px solid #d6d6d6;
            border-left: 1px solid #d6d6d6;
            border-right: 1px solid #d6d6d6;
            padding: 10px 10px 10px 10px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px; /*IE 7 AND 8 DO NOT SUPPORT BORDER RADIUS*/
            -moz-box-shadow: 0px 0px 1px #000000;
            -webkit-box-shadow: 0px 0px 1px #000000;
            box-shadow: 0px 0px 1px #000000; /*IE 7 AND 8 DO NOT SUPPORT BLUR PROPERTY OF SHADOWS*/
        }
        #forie ul
        {
            display: inline;
            padding: 0 0 0 0;
        }
        #forie ul li
        {
            display: inline;
            padding: 0 0 0 0;
            width: 80px;
            text-align: center;
            font-size: 14px;
            font-family: 'Arial Narrow';
            padding: 5px 5px 5px 5px;
        }
        #forie ul li img
        {
            width: 36px;
            height: 36px;
        }
        #forie strong
        {
            line-height: 30px;
            font-size: 13px;
            font-weight: bold;
        }
        #forie h3
        {
            color: #0f6db3;
            display: inline;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="hidden" name="proyecto_id" value=" " id="ban_proyecto_id" runat="server" />
    <input type="hidden" name="ban_files" value=" " id="ban_files" runat="server" />
    <input type="hidden" name="post_back" value=" " id="post_back" runat="server" />
    <input type="hidden" name="ban_files" value=" " id="htmlc_s" runat="server" />
    <ul id="nav_page" style="text-align: center;">
        <li><span style="margin-top: 15px; cursor: pointer;" onclick="j('#magazine').turn('previous'); if(parseInt(j('#magazine').turn('page'))==1){j('#magazine').css('margin-left', '-20%');}"
            id="back">
            <img width="24px" src="/Icons/back_turn.png" alt="previous" /></span></li>
        <li>
            <div id="proyecto_header" runat="server">
            </div>
            <div id="div_ir">
                Navegar a la p�gina:
                <input type="text" form="ir" style="width: 20px; height: 10px;" name="pagenumber"
                    id="pagenumber" value="1" /><input type="button" id="ir" name="ir" value="Ir" onclick=" if(document.getElementById('ContentPlaceHolder1_ban_proyecto_id').value != ' ' && document.getElementById('ContentPlaceHolder1_ban_proyecto_id').value != '' && document.getElementById('ContentPlaceHolder1_ban_proyecto_id').value != '0' ){j('#magazine').turn('page',j('#pagenumber').val()); if(parseInt(j('#magazine').turn('page'))>=2){j('#magazine').css('margin-left', 'auto');}else{j('#magazine').css('margin-left', '-20%');}}"
                        style="width: 30px;" /></div>
        </li>
        <li><span style="margin-top: 15px; cursor: pointer;" id="next" onclick="if(document.getElementById('ContentPlaceHolder1_ban_proyecto_id').value != ' ' && document.getElementById('ContentPlaceHolder1_ban_proyecto_id').value != '' && document.getElementById('ContentPlaceHolder1_ban_proyecto_id').value != '0' ){j('#magazine').turn('next'); j('#magazine').css('margin-left', 'auto');}">
            <img src="/Icons/next_turn.png" width="24px;" alt="next" /></span></li>
    </ul>
    <!--[if IE]>
    <div id="forie">
        <h3>
            �Atenci�n!</h3>
        <strong>Para lograr un mejor desempe�o en el uso adecuado de esta aplicaci�n es recomendable
            utilizar:</strong>
        <ul>
            <li><a href="https://www.google.com/chrome?hl=es">
                <img src="/Icons/chrome.png" alt="google chrome" />Google Chrome</a> </li>
            <li><a href="http://www.mozilla.org/en-US/firefox/new/">
                <img src="Icons/firefox.png" alt="mozilla firefox" />Mozilla Firefox</a> </li>
            <li><a href="http://www.apple.com/safari/download/">
                <img src="Icons/safari.png" alt="safari" />Safari</a> </li>
            <li><a href="http://www.opera.com/download/">
                <img src="Icons/opera.png" alt="opera" />Opera</a> </li>
        </ul>
        <a id="close_message" style="margin-left: 80px;" href="#">
            <img alt="close button" style="width: 24px; right: 0;" src="/Icons/close_bottom.png" /></a>
    </div>
    <![endif]-->
    <div id='magazine'>
        <div class="page_magazine" id="page1">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 1 de 17</p>
            <img src="/Icons/ProsperidadTodosFooter.png" width="150px" style="position: absolute;
                margin-top: 50px; left: 30px;" alt="Alternate Text" />
            <img src="/Icons/MENHeader.png" width="150px" style="position: absolute; right: 30px;
                margin-top: 50px;" alt="Alternate Text" />
            <hgroup style="text-align: center; font-weight: bold; margin-top: 100px;">
                <h4>
                    REP�BLICA DE COLOMBIA</h4>
                <h4>
                    MINISTERIO DE EDUCACI�N NACIONAL</h4>
                <h5 style="margin-top: 30px;">
                    Vice ministerio de educaci�n preescolar b�sica y media</h5>
                <h5>
                    Direcci�n de calidad para la educaci�n preescolar, b�sica y media</h5>
                <h5>
                    Subdirecci�n fomento de competencias</h5>
                <h5>
                    Programa de competencias ciudadanas</h5>
                <h1 style="color: #005EA7; margin: 50px auto; width: 80%; text-align: center;">
                    Bienvenidos al Banco de Proyectos de la Subdirecci�n de Fomento y Competencias</h1>
                <h2 style="color: #005EA7; margin: 50px auto; width: 80%; text-align: center;" id="titulo_proyecto"
                    runat="server">
                </h2>
            </hgroup>
            <section style="margin: 0 auto; width: 60%; border: 1px solid #005EA7; text-align: center;">
                <a href="#" onclick="j('#magazine').turn('next'); j('#magazine').css('margin-left','auto'); nuevo=true;">
                    Nuevo Proyecto</a>
                <br />
                <asp:DropDownList ID="cmbproyectos" Style="width: 90%;" runat="server">
                </asp:DropDownList>
                <asp:Button Text="Actualizar Proyecto" ID="btncargar" Width="70%" OnClick="btncargar_Click"
                    runat="server" />
                <asp:Button ID="btnExportarProyecto" Width="70%" Text="Exportar Proyecto" runat="server"
                    OnClick="btnExportarProyecto_Click" />
            </section>
            <p style="width: 100%; text-align: center;">
                Versi�n No. 1.0</p>
        </div>
        <div class="page_magazine" id="basicaproyecto" style="overflow-y: scroll;">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 2 de 17</p>
            <h1>
                INFORMACI�N B�SICA DEL PROYECTO</h1>
            <br />
            * Nombre de Proyecto:
            <textarea id="txtnombreproyecto" runat="server" style="display: block; width: 80%;
                height: 100px;" cols="20" rows="50"></textarea>
            <br />
            * Prop�sito:
            <textarea id="txtproposito" runat="server" style="display: block; width: 80%; height: 100px;"
                cols="20" rows="50"></textarea>
            <br />
            * Finalidad:
            <textarea id="txtfinalidad" runat="server" style="display: block; width: 80%; height: 100px;"
                cols="20" rows="50"></textarea>
            <br />
            Problema Central
            <textarea cols="20" rows="50" id="txtproblema" runat="server" style="display: block;
                width: 80%; height: 100px;"></textarea>
            <asp:Button Text="Almacenar Proyecto" ID="btnalmacenarproyecto" runat="server" OnClick="btnalmacenarproyecto_Click" />
            <h3 style="color: #005EA7; width: 80%;">
                Registro de Proyecto</h3>
            <br />
            *Responsable Funcional:
            <table border="0" cellpadding="0" cellspacing="0" style="width: 80%; margin: 0 auto;">
                <tr>
                    <td>
                        Dependencia:
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="txtdependencia" style="width: 100%;" runat="server" name="name"></textarea>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Responsable (Nombre y Apellido):
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea name="name" style="width: 100%;" id="txtresponsable" runat="server"></textarea>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cargo:
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea name="name" style="width: 100%;" runat="server" id="txtcargo"></textarea>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Fecha Elaboraci�n del proyecto:
                    </td>
                    <td>
                        <input type="text" name="name" id="txtfechaelaboracion" runat="server" value=" " />
                    </td>
                </tr>
            </table>
            <br />
            <h3>
                * Marco de Pol�tica Publica:</h3>
            <br />
            <ul id="" style="list-style: none; width: 80%; margin: 0 auto;">
                <li>Estrategia o programa del Plan Nacional de Desarrollo con la que se relaciona el
                    proyecto:
                    <br />
                    <input type="text" id="txtmpp1" runat="server" style="width: 100%;" name="name" value=" " /></li>
                <li>Estrategia o programa del Plan Sectorial de Educaci�n con la que se relaciona el
                    proyecto:
                    <br />
                    <input type="text" id="txtmpp2" runat="server" style="width: 100%;" name="name" value=" " /></li>
                <li>Objetivo Misional de la Subdirecci�n con el que se relaciona el proyecto:
                    <br />
                    <input type="text" id="txtmpp3" runat="server" style="width: 100%;" name="name" value=" " /></li>
            </ul>
            <br />
            *Justifique brevemente la necesidad del proyecto:
            <br />
            <ul style="list-style: none; width: 80%; margin: 0 auto;">
                <li>
                    <textarea id="txtjustificacion" runat="server" style="width: 500px; height: 100px;"></textarea>
                </li>
            </ul>
            <br />
            <asp:Button ID="btnalmacenarregistro" Text="Almacenar informaci�n para registro de proyecto"
                runat="server" OnClick="btnalmacenarregistro_Click" />
            <br />
            <h3>
                Visualizaci�n para Fuentes de Financiaci�n</h3>
            <div id="fuentesfinanciacion" runat="server" style="height: 300px; width: 100%; overflow-y: scroll;">
            </div>
            <h3 style="color: #005EA7; width: 80%;">
                * Fuentes de Financiaci�n</h3>
            <br />
            <table id="jqgrid_table" style="width: 100%;">
            </table>
            <div id="jqgrid_div" style="width: 100%;">
            </div>
            <br />
        </div>
        <div class="page_magazine" id="page3">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 3 de 17</p>
            <div id="accordion">
                <h3>
                    <a href="#">Introducci�n</a></h3>
                <div>
                    El proyecto es la unidad operacional de la planeaci�n del desarrollo que vincula
                    recursos, actividades y productos durante un periodo determinado y con una ubicaci�n
                    definida para la resoluci�n de problemas o necesidades sentidas de la poblaci�n.
                    Un proyecto supone la b�squeda de una alternativa viable al planteamiento de un
                    objetivo que est� concebido para resolver un problema o necesidad y que para ello
                    requiere la producci�n de bienes o servicios, de tal suerte que, una vez tomada
                    la decisi�n de llevar a cabo un proyecto, sea necesaria la realizaci�n de una serie
                    de actividades previstas que conllevar�n al logro de los objetivos propuestos para
                    el proyecto (www.dnp.gov.co) Para tal efecto y de conformidad con los ejercicios
                    adelantados previamente con los grupos de trabajo del Programa de Competencias del
                    Ministerio de Educaci�n Nacional se describen los pasos adelantados para el cumplimento
                    de los objetivos del programa as� como de cada una de sus l�neas de acci�n.
                    <br />
                    <br />
                    <ol>
                        <li>IDENTIFICACI�N </li>
                        <li>DISE�O Y FORMULACI�N</li>
                        <li>EJECUCI�N Y SEGUIMIENTO</li>
                        <li>EVALUACI�N POSTERIOR</li>
                    </ol>
                </div>
                <h3>
                    <a href="#">Ciclo del Proyecto</a></h3>
                <div>
                    El proyecto es la unidad operacional de la planeaci�n del desarrollo que vincula
                    recursos, actividades y productos durante un periodo determinado y con una ubicaci�n
                    definida para la resoluci�n de problemas o necesidades sentidas de la poblaci�n.
                    Un proyecto supone la b�squeda de una alternativa viable al planteamiento de un
                    objetivo que est� concebido para resolver un problema o necesidad y que para ello
                    requiere la producci�n de bienes o servicios , de tal suerte que, una vez tomada
                    la decisi�n de llevar a cabo un proyecto, sea necesaria la realizaci�n de una serie
                    de actividades previstas que conllevar�n al logro de los objetivos propuestos para
                    el proyecto (www.dnp.gov.co)
                    <br />
                    <br />
                    <a href="/Icons/cicloproyecto.png">
                        <img src="/Icons/cicloproyecto.png" width="100%" /></a>
                </div>
                <h3>
                    <a href="#">Identificaci�n</a></h3>
                <div>
                    Tambi�n conocida como pre inversi�n, comporta la primera etapa de formulaci�n del
                    proyecto y tiene por objetivo el acopio y preparaci�n de informaci�n suficiente
                    y pertinente (en ocasiones organizada en estudios t�cnicos, econ�micos, financieros,
                    legales y de mercado) para determinar de forma preliminar la posibilidad real de
                    resolver un problema o satisfacer una necesidad as� como reducir la incertidumbre
                    en el logro de los objetivos de dicha empresa. AN�LISIS DE ACTORES PARTICIPANTES
                    Consiste en identificar las personas, grupos, entidades o instituciones p�blicas
                    o privadas que de alguna forma se relacionan con el proyecto. Debe incorporar los
                    intereses, expectativas representaciones y dem�s de dichos actores y que pueden
                    resultar de importancia para el proyecto: �C�mo elaborar el an�lisis de la participaci�n?
                    Identifique todos aquellos actores relacionados con el proyecto y que se pudieran
                    verse beneficiados o incluso afectados por la ejecuci�n del mismo. Puede categorizarlos
                    seg�n su nivel o �mbito (Nacional, regional, local etc.) Puede categorizarlos tambi�n
                    seg�n sean afectados, beneficiados, cooperantes, oponentes, o perjudicados. De las
                    anteriores categorizaciones proceda a determinar c�mo pueden ser incorporados en
                    el desarrollo del proyecto. Matriz de actores participantes.
                </div>
                <h3>
                    <a href="#">- An�lisis de actores</a></h3>
                <div>
                    Identificaci�n del problema o necesidad: Como ya se hab�a expuesto, un proyecto
                    supone la b�squeda de una alternativa viable al planteamiento de un objetivo que
                    est� concebido para resolver un problema o necesidad y que para ello requiere la
                    producci�n de bienes o servicios , de tal suerte que, una vez tomada la decisi�n
                    de llevar a cabo un proyecto, sea necesaria la realizaci�n de una serie de actividades
                    previstas que conllevar�n al logro de los objetivos propuestos para el proyecto
                    Es un conjunto de t�cnicas para:
                    <br />
                    <br />
                    <ul>
                        <li>Analizar la situaci�n en relaci�n con un problema</li>
                        <li>Identificar los problemas principales de este contexto</li>
                        <li>Visualizar las relaciones de causa efecto en el �rbol de problemas &#8226; Definir
                            el problema central de la situaci�n</li>
                    </ul>
                    <br />
                    <br />
                    <h3>
                        EL �RBOL DE PROBLEMAS
                    </h3>
                    <p>
                        Es una t�cnica que se emplea para identificar una situaci�n problem�tica la cual
                        se intenta solucionar mediante la intervenci�n del proyecto utilizando una relaci�n
                        de tipo causa-efecto. El identificar de forma clara la situaci�n problem�tica no
                        siempre es un ejercicio f�cil, suele pasar que, al identificar un problema emergen
                        muchos otros asociados algunos de los cu�les se nos pueden presentar como causas,
                        o efectos del mismo o incluso hacer dudar sobre si, el problema inicialmente considerado
                        esta correctamente formulado.
                    </p>
                    <br />
                    </h3> �C�mo realizar un �rbol de problemas?</h3>
                    <p>
                        Se recomienda realizar las siguientes tareas:</p>
                    <ol>
                        <li>Convoque a los miembros de su grupo de trabajo o personas de la comunidad interesadas.</li>
                        <li>Describa de forma general el objetivo del proceso y subraye en la necesidad de identificar
                            de forma concertada el problema a resolver </li>
                        <li>Entregue a cada uno de ellos un paquete de tarjetas y solicite que escriban en ellas
                            los problemas de su comunidad, entidad y organizaci�n solicite guardar especial
                            cuidado de: a. Formular el problema como una situaci�n negativa. b. Utilizar una
                            oraci�n corta con palabras que sean, claras, simples y concretas. c. Identificar
                            �nicamente los problemas existentes. Descarte los posibles o potenciales. </li>
                        <li>�C�mo elaborar el �rbol?: Dibuje el tronco de un �rbol para representar su problema
                            central. A�ada ra�ces y rad�culas para representar las causas directas e indirectas,
                            y ramas y ramitas para representar los efectos (o implicaciones) directos e indirectos
                            de su problema central (ver gr�fico 03) </li>
                        <li>Con la ayuda de un facilitador, as� como de todos los participantes ubique las tarjetas
                            comience seg�n sean causas directas, indirectas, efectos directos o indirectos
                        </li>
                        <li>En este nivel puede hacer uso de una matriz de Vester para la calificaci�n de los
                            diferentes causas</li>
                    </ol>
                </div>
            </div>
        </div>
        <div class="page_magazine" id="page4">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 4 de 17</p>
            <h1>
                IDENTIFICACI�N</h1>
            <p>
                Tambi�n conocida como pre inversi�n, comporta la primera etapa de formulaci�n del
                proyecto y tiene por objetivo el acopio y preparaci�n de informaci�n suficiente
                y pertinente (en ocasiones organizada en estudios t�cnicos, econ�micos, financieros,
                legales y de mercado) para determinar de forma preliminar la posibilidad real de
                resolver un problema o satisfacer una necesidad as� como reducir la incertidumbre
                en el logro de los objetivos de dicha empresa.
            </p>
            <br />
            <h3>
                AN�LISIS DE ACTORES PARTICIPANTES</h3>
            <p>
                Consiste en identificar las personas, grupos, entidades o instituciones p�blicas
                o privadas que de alguna forma se relacionan con el proyecto. Debe incorporar los
                intereses, expectativas representaciones y dem�s de dichos actores y que pueden
                resultar de importancia para el proyecto:<br />
                �C�mo elaborar el an�lisis de la participaci�n?<br />
                Identifique todos aquellos actores relacionados con el proyecto y que se pudieran
                verse beneficiados o incluso afectados por la ejecuci�n del mismo.
            </p>
            <p>
                Puede categorizarlos seg�n su nivel o �mbito (Nacional, regional, local etc.) Puede
                categorizarlos tambi�n seg�n sean afectados, beneficiados, cooperantes, oponentes,
                o perjudicados. De las anteriores categorizaciones proceda a determinar c�mo pueden
                ser incorporados en el desarrollo del proyecto.
            </p>
            <br />
            <strong><em>Matriz de actores participantes. Tabla 01 </em></strong>
            <br />
            <div id="matrizactores_div" style="width: 100%; height: 800px; overflow-x: scroll;
                overflow-y: scroll;" runat="server">
                <table class="table_class" border="1" cellspacing="0" cellpadding="0" style="border: #000;">
                    <tr>
                        <td width="121" valign="top">
                            <p>
                                GRUPOS
                            </p>
                        </td>
                        <td width="104" valign="top">
                            <p>
                                INTERESES</p>
                        </td>
                        <td width="173" valign="top">
                            <p>
                                PROBLEMA PERCIBIDO</p>
                        </td>
                        <td width="184" valign="top">
                            <p>
                                RECURSOS Y MANDATOS</p>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="page_magazine" id="page5">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 5 de 17</p>
            <h1>
                IDENTIFICACI�N</h1>
            <h3>
                ANALISIS DE ACTORES PARTICIPANTES</h3>
            <table id="jqgrid_matriz_identificacion_t">
            </table>
            <div id="jqgrid_matriz_identificacion_d">
            </div>
            <!-- Hasta n registros de la tabla actores_participantes -->
        </div>
        <div class="page_magazine" id="page6">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 6 de 17</p>
            <h1>
                DISE�O Y FORMULACI�N</h1>
            <p>
                Identificaci�n del problema o necesidad:<br />
                Como ya se hab�a expuesto, un proyecto supone la b�squeda de una alternativa viable
                al planteamiento de un objetivo que est� concebido para resolver un problema o necesidad
                y que para ello requiere la producci�n de bienes o servicios , de tal suerte que,
                una vez tomada la decisi�n de llevar a cabo un proyecto, sea necesaria la realizaci�n
                de una serie de actividades previstas que conllevar�n al logro de los objetivos
                propuestos para el proyecto<br />
                Es un conjunto de t�cnicas para:</p>
            <ul>
                <li>Analizar la situaci�n en relaci�n con un problema</li>
                <li>Identificar los problemas principales de este contexto</li>
                <li>Visualizar las relaciones de causa efecto en el �rbol de problemas</li>
                <li>Definir el problema central de la situaci�n</li>
            </ul>
            <br />
            <h3>
                EL �RBOL DE PROBLEMAS</h3>
            <a href="/Icons/arbol_problemas_ayuda.png" target="_blank" style="width: 85%; margin: 0 auto;">
                <img src="/Icons/arbol_problemas_ayuda.png" style="left: 50px; clear: both;" width="50%"
                    alt="ayuda arbol problemas" /></a>
            <p>
                Es una t�cnica que se emplea para identificar una situaci�n problem�tica la cual
                se intenta solucionar mediante la intervenci�n del proyecto utilizando una relaci�n
                de tipo causa-efecto.
                <br />
                El identificar de forma clara la situaci�n problem�tica no siempre es un ejercicio
                f�cil, suele pasar que, al identificar un problema emergen muchos otros asociados
                algunos de los cu�les se nos pueden presentar como causas, o efectos del mismo o
                incluso hacer dudar sobre si, el problema inicialmente considerado esta correctamente
                formulado.
            </p>
            <br />
            <h3>
                �C�mo realizar un �rbol de problemas?</h3>
            <p>
                Se recomienda realizar las siguientes tareas</p>
            <ul>
                <li>Convoque a los miembros de su grupo de trabajo o personas de la comunidad interesadas</li>
                <li>Describa de forma general el objetivo del proceso y subraye en la necesidad de identificar
                    de forma concertada el problema a resolver</li>
                <li>Entregue a cada uno de ellos un paquete de tarjetas y solicite que escriban en ellas
                    los problemas de su comunidad, entidad y organizaci�n solicite guardar especial
                    cuidado de:</li>
                <li>Formular el problema como una situaci�n negativa.</li>
                <li>Utilizar una oraci�n corta con palabras que sean, claras, simples y concretas.</li>
                <li>Identificar �nicamente los problemas existentes. Descarte los posibles o potenciales.</li>
                <li>�C�mo elaborar el �rbol?:
                    <p>
                        Dibuje el tronco de un �rbol para representar su problema central. A�ada ra�ces
                        y rad�culas para representar las causas directas e indirectas, y ramas y ramitas
                        para representar los efectos (o implicaciones) directos e indirectos de su problema
                        central (ver gr�fico 03)</p>
                </li>
            </ul>
            <ul>
                <li>Con la ayuda de un facilitador, as� como de todos los participantes ubique las tarjetas
                    comience seg�n sean causas directas, indirectas, efectos directos o indirectos
                </li>
            </ul>
            <p>
                En este nivel puede hacer uso de una matriz de Vester para la calificaci�n de los
                diferentes causas o efectos (para un ejercicio de priorizaci�n m�s complejo se recomienda
                utilizar la matriz de vester)</p>
            <br />
        </div>
        <div class="page_magazine" id="page7">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 7 de 17</p>
            <h1>
                DISE�O Y FORMULACI�N</h1>
            <h1>
                ARBOL DE PROBLEMAS</h1>
            <br />
            <br />
            <section id="org" runat="server" style="overflow-x: scroll;">
            </section>
            <br />
            <a id="refreshOrganigrama" href="" target="_blank">Visualizar Pantalla Completa</a>
            <br />
            <iframe id="if_c_e" runat="server" src="" width="100%" height="900px"></iframe>
        </div>
        <div class="page_magazine" id="page8">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 8 de 17</p>
            <h1>
                AN�LISIS DE OBJETIVOS</h1>
            <p>
                Representa la situaci�n futura alcanzada mediante la soluci�n de los problemas previamente
                identificados. Se logra mediante la formulaci�n (Redacci�n) de las condiciones negativas
                del �rbol de problemas en forma de condiciones positivas que son deseadas y realizables
                en la pr�ctica.</p>
            <p>
                Se elabora a partir del �rbol de problemas facilitando as� examinar de forma gr�fica
                la relaci�n entre medios y fines. Suele suceder que, en desarrollo del ejercicio
                se identifica que algunos de los problemas, as� como sus causas y consecuencia quedaron
                mal planteados o redactados por lo que conviene modificar las frases existentes,
                a�adir nuevas frases en el contexto de las relaciones &#8220;medios-fines&#8221;,
                eliminar los objetivos &#8211; problemas mal planteados o no viables de soluci�n.</p>
            <br />
            <a href="/Icons/arbolobjetivos.png" style="width: 100%; margin: 0 auto;">
                <img style="margin: 0 auto;" src="/Icons/arbolobjetivos.png" alt="arbolobjetivos" /></a>
        </div>
        <div class="page_magazine" id="page9">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 9 de 17</p>
            <h1>
                AN�LISIS DE OBJETIVOS
            </h1>
            <section id="org_objetivos" runat="server" style="overflow-x: scroll;">
            </section>
            <br />
            <a id="refreshOrganigrama_obj" href="" target="_blank">Visualizar Pantalla Completa</a>
            <iframe id="if_objetivos_causas" runat="server" src="" width="100%" height="900px">
            </iframe>
        </div>
        <div class="page_magazine" id="page10">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 10 de 17</p>
            <h1>
                <a name="_Toc315687137" id="_Toc315687137">DISE�O Y FORMULACI�N</a></h1>
            <div>
                <h3>
                    El Enfoque de Marco l�gico (MATRIZ DE MARCO L�GICO)</h3>
            </div>
            <p>
                La matriz de marco l�gico es el procedimiento para la organizaci�n y visualizaci�n
                del proyecto facilitando la articulaci�n de forma sistem�tica y l�gica de los objetivos
                y resultados del mismo. De su estructuraci�n se desprende el seguimiento y evaluaci�n
                del proyecto. En t�rminos pr�cticos supone que la inversi�n de determinados <strong>
                    recursos</strong> soportan la realizaci�n de <strong>actividades</strong> que
                permiten a su vez obtener determinaos <strong>productos</strong> que facilitan el
                logro de un prop�sito y el fin.
            </p>
            <br />
            <h3>
                Estructura marco l�gico</h3>
            <p>
                El marco l�gico se representa en una matriz (tabla) 4 X 4. Cuatro columnas por cuatro
                filas. Las columnas incluyen la siguiente informaci�n:</p>
            <br />
            <h3>
                Matriz (cuatro x cuatro) Tabla 02</h3>
            <br />
            <table class="table_class" border="1" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="144" valign="top">
                        <p>
                            FIN</p>
                    </td>
                    <td width="144" valign="top">
                        <p>
                            INDICADOR</p>
                    </td>
                    <td width="144" valign="top">
                        <p>
                            MEDIO DE VERIFICACI�N</p>
                    </td>
                    <td width="144" valign="top">
                        <p>
                            SUPUESTOS</p>
                    </td>
                </tr>
                <tr>
                    <td width="144" valign="top">
                        <p>
                            PROP�SITO (PROCESO)</p>
                    </td>
                    <td width="144" valign="top">
                        <p>
                            &nbsp;</p>
                    </td>
                    <td width="144" valign="top">
                        <p>
                            &nbsp;</p>
                    </td>
                    <td width="144" valign="top">
                        <p>
                            &nbsp;</p>
                    </td>
                </tr>
                <tr>
                    <td width="144" valign="top">
                        <p>
                            COMPONENTES (SUBPROCESO)</p>
                    </td>
                    <td width="144" valign="top">
                        <p>
                            &nbsp;</p>
                    </td>
                    <td width="144" valign="top">
                        <p>
                            &nbsp;</p>
                    </td>
                    <td width="144" valign="top">
                        <p>
                            &nbsp;</p>
                    </td>
                </tr>
                <tr>
                    <td width="144" valign="top">
                        <p>
                            ACTIVIDADES</p>
                    </td>
                    <td width="144" valign="top">
                        <p>
                            &nbsp;</p>
                    </td>
                    <td width="144" valign="top">
                        <p>
                            &nbsp;</p>
                    </td>
                    <td width="144" valign="top">
                        <p>
                            &nbsp;</p>
                    </td>
                </tr>
            </table>
            <br />
            <div id="content_marcologico" style="width: 100%; height: 800px; overflow-y: scroll;"
                runat="server">
            </div>
        </div>
        <div class="page_magazine" id="page11">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 12 de 17</p>
            <h1>
                DISE�O Y FORMULACI�N
            </h1>
            <iframe id="if_marco_logico" runat="server" width="100%" height="1500px"></iframe>
        </div>
        <div>
        </div>
        <div>
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 13 de 17</p>
            <h1>
                PLAN ACCI�N
            </h1>
            <a id="planaccion" target="_blank" href="">Visualizar en pantalla completa</a>
            <div id="planoperativo" runat="server">
            </div>
        </div>
        <div>
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 14 de 17</p>
            <h1>
                EJECUCI�N</h1>
        </div>
        <div>
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 15 de 17</p>
            <h1>
                EJECUCI�N</h1>
            <iframe id="if_valores_indicadores" style="overflow-y: scroll;" runat="server" src=""
                width="100%" height="1500px"></iframe>
        </div>
        <div>
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 16 de 17</p>
            <h1>
                Directorio de proyecto</h1>
        </div>
        <div>
            <p style="font-size: 14px; width: 90%; text-align: right;">
                P�gina 17 de 17</p>
            <h1>
                Directorio de proyecto</h1>
            <br />
            <div style="width: 100%;">
                <span class="button" name="addfiles" onclick="j('#ContentPlaceHolder1_files').trigger('click');">
                    <img id="addfileimg" src="/Icons/addfile.png" width="16px" alt="icon" />
                    Selecci�n de Archivos... </span>
            </div>
            <asp:FileUpload ID="files" Style="width: 0px;" name="files[]" runat="server" onchange="addlist(this);" />
            <output runat="server" id="list">
            </output>
        </div>
    </div>
    <asp:Button Style="width: 0px; background-color: white; border: none;" Text="" runat="server"
        ID="btnuploadfile" OnClick="btnuploadfile_Click" />
    <div id="dialog_proyectos" style="dysplay: none;">
    </div>
    <div class="qtip qtip-stylename">
        <div class="qtip-tip" rel="cornerValue">
        </div>
        <div class="qtip-wrapper">
            <div class="qtip-borderTop">
            </div>
            <div class="qtip-contentWrapper">
                <div class="qtip-title">
                    <div class="qtip-button">
                    </div>
                </div>
                <div class="qtip-content">
                </div>
            </div>
            <div class="qtip-borderBottom">
            </div>
        </div>
    </div>
</asp:Content>
