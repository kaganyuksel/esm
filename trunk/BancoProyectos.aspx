<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BancoProyectos.aspx.cs" Inherits="ESM.BancoProyectos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ayuda_marco
        {
            width: 95%;
            height: 700px;
            border: 3px solid #F7F7F7;
            background: #F2F2F2;
            top: 140px;
            right: 5%;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            border-radius: 4px; /*IE 7 AND 8 DO NOT SUPPORT BORDER RADIUS*/
            -moz-box-shadow: 0px 0px 3px #8b8b8b;
            -webkit-box-shadow: 0px 0px 3px #8b8b8b;
            box-shadow: 0px 0px 3px #8b8b8b; /*IE 7 AND 8 DO NOT SUPPORT BLUR PROPERTY OF SHADOWS*/
            overflow-y: scroll;
        }
        .ayuda_marco p
        {
            width: 100%;
        }
        #accordion
        {
            text-align: justify;
        }
        #magazine
        {
            width: 90%;
            height: 1500px;
            margin: 50px auto;
        }
        #magazine .turn-page
        {
            width: 400px;
            height: 400px;
            background-color: #fff;
        }
        #page1
        {
            border: 3px solid #D6D6D6;
            padding: 5 5 5 5; /*background-image: url('/Icons/fondo_banco_proyectos.png');
            background-repeat: no-repeat;
            background-position: right;*/
        }
        #responsables
        {
        }
        #responsables li
        {
            text-align: justify;
        }
        #responsables input[type="text"]
        {
            right: 50%;
        }
    </style>
    <script type="text/javascript">

        window.pages = ["portada", "proyecto", "registro", "introduccion", "identificacion_info", "identificacion", "diseño_info", "diseño", "ejecucion_info", "ejecucion"];

        function getUrl() {
            return window.location.href.split("#").shift();
        }

        function getHash() {
            return window.location.hash.slice(1);
        }

        function checkHash(hash) {
            var hash = getHash(), k;
            if ((k = jQuery.inArray(hash, pages)) != -1) {
                $('nav').children().each(
                    function (index, value) {
                        if ($(value).attr('href').indexOf(hash) != -1) $(value).addClass('on');
                        else $(value).removeClass('on');
                    });
                return k + 1;
            }
            return 1;
        }



        var j = jQuery.noConflict();
        j(document).ready(function () {

            $('#magazine').turn({ gradients: true, acceleration: true });

            j('#accordion').accordion({ autoHeight: false,
                navigation: true,
                collapsible: true
            });

            j("#btnalmacenarproyecto").click(function () {
                if (j("#ContentPlaceHolder1_txtnombreproyecto").val() == "" && j("#ContentPlaceHolder1_txtproblema").val() == "")
                    return false;
            });
        });

        function Actualizar() {
            j("#dialog_proyectos").css("display", "block");
            j("#dialog_proyectos").dialog({
                open: true,
                height: 300,
                modal: true,
                close: function () { j("#dialog_proyectos").css("display", "none"); }
            });
            return false;

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script src="Scripts/turn.js" type="text/javascript"></script>
    <div id='magazine'>
        <div id="page1">
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
                <a href="#" onclick="$('#magazine').turn('next');">Nuevo Proyecto</a>
                <br />
                <a id="btnActualizar" href="#" onclick="Actualizar();">Actualizar Proyecto</a>
                </button>
                <asp:Button ID="btnExportarProyecto" Width="70%" Text="Exportar Proyecto" runat="server" />
            </section>
            <p style="width: 100%; text-align: center;">
                Version No. 0.1</p>
        </div>
        <div id="basicaproyecto">
            <h1>
                Información Básica del Proyecto</h1>
            Nombre de Proyecto:
            <input type="text" style="display: block; width: 80%;" runat="server" name="nombreproblema"
                id="txtnombreproyecto" value="" />
            Problema Central
            <textarea cols="20" rows="50" id="txtproblema" runat="server" style="display: block;
                width: 80%;"></textarea>
            <asp:Button Text="Almacenar Proyecto" ID="btnalmacenarproyecto" runat="server" OnClick="btnalmacenarproyecto_Click" />
        </div>
        <div>
            <h1 style="color: #005EA7; margin: 50px auto; width: 80%; text-align: center;">
                REGISTRO DE PROYECTO</h1>
            <section style="margin: 0 auto; width: 90%; border: 1px solid #005EA7;">
                <h1 style="color: #005EA7; margin: 50px auto; width: 80%; text-align: center;">
                    Responsable Funcional</h1>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 80%; margin: 0 auto;">
                    <tr>
                        <td>
                            Dependencia:
                        </td>
                        <td>
                            <input type="text" name="name" value=" " />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Responsable (Nombre y Apellido):
                        </td>
                        <td>
                            <input type="text" name="name" value=" " />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cargo:
                        </td>
                        <td>
                            <input type="text" name="name" value=" " />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Fecha Elaboración del proyecto:
                        </td>
                        <td>
                            <input type="text" name="name" value=" " />
                        </td>
                    </tr>
                </table>
                <br />
            </section>
            <br />
            <section style="margin: 0 auto; width: 90%; border: 1px solid #005EA7; text-align: justify;
                font-size: 14px;">
                <h1 style="color: #005EA7; margin: 50px auto; width: 80%; text-align: center;">
                    Marco de Política Publica</h1>
                <ul id="" style="list-style: none; width: 80%; margin: 0 auto;">
                    <li>Estrategia o programa del Plan Nacional de Desarrollo con la que se relaciona el
                        proyecto:
                        <br />
                        <input type="text" style="width: 100%;" name="name" value=" " /></li>
                    <li>Estrategia o programa del Plan Sectorial de Educación con la que se relaciona el
                        proyecto:
                        <br />
                        <input type="text" style="width: 100%;" name="name" value=" " /></li>
                    <li>Objetivo Misional de la Subdirección con el que se relaciona el proyecto:
                        <br />
                        <input type="text" style="width: 100%;" name="name" value=" " /></li>
                </ul>
                <br />
            </section>
            <br />
            <section style="margin: 0 auto; width: 90%; border: 1px solid #005EA7; text-align: center;
                font-size: 14px;">
                <h1 style="color: #005EA7; margin: 50px auto; width: 80%; text-align: center;">
                    Justifique brevemente la necesidad del proyecto</h1>
                <textarea style="width: 500px; height: 100px;"></textarea>
            </section>
            <br />
            <section style="margin: 0 auto; width: 90%; border: 1px solid #005EA7; text-align: center;
                font-size: 14px;">
                <h1 style="color: #005EA7; margin: 50px auto; width: 80%; text-align: center;">
                    Fuentes de Financiación</h1>
            </section>
            <br />
            <section style="margin: 0 auto; width: 90%; border: 1px solid #005EA7; text-align: center;
                font-size: 14px;">
                <h1 style="color: #005EA7; margin: 50px auto; width: 80%; text-align: center;">
                    Información de Referencia (Anexos)</h1>
                <p style="color: #005EA7; margin: 50px auto; width: 80%; text-align: center;">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </p>
            </section>
            <asp:Button ID="btnalmacenarregistro" Text="Almacenar información" runat="server" />
        </div>
        <div>
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
                        <li>Visualizar las relaciones de causa efecto en el árbol de problemas • Definir el
                            problema central de la situación</li>
                    </ul>
                    <br />
                    <br />
                    EL ÁRBOL DE PROBLEMAS Es una técnica que se emplea para identificar una situación
                    problemática la cual se intenta solucionar mediante la intervención del proyecto
                    utilizando una relación de tipo causa-efecto. El identificar de forma clara la situación
                    problemática no siempre es un ejercicio fácil, suele pasar que, al identificar un
                    problema emergen muchos otros asociados algunos de los cuáles se nos pueden presentar
                    como causas, o efectos del mismo o incluso hacer dudar sobre si, el problema inicialmente
                    considerado esta correctamente formulado.
                    <br />
                    <br />
                    ¿Cómo realizar un árbol de problemas? Se recomienda realizar las siguientes tareas
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
        <div>
            <h1>
                IDENTIFICACIÓN</h1>
            <p>
                También conocida como pre inversión, comporta la primera etapa de formulación del
                proyecto y tiene por objetivo el acopio y preparación de información suficiente
                y pertinente (en ocasiones organizada en estudios técnicos, económicos, financieros,
                legales y de mercado) para determinar de forma preliminar la posibilidad real de
                resolver un problema o satisfacer una necesidad así como reducir la incertidumbre
                en el logro de los objetivos de dicha empresa.
                <br />
                <a name="_Toc315687134" id="_Toc315687134">ANÁLISIS DE ACTORES PARTICIPANTES</a>
                <br />
                Consiste en identificar las personas, grupos, entidades o instituciones públicas
                o privadas que de alguna forma se relacionan con el proyecto. Debe incorporar los
                intereses, expectativas representaciones y demás de dichos actores y que pueden
                resultar de importancia para el proyecto:<br />
                ¿Cómo elaborar el análisis de la participación?<br />
                Identifique todos aquellos actores relacionados con el proyecto y que se pudieran
                verse beneficiados o incluso afectados por la ejecución del mismo.
                <br />
                Puede categorizarlos según su nivel o ámbito (Nacional, regional, local etc.)
                <br />
                Puede categorizarlos también según sean afectados, beneficiados, cooperantes, oponentes,
                o perjudicados.<br />
                De las anteriores categorizaciones proceda a determinar cómo pueden ser incorporados
                en el desarrollo del proyecto.<br />
                <strong><em>Matriz de actores participantes. Tabla 01 </em></strong>
            </p>
            <table border="1" cellspacing="0" cellpadding="0" style="border: #000;">
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
        <div>
            <h1>
                IDENTIFICACIÓN</h1>
            <h3>
                ANALISIS DE ACTORES PARTICIPANTES</h3>
            <strong><em>Matriz de actores participantes. Tabla 01 </em></strong>
            <table id="actores_participantes" border="1" cellspacing="0" cellpadding="0" style="border: #000;">
                <tr style="border: 1px solid #000;">
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
            <!-- Hasta n registros de la tabla actores_participantes -->
        </div>
        <div>
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
            <h4>
                EL ÁRBOL DE PROBLEMAS</h4>
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
                <br />
                ¿Cómo realizar un árbol de problemas? Se recomienda realizar las siguientes tareas</p>
            <ul>
                <li>Convoque a los miembros de su grupo de trabajo o personas de la comunidad interesadas</li>
                <li>Describa de forma general el objetivo del proceso y subraye en la necesidad de identificar
                    de forma concertada el problema a resolver</li>
                <li>Entregue a cada uno de ellos un paquete de tarjetas y solicite que escriban en ellas
                    los problemas de su comunidad, entidad y organización solicite guardar especial
                    cuidado de:</li>
                <ul>
                    <li>Formular el problema como una situación negativa.</li>
                    <li>Utilizar una oración corta con palabras que sean, claras, simples y concretas.</li>
                    <li>Identificar únicamente los problemas existentes. Descarte los posibles o potenciales.</li>
                </ul>
                <li>¿Cómo elaborar el árbol?: </li>
            </ul>
            <p>
                Dibuje el tronco de un árbol para representar su problema central. Añada raíces
                y radículas para representar las causas directas e indirectas, y ramas y ramitas
                para representar los efectos (o implicaciones) directos e indirectos de su problema
                central (ver gráfico 03)</p>
            <ul>
                <li>Con la ayuda de un facilitador, así como de todos los participantes ubique las tarjetas
                    comience según sean causas directas, indirectas, efectos directos o indirectos
                </li>
            </ul>
            En este nivel puede hacer uso de una matriz de Vester para la calificación de los
            diferentes causas o efectos (para un ejercicio de priorización más complejo se recomienda
            utilizar la matriz de vester)
            <br />
            <a href="/Icons/arbol_problemas_ayuda.png" target="_blank">
                <img src="/Icons/arbol_problemas_ayuda.png" style="left: 50px; clear: both;" width="50%"
                    alt="ayuda arbol problemas" /></a>
        </div>
        <div>
            <h1>
                DISEÑO Y FORMULACIÓN</h1>
            <h1>
                ARBOL DE PROBLEMAS</h1>
        </div>
        <div>
            <h1>
                EJECUCIÓN SEGUIMIENTO Y MONITOREO</h1>
        </div>
        <div>
            <h1>
                EVALUACIÓN (POSTERIOR)</h1>
        </div>
        <div>
            <h1>
                REFERENCIAS BIBLIOGRAFICAS</h1>
        </div>
    </div>
    <input type="hidden" name="proyecto_id" value=" " id="ban_proyecto_id" runat="server" />
    <div id="dialog_proyectos" style="dysplay: none;">
        <asp:DropDownList ID="cmbproyectos" Style="width: 90%;" runat="server">
        </asp:DropDownList>
        <asp:Button Text="Cargar" runat="server" ID="btncargarproyecto" 
            onclick="btncargarproyecto_Click" />
    </div>
</asp:Content>
