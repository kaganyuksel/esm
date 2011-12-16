using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;
using System.Web.UI.HtmlControls;
using System.Text;

namespace ESM
{
    public partial class ArbolProblemas : System.Web.UI.Page
    {
        #region Propiedades Publicas y Privadas

        protected Cproyecto objCpoyecto = new Cproyecto();
        protected CEfectos objCEfectos = new CEfectos();
        protected CSubprocesos objCSubprocesos = new CSubprocesos();
        protected CActividades objCActividades = new CActividades();
        int _idproyecto = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idproyecto"] != null && Convert.ToInt32(Session["Cant_Load"]) == 0)
            {
                hidproyecto.Value = Session["idproyecto"].ToString();
                _idproyecto = Convert.ToInt32(Session["idproyecto"]);

            }
            if (Bandera.Value == "1")
            {
                Bandera.Value = "-1";
            }
            if (!Page.IsPostBack)
            {

            }
        }

        protected void lknAlmacenarP_Click(object sender, EventArgs e)
        {
            #region Insercion de Proyecto

            int idproyecto = objCpoyecto.Add(txtproblema.Text, txtname_project.Text);
            if (idproyecto != 0)
            {
                Session.Add("idproyecto", idproyecto);
                alerthq.Value = "1";
            }
            else
                alerthq.Value = "0";

            #endregion

        }

        protected void lknAlmacenarE_Click(object sender, EventArgs e)
        {
            #region Insercion de Efecto

            int idproyecto = Convert.ToInt32(Session["idproyecto"]);
            bool agroefecto = objCEfectos.Add(txtEfecto1.Text, txtCausa1.Text, idproyecto, mycolor.Value);

            if (agroefecto)
                alerthq.Value = "1";
            else
                alerthq.Value = "0";

            gvEfectos.DataBind();

            Load_Project();
            #endregion

        }

        protected void gvProyectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.Remove("idproyecto");

            GridViewRow objRow = gvProyectos.SelectedRow;

            int idproyecto = Convert.ToInt32(objRow.Cells[1].Text);
            Session.Add("idproyecto", idproyecto);

            txtproblema.Text = objRow.Cells[2].Text;
            txtProposito.Text = objRow.Cells[2].Text;
            txtproblema.ReadOnly = true;

            txtname_project.Text = objCpoyecto.getname(idproyecto);
            txtname_project.ReadOnly = true;
            txtname_project_pro.Text = txtname_project.Text;
            txtname_project_pro.ReadOnly = true;
            txtname_project_estra.Text = txtname_project.Text;
            txtname_project_estra.ReadOnly = true;
            txtnameproject_activ.Text = txtname_project.Text;
            txtnameproject_activ.ReadOnly = true;
            txtname_project_sub.Text = txtname_project.Text;
            txtname_project_sub.ReadOnly = true;

            Load_Project();

            lknAlmacenarP.Enabled = false;

            divefectos.Visible = true;
            divproblema.Visible = true;
            Mod_Name_Project.Visible = true;
            lknAlmacenarP.Enabled = true;
            divNuevo.Visible = false;
            divCargado.Visible = true;
            divproyectos.Visible = false;

            lknAlmacenarP.Enabled = false;
        }

        protected void Load_Project()
        {
            try
            {
                int idproyecto = Convert.ToInt32(Session["idproyecto"]); ;

                _idproyecto = idproyecto;

                CargarProposito(txtproblema.Text);
                load_procesos();
                getCronograma();
                load_subprocesos();
                Load_resultados();
                Load_estrategias();
            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        protected void CargarProposito(string problema)
        {
            try
            {
                string proposito = Cproyecto.CargarProposito(_idproyecto);

                if (proposito != null)
                {
                    txtProposito.Text = proposito;
                    txtproposito_po.Text = proposito;
                    txtProposito_sub.Text = proposito;
                    txtProposito_Estra.Text = proposito;

                }
                else
                {
                    txtProposito.Text = problema;
                    txtproposito_po.Text = problema;
                }

                CargarFinalidad();
            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        protected void btnnuevo_Click(object sender, EventArgs e)
        {
            Session.Remove("idproyecto");
            divefectos.Visible = true;
            divproblema.Visible = true;
            divproyectos.Visible = false;
            lknAlmacenarP.Enabled = true;
            Mod_Name_Project.Visible = true;
            txtproblema.Text = "";
            txtCausa1.Text = "";
            txtEfecto1.Text = "";
            divNuevo.Visible = true;
            divCargado.Visible = false;
            divseleccion.Visible = false;
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            divproyectos.Visible = true;
            divseleccion.Visible = false;
        }

        void AddResultado(object sender, ImageClickEventArgs e)
        {
            Button objbtn = (Button)sender;
        }

        protected void lknAlmacenarProposito_Click(object sender, EventArgs e)
        {
            Cproyecto objCproyecto = new Cproyecto();
            int idproyecto = Convert.ToInt32(hidproyecto.Value);
            objCproyecto.Update(idproyecto, null, txtProposito.Text);

        }

        protected void CargarFinalidad()
        {
            try
            {
                txtFinalidad.Text = objCEfectos.getEfectos(_idproyecto);
                txtfinalidad_po.Text = txtFinalidad.Text;
                txtFinalidad_Sub.Text = txtFinalidad.Text;
                txtfinalidad_estra.Text = txtFinalidad.Text;
            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        protected void lknAlmacenarFinalidad_Click(object sender, EventArgs e)
        {
            objCpoyecto.Update(_idproyecto, null, null, txtFinalidad.Text);
        }

        protected void getCronograma()
        {
            try
            {
                int idproyecto = Convert.ToInt32(Session["idproyecto"]);
                JsGantt Gantt = new JsGantt();
                Gantt.genera_gantt("GanttChartDIV", idproyecto, Page, true);
            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        protected void btnvolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ArbolProblemas.aspx");
        }

        protected bool Export(HtmlTable table)
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
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=MarcoLogico.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(objsb.ToString());
                Response.End();
                return true;
            }
            catch (Exception) { return false; }

        }

        protected void lknexport_gantt_Click(object sender, EventArgs e)
        {
            buildgantt();
        }

        protected void buildgantt()
        {
            var gantt = (from g in new ESM.Model.ESMBDDataContext().gantts
                         where g.Proyecto_id == _idproyecto
                         select g);

            StringBuilder objStringBuilder = new StringBuilder();

            //objStringBuilder.Append("<table> ");

            objStringBuilder.Append("<tr> ");
            objStringBuilder.Append("<th> ");
            objStringBuilder.Append("Actividad ");
            objStringBuilder.Append("</th> ");
            objStringBuilder.Append("</tr> ");
            objStringBuilder.Append("<tr> ");
            objStringBuilder.Append("<th> ");
            objStringBuilder.Append("Indicador ");
            objStringBuilder.Append("</th> ");
            objStringBuilder.Append("<tr> ");
            objStringBuilder.Append("<th> ");
            objStringBuilder.Append("Fecha Inicial ");
            objStringBuilder.Append("</th> ");
            objStringBuilder.Append("</tr> ");
            objStringBuilder.Append("<tr> ");
            objStringBuilder.Append("<th> ");
            objStringBuilder.Append("Fecha Final ");
            objStringBuilder.Append("</th> ");
            objStringBuilder.Append("</tr> ");

            foreach (var item in gantt)
            {
                objStringBuilder.Append("<tr> ");
                objStringBuilder.Append("<td> ");
                objStringBuilder.Append(item.Actividad.ToString());
                objStringBuilder.Append("</td> ");
                objStringBuilder.Append("<td> ");
                objStringBuilder.Append(item.Indicador.ToString());
                objStringBuilder.Append("</td> ");
                objStringBuilder.Append("<td>");
                objStringBuilder.Append(item.fecha_inicial.ToString());
                objStringBuilder.Append("</td> ");
                objStringBuilder.Append("<td>");
                objStringBuilder.Append(item.fecha_final.ToString());
                objStringBuilder.Append("</td> ");
                objStringBuilder.Append("</tr> ");
            }

            //objStringBuilder.Append("</table> ");

            t_gantt.InnerHtml = objStringBuilder.ToString();

            Export(t_gantt);

        }

        /// <summary>
        /// Consulta y realiza el proceso de carga de procesos para el marco logico
        /// asociado a un proyecto
        /// </summary>
        protected void load_procesos()
        {
            try
            {
                IQueryable<Model.Causas_Efecto> objCausas_Efectos = objCEfectos.getCount(_idproyecto);
                int enumeracion = 1;
                foreach (var item in objCausas_Efectos)
                {
                    HtmlTable objHtmlTable = new HtmlTable();

                    objHtmlTable.Width = "80%";

                    HtmlTableRow objRow_Causa = new HtmlTableRow();

                    HtmlTableCell objCell_Causa = new HtmlTableCell();
                    objCell_Causa.InnerHtml = "<label style='color:" + item.Color + ";'>Causa No." + enumeracion.ToString() + ": " + item.Causa + "</label>";

                    objCell_Causa.Attributes.CssStyle.Add("padding-left", "8px");
                    objRow_Causa.Cells.Add(objCell_Causa);
                    objRow_Causa.Attributes.Add("class", "trheader");
                    objCell_Causa.Attributes.CssStyle.Add("-moz-border-radius", "3px");
                    objCell_Causa.Attributes.CssStyle.Add("-webkit-border-radius", "3px");
                    objCell_Causa.Attributes.CssStyle.Add("border-radius", "3px");

                    HtmlTableRow objRow_Proceso = new HtmlTableRow();

                    HtmlTableCell objCell_Proceso = new HtmlTableCell();
                    objCell_Proceso.InnerHtml = "<h3>Proceso</h3><textarea id='txt_area_proceso_id_" + item.Id + "' placeholder='Ingrese el texto para proceso correspondiente.'>" + item.Proceso + "</textarea><br/>";


                    HtmlInputButton objAlmacenar_proceso = new HtmlInputButton();
                    objAlmacenar_proceso.ID = "btn_proceso_almacenar_id" + item.Id.ToString();
                    objAlmacenar_proceso.Attributes.Add("onclick", String.Format("AlmacenarProceso('{0}','{1}','{2}');", item.Id, item.Causa, "txt_area_proceso_id_" + item.Id));
                    objAlmacenar_proceso.Value = "Almacenar proceso";

                    objCell_Proceso.Controls.Add(objAlmacenar_proceso);
                    objRow_Proceso.Cells.Add(objCell_Proceso);

                    objRow_Proceso.Attributes.Add("class", "trgris");

                    objHtmlTable.Rows.Add(objRow_Causa);
                    objHtmlTable.Rows.Add(objRow_Proceso);

                    pnl_procesos.Controls.Add(objHtmlTable);

                    HtmlGenericControl salto_linea = new HtmlGenericControl("br");

                    pnl_procesos.Controls.Add(salto_linea);

                    enumeracion++;
                }
            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        protected void load_subprocesos()
        {
            try
            {
                IQueryable<Model.Causas_Efecto> objCausas_Efecto = objCSubprocesos.getProcesos(_idproyecto);
                int enumeracion = 1;
                foreach (var item in objCausas_Efecto)
                {
                    HtmlTable objHtmlTable = new HtmlTable();

                    objHtmlTable.Width = "80%";

                    HtmlTableRow objRow_Proceso = new HtmlTableRow();

                    HtmlTableCell objCell_Proceso = new HtmlTableCell();
                    objCell_Proceso.InnerHtml = "<label style='color:" + item.Color + ";'>Proceso No. " + enumeracion.ToString() + ": " + item.Proceso + "</label>";

                    objCell_Proceso.Attributes.CssStyle.Add("padding-left", "8px");
                    objRow_Proceso.Cells.Add(objCell_Proceso);
                    objRow_Proceso.Attributes.Add("class", "trheader");
                    objCell_Proceso.Attributes.CssStyle.Add("-moz-border-radius", "3px");
                    objCell_Proceso.Attributes.CssStyle.Add("-webkit-border-radius", "3px");
                    objCell_Proceso.Attributes.CssStyle.Add("border-radius", "3px");

                    HtmlTableRow objRow_SubProceso = new HtmlTableRow();

                    HtmlTableCell objCell_SubProceso = new HtmlTableCell();
                    objCell_SubProceso.InnerHtml = "<h3>Crear Subproceso</h3><textarea id='txt_area_subproceso_id_" + item.Id + "' placeholder='Ingrese el texto para subproceso correspondiente correspondiente.'></textarea> ";

                    HtmlInputButton objAlmacenar_proceso = new HtmlInputButton();
                    objAlmacenar_proceso.ID = "btn_subproceso_almacenar_id" + item.Id.ToString();
                    objAlmacenar_proceso.Attributes.Add("onclick", String.Format("AlmacenarSubProceso('{0}','{1}');", item.Id, "txt_area_subproceso_id_" + item.Id));

                    objAlmacenar_proceso.Value = "Almacenar Subproceso";

                    objCell_SubProceso.Attributes.CssStyle.Add("padding-left", "20px");
                    objRow_SubProceso.Cells.Add(objCell_SubProceso);
                    objRow_SubProceso.Attributes.Add("class", "trgris");
                    objCell_SubProceso.Attributes.CssStyle.Add("-moz-border-radius", "3px");
                    objCell_SubProceso.Attributes.CssStyle.Add("-webkit-border-radius", "3px");
                    objCell_SubProceso.Attributes.CssStyle.Add("border-radius", "3px");

                    objCell_SubProceso.Controls.Add(objAlmacenar_proceso);

                    objRow_SubProceso.Cells.Add(objCell_SubProceso);

                    objHtmlTable.Rows.Add(objRow_Proceso);
                    objHtmlTable.Rows.Add(objRow_SubProceso);

                    StringBuilder objsb = new StringBuilder();

                    objsb.Append("<div class='accordion'><h3><a href='#'>Subprocesos</a></h3>");
                    objsb.Append("<div id='col_subprocesos_" + item.Id + "'>");

                    IQueryable<Model.Subproceso> objSubprocesos = objCSubprocesos.LoadSubprocesos(item.Id);

                    int enumeracion_subprocesos = 1;
                    objsb.Append("<table style='width:100%;'> ");
                    foreach (var item_subroceso in objSubprocesos)
                    {

                        objsb.Append("<tr>");
                        objsb.Append("<td> <label style='color:#000;'> Subproceso No. " + enumeracion_subprocesos.ToString() + "<label>");
                        objsb.Append(" </td> ");
                        objsb.Append("</tr> ");
                        objsb.Append("<tr>");
                        string parametros = "'" + item_subroceso.Id.ToString() + "','txt_area_subproceso_up_" + item_subroceso.Id + "'";
                        objsb.Append("<td> <textarea id='txt_area_subproceso_up_" + item_subroceso.Id + "' placeholder='Texto correspondiente a Subproceso'>" + item_subroceso.Subproceso1 + "</textarea>");
                        objsb.Append(" <input type='button' value='Actualizar Subproceso' onclick=\"ActualizarSubProceso(" + parametros + ");\" /> ");
                        objsb.Append("</td> ");
                        objsb.Append("</tr> ");


                        enumeracion_subprocesos++;
                    }
                    objsb.Append("</table><br/>");
                    objsb.Append("</div></div>");

                    HtmlTableRow objRow_Collection = new HtmlTableRow();

                    HtmlTableCell objCell_Collection = new HtmlTableCell();
                    objCell_Collection.InnerHtml = objsb.ToString();

                    objRow_Collection.Cells.Add(objCell_Collection);

                    objHtmlTable.Rows.Add(objRow_Collection);

                    pnl_Subprocesos.Controls.Add(objHtmlTable);

                    HtmlGenericControl salto_linea = new HtmlGenericControl("br");

                    pnl_procesos.Controls.Add(salto_linea);

                    enumeracion++;
                }
            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        protected void Load_resultados()
        {
            try
            {
                IQueryable<Model.Causas_Efecto> objCausas_Efecto = objCSubprocesos.getProcesos(_idproyecto);
                int enumeracion = 1;
                foreach (var item in objCausas_Efecto)
                {
                    HtmlTable objHtmlTable = new HtmlTable();

                    objHtmlTable.Width = "80%";

                    HtmlTableRow objRow_Proceso = new HtmlTableRow();

                    HtmlTableCell objCell_Proceso = new HtmlTableCell();
                    objCell_Proceso.InnerHtml = "<label style='color:" + item.Color + ";'>Proceso No. " + enumeracion.ToString() + ": " + item.Proceso + "</label>";

                    objCell_Proceso.Attributes.CssStyle.Add("padding-left", "8px");
                    objRow_Proceso.Cells.Add(objCell_Proceso);
                    objRow_Proceso.Attributes.Add("class", "trheader");
                    objCell_Proceso.Attributes.CssStyle.Add("-moz-border-radius", "3px");
                    objCell_Proceso.Attributes.CssStyle.Add("-webkit-border-radius", "3px");
                    objCell_Proceso.Attributes.CssStyle.Add("border-radius", "3px");

                    objHtmlTable.Rows.Add(objRow_Proceso);

                    IQueryable<Model.Subproceso> objSubprocesos = objCSubprocesos.LoadSubprocesos(item.Id);

                    int enumeracion_Subproceso = 1;
                    foreach (var item_subroceso in objSubprocesos)
                    {
                        HtmlTableRow objRow_SubProceso = new HtmlTableRow();

                        HtmlTableCell objCell_SubProceso = new HtmlTableCell();
                        objCell_SubProceso.InnerHtml = "<label style='color:#000;'>Subproceso No. " + enumeracion_Subproceso.ToString() + ": " + item_subroceso.Subproceso1 + "</label>";

                        objCell_SubProceso.Attributes.CssStyle.Add("padding-left", "20px");
                        objRow_SubProceso.Cells.Add(objCell_SubProceso);
                        objRow_SubProceso.Attributes.Add("class", "trgris");
                        objCell_SubProceso.Attributes.CssStyle.Add("-moz-border-radius", "3px");
                        objCell_SubProceso.Attributes.CssStyle.Add("-webkit-border-radius", "3px");
                        objCell_SubProceso.Attributes.CssStyle.Add("border-radius", "3px");

                        objHtmlTable.Rows.Add(objRow_SubProceso);

                        HtmlTableRow objRow_Estrategias = new HtmlTableRow();

                        HtmlTableCell objCell_Estrategias = new HtmlTableCell();
                        objCell_Estrategias.InnerHtml = "<h3>Crear Estrategia</h3><textarea id='txt_area_estrategia_id_" + item_subroceso.Id + "' placeholder='Ingrese el texto para estrategia correspondiente.'></textarea> ";

                        HtmlInputButton objAlmacenar_estrategia = new HtmlInputButton();
                        objAlmacenar_estrategia.ID = "btn_estrategia_almacenar_id" + item.Id.ToString();
                        objAlmacenar_estrategia.Attributes.Add("onclick", String.Format("AlmacenarEstrategia('{0}','{1}');", item_subroceso.Id, "txt_area_estrategia_id_" + item_subroceso.Id));
                        objAlmacenar_estrategia.Value = "Almacenar Estrategia";

                        objCell_Estrategias.Controls.Add(objAlmacenar_estrategia);
                        objCell_Estrategias.Attributes.Add("class", "trgris");
                        objCell_Estrategias.Attributes.CssStyle.Add("-moz-border-radius", "3px");
                        objCell_Estrategias.Attributes.CssStyle.Add("-webkit-border-radius", "3px");
                        objCell_Estrategias.Attributes.CssStyle.Add("border-radius", "3px");
                        objRow_Estrategias.Cells.Add(objCell_Estrategias);

                        objHtmlTable.Rows.Add(objRow_Estrategias);

                        IQueryable<Model.Resultados_Proyecto> objEstrategias = new CResultados_proyecto().LoadResultados(item_subroceso.Id);

                        StringBuilder objsb = new StringBuilder();

                        objsb.Append("<div class='accordion'><h3><a href='#'>Estrategias</a></h3>");
                        objsb.Append("<div id='col_estrategias_" + item_subroceso.Id + "'>");

                        var detalles = new HtmlAnchor();
                        var cronograma = new HtmlAnchor();

                        int enumeracion_Estrategias = 1;
                        objsb.Append("<table style='width:100%;'> ");
                        foreach (var item_estrategias in objEstrategias)
                        {

                            objsb.Append("<tr>");
                            objsb.Append("<td> <label style='color:#000;'> Estrategia No. " + enumeracion_Estrategias.ToString() + "<label>");
                            objsb.Append(" </td> ");
                            objsb.Append("</tr> ");
                            objsb.Append("<tr>");
                            string parametros = "'" + item_estrategias.Id.ToString() + "','txt_area_estrategias_up_" + item_estrategias.Id + "'";
                            objsb.Append("<td> <textarea id='txt_area_estrategias_up_" + item_estrategias.Id + "' placeholder='Texto correspondiente a estrategia'>" + item_estrategias.Resultado + "</textarea>");
                            objsb.Append(" <input type='button' value='Actualizar Estrategia' onclick=\"ActualizarEstrategia(" + parametros + ");\" /> ");
                            objsb.Append(" <a title=\"Detalles para Resultado No." + enumeracion_Estrategias.ToString() + "\" class='pretty' href=\"" + Request.Url.Scheme + "://" + Request.Url.Authority + "/DetallesMarcoLogico.aspx?idResultado=" + item_estrategias.Id + "&iframe=true&amp;width=100%&amp;height=100%\"\"><img alt='Detalles' src='/Icons/details.png' width='24px' /></a>");
                            objsb.Append(" <a title=\"Cronograma Resultado No." + enumeracion_Estrategias.ToString() + "\" class='pretty' href=\"" + Request.Url.Scheme + "://" + Request.Url.Authority + "/DiagramaGant.aspx?idResultado=" + item_estrategias.Id + "&iframe=true&amp;width=100%&amp;height=100%\"\"><img alt='Cronograma' src='/Icons/Calender.png' width='24px' /></a>");
                            objsb.Append("</td> ");
                            objsb.Append("</tr> ");

                            enumeracion_Estrategias++;
                        }
                        objsb.Append("</table><br/>");
                        objsb.Append("</div></div>");
                        HtmlTableRow objRow_Collection_estrategias = new HtmlTableRow();

                        HtmlTableCell objCell_Collection_estrategias = new HtmlTableCell();
                        objCell_Collection_estrategias.InnerHtml = objsb.ToString();

                        objRow_Collection_estrategias.Cells.Add(objCell_Collection_estrategias);

                        objHtmlTable.Rows.Add(objRow_Collection_estrategias);
                        enumeracion_Subproceso++;
                    }

                    presultados.Controls.Add(objHtmlTable);

                    HtmlGenericControl salto_linea = new HtmlGenericControl("br");

                    presultados.Controls.Add(salto_linea);

                    enumeracion++;
                }
            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        protected void Load_estrategias()
        {
            try
            {
                IQueryable<Model.Causas_Efecto> objCausas_Efecto = objCSubprocesos.getProcesos(_idproyecto);
                int enumeracion = 1;
                foreach (var item in objCausas_Efecto)
                {
                    HtmlTable objHtmlTable = new HtmlTable();

                    objHtmlTable.Width = "80%";

                    HtmlTableRow objRow_Proceso = new HtmlTableRow();

                    HtmlTableCell objCell_Proceso = new HtmlTableCell();
                    objCell_Proceso.InnerHtml = "<label style='color:" + item.Color + ";'>Proceso No. " + enumeracion.ToString() + ": " + item.Proceso + "</label>";
                    objCell_Proceso.Attributes.CssStyle.Add("padding-left", "8px");
                    objRow_Proceso.Cells.Add(objCell_Proceso);
                    objRow_Proceso.Attributes.Add("class", "trheader");
                    objCell_Proceso.Attributes.CssStyle.Add("-moz-border-radius", "3px");
                    objCell_Proceso.Attributes.CssStyle.Add("-webkit-border-radius", "3px");
                    objCell_Proceso.Attributes.CssStyle.Add("border-radius", "3px");

                    objHtmlTable.Rows.Add(objRow_Proceso);

                    IQueryable<Model.Subproceso> objSubprocesos = objCSubprocesos.LoadSubprocesos(item.Id);

                    int enumeracion_Subproceso = 1;
                    foreach (var item_subroceso in objSubprocesos)
                    {
                        HtmlTableRow objRow_SubProceso = new HtmlTableRow();

                        HtmlTableCell objCell_SubProceso = new HtmlTableCell();
                        objCell_SubProceso.InnerHtml = "<label style='color:#000;'>Subproceso No. " + enumeracion_Subproceso.ToString() + ": " + item_subroceso.Subproceso1 + "</label>";

                        objCell_SubProceso.Attributes.CssStyle.Add("padding-left", "20px");
                        objRow_SubProceso.Cells.Add(objCell_SubProceso);
                        objRow_SubProceso.Attributes.Add("class", "trgris");
                        objCell_SubProceso.Attributes.CssStyle.Add("-moz-border-radius", "3px");
                        objCell_SubProceso.Attributes.CssStyle.Add("-webkit-border-radius", "3px");
                        objCell_SubProceso.Attributes.CssStyle.Add("border-radius", "3px");

                        objHtmlTable.Rows.Add(objRow_SubProceso);

                        IQueryable<Model.Resultados_Proyecto> objEstrategias = new CResultados_proyecto().LoadResultados(item_subroceso.Id);

                        int enumeracion_Estrategias = 1;
                        foreach (var item_estrategias in objEstrategias)
                        {
                            HtmlTableRow objRow_estrategia_ = new HtmlTableRow();

                            HtmlTableCell objCell_estrategia_ = new HtmlTableCell();
                            objCell_estrategia_.InnerHtml = "<label style='color:#000;'>Estrategia No. " + enumeracion_Estrategias.ToString() + ": " + item_estrategias.Resultado + "</label>";

                            objCell_estrategia_.Attributes.CssStyle.Add("padding-left", "30px");
                            objRow_estrategia_.Cells.Add(objCell_estrategia_);
                            objRow_estrategia_.Attributes.Add("class", "trblanca");
                            objRow_estrategia_.Attributes.CssStyle.Add("-moz-border-radius", "3px");
                            objRow_estrategia_.Attributes.CssStyle.Add("-webkit-border-radius", "3px");
                            objRow_estrategia_.Attributes.CssStyle.Add("border-radius", "3px");

                            objHtmlTable.Rows.Add(objRow_estrategia_);

                            HtmlTableRow objRow_actividades = new HtmlTableRow();

                            HtmlTableCell objCell_actividades = new HtmlTableCell();
                            objCell_actividades.InnerHtml = "<h3>Crear Actividad</h3>Actividad<br/><textarea class='speech' id='txt_area_actividad_id_" + item_estrategias.Id + "' placeholder='Ingrese el texto para actividad correspondiente.'></textarea> <br/>Presupuesto<br/><input type='text' placeholder='Campo exclusivamente numerico' class='numerico' style='width:80%;' value='0' id='txt_actividad_presupuesto" + item_estrategias.Id + "'> <br/>";

                            objCell_actividades.Attributes.CssStyle.Add("padding-left", "40px");

                            HtmlInputButton objAlmacenar_actividades = new HtmlInputButton();
                            objAlmacenar_actividades.ID = "btn_actividad_almacenar_id" + item.Id.ToString();
                            objAlmacenar_actividades.Attributes.Add("onclick", String.Format("AlmacenarActividad('{0}','{1}','{2}');", item_estrategias.Id, "txt_area_actividad_id_" + item_estrategias.Id, "txt_actividad_presupuesto" + item_estrategias.Id));
                            objAlmacenar_actividades.Value = "Almacenar Actividad";

                            objCell_actividades.Controls.Add(objAlmacenar_actividades);
                            objCell_actividades.Attributes.CssStyle.Add("-moz-border-radius", "3px");
                            objCell_actividades.Attributes.CssStyle.Add("-webkit-border-radius", "3px");
                            objCell_actividades.Attributes.CssStyle.Add("border-radius", "3px");
                            objRow_actividades.Cells.Add(objCell_actividades);
                            objRow_actividades.Attributes.Add("class", "trgris");
                            objRow_actividades.Attributes.CssStyle.Add("color", "#007CB6");

                            objHtmlTable.Rows.Add(objRow_actividades);

                            IQueryable<Model.Actividade> objActividades = new CActividades().getActividades(item_estrategias.Id);

                            StringBuilder objsb_actividades = new StringBuilder();

                            objsb_actividades.Append("<div class='accordion'><h3><a href='#'>Actividades</a></h3>");
                            objsb_actividades.Append("<div id='col_actividades_" + item_estrategias.Id + "'>");

                            var detalles_Actividades = new HtmlAnchor();
                            var cronograma_Cronograma = new HtmlAnchor();

                            int enumeracion_Actividades = 1;
                            objsb_actividades.Append("<table style='width:100%;'> ");
                            foreach (var item_actividades in objActividades)
                            {
                                objsb_actividades.Append("<tr>");
                                objsb_actividades.Append("<td> <label style='color:#000;'> Actividad No. " + enumeracion_Actividades.ToString() + "<label>");
                                objsb_actividades.Append(" </td> ");
                                objsb_actividades.Append("</tr> ");
                                objsb_actividades.Append("<tr>");
                                string parametros = "'" + item_actividades.Id.ToString() + "','txt_area_actividades_up_" + item_actividades.Id + "','txt_actividad_presupuesto_up" + item_estrategias.Id + "'";
                                objsb_actividades.Append("<td> <textarea id='txt_area_actividades_up_" + item_actividades.Id + "' placeholder='Texto correspondiente a actividades'>" + item_actividades.Actividad + "</textarea><br/>Presupuesto<br/><input type='text' placeholder='Campo exclusivamente numerico' class='numerico' style='width:80%;' value=" + item_actividades.Presupuesto + "  id='txt_actividad_presupuesto_up" + item_estrategias.Id + "'> <br/>");
                                objsb_actividades.Append(" <input type='button' value='Actualizar Actividad' onclick=\"ActualizarActividad(" + parametros + ");\" /> ");
                                objsb_actividades.Append(" <a title=\"Detalles para Actividad No." + enumeracion_Actividades.ToString() + "\" class='pretty' href=\"" + Request.Url.Scheme + "://" + Request.Url.Authority + "/DetallesMarcoLogico.aspx?idActividad=" + item_actividades.Id + "&iframe=true&amp;width=100%&amp;height=100%\"\"><img alt='Detalles' src='/Icons/details.png' width='24px' /></a>");
                                objsb_actividades.Append(" <a title=\"Cronograma Actividad No." + enumeracion_Actividades.ToString() + "\" class='pretty' href=\"" + Request.Url.Scheme + "://" + Request.Url.Authority + "/DiagramaGant.aspx?idActividad=" + item_actividades.Id + "&iframe=true&amp;width=100%&amp;height=100%\"\"><img alt='Cronograma' src='/Icons/Calender.png' width='24px' /></a>");
                                objsb_actividades.Append("</td> ");
                                objsb_actividades.Append("</tr> ");

                                enumeracion_Actividades++;
                            }

                            objsb_actividades.Append("</table><br/>");
                            objsb_actividades.Append("</div></div>");

                            HtmlTableRow objRow_Collection_actividades = new HtmlTableRow();
                            objRow_Collection_actividades.Attributes.CssStyle.Add("padding-left", "40px");
                            HtmlTableCell objCell_Collection_actividades = new HtmlTableCell();
                            objCell_Collection_actividades.InnerHtml = objsb_actividades.ToString();

                            objRow_Collection_actividades.Cells.Add(objCell_Collection_actividades);

                            objHtmlTable.Rows.Add(objRow_Collection_actividades);

                            enumeracion_Estrategias++;
                        }

                        enumeracion_Subproceso++;
                    }

                    pnlActividades.Controls.Add(objHtmlTable);

                    HtmlGenericControl salto_linea = new HtmlGenericControl("br");

                    pnlActividades.Controls.Add(salto_linea);

                    enumeracion++;
                }
            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }
    }

}