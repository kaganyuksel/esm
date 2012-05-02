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
    <script src="Scripts/jquery.qtip-1.0.0-rc3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var j = jQuery.noConflict();

        j(document).ready(function () {

            j("#ContentPlaceHolder1_org").jOrgChart({
                chartElement: '#chart'
            });

            j("#ContentPlaceHolder1_org_objetivos").jOrgChart({
                chartElement: '#chart_objetivos'
            });
            j('#magazine').turn({ gradients: true, acceleration: true });

            j.extend(j.jgrid.edit, { width: "500" });

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
                caption: "Fuentes de Financiación",
                autowidth: true,
                add: { width: 500 },
                edit: { width: '500px' }

            });
            j("#jqgrid_table").jqGrid('navGrid', "#jqgrid_div", { edit: true, add: true, del: false, width: 500 });
            j("#jqgrid_table").jqGrid('inlineNav', "#jqgrid_div");



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
                caption: "Fuentes de Financiación"
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
            //table:last td.active
            j('.node').qtip({
                content: 'This is an active list element',
                show: 'mouseover',
                hide: 'mouseout'
            })

            setTimeout('tooltip();', 5000);




        });

        setInterval('var numeric_text = j("#ContentPlaceHolder1_if_marco_logico").contents().find("#presupuesto"); j(numeric_text).change(function () { if(isNaN(j(this).val())){j(this).val("0");} });', 3000);

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
                    console.log(result);

                    j("#chart").html("");

                    j("#ContentPlaceHolder1_org").html(result);

                    j("#ContentPlaceHolder1_org").jOrgChart({
                        chartElement: '#chart'
                    });
                },
                error: function (result) {
                    alert("Error " + result.status + ' ' + result.statusText);
                }
            });

            j.ajax({
                url: "ajaxBancoProyectos.aspx?proyecto_id=" + id + "&actualizararbolobjetivos=true",
                async: false,
                success: function (result) {
                    console.log(result);

                    j("#chart_objetivos").html("");

                    j("#ContentPlaceHolder1_org_objetivos").html(result);

                    j("#ContentPlaceHolder1_org_objetivos").jOrgChart({
                        chartElement: '#chart_objetivos'
                    });
                },
                error: function (result) {
                    alert("Error " + result.status + ' ' + result.statusText);
                }
            });

            tooltip();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='magazine'>
        <div class="page_magazine" id="page1">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                Página 1 de 13</p>
            <img src="/Icons/ProsperidadTodosFooter.png" width="150px" style="position: absolute;
                margin-top: 50px; left: 30px;" alt="Alternate Text" />
            <img src="/Icons/MENHeader.png" width="150px" style="position: absolute; right: 30px;
                margin-top: 50px;" alt="Alternate Text" />
            <hgroup style="text-align: center; font-weight: bold; margin-top: 100px;">
                <h4>
                    REPÚBLICA DE COLOMBIA</h4>
                <h4>
                    MINISTERIO DE EDUCACIÓN NACIONAL</h4>
                <h5 style="margin-top: 30px;">
                    Vice ministerio de educación preescolar básica y media</h5>
                <h5>
                    Dirección de calidad para la educación preescolar, básica y media</h5>
                <h5>
                    Subdirección fomento de competencias</h5>
                <h5>
                    Programa de competencias ciudadanas</h5>
                <h1 style="color: #005EA7; margin: 50px auto; width: 80%; text-align: center;">
                    Bienvenidos al Banco de Proyectos de la Subdirección de Fomento y Competencias</h1>
            </hgroup>
            <section style="margin: 0 auto; width: 60%; border: 1px solid #005EA7; text-align: center;">
                <a href="#" onclick="j('#magazine').turn('next');">Nuevo Proyecto</a>
                <br />
                <asp:DropDownList ID="cmbproyectos" Style="width: 90%;" runat="server">
                </asp:DropDownList>
        <%--<a href="#" style="display: none;" onclick="CargarProyecto(j('#ContentPlaceHolder1_cmbproyectos option:selected').val(), 'true'); j('ContentPlaceHolder1_btncargar').trigger('click');">--%>
           <%-- Cargar</a>--%>
            <asp:Button Text="Actualizar Proyecto" ID="btncargar" Width="70%" OnClick="btncargar_Click"
                runat="server" />
                <%--<a id="btnActualizar" style="display: none;" href="#" onclick="Actualizar();">Actualizar
                    Proyecto</a>--%>
                </button>
                <asp:Button ID="btnExportarProyecto" Width="70%" Text="Exportar Proyecto" runat="server" />
            </section>
            <p style="width: 100%; text-align: center;">
                Version No. 0.1</p>
        </div>
        <div class="page_magazine" id="basicaproyecto">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                Página 2 de 13</p>
            <h1>
                INFORMACIÓN BASICA DEL PROYECTO</h1>
            <br />
            * Nombre de Proyecto:
            <textarea id="txtnombreproyecto" runat="server" style="display: block; width: 80%;
                height: 100px;" cols="20" rows="50"></textarea>
            <br />
            * Proposito:
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
                        <input type="text" id="txtdependencia" runat="server" name="name" value="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Responsable (Nombre y Apellido):
                    </td>
                    <td>
                        <input type="text" name="name" id="txtresponsable" runat="server" value=" " />
                    </td>
                </tr>
                <tr>
                    <td>
                        Cargo:
                    </td>
                    <td>
                        <input type="text" name="name" runat="server" id="txtcargo" value=" " />
                    </td>
                </tr>
                <tr>
                    <td>
                        Fecha Elaboración del proyecto:
                    </td>
                    <td>
                        <input type="text" name="name" id="txtfechaelaboracion" runat="server" value=" " />
                    </td>
                </tr>
            </table>
            <br />
            <h3>
                * Marco de Política Publica:</h3>
            <br />
            <ul id="" style="list-style: none; width: 80%; margin: 0 auto;">
                <li>Estrategia o programa del Plan Nacional de Desarrollo con la que se relaciona el
                    proyecto:
                    <br />
                    <input type="text" id="txtmpp1" runat="server" style="width: 100%;" name="name" value=" " /></li>
                <li>Estrategia o programa del Plan Sectorial de Educación con la que se relaciona el
                    proyecto:
                    <br />
                    <input type="text" id="txtmpp2" runat="server" style="width: 100%;" name="name" value=" " /></li>
                <li>Objetivo Misional de la Subdirección con el que se relaciona el proyecto:
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
            <h3 style="color: #005EA7; width: 80%;">
                * Fuentes de Financiación</h3>
            <br />
            <table id="jqgrid_table" style="width: 100%;">
            </table>
            <div id="jqgrid_div" style="width: 100%;">
            </div>
            <br />
            * Información de Referencia (Anexos)
            <br />
            <div>
                <input type="text" name="txtfile" value="txtfile" /><asp:FileUpload ID="FileUpload1"
                    runat="server" /></div>
            <asp:Button ID="btnalmacenarregistro" Text="Almacenar información" runat="server"
                OnClick="btnalmacenarregistro_Click" />
        </div>
        <div class="page_magazine" id="page3">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                Página 3 de 13</p>
            <div id="accordion">
                <h3>
                    <a href="#">Introducción</a></h3>
                <div>
                    El proyecto es la unidad operacional de la planeación del desarrollo que vincula
                    recursos, actividades y productos durante un periodo determinado y con una ubicación
                    definida para la resolución de problemas o necesidades sentidas de la población.
                    Un proyecto supone la búsqueda de una alternativa viable al planteamiento de un
                    objetivo que está concebido para resolver un problema o necesidad y que para ello
                    requiere la producción de bienes o servicios, de tal suerte que, una vez tomada
                    la decisión de llevar a cabo un proyecto, sea necesaria la realización de una serie
                    de actividades previstas que conllevarán al logro de los objetivos propuestos para
                    el proyecto (www.dnp.gov.co) Para tal efecto y de conformidad con los ejercicios
                    adelantados previamente con los grupos de trabajo del Programa de Competencias del
                    Ministerio de Educación Nacional se describen los pasos adelantados para el cumplimento
                    de los objetivos del programa así como de cada una de sus líneas de acción.
                    <br />
                    <br />
                    <ol>
                        <li>IDENTIFICACIÓN </li>
                        <li>DISEÑO Y FORMULACIÓN</li>
                        <li>EJECUCIÓN Y SEGUIMIENTO</li>
                        <li>EVALUACIÓN POSTERIOR</li>
                    </ol>
                </div>
                <h3>
                    <a href="#">Ciclo del Proyecto</a></h3>
                <div>
                    El proyecto es la unidad operacional de la planeación del desarrollo que vincula
                    recursos, actividades y productos durante un periodo determinado y con una ubicación
                    definida para la resolución de problemas o necesidades sentidas de la población.
                    Un proyecto supone la búsqueda de una alternativa viable al planteamiento de un
                    objetivo que está concebido para resolver un problema o necesidad y que para ello
                    requiere la producción de bienes o servicios , de tal suerte que, una vez tomada
                    la decisión de llevar a cabo un proyecto, sea necesaria la realización de una serie
                    de actividades previstas que conllevarán al logro de los objetivos propuestos para
                    el proyecto (www.dnp.gov.co)
                    <br />
                    <br />
                    <a href="/Icons/cicloproyecto.png">
                        <img src="/Icons/cicloproyecto.png" width="100%" /></a>
                </div>
                <h3>
                    <a href="#">Identificación</a></h3>
                <div>
                    También conocida como pre inversión, comporta la primera etapa de formulación del
                    proyecto y tiene por objetivo el acopio y preparación de información suficiente
                    y pertinente (en ocasiones organizada en estudios técnicos, económicos, financieros,
                    legales y de mercado) para determinar de forma preliminar la posibilidad real de
                    resolver un problema o satisfacer una necesidad así como reducir la incertidumbre
                    en el logro de los objetivos de dicha empresa. ANÁLISIS DE ACTORES PARTICIPANTES
                    Consiste en identificar las personas, grupos, entidades o instituciones públicas
                    o privadas que de alguna forma se relacionan con el proyecto. Debe incorporar los
                    intereses, expectativas representaciones y demás de dichos actores y que pueden
                    resultar de importancia para el proyecto: ¿Cómo elaborar el análisis de la participación?
                    Identifique todos aquellos actores relacionados con el proyecto y que se pudieran
                    verse beneficiados o incluso afectados por la ejecución del mismo. Puede categorizarlos
                    según su nivel o ámbito (Nacional, regional, local etc.) Puede categorizarlos también
                    según sean afectados, beneficiados, cooperantes, oponentes, o perjudicados. De las
                    anteriores categorizaciones proceda a determinar cómo pueden ser incorporados en
                    el desarrollo del proyecto. Matriz de actores participantes.
                </div>
                <h3>
                    <a href="#">- Análisis de actores</a></h3>
                <div>
                    Identificación del problema o necesidad: Como ya se había expuesto, un proyecto
                    supone la búsqueda de una alternativa viable al planteamiento de un objetivo que
                    está concebido para resolver un problema o necesidad y que para ello requiere la
                    producción de bienes o servicios , de tal suerte que, una vez tomada la decisión
                    de llevar a cabo un proyecto, sea necesaria la realización de una serie de actividades
                    previstas que conllevarán al logro de los objetivos propuestos para el proyecto
                    Es un conjunto de técnicas para:
                    <br />
                    <br />
                    <ul>
                        <li>Analizar la situación en relación con un problema</li>
                        <li>Identificar los problemas principales de este contexto</li>
                        <li>Visualizar las relaciones de causa efecto en el árbol de problemas &#8226; Definir
                            el problema central de la situación</li>
                    </ul>
                    <br />
                    <br />
                    <h3>
                        EL ÁRBOL DE PROBLEMAS
                    </h3>
                    <p>
                        Es una técnica que se emplea para identificar una situación problemática la cual
                        se intenta solucionar mediante la intervención del proyecto utilizando una relación
                        de tipo causa-efecto. El identificar de forma clara la situación problemática no
                        siempre es un ejercicio fácil, suele pasar que, al identificar un problema emergen
                        muchos otros asociados algunos de los cuáles se nos pueden presentar como causas,
                        o efectos del mismo o incluso hacer dudar sobre si, el problema inicialmente considerado
                        esta correctamente formulado.
                    </p>
                    <br />
                    </h3> ¿Cómo realizar un árbol de problemas?</h3>
                    <p>
                        Se recomienda realizar las siguientes tareas:</p>
                    <ol>
                        <li>Convoque a los miembros de su grupo de trabajo o personas de la comunidad interesadas.</li>
                        <li>Describa de forma general el objetivo del proceso y subraye en la necesidad de identificar
                            de forma concertada el problema a resolver </li>
                        <li>Entregue a cada uno de ellos un paquete de tarjetas y solicite que escriban en ellas
                            los problemas de su comunidad, entidad y organización solicite guardar especial
                            cuidado de: a. Formular el problema como una situación negativa. b. Utilizar una
                            oración corta con palabras que sean, claras, simples y concretas. c. Identificar
                            únicamente los problemas existentes. Descarte los posibles o potenciales. </li>
                        <li>¿Cómo elaborar el árbol?: Dibuje el tronco de un árbol para representar su problema
                            central. Añada raíces y radículas para representar las causas directas e indirectas,
                            y ramas y ramitas para representar los efectos (o implicaciones) directos e indirectos
                            de su problema central (ver gráfico 03) </li>
                        <li>Con la ayuda de un facilitador, así como de todos los participantes ubique las tarjetas
                            comience según sean causas directas, indirectas, efectos directos o indirectos
                        </li>
                        <li>En este nivel puede hacer uso de una matriz de Vester para la calificación de los
                            diferentes causas</li>
                    </ol>
                </div>
            </div>
        </div>
        <div class="page_magazine" id="page4">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                Página 4 de 13</p>
            <h1>
                IDENTIFICACIÓN</h1>
            <p>
                También conocida como pre inversión, comporta la primera etapa de formulación del
                proyecto y tiene por objetivo el acopio y preparación de información suficiente
                y pertinente (en ocasiones organizada en estudios técnicos, económicos, financieros,
                legales y de mercado) para determinar de forma preliminar la posibilidad real de
                resolver un problema o satisfacer una necesidad así como reducir la incertidumbre
                en el logro de los objetivos de dicha empresa.
            </p>
            <br />
            <h3>
                ANÁLISIS DE ACTORES PARTICIPANTES</h3>
            <p>
                Consiste en identificar las personas, grupos, entidades o instituciones públicas
                o privadas que de alguna forma se relacionan con el proyecto. Debe incorporar los
                intereses, expectativas representaciones y demás de dichos actores y que pueden
                resultar de importancia para el proyecto:<br />
                ¿Cómo elaborar el análisis de la participación?<br />
                Identifique todos aquellos actores relacionados con el proyecto y que se pudieran
                verse beneficiados o incluso afectados por la ejecución del mismo.
            </p>
            <p>
                Puede categorizarlos según su nivel o ámbito (Nacional, regional, local etc.) Puede
                categorizarlos también según sean afectados, beneficiados, cooperantes, oponentes,
                o perjudicados. De las anteriores categorizaciones proceda a determinar cómo pueden
                ser incorporados en el desarrollo del proyecto.
            </p>
            <br />
            <strong><em>Matriz de actores participantes. Tabla 01 </em></strong>
            <br />
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
            </table>
        </div>
        <div class="page_magazine" id="page5">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                Página 5 de 13</p>
            <h1>
                IDENTIFICACIÓN</h1>
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
                Página 6 de 13</p>
            <h1>
                DISEÑO Y FORMULACIÓN</h1>
            <p>
                Identificación del problema o necesidad:<br />
                Como ya se había expuesto, un proyecto supone la búsqueda de una alternativa viable
                al planteamiento de un objetivo que está concebido para resolver un problema o necesidad
                y que para ello requiere la producción de bienes o servicios , de tal suerte que,
                una vez tomada la decisión de llevar a cabo un proyecto, sea necesaria la realización
                de una serie de actividades previstas que conllevarán al logro de los objetivos
                propuestos para el proyecto<br />
                Es un conjunto de técnicas para:</p>
            <ul>
                <li>Analizar la situación en relación con un problema</li>
                <li>Identificar los problemas principales de este contexto</li>
                <li>Visualizar las relaciones de causa efecto en el árbol de problemas</li>
                <li>Definir el problema central de la situación</li>
            </ul>
            <br />
            <h3>
                EL ÁRBOL DE PROBLEMAS</h3>
            <a href="/Icons/arbol_problemas_ayuda.png" target="_blank" style="width: 85%; margin: 0 auto;">
                <img src="/Icons/arbol_problemas_ayuda.png" style="left: 50px; clear: both;" width="50%"
                    alt="ayuda arbol problemas" /></a>
            <p>
                Es una técnica que se emplea para identificar una situación problemática la cual
                se intenta solucionar mediante la intervención del proyecto utilizando una relación
                de tipo causa-efecto.
                <br />
                El identificar de forma clara la situación problemática no siempre es un ejercicio
                fácil, suele pasar que, al identificar un problema emergen muchos otros asociados
                algunos de los cuáles se nos pueden presentar como causas, o efectos del mismo o
                incluso hacer dudar sobre si, el problema inicialmente considerado esta correctamente
                formulado.
            </p>
            <br />
            <h3>
                ¿Cómo realizar un árbol de problemas?</h3>
            <p>
                Se recomienda realizar las siguientes tareas</p>
            <ul>
                <li>Convoque a los miembros de su grupo de trabajo o personas de la comunidad interesadas</li>
                <li>Describa de forma general el objetivo del proceso y subraye en la necesidad de identificar
                    de forma concertada el problema a resolver</li>
                <li>Entregue a cada uno de ellos un paquete de tarjetas y solicite que escriban en ellas
                    los problemas de su comunidad, entidad y organización solicite guardar especial
                    cuidado de:</li>
                <li>Formular el problema como una situación negativa.</li>
                <li>Utilizar una oración corta con palabras que sean, claras, simples y concretas.</li>
                <li>Identificar únicamente los problemas existentes. Descarte los posibles o potenciales.</li>
                <li>¿Cómo elaborar el árbol?:
                    <p>
                        Dibuje el tronco de un árbol para representar su problema central. Añada raíces
                        y radículas para representar las causas directas e indirectas, y ramas y ramitas
                        para representar los efectos (o implicaciones) directos e indirectos de su problema
                        central (ver gráfico 03)</p>
                </li>
            </ul>
            <ul>
                <li>Con la ayuda de un facilitador, así como de todos los participantes ubique las tarjetas
                    comience según sean causas directas, indirectas, efectos directos o indirectos
                </li>
            </ul>
            <p>
                En este nivel puede hacer uso de una matriz de Vester para la calificación de los
                diferentes causas o efectos (para un ejercicio de priorización más complejo se recomienda
                utilizar la matriz de vester)</p>
            <br />
        </div>
        <div class="page_magazine" id="page7">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                Página 7 de 13</p>
            <h1>
                DISEÑO Y FORMULACIÓN</h1>
            <h1>
                ARBOL DE PROBLEMAS</h1>
            <br />
            <br />
            <ul id="org" runat="server" style="display: none;">
            </ul>
            <div id="chart" class="jOrgChart">
            </div>
            <br />
            <a href="#" onclick="UpdateArbolProblemas(j('#ContentPlaceHolder1_ban_proyecto_id').val()); j('#if_google').trigger('click');">
                Actualizar Organigrama</a>
            <br />
            <iframe id="if_c_e" runat="server" src="" width="100%" height="500px"></iframe>
        </div>
        <div class="page_magazine" id="page8">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                Página 8 de 13</p>
            <h1>
                ANÁLISIS DE OBJETIVOS</h1>
            <p>
                Representa la situación futura alcanzada mediante la solución de los problemas previamente
                identificados. Se logra mediante la formulación (Redacción) de las condiciones negativas
                del árbol de problemas en forma de condiciones positivas que son deseadas y realizables
                en la práctica.</p>
            <p>
                Se elabora a partir del árbol de problemas facilitando así examinar de forma gráfica
                la relación entre medios y fines. Suele suceder que, en desarrollo del ejercicio
                se identifica que algunos de los problemas, así como sus causas y consecuencia quedaron
                mal planteados o redactados por lo que conviene modificar las frases existentes,
                añadir nuevas frases en el contexto de las relaciones “medios-fines”, eliminar los
                objetivos – problemas mal planteados o no viables de solución.</p>
            <br />
            <a href="/Icons/arbolobjetivos.png" style="width: 100%; margin: 0 auto;">
                <img style="margin: 0 auto;" src="/Icons/arbolobjetivos.png" alt="arbolobjetivos" /></a>
        </div>
        <div class="page_magazine" id="page9">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                Página 9 de 13</p>
            <h1>
                ANÁLISIS DE OBJETIVOS
            </h1>
            <ul id="org_objetivos" runat="server" style="display: none;">
            </ul>
            <div id="chart_objetivos" class="jOrgChart_o">
            </div>
        </div>
        <div class="page_magazine" id="page10">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                Página 10 de 13</p>
            <h1>
                <a name="_Toc315687137" id="_Toc315687137">DISEÑO Y FORMULACIÓN</a></h1>
            <div>
                <h3>
                    El Enfoque de Marco lógico (MATRIZ DE MARCO LÓGICO)</h3>
            </div>
            <p>
                La matriz de marco lógico es el procedimiento para la organización y visualización
                del proyecto facilitando la articulación de forma sistemática y lógica de los objetivos
                y resultados del mismo. De su estructuración se desprende el seguimiento y evaluación
                del proyecto. En términos prácticos supone que la inversión de determinados <strong>
                    recursos</strong> soportan la realización de <strong>actividades</strong> que
                permiten a su vez obtener determinaos <strong>productos</strong> que facilitan el
                logro de un propósito y el fin.
            </p>
            <br />
            <h3>
                Estructura marco lógico</h3>
            <p>
                El marco lógico se representa en una matriz (tabla) 4 X 4. Cuatro columnas por cuatro
                filas. Las columnas incluyen la siguiente información:</p>
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
                            MEDIO DE VERIFICACIÓN</p>
                    </td>
                    <td width="144" valign="top">
                        <p>
                            SUPUESTOS</p>
                    </td>
                </tr>
                <tr>
                    <td width="144" valign="top">
                        <p>
                            PROPÓSITO (PROCESO)</p>
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
        </div>
        <div class="page_magazine" id="page11">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                Página 11 de 13</p>
            <h1>
                DISEÑO Y FORMULACIÓN
            </h1>
            <iframe id="if_marco_logico" runat="server" width="100%" height="1500px"></iframe>
        </div>
        <div class="page_magazine" id="page12">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                Página 12 de 13</p>
            <h1>
                <a name="_Toc315687137" id="A1">DISEÑO Y FORMULACIÓN</a></h1>
            <div>
                <h3>
                    El Enfoque de Marco lógico (MATRIZ DE MARCO LÓGICO)</h3>
            </div>
            <p>
                La matriz de marco lógico es el procedimiento para la organización y visualización
                del proyecto facilitando la articulación de forma sistemática y lógica de los objetivos
                y resultados del mismo. De su estructuración se desprende el seguimiento y evaluación
                del proyecto. En términos prácticos supone que la inversión de determinados <strong>
                    recursos</strong> soportan la realización de <strong>actividades</strong> que
                permiten a su vez obtener determinaos <strong>productos</strong> que facilitan el
                logro de un propósito y el fin.
            </p>
            <br />
            <h3>
                Estructura marco lógico</h3>
            <p>
                El marco lógico se representa en una matriz (tabla) 4 X 4. Cuatro columnas por cuatro
                filas. Las columnas incluyen la siguiente información:</p>
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
                            MEDIO DE VERIFICACIÓN</p>
                    </td>
                    <td width="144" valign="top">
                        <p>
                            SUPUESTOS</p>
                    </td>
                </tr>
                <tr>
                    <td width="144" valign="top">
                        <p>
                            PROPÓSITO (PROCESO)</p>
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
        </div>
        <div class="page_magazine" id="page13">
            <p style="font-size: 14px; width: 90%; text-align: right;">
                Página 13 de 13</p>
            <h1>
                VISUALIZACIÓN PARA EL MARCO LÓGICO
            </h1>
            <iframe id="if_ejecucion" runat="server" width="100%" height="1500px"></iframe>
        </div>
        <%--<div>
            <h1>
                EVALUACIÓN (POSTERIOR)</h1>
        </div>
        <div>
            <h1>
                REFERENCIAS BIBLIOGRAFICAS</h1>
        </div>--%>
        <div>
        </div>
    </div>
    <input type="hidden" name="proyecto_id" value=" " id="ban_proyecto_id" runat="server" />
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
