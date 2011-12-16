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
            //if (Convert.ToBoolean(Request.QueryString["planoperativo"]))
            //{
            //    rbtndetalle.Checked = true;

            //    gvDetalleActividades.Visible = true;
            //    gvresultados.Visible = false;
            //}
            //else
            //{
            //    rbtnresumen.Checked = true;

            //    gvDetalleActividades.Visible = false;
            //    gvresultados.Visible = true;
            //}

            //CEfectos objCEfectos = new CEfectos();

            //lblfinalidad.Text = objCEfectos.getEfectos(Convert.ToInt32(Request.QueryString["idproyecto"]));

            //GridViewHelper helper = new GridViewHelper(this.gvDetalleActividades);
            //helper.RegisterGroup("Resultado", true, true);
            ////helper.RegisterSummary("Actividad", SummaryOperation.Sum, "Resultado");
            //helper.GroupHeader += new GroupEvent(helper_GroupHeader);
            //helper.ApplyGroupSort();

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

        protected bool Export(HtmlTable table, GridView gvpro_Table, GridView gvresul_Table, GridView gvAct_Table)
        {
            try
            {

                StringBuilder objsb = new StringBuilder();
                System.IO.StringWriter sw = new System.IO.StringWriter(objsb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                System.Web.UI.Page pagina = new System.Web.UI.Page();
                var form = new HtmlForm();
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(table);
                form.Controls.Add(gvpro_Table);
                if (rbtndetalle.Checked)
                    form.Controls.Add(gvAct_Table);
                else if (rbtnresumen.Checked)
                    form.Controls.Add(gvresul_Table);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";

                if (rbtndetalle.Checked)
                    Response.AddHeader("Content-Disposition", "attachment;filename=Plan_Operativo.xls");
                else if (rbtnresumen.Checked)
                    Response.AddHeader("Content-Disposition", "attachment;filename=MarcoLogico.xls");

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
            Export(tbFinalidad, gvproposito, gvresultados, gvDetalleActividades);
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

                IQueryable<Model.Causas_Efecto> col_procesos = objCProcesos.getCount(idproyecto);
                int rowspan_proceso = 1;
                int contador_filas = 0;
                foreach (var item_procesos in col_procesos)
                {
                    bool tiene_subprocesos = true;

                    HtmlTableRow objrow_procesos = new HtmlTableRow();

                    HtmlTableCell objcell_name_proceso = new HtmlTableCell();
                    objcell_name_proceso.InnerHtml = item_procesos.Proceso;

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

                        //objrow_subprocesos.Cells.Add(objcell_name_proceso_null);

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
                            //objrow_subprocesos.Cells.Add(objcell_name_estrategia_null_null);
                        }
                        else
                            objrow_subprocesos.Cells.Add(objcell_name_subproceso);

                        objcell_name_subproceso.Attributes.CssStyle.Add("background", item_procesos.Color);
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

                            objcell_name_estrategia.Attributes.CssStyle.Add("background", item_procesos.Color);
                            objcell_name_estrategia.Attributes.CssStyle.Add("vertical-align", "middle");
                            objcell_name_estrategia.Attributes.CssStyle.Add("text-align", "center");

                            if (col_actividades.Count() != 0)
                                objcell_name_estrategia.Attributes.Add("rowspan", (col_actividades.Count() + 1).ToString());
                            else
                                tiene_actividades = false;

                            if (!tiene_actividades)
                            {
                                //HtmlTableCell objcell_name_estrategia_null_null_null = new HtmlTableCell();
                                //objrow_estrategias.Cells.Add(objcell_name_estrategia_null_null_null);
                                objrow_estrategias.Cells.Add(objcell_name_estrategia);
                            }
                            else
                            {
                                HtmlTableCell objcell_name_estrategia_null_null_null = new HtmlTableCell();
                                //objrow_estrategias.Cells.Add(objcell_name_estrategia_null_null_null);
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

                                //objrow_actividades.Cells.Add(objcell_name_proceso_null_null_null);
                                //objrow_actividades.Cells.Add(objcell_name_subproceso_null_null);
                                //objrow_actividades.Cells.Add(objcell_name_estrategia_null);

                                objrow_actividades.Cells.Add(objcell_name_actividad);

                                objtable.Rows.Add(objrow_actividades);

                                rowspan_proceso++;
                            }
                        }

                    }

                    objtable.Rows[contador_filas].Cells[0].Attributes.Add("rowspan", (rowspan_proceso + 1).ToString());
                    objtable.Rows[contador_filas].Cells[0].Attributes.CssStyle.Add("background", item_procesos.Color);
                    objtable.Rows[contador_filas].Cells[0].Attributes.CssStyle.Add("vertical-align", "middle");
                    objtable.Rows[contador_filas].Cells[0].Attributes.CssStyle.Add("text-align", "center");
                }

                matriz.Controls.Add(objtable);
            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }


    }
}