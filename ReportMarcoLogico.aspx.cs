using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;
using System.Text;
using System.Web.UI.HtmlControls;

namespace ESM
{
    public partial class ReportMarcoLogico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idproyecto = Convert.ToInt32(Session["idproyecto"]);

            getNew_Plan_Operativo(idproyecto);
        }

        void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == "Resultado")
            {
                row.Attributes.CssStyle.Add("font-size", "1em");
                row.Attributes.CssStyle.Add("text-align", "center");
                row.Attributes.CssStyle.Add("color", "#005EA7");
                row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
            }
        }

        protected bool Export()
        {
            try
            {
                //Descargo el objeto tabla de la sesion con nombre "p_c" ó (Plan Operativo)
                HtmlTable objTable = (HtmlTable)Session["p_c"];

                //Instancio un objeto del tipo StringBuilder objsb
                StringBuilder objsb = new StringBuilder();

                //Instancio un objeto del tipo System.IO.StringWriter sw
                System.IO.StringWriter sw = new System.IO.StringWriter(objsb);

                //Instancio un objeto del tipo HtmlTextWriter htw
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                //Instancio un objeto del tipo System.Web.UI.Page
                System.Web.UI.Page pagina = new System.Web.UI.Page();

                //Instancio un objeto del tipo HtmlForm
                var form = new HtmlForm();

                //Asigno valores a propiedades del objeto page instanciado
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();

                //Agrego el formulario instaciado a la coleccionde paginas
                pagina.Controls.Add(form);

                //Agrego el objeto tabla a la coleccion de controles del formulario instanciado anteriormente
                form.Controls.Add(objTable);

                //Realizo el proceso de renderizacion para los elementos agregados a la pagina instanciada
                pagina.RenderControl(htw);

                //Limpio el canal de respuesta para esta peticion
                Response.Clear();

                //Habilito el buffer
                Response.Buffer = true;

                //Asigno el tipo de contenido
                Response.ContentType = "application/vnd.ms-excel";
                //Asigno el nombre del documento a exportar en el header del response
                Response.AddHeader("Content-Disposition", "attachment;filename=Plan_Operativo.xls");
                //Asigno el tipo de charset "UTF-8"
                Response.Charset = "UTF-8";
                //Establesco la configuracion por defecto para la codificacion
                Response.ContentEncoding = Encoding.Default;
                //Realizo el proceso de escritura para el objeto objsb
                Response.Write(objsb.ToString());
                //Finalizo la respuesta
                Response.End();
                return true;
            }
            catch (Exception) { return false; }
        }

        protected void lknExport_Click(object sender, EventArgs e)
        {
            //Metodo utilizado para exportar a formato Excel
            Export();
        }

        protected void rbtndetalle_CheckedChanged(object sender, EventArgs e)
        {
            gvDetalleActividades.Visible = true;
            gvresultados.Visible = false;
        }

        protected void rbtnresumen_CheckedChanged(object sender, EventArgs e)
        {
            gvDetalleActividades.Visible = false;
            gvresultados.Visible = true;
        }
        /// <summary>
        /// Ejecuta el proceso de carga para la nueva estructura de la matriz para Marco Logico
        /// </summary>
        /// <param name="idproyecto">Identificador de proyecto (Id por el cual se ejecut la busqueda y la carga de la matriz)</param>
        protected void getNew_Plan_Operativo(int idproyecto)
        {
            try
            {
                //Instancio un objecto del tipo proyecto
                Cproyecto objCproyecto = new Cproyecto();
                //Instancion un objeto del tipo Efectos
                CEfectos objCProcesos = new CEfectos();
                //Instancio un objeto del tipo HtmlTable
                HtmlTable objtable = new HtmlTable();
                //Agrego estilos personalizados al objeto HtmlTable
                objtable.Attributes.CssStyle.Add("border", "dashed 2px #222");
                objtable.Attributes.CssStyle.Add("width", "95%");

                //Instancio un objeto del tipo HtmlRow
                HtmlTableRow objrowtitle = new HtmlTableRow();

                #region Instancia de celdas para titulo de tabla

                /* Instancio cada una de las celdas que serviran como titulo de la tabla que se genera
                 * de manera dinamica
                 */
                //1
                HtmlTableCell cell_Proceso = new HtmlTableCell();
                //2
                HtmlTableCell cell_Subproceso = new HtmlTableCell();
                //3
                HtmlTableCell cell_Estrategia = new HtmlTableCell();
                //4
                HtmlTableCell cell_Actividad = new HtmlTableCell();
                //5
                HtmlTableCell cell_Indicador = new HtmlTableCell();
                //6
                HtmlTableCell cell_Medios_Verificacion = new HtmlTableCell();
                //7
                HtmlTableCell cell_Supuestos = new HtmlTableCell();
                //8
                HtmlTableCell cell_Responsables = new HtmlTableCell();

                #endregion

                #region Insercion de HTMl para las celdas th

                //1
                cell_Proceso.InnerHtml = "PROCESO";
                //2
                cell_Subproceso.InnerHtml = "SUBPROCESO";
                //3
                cell_Estrategia.InnerHtml = "ESTRATEGIA";
                //4
                cell_Actividad.InnerHtml = "ACTIVIDAD";
                //5
                cell_Indicador.InnerHtml = "INDICADOR";
                //6
                cell_Medios_Verificacion.InnerHtml = "MEDIOS DE VERIFICACIÓN";
                //7
                cell_Supuestos.InnerHtml = "SUPUESTOS";
                //8
                cell_Responsables.InnerHtml = "RESPONSABLES";

                #endregion

                /*Agrego cada una de las celdas a la fila principal (HtmlRow)*/
                #region Adicion de celdas a la primera fila de la tabla

                objrowtitle.Cells.Add(cell_Proceso);
                objrowtitle.Cells.Add(cell_Subproceso);
                objrowtitle.Cells.Add(cell_Estrategia);
                objrowtitle.Cells.Add(cell_Actividad);
                objrowtitle.Cells.Add(cell_Indicador);
                objrowtitle.Cells.Add(cell_Medios_Verificacion);
                objrowtitle.Cells.Add(cell_Supuestos);
                objrowtitle.Cells.Add(cell_Responsables);
                #endregion

                #region Estilos Celdas th


                /*Asigno estilos personalisados a cada una de las celdas */
                cell_Proceso.Attributes.CssStyle.Add("border", "dashed 1px #000");
                cell_Proceso.Attributes.CssStyle.Add("vertical-align", "middle");
                cell_Proceso.Attributes.CssStyle.Add("text-align", "center");

                cell_Subproceso.Attributes.CssStyle.Add("border", "dashed 1px #000");
                cell_Subproceso.Attributes.CssStyle.Add("vertical-align", "middle");
                cell_Subproceso.Attributes.CssStyle.Add("text-align", "center");

                cell_Estrategia.Attributes.CssStyle.Add("border", "dashed 1px #000");
                cell_Estrategia.Attributes.CssStyle.Add("vertical-align", "middle");
                cell_Estrategia.Attributes.CssStyle.Add("text-align", "center");

                cell_Actividad.Attributes.CssStyle.Add("border", "dashed 1px #000");
                cell_Actividad.Attributes.CssStyle.Add("vertical-align", "middle");
                cell_Actividad.Attributes.CssStyle.Add("text-align", "center");

                cell_Indicador.Attributes.CssStyle.Add("border", "dashed 1px #000");
                cell_Indicador.Attributes.CssStyle.Add("vertical-align", "middle");
                cell_Indicador.Attributes.CssStyle.Add("text-align", "center");

                cell_Medios_Verificacion.Attributes.CssStyle.Add("border", "dashed 1px #000");
                cell_Medios_Verificacion.Attributes.CssStyle.Add("vertical-align", "middle");
                cell_Medios_Verificacion.Attributes.CssStyle.Add("text-align", "center");

                cell_Supuestos.Attributes.CssStyle.Add("border", "dashed 1px #000");
                cell_Supuestos.Attributes.CssStyle.Add("vertical-align", "middle");
                cell_Supuestos.Attributes.CssStyle.Add("text-align", "center");

                cell_Responsables.Attributes.CssStyle.Add("border", "dashed 1px #000");
                cell_Responsables.Attributes.CssStyle.Add("vertical-align", "middle");
                cell_Responsables.Attributes.CssStyle.Add("text-align", "center");

                #endregion

                //Agrego el objeto HtmlTableRow al objeto HtmlTable
                objtable.Rows.Add(objrowtitle);

                //Consula No. 1
                //realizo la consulta de procesos por identificador de proyecto (idproyecto)
                IQueryable<Model.Causas_Efecto> col_procesos = objCProcesos.getCount(idproyecto);

                int rowspan_proceso = 1;
                int contador_filas = 0;
                //Iteracion del arreglo generado por la consulta No. 1
                foreach (var item_procesos in col_procesos)
                {
                    //Creo una variable para verificar si el proceso tiene subprocesos asociados
                    bool tiene_subprocesos = true;
                    //Instancio un nuevo objeto del tipo HtmlTableRow
                    HtmlTableRow objrow_procesos = new HtmlTableRow();
                    //Instancio un nuevo objeto del tipo HtmlTableCell
                    HtmlTableCell objcell_name_proceso = new HtmlTableCell();
                    //Agrego el nombre del proceso como html en el objeto objcell_name_proceso instanciado 
                    objcell_name_proceso.InnerHtml = item_procesos.Proceso;

                    //Agrergo estilos personalizados a la columna objcell_name_proceso
                    objcell_name_proceso.Attributes.CssStyle.Add("border", "dashed 2px " + item_procesos.Color);
                    objcell_name_proceso.Attributes.CssStyle.Add("background", "#ccc");
                    objcell_name_proceso.Attributes.CssStyle.Add("vertical-align", "middle");
                    objcell_name_proceso.Attributes.CssStyle.Add("text-align", "center");
                    objcell_name_proceso.Attributes.CssStyle.Add("color", "#005EA7");
                    objcell_name_proceso.Attributes.CssStyle.Add("font-weight", "bold");
                    //Consulta No. 2
                    //realizo la consulta de subprocesos por identificador proceso (item.Id)
                    IQueryable<Model.Subproceso> col_subprocesos = new CSubprocesos().LoadSubprocesos(item_procesos.Id);
                    //valido si la cantidad de subprocesos para el proceso actual
                    if (col_subprocesos.Count() != 0)
                        objcell_name_proceso.Attributes.Add("rowspan", (col_subprocesos.Count() + 1).ToString());
                    else
                        //En caso de no tener subprocesos asigno a la variable tiene_subprocesos el estado false
                        tiene_subprocesos = false;

                    /*En caso de que la variable tenga estado tiene_subprocesos = false
                     * inserto una celda vacia para que el rowspan se adapte normalmente
                     */
                    if (!tiene_subprocesos)
                    {
                        objrow_procesos.Cells.Add(objcell_name_proceso);
                        //Instancio un objeto del tipo HtmlTableCell
                        HtmlTableCell objcell_name_subproceso_null = new HtmlTableCell();
                        //Agrego a los controles de la fila objrow_procesos  
                        objrow_procesos.Cells.Add(objcell_name_subproceso_null);

                    }
                    else
                        //Agrego a la fila objrow_procesos el control objcell_name_proceso
                        objrow_procesos.Cells.Add(objcell_name_proceso);

                    //Agrego a la tabla la fila objrow_procesos
                    objtable.Rows.Add(objrow_procesos);

                    //Autoincremento para contedo de row span
                    rowspan_proceso++;

                    /*Iteracion del objeto que almacena el resultado para los
                     * subprocesos
                     */
                    foreach (var item_subprocesos in col_subprocesos)
                    {
                        //Instancio una variable que almacena el estado de Cantidad de estrategias en el subproceso
                        bool tiene_estrategias = true;
                        //Instacio un oibjeto del tipo HtmlTableRow
                        HtmlTableRow objrow_subprocesos = new HtmlTableRow();
                        //Instacio un oibjeto del tipo HtmlTableCell
                        HtmlTableCell objcell_name_proceso_null = new HtmlTableCell();
                        //Instacio un oibjeto del tipo HtmlTableCell
                        HtmlTableCell objcell_name_subproceso = new HtmlTableCell();
                        /* Inserto como html el nombre del subpropceso perteneciente
                         * al item actualmente iterado*/
                        objcell_name_subproceso.InnerHtml = item_subprocesos.Subproceso1;
                        //Almaceno el resultado de los resultados o estrategias en el subproceso actualmente iterado
                        IQueryable<Model.Resultados_Proyecto> col_estrategias = new CResultados_proyecto().LoadResultados(item_subprocesos.Id);
                        //Variable que almacena la cantidad de veces que se ralizara el proceso de span en la fila
                        int rowspan_subproceso = 1;
                        //Realizo un preconteo de la cantidad de estrategias asignadas al subproceso actual
                        foreach (var item_contador in col_estrategias)
                        {
                            rowspan_subproceso = rowspan_subproceso + item_contador.Actividades.Count();
                        }

                        if (col_estrategias.Count() != 0)
                            objcell_name_subproceso.Attributes.Add("rowspan", (col_estrategias.Count() + rowspan_subproceso).ToString());
                        else
                            tiene_estrategias = false;

                        if (!tiene_estrategias)
                        {
                            objrow_subprocesos.Cells.Add(objcell_name_subproceso);
                            HtmlTableCell objcell_name_estrategia_null_null = new HtmlTableCell();
                        }
                        else
                            objrow_subprocesos.Cells.Add(objcell_name_subproceso);

                        objcell_name_subproceso.Attributes.CssStyle.Add("border", "dashed 1px " + item_procesos.Color);
                        objcell_name_subproceso.Attributes.CssStyle.Add("vertical-align", "middle");
                        objcell_name_subproceso.Attributes.CssStyle.Add("text-align", "center");

                        objtable.Rows.Add(objrow_subprocesos);

                        foreach (var item_estrategias in col_estrategias)
                        {
                            bool tiene_actividades = true;

                            HtmlTableRow objrow_estrategias = new HtmlTableRow();

                            HtmlTableCell objcell_name_proceso_null_null = new HtmlTableCell();
                            HtmlTableCell objcell_name_subproceso_null = new HtmlTableCell();
                            HtmlTableCell objcell_name_estrategia = new HtmlTableCell();

                            objcell_name_estrategia.InnerHtml = item_estrategias.Resultado;

                            rowspan_proceso++;

                            IQueryable<Model.Actividade> col_actividades = new CActividades().getActividades(item_estrategias.Id);

                            objcell_name_estrategia.Attributes.CssStyle.Add("border", " dashed 1px " + item_procesos.Color);
                            objcell_name_estrategia.Attributes.CssStyle.Add("vertical-align", "middle");
                            objcell_name_estrategia.Attributes.CssStyle.Add("text-align", "center");

                            if (col_actividades.Count() != 0)
                                objcell_name_estrategia.Attributes.Add("rowspan", (col_actividades.Count() + 1).ToString());
                            else
                                tiene_actividades = false;

                            if (!tiene_actividades)
                            {
                                objrow_estrategias.Cells.Add(objcell_name_estrategia);
                            }
                            else
                            {
                                HtmlTableCell objcell_name_estrategia_null_null_null = new HtmlTableCell();
                                objrow_estrategias.Cells.Add(objcell_name_estrategia);
                            }

                            objtable.Rows.Add(objrow_estrategias);

                            foreach (var item_actividades in col_actividades)
                            {
                                HtmlTableRow objrow_actividades = new HtmlTableRow();

                                HtmlTableCell objcell_name_proceso_null_null_null = new HtmlTableCell();
                                HtmlTableCell objcell_name_subproceso_null_null = new HtmlTableCell();
                                HtmlTableCell objcell_name_estrategia_null = new HtmlTableCell();
                                HtmlTableCell objcell_name_actividad = new HtmlTableCell();

                                objcell_name_actividad.InnerHtml = item_actividades.Actividad;

                                objcell_name_actividad.Attributes.CssStyle.Add("border", " dashed 1px " + item_procesos.Color);
                                objcell_name_actividad.Attributes.CssStyle.Add("vertical-align", "middle");
                                objcell_name_actividad.Attributes.CssStyle.Add("text-align", "center");

                                objrow_actividades.Cells.Add(objcell_name_actividad);

                                HtmlTableCell objcell_Indicadores = new HtmlTableCell();
                                HtmlTableCell objcell_medios_verificacion = new HtmlTableCell();
                                HtmlTableCell objcell_supuestos = new HtmlTableCell();
                                HtmlTableCell objcell_responsables = new HtmlTableCell();

                                string medios_verificacion = "";
                                string supuestos = "";
                                string responsables = "";
                                string indicadores = "";

                                foreach (var item_medios in item_actividades.Actividades_Medios)
                                {
                                    medios_verificacion = medios_verificacion + ", " + item_medios.Medios_de_verificacion.Medio_de_verificacion;
                                }

                                foreach (var item_supuestos in item_actividades.Actividades_Supuestos)
                                {
                                    supuestos = supuestos + ", " + item_supuestos.Supuesto.supuesto1;
                                }

                                foreach (var item_responsables in item_actividades.Actividades_Responsables)
                                {
                                    responsables = responsables + ", " + item_responsables.Usuario.Nombre;
                                }

                                foreach (var item_indicadores in item_actividades.Indicadores)
                                {
                                    indicadores = indicadores + ", " + item_indicadores.Indicador;
                                }

                                if (supuestos.Length != 0)
                                    supuestos = supuestos.Trim(',');

                                if (medios_verificacion.Length != 0)
                                    medios_verificacion = medios_verificacion.Trim(',');

                                if (responsables.Length != 0)
                                    responsables = responsables.Trim(',');

                                if (indicadores.Length != 0)
                                    indicadores = indicadores.Trim(',');

                                objcell_Indicadores.InnerHtml = indicadores;
                                objcell_Indicadores.Attributes.CssStyle.Add("border", " dashed 1px " + item_procesos.Color);
                                objcell_Indicadores.Attributes.CssStyle.Add("vertical-align", "middle");
                                objcell_Indicadores.Attributes.CssStyle.Add("text-align", "center");

                                objcell_medios_verificacion.InnerHtml = medios_verificacion;
                                objcell_medios_verificacion.Attributes.CssStyle.Add("border", " dashed 1px " + item_procesos.Color);
                                objcell_medios_verificacion.Attributes.CssStyle.Add("vertical-align", "middle");
                                objcell_medios_verificacion.Attributes.CssStyle.Add("text-align", "center");

                                objcell_supuestos.InnerHtml = supuestos;
                                objcell_supuestos.Attributes.CssStyle.Add("border", " dashed 1px " + item_procesos.Color);
                                objcell_supuestos.Attributes.CssStyle.Add("vertical-align", "middle");
                                objcell_supuestos.Attributes.CssStyle.Add("text-align", "center");

                                objcell_responsables.InnerHtml = responsables;
                                objcell_responsables.Attributes.CssStyle.Add("border", " dashed 1px " + item_procesos.Color);
                                objcell_responsables.Attributes.CssStyle.Add("vertical-align", "middle");
                                objcell_responsables.Attributes.CssStyle.Add("text-align", "center");

                                objrow_actividades.Cells.Add(objcell_Indicadores);
                                objrow_actividades.Cells.Add(objcell_medios_verificacion);
                                objrow_actividades.Cells.Add(objcell_supuestos);
                                objrow_actividades.Cells.Add(objcell_responsables);

                                objtable.Rows.Add(objrow_actividades);

                                rowspan_proceso++;
                            }
                        }

                    }

                    objtable.Rows[contador_filas + 1].Cells[0].Attributes.Add("rowspan", (rowspan_proceso + 1).ToString());
                }

                matriz.Controls.Add(objtable);

                Session.Add("p_c", objtable);
            }
            catch (Exception) { }

        }

        ~ReportMarcoLogico()
        {
            Session.Remove("p_c");
        }
    }
}