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
                HtmlTable objTable = (HtmlTable)Session["p_c"];

                StringBuilder objsb = new StringBuilder();
                System.IO.StringWriter sw = new System.IO.StringWriter(objsb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                System.Web.UI.Page pagina = new System.Web.UI.Page();
                var form = new HtmlForm();
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(objTable);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";

                Response.AddHeader("Content-Disposition", "attachment;filename=Plan_Operativo.xls");

                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(objsb.ToString());
                Response.End();
                return true;
            }
            catch (Exception) { return false; }

        }

        protected void lknExport_Click(object sender, EventArgs e)
        {
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

        protected void getNew_Plan_Operativo(int idproyecto)
        {
            try
            {
                Cproyecto objCproyecto = new Cproyecto();

                CEfectos objCProcesos = new CEfectos();

                HtmlTable objtable = new HtmlTable();

                objtable.Attributes.CssStyle.Add("border", "dashed 2px #222");
                objtable.Attributes.CssStyle.Add("width", "95%");

                HtmlTableRow objrowtitle = new HtmlTableRow();

                HtmlTableCell cell_Proceso = new HtmlTableCell();
                HtmlTableCell cell_Subproceso = new HtmlTableCell();
                HtmlTableCell cell_Estrategia = new HtmlTableCell();
                HtmlTableCell cell_Actividad = new HtmlTableCell();
                HtmlTableCell cell_Indicador = new HtmlTableCell();
                HtmlTableCell cell_Medios_Verificacion = new HtmlTableCell();
                HtmlTableCell cell_Supuestos = new HtmlTableCell();
                HtmlTableCell cell_Responsables = new HtmlTableCell();

                cell_Proceso.InnerHtml = "Proceso";
                cell_Subproceso.InnerHtml = "SubProceso";
                cell_Estrategia.InnerHtml = "Estrategia";
                cell_Actividad.InnerHtml = "Actividad";
                cell_Indicador.InnerHtml = "Indicador";
                cell_Medios_Verificacion.InnerHtml = "Medios de Verificación";
                cell_Supuestos.InnerHtml = "Supuestos";
                cell_Responsables.InnerHtml = "Responsables";

                objrowtitle.Cells.Add(cell_Proceso);
                objrowtitle.Cells.Add(cell_Subproceso);
                objrowtitle.Cells.Add(cell_Estrategia);
                objrowtitle.Cells.Add(cell_Actividad);
                objrowtitle.Cells.Add(cell_Indicador);
                objrowtitle.Cells.Add(cell_Medios_Verificacion);
                objrowtitle.Cells.Add(cell_Supuestos);
                objrowtitle.Cells.Add(cell_Responsables);

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

                objtable.Rows.Add(objrowtitle);

                IQueryable<Model.Causas_Efecto> col_procesos = objCProcesos.getCount(idproyecto);

                int rowspan_proceso = 1;
                int contador_filas = 0;

                foreach (var item_procesos in col_procesos)
                {
                    bool tiene_subprocesos = true;

                    HtmlTableRow objrow_procesos = new HtmlTableRow();

                    HtmlTableCell objcell_name_proceso = new HtmlTableCell();
                    objcell_name_proceso.InnerHtml = item_procesos.Proceso;

                    objcell_name_proceso.Attributes.CssStyle.Add("border", "dashed 2px " + item_procesos.Color);
                    objcell_name_proceso.Attributes.CssStyle.Add("background", "#ccc");
                    objcell_name_proceso.Attributes.CssStyle.Add("vertical-align", "middle");
                    objcell_name_proceso.Attributes.CssStyle.Add("text-align", "center");
                    objcell_name_proceso.Attributes.CssStyle.Add("color", "#005EA7");
                    objcell_name_proceso.Attributes.CssStyle.Add("font-weight", "bold");

                    IQueryable<Model.Subproceso> col_subprocesos = new CSubprocesos().LoadSubprocesos(item_procesos.Id);

                    if (col_subprocesos.Count() != 0)
                        objcell_name_proceso.Attributes.Add("rowspan", (col_subprocesos.Count() + 1).ToString());
                    else
                        tiene_subprocesos = false;

                    if (!tiene_subprocesos)
                    {
                        objrow_procesos.Cells.Add(objcell_name_proceso);
                        HtmlTableCell objcell_name_subproceso_null = new HtmlTableCell();
                        objrow_procesos.Cells.Add(objcell_name_subproceso_null);

                    }
                    else
                        objrow_procesos.Cells.Add(objcell_name_proceso);

                    objtable.Rows.Add(objrow_procesos);

                    rowspan_proceso++;

                    foreach (var item_subprocesos in col_subprocesos)
                    {
                        bool tiene_estrategias = true;
                        HtmlTableRow objrow_subprocesos = new HtmlTableRow();

                        HtmlTableCell objcell_name_proceso_null = new HtmlTableCell();
                        HtmlTableCell objcell_name_subproceso = new HtmlTableCell();

                        objcell_name_subproceso.InnerHtml = item_subprocesos.Subproceso1;

                        IQueryable<Model.Resultados_Proyecto> col_estrategias = new CResultados_proyecto().LoadResultados(item_subprocesos.Id);

                        int rowspan_subproceso = 1;

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