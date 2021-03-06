﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using ESM.Objetos;
using EvaluationSettings;
using ESM.Model;
using System.Data.Linq.SqlClient;

namespace ESM.Evaluacion
{
    public partial class Evaluacion : System.Web.UI.Page
    {
        #region Propiedades Privadas y Publicas
        protected EvaluationSettings.CEvaluacion _objevaluacion = new EvaluationSettings.CEvaluacion();
        int _tipo = 0;
        System.Timers.Timer _objTimer = new System.Timers.Timer();
        #endregion

        #region Almacenar Informacion de Evaluaciones


        protected void FinalizarProcesoEvaluacionEstado()
        {
            gvMediciones.DataBind();
            gvTopEval.Visible = true;
            //btnVolverEE.Visible = true;
            lbloki.Visible = true;
            divmensaje.Visible = true;
        }


        protected void btnVolver_Click(object sender, EventArgs e)
        {
            ModEvaluacion.Visible = false;
            btnMedicion.Visible = true;
            ModTopEval.Visible = false;
            ModDocumentos.Visible = false;

            int idmedicion = Convert.ToInt32(Session["idie"]);
            var mediciones = _objevaluacion.MedicionesIE(idmedicion);
            if (mediciones != null)
            {
                gvMediciones.DataSource = mediciones;
                gvMediciones.DataBind();

                #region Visualizacion de Controles

                modSEseleccion.Visible = false;
                modEESeleccion.Visible = false;
                ModMediciones.Visible = true;
                btnMedicion.Visible = false;
                gvMediciones.Visible = true;
                modEESeleccion.Visible = false;

                for (int i = 0; i < gvMediciones.Rows.Count; i++)
                {
                    if (i == gvMediciones.Rows.Count - 1)
                        gvMediciones.Rows[i].Visible = true;
                    else
                        gvMediciones.Rows[i].Visible = false;
                }
                #endregion
            }
            ObtenerTema(gvMediciones);
        }

        #endregion

        #region Eventos de Servidor
        /// <summary>
        /// Se ejecuta al inicializar el formulario en el proceso de carga de controles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idrama"] != null)
            {
                _tipo = Convert.ToInt32(Request.QueryString["idrama"]);
            }
            if (Request.IsAuthenticated)
            {
                /*Prueba de ajax realizada para actualizar un gridview fallida*/
                if (!Page.IsPostBack)
                {
                    Session.Remove("ideval");
                    CRoles objCRoles = new CRoles();

                    int idusuario = Convert.ToInt32(Session["idusuario"]);
                    string rol = objCRoles.ObtenerRol(idusuario);
                    if (_tipo == 2)
                    {
                        Session.Remove("ideval");
                        /*Asigno un valor false a las propiedades del objeto del tipo CEvaluacion para
                         *Evitar que haya error por recivir mas de un tipo de actor al momento de carga de la evaluacion
                         */
                        _objevaluacion.Profesional = false;
                        _objevaluacion.Estudiantes = false;
                        _objevaluacion.SecretariaEducacion = false;
                        _objevaluacion.Padres = false;
                        _objevaluacion.Docentes = false;
                        if (rol == "Administrador")
                        {
                            /*Cargo el control gridview con el data source obtenido de instituciones educativas*/
                            gvResultados.DataSource = CEE.ObtenerEEs(objCRoles.IdConsultor, false, true);
                            gvResultados.DataBind();
                        }
                        else if (rol == "Consultor" || rol == "MEN" || rol == "Revisor")
                        {
                            gvResultados.DataSource = CEE.ObtenerEEs(objCRoles.IdConsultor);
                            gvResultados.DataBind();
                        }
                        else
                        {
                            Response.Write("<script>alert('Acceso Denagado!');</script>");
                            Response.Redirect("Login.aspx");
                        }
                        /*Actualizo el control griview para que el formulario web tome los ultimos cambios realizados*/
                        ObtenerTema(gvResultados);
                        cboActores.DataSourceID = "ldsActores";
                        cboActores.DataBind();
                        gvSE.Visible = false;

                        #region Visualizacion de Controles

                        modSEseleccion.Visible = false;
                        modEESeleccion.Visible = true;

                        #endregion
                    }
                    else if (_tipo == 1)
                    {
                        ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                        var a = from ac in db.Actores
                                where ac.IdRama == 1
                                select ac;

                        cboActores.DataSource = a;
                        cboActores.DataBind();

                        #region Visualizacion de Controles
                        modSEseleccion.Visible = true;
                        modEESeleccion.Visible = false;
                        #endregion

                        ObtenerTema(gvSE);
                    }

                }
            }
            else
                Response.Redirect("/Login.aspx");

        }
        /// <summary>
        /// Se ejecuta al momento de la carga de controles dentro del gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAmb1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                switch (e.Row.RowType)
                {
                    case DataControlRowType.DataRow:


                        System.Web.UI.HtmlControls.HtmlAnchor a = (HtmlAnchor)e.Row.FindControl("lknAyuda");
                        Label lblIdPregunta = (Label)e.Row.FindControl("lblIdPregunta");
                        RadioButton objrbtnSi = (RadioButton)e.Row.FindControl("rbtnSi");
                        RadioButton objrbtnNo = (RadioButton)e.Row.FindControl("rbtnNo");
                        TextBox objTextBox = (TextBox)e.Row.FindControl("txtsesion");
                        CheckBox objpendiente = (CheckBox)e.Row.FindControl("chxPendiente");
                        objpendiente.ID = "chxPendiente" + lblIdPregunta.Text;
                        objTextBox.CssClass = String.Concat("sesion_", lblIdPregunta.Text);
                        objrbtnSi.ToolTip = lblIdPregunta.Text;
                        objrbtnNo.ToolTip = lblIdPregunta.Text;
                        objrbtnSi.CssClass = "radiosi";
                        objrbtnNo.CssClass = "radiono";
                        objrbtnSi.Attributes.Add("name", lblIdPregunta.Text);
                        if (objrbtnSi.Checked)
                            objTextBox.Enabled = true;
                        objTextBox.ID = "txtsesion" + lblIdPregunta.Text;
                        a.Title = "Información ESM";
                        a.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/Ayuda.aspx?id=" + lblIdPregunta.Text + "&iframe=true&amp;width=500&amp;height=300";
                        break;
                }
            }


            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Se ejecuta al momento de la carga de controles dentro del gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAmb2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                switch (e.Row.RowType)
                {
                    case DataControlRowType.DataRow:


                        System.Web.UI.HtmlControls.HtmlAnchor a = (HtmlAnchor)e.Row.FindControl("lknAyuda");
                        Label lblIdPregunta = (Label)e.Row.FindControl("lblIdPregunta");
                        RadioButton objrbtnSi = (RadioButton)e.Row.FindControl("rbtnSi");
                        RadioButton objrbtnNo = (RadioButton)e.Row.FindControl("rbtnNo");
                        TextBox objTextBox = (TextBox)e.Row.FindControl("txtsesion");
                        CheckBox objpendiente = (CheckBox)e.Row.FindControl("chxPendiente");
                        objpendiente.ID = "chxPendiente" + lblIdPregunta.Text;
                        objTextBox.CssClass = String.Concat("sesion_", lblIdPregunta.Text);
                        objrbtnSi.ToolTip = lblIdPregunta.Text;
                        objrbtnNo.ToolTip = lblIdPregunta.Text;
                        objrbtnSi.CssClass = "radiosi";
                        objrbtnNo.CssClass = "radiono";
                        if (objrbtnSi.Checked)
                            objTextBox.Enabled = true;
                        objTextBox.ID = "txtsesion" + lblIdPregunta.Text;
                        a.Title = "Información ESM";
                        a.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/Ayuda.aspx?id=" + lblIdPregunta.Text + "&iframe=true&amp;width=500&amp;height=300";
                        break;
                }
            }


            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Se ejecuta al momento de la carga de controles dentro del gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAmb3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                switch (e.Row.RowType)
                {
                    case DataControlRowType.DataRow:


                        System.Web.UI.HtmlControls.HtmlAnchor a = (HtmlAnchor)e.Row.FindControl("lknAyuda");
                        Label lblIdPregunta = (Label)e.Row.FindControl("lblIdPregunta");
                        RadioButton objrbtnSi = (RadioButton)e.Row.FindControl("rbtnSi");
                        RadioButton objrbtnNo = (RadioButton)e.Row.FindControl("rbtnNo");
                        TextBox objTextBox = (TextBox)e.Row.FindControl("txtsesion");
                        CheckBox objpendiente = (CheckBox)e.Row.FindControl("chxPendiente");
                        objpendiente.ID = "chxPendiente" + lblIdPregunta.Text;
                        objTextBox.CssClass = String.Concat("sesion_", lblIdPregunta.Text);
                        objrbtnSi.ToolTip = lblIdPregunta.Text;
                        objrbtnNo.ToolTip = lblIdPregunta.Text;
                        objrbtnSi.CssClass = "radiosi";
                        objrbtnNo.CssClass = "radiono";
                        if (objrbtnSi.Checked)
                            objTextBox.Enabled = true;
                        objTextBox.ID = "txtsesion" + lblIdPregunta.Text;
                        a.Title = "Información ESM";
                        a.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/Ayuda.aspx?id=" + lblIdPregunta.Text + "&iframe=true&amp;width=500&amp;height=300";
                        break;
                }
            }


            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Se ejecuta al momento de la carga de controles dentro del gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAmb5_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                switch (e.Row.RowType)
                {
                    case DataControlRowType.DataRow:


                        System.Web.UI.HtmlControls.HtmlAnchor a = (HtmlAnchor)e.Row.FindControl("lknAyuda");
                        Label lblIdPregunta = (Label)e.Row.FindControl("lblIdPregunta");
                        RadioButton objrbtnSi = (RadioButton)e.Row.FindControl("rbtnSi");
                        RadioButton objrbtnNo = (RadioButton)e.Row.FindControl("rbtnNo");
                        TextBox objTextBox = (TextBox)e.Row.FindControl("txtsesion");
                        CheckBox objpendiente = (CheckBox)e.Row.FindControl("chxPendiente");
                        objpendiente.ID = "chxPendiente" + lblIdPregunta.Text;
                        objTextBox.CssClass = String.Concat("sesion_", lblIdPregunta.Text);
                        objrbtnSi.ToolTip = lblIdPregunta.Text;
                        objrbtnNo.ToolTip = lblIdPregunta.Text;
                        objrbtnSi.CssClass = "radiosi";
                        objrbtnNo.CssClass = "radiono";
                        if (objrbtnSi.Checked)
                            objTextBox.Enabled = true;
                        objTextBox.ID = "txtsesion" + lblIdPregunta.Text;
                        a.Title = "Información ESM";
                        a.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/Ayuda.aspx?id=" + lblIdPregunta.Text + "&iframe=true&amp;width=500&amp;height=300";
                        break;
                }
            }


            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Se ejecuta al momento de la carga de controles dentro del gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAmb4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                switch (e.Row.RowType)
                {
                    case DataControlRowType.DataRow:


                        System.Web.UI.HtmlControls.HtmlAnchor a = (HtmlAnchor)e.Row.FindControl("lknAyuda");
                        Label lblIdPregunta = (Label)e.Row.FindControl("lblIdPregunta");
                        RadioButton objrbtnSi = (RadioButton)e.Row.FindControl("rbtnSi");
                        RadioButton objrbtnNo = (RadioButton)e.Row.FindControl("rbtnNo");
                        TextBox objTextBox = (TextBox)e.Row.FindControl("txtsesion");
                        CheckBox objpendiente = (CheckBox)e.Row.FindControl("chxPendiente");
                        objpendiente.ID = "chxPendiente" + lblIdPregunta.Text;
                        objTextBox.CssClass = String.Concat("sesion_", lblIdPregunta.Text);
                        objrbtnSi.ToolTip = lblIdPregunta.Text;
                        objrbtnNo.ToolTip = lblIdPregunta.Text;
                        objrbtnSi.CssClass = "radiosi";
                        objrbtnNo.CssClass = "radiono";
                        if (objrbtnSi.Checked)
                            objTextBox.Enabled = true;
                        objTextBox.ID = "txtsesion" + lblIdPregunta.Text;
                        a.Title = "Información ESM";
                        a.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/Ayuda.aspx?id=" + lblIdPregunta.Text + "&iframe=true&amp;width=500&amp;height=300";
                        break;
                }
            }


            catch (Exception)
            {

                throw;
            }

        }

        protected void gvResultados_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow _objRow = gvResultados.SelectedRow;
            Label lblIdIe = (Label)_objRow.Cells[5].FindControl("IDIE");
            Session.Add("idie", lblIdIe.Text);
            lblCodIe.Text = _objRow.Cells[1].Text;
            lblIE.Text = _objRow.Cells[2].Text;
            lblMunicipio.Text = _objRow.Cells[3].Text;
            btnMedicion.Visible = true;
            var mediciones = _objevaluacion.MedicionesIE(Convert.ToInt32(lblIdIe.Text));

            if (mediciones != null)
            {
                gvMediciones.DataSource = mediciones;
                gvMediciones.DataBind();
                ObtenerTema(gvMediciones);

                #region Visualizacion de Controles

                modSEseleccion.Visible = false;
                modEESeleccion.Visible = false;
                ModMediciones.Visible = true;
                btnMedicion.Visible = false;
                gvMediciones.Visible = true;
                modEESeleccion.Visible = false;

                btnMedicion.Visible = true;
                #endregion


            }
            else
            {
                #region Visualizacion de Controles

                modSEseleccion.Visible = false;
                modEESeleccion.Visible = false;
                ModMediciones.Visible = true;
                btnMedicion.Visible = true;
                gvMediciones.Visible = true;
                modEESeleccion.Visible = false;

                #endregion
            }
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            CRoles objCRoles = new CRoles();
            int idusuario = Convert.ToInt32(Session["idusuario"]);
            string rol = objCRoles.ObtenerRol(idusuario);
            int idconsultor = objCRoles.IdConsultor;
            Filtro(txtFiltro.Text, idconsultor);
        }

        protected void ldsIes_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {


        }

        protected void cboActores_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.Remove("ideval");

            bool actorexiste = false;
            for (int i = 0; i < gvTopEval.Rows.Count; i++)
            {
                if (cboActores.SelectedValue == gvTopEval.Rows[i].Cells[2].Text)
                {
                    if (gvTopEval.Rows[i].Cells[5].Text != "Parcial")
                        actorexiste = true;
                }
            }
            if (!actorexiste)
            {
                EvaluarActorSeleccionado(cboActores.SelectedItem.Text, Convert.ToInt32(cboActores.SelectedItem.Value));
                gvAmb1.Visible = true;
                gvAmb2.Visible = true;
                gvAmb3.Visible = true;
                gvAmb4.Visible = true;
                gvAmb5.Visible = true;
            }
            else
            {
                gvAmb1.Visible = false;
                gvAmb2.Visible = false;
                gvAmb3.Visible = false;
                gvAmb4.Visible = false;
                gvAmb5.Visible = false;
                informacionuno.Visible = false;
            }



        }

        protected void gvTopEval_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvAmb1.Visible = true;
            gvAmb2.Visible = true;
            gvAmb3.Visible = true;
            gvAmb4.Visible = true;
            gvAmb5.Visible = true;

            GridViewRow objrow = gvTopEval.SelectedRow;

            Session.Remove("loadideval");
            Session.Remove("loadidactor");

            if (objrow.Cells[1].Text != null && objrow.Cells[2].Text != null)
            {
                Session.Add("loadideval", objrow.Cells[1].Text);
                Session.Add("loadidactor", objrow.Cells[2].Text);
            }

            int idevaluacion = Convert.ToInt32(Session["loadideval"]);
            int idactor = Convert.ToInt32(Session["loadidactor"]);

            CargarParcial(idevaluacion, idactor);
            #region Visualizacion de Controles
            ModDocumentos.Visible = true;
            ModEvaluacion.Visible = true;
            //btnVolverEE.Visible = true;
            #endregion

        }

        protected void lbtnVolver_Click(object sender, EventArgs e)
        {
        }

        protected void btnMedicion_Click(object sender, EventArgs e)
        {
            int idmedicion = _objevaluacion.CrearMedion();
            Session.Remove("idmedicion");
            Session.Remove("ideval");
            Session.Add("idmedicion", idmedicion);
            btnMedicion.Visible = false;
            adocumentos.Title = "Información ESM";
            adocumentos.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/ModuloDocumentos.aspx?idmedicion=" + idmedicion.ToString() + "&iframe=true&amp;width=100%&amp;height=100%";
            #region Visualizacion de Controles
            btnMedicion.Visible = false;
            gvTopEval.Visible = false;
            ModMediciones.Visible = true;
            ModDocumentos.Visible = true;
            ModEvaluacion.Visible = true;

            gvAmb1.Visible = true;
            gvAmb2.Visible = true;
            gvAmb3.Visible = true;
            gvAmb4.Visible = true;
            gvAmb5.Visible = true;
            gvAmb1.DataSource = null;
            gvAmb1.DataBind();
            gvAmb2.DataSource = null;
            gvAmb2.DataBind();
            gvAmb3.DataSource = null;
            gvAmb3.DataBind();
            gvAmb4.DataSource = null;
            gvAmb4.DataBind();
            gvAmb5.DataSource = null;
            gvAmb5.DataBind();

            gvMediciones.Visible = false;
            cboActores.SelectedIndex = 5;
            //btnVolverEE.Visible = false;
            //btnalmacenarparcial.Visible = false;
            //btnDefinitiva.Visible = false;
            informacionuno.Visible = false;
            #endregion

        }

        protected void gvMediciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objrow = this.gvMediciones.SelectedRow;

            int idmedicion = Convert.ToInt32(objrow.Cells[1].Text);
            TopEvaluaciones(idmedicion);

            #region Visualizacion de Controles
            btnMedicion.Visible = false;
            gvTopEval.Visible = true;
            ModTopEval.Visible = true;
            ModMediciones.Visible = true;
            ModDocumentos.Visible = true;
            ModEvaluacion.Visible = true;
            cboActores.SelectedValue = "7";

            adocumentos.Title = "Información";
            adocumentos.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/ModuloDocumentos.aspx?idmedicion=" + idmedicion.ToString() + "&iframe=true&amp;width=100%&amp;height=100%";
            #endregion

        }
        #endregion

        #region Carga de Evaluaciones Parciales
        protected void CargarParcial(int ideval, int idactor)
        {
            gvAmb1.Enabled = true;
            gvAmb2.Enabled = true;
            gvAmb3.Enabled = true;
            gvAmb4.Enabled = true;
            gvAmb5.Enabled = true;

            Session.Add("ideval", ideval);
            var objList = _objevaluacion.LoadEvalParcial(ideval, idactor);
            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

            bool reload = false;

            var resultbyeval = from rbe in db.ResultadosByEvaluacions
                               join res in db.Resultados on rbe.IdResultado equals res.IdResultados
                               join pre in db.Preguntas on res.IdPregunta equals pre.IdPregunta
                               where rbe.IdEvaluacion == ideval
                               select new { res.IdPregunta, res.IdResultados, res.Valor, res.Pendiente, pre.Pregunta1, res.Sesiones, Ocultar = pre.Ocultar, Etiqueta = pre.Etiqueta };

            for (int g = 0; g < 5; g++)
            {
                #region Cargar resultados segun ambientes

                switch (g + 1)
                {
                    case 1:
                        resultbyeval = from rbe in db.ResultadosByEvaluacions
                                       join res in db.Resultados on rbe.IdResultado equals res.IdResultados
                                       join pre in db.Preguntas on res.IdPregunta equals pre.IdPregunta
                                       where rbe.IdEvaluacion == ideval && pre.Componente.Proceso.Ambiente.IdAmbiente == 1
                                       select new { res.IdPregunta, res.IdResultados, res.Valor, res.Pendiente, pre.Pregunta1, res.Sesiones, pre.Ocultar, pre.Etiqueta };
                        break;
                    case 2:
                        resultbyeval = from rbe in db.ResultadosByEvaluacions
                                       join res in db.Resultados on rbe.IdResultado equals res.IdResultados
                                       join pre in db.Preguntas on res.IdPregunta equals pre.IdPregunta
                                       where rbe.IdEvaluacion == ideval && pre.Componente.Proceso.Ambiente.IdAmbiente == 2
                                       select new { res.IdPregunta, res.IdResultados, res.Valor, res.Pendiente, pre.Pregunta1, res.Sesiones, pre.Ocultar, pre.Etiqueta };
                        break;
                    case 3:
                        resultbyeval = from rbe in db.ResultadosByEvaluacions
                                       join res in db.Resultados on rbe.IdResultado equals res.IdResultados
                                       join pre in db.Preguntas on res.IdPregunta equals pre.IdPregunta
                                       where rbe.IdEvaluacion == ideval && pre.Componente.Proceso.Ambiente.IdAmbiente == 3
                                       select new { res.IdPregunta, res.IdResultados, res.Valor, res.Pendiente, pre.Pregunta1, res.Sesiones, pre.Ocultar, pre.Etiqueta };
                        break;

                    case 4:
                        resultbyeval = from rbe in db.ResultadosByEvaluacions
                                       join res in db.Resultados on rbe.IdResultado equals res.IdResultados
                                       join pre in db.Preguntas on res.IdPregunta equals pre.IdPregunta
                                       where rbe.IdEvaluacion == ideval && pre.Componente.Proceso.Ambiente.IdAmbiente == 4
                                       select new { res.IdPregunta, res.IdResultados, res.Valor, res.Pendiente, pre.Pregunta1, res.Sesiones, pre.Ocultar, pre.Etiqueta };
                        break;
                    case 5:
                        resultbyeval = from rbe in db.ResultadosByEvaluacions
                                       join res in db.Resultados on rbe.IdResultado equals res.IdResultados
                                       join pre in db.Preguntas on res.IdPregunta equals pre.IdPregunta
                                       where rbe.IdEvaluacion == ideval && pre.Componente.Proceso.Ambiente.IdAmbiente == 5
                                       select new { res.IdPregunta, res.IdResultados, res.Valor, res.Pendiente, pre.Pregunta1, res.Sesiones, pre.Ocultar, pre.Etiqueta };
                        break;
                }

                #endregion

                GridView objGridView = (GridView)this.pnlEvaluacion.FindControl(String.Format("gvAmb{0}", g + 1));
                objGridView.DataSource = objList[g];
                objGridView.DataBind();

                ObtenerTema(objGridView);

                var listresults = resultbyeval.ToList();

                for (int i = 0; i < objGridView.Rows.Count; i++)
                {
                    for (int e = 0; e < listresults.Count; e++)
                    {
                        Label lbllp = (Label)objGridView.Rows[i].Cells[1].FindControl("lblLP");
                        Label idpregunta = (Label)objGridView.Rows[i].Cells[1].FindControl("lblIdPregunta");
                        TextBox objSesiones = (TextBox)objGridView.Rows[i].Cells[1].FindControl("txtsesion" + idpregunta.Text);
                        CheckBox objprendiente = (CheckBox)objGridView.Rows[i].Cells[1].FindControl("chxPendiente" + idpregunta.Text);
                        HtmlAnchor objlknayuda = (HtmlAnchor)objGridView.Rows[i].FindControl("lknAyuda");
                        RadioButton objsi = (RadioButton)objGridView.Rows[i].FindControl("rbtnSi");
                        RadioButton objno = (RadioButton)objGridView.Rows[i].FindControl("rbtnNo");

                        if (idpregunta.Text == listresults[e].IdPregunta.ToString())
                        {

                            ESM.Model.AyudaByPregunta objAyudaByPregunta = _objevaluacion.ObtenerAyudaPregunta(listresults[e].IdPregunta);

                            if ((bool)objAyudaByPregunta.Lectura && (bool)objAyudaByPregunta.Participacion)
                            {
                                objprendiente.Visible = true;
                                if (listresults[e].Pendiente != null)
                                {
                                    objprendiente.Checked = (bool)listresults[e].Pendiente;
                                }
                                lbllp.Visible = true;
                                lbllp.Text = "LP";
                            }
                            else if ((bool)objAyudaByPregunta.Lectura)
                            {
                                lbllp.Visible = true;
                                lbllp.Text = "L";
                            }
                            else if ((bool)objAyudaByPregunta.Participacion)
                            {
                                lbllp.Visible = true;
                                lbllp.Text = "P";
                            }

                            if (listresults[e].Pregunta1.Trim().Length == 0 || listresults[e].Pregunta1 == null || listresults[e].Pregunta1 == "")
                                reload = true;

                            if (reload)
                            {
                                objGridView.DataBind();
                                ObtenerTema(objGridView);
                                reload = false;
                            }

                            if (listresults[e].Ocultar != null)
                            {
                                if ((bool)listresults[e].Ocultar == true)
                                {
                                    objSesiones.Visible = false;
                                    objprendiente.Visible = false;
                                    objsi.Visible = false;
                                    objno.Visible = false;
                                    objlknayuda.Visible = false;
                                    lbllp.Visible = false;
                                }

                            }

                            ESM.Model.Pregunta objPreguntas = _objevaluacion.ObtenerDatosPregunta(Convert.ToInt32(idpregunta.Text));
                            if ((bool)objPreguntas.Sesiones)
                            {
                                objSesiones.Visible = true;
                                objSesiones.Enabled = false;
                                objSesiones.Text = listresults[e].Sesiones.ToString();
                            }
                            if (listresults[e].Valor == true)
                            {
                                objsi.Checked = true;
                                objSesiones.Enabled = true;
                            }
                            else if (listresults[e].Valor == false)
                            {
                                objno.Checked = true;
                            }

                            break;

                        }

                    }

                }
                cboActores.SelectedValue = idactor.ToString();
                //lblActorEvaluado.Text = cboActores.SelectedItem.Text;

                var estadoeval = from e in db.Evaluacions
                                 where e.IdEvaluacion == ideval
                                 select e;

                var evalsta = estadoeval.ToList();

                if (evalsta[0].IdEstado == 1)
                {
                    objGridView.Visible = true;
                    objGridView.Enabled = false;
                    informacionuno.Visible = false;
                }
                else
                {
                    objGridView.Visible = true;
                    informacionuno.Visible = true;
                }

            }

            activa_timer.Value = "1";

        }
        #endregion

        #region Metodos

        //<summary>
        //Carga el datasource del gridview evaluacion deacuerdo a al actor seleccionado
        //</summary>
        //<param name="objevaluacion"></param>
        protected void CargarPreguntas(EvaluationSettings.CEvaluacion objevaluacion)
        {
            try
            {
                #region Cargar Preguntas Ambientes
                List<IQueryable<ESM.Model.Pregunta>> cpreguntas = new List<IQueryable<Model.Pregunta>>();
                cpreguntas = objevaluacion.LoadEvaluation();
                int contador = 0;

                for (int p = 0; p < cpreguntas.Count; p++)
                {


                    GridView objGridView = (GridView)this.pnlEvaluacion.FindControl(String.Format("gvAmb{0}", p + 1));
                    //Llama al metodo load del objeto CEvaluacion que obtiene el Iqueryable para asignar al datasource del control
                    objGridView.DataSource = cpreguntas[p];
                    //Actualizo el control Gridview en el formulario para que almacene los cambio realizados
                    objGridView.DataBind();
                    //Asigna el tema para los griviews
                    ObtenerTema(objGridView);

                    for (int i = 0; i < objGridView.Rows.Count; i++)
                    {

                        Label objlabel = (Label)objGridView.Rows[i].FindControl("lblIdPregunta");
                        TextBox objtextbox = (TextBox)objGridView.Rows[i].FindControl("txtsesion" + objlabel.Text);
                        CheckBox objcheckbox = (CheckBox)objGridView.Rows[i].FindControl("chxPendiente" + objlabel.Text);
                        Label objlabelLP = (Label)objGridView.Rows[i].FindControl("lblLP");
                        HtmlAnchor objlknayuda = (HtmlAnchor)objGridView.Rows[i].FindControl("lknAyuda");
                        RadioButton objsi = (RadioButton)objGridView.Rows[i].FindControl("rbtnSi");
                        RadioButton objno = (RadioButton)objGridView.Rows[i].FindControl("rbtnNo");

                        int idPregunta = Convert.ToInt32(objlabel.Text);
                        foreach (var item in cpreguntas[p])
                        {
                            if (item.IdPregunta == idPregunta)
                            {
                                if (item.Sesiones != null)
                                {
                                    if ((bool)item.Sesiones)
                                    {
                                        objtextbox.Visible = true;
                                        objtextbox.Enabled = false;
                                    }
                                }
                                ESM.Model.AyudaByPregunta objAyudaByPregunta = _objevaluacion.ObtenerAyudaPregunta(item.IdPregunta);
                                if ((bool)objAyudaByPregunta.Lectura && (bool)objAyudaByPregunta.Participacion)
                                {
                                    objlabelLP.Text = "LP";
                                    objlabelLP.Visible = true;
                                    objcheckbox.Visible = true;
                                }
                                else if ((bool)objAyudaByPregunta.Lectura)
                                {
                                    objlabelLP.Text = "L";
                                    objlabelLP.Visible = true;
                                }
                                else if ((bool)objAyudaByPregunta.Participacion)
                                {
                                    objlabelLP.Text = "P";
                                    objlabelLP.Visible = true;
                                }
                                else
                                    objlabelLP.Visible = false;

                                if (item.Ocultar != null)
                                {
                                    if ((bool)item.Ocultar == true)
                                    {
                                        objtextbox.Visible = false;
                                        objcheckbox.Visible = false;
                                        objsi.Visible = false;
                                        objno.Visible = false;
                                        objlknayuda.Visible = false;
                                        objlabelLP.Visible = false;
                                    }

                                }

                            }

                        }

                    }

                    contador++;
                }

                #endregion
                activa_timer.Value = "1";
            }
            /*En caso de presentar excepcion retorno una alerta en javascript que me muestra y me controla el error presentado*/
            catch (Exception) { Response.Write("<script>alert('Ocurrio un error inesperado.');</script>"); }
        }

        protected bool Filtro(string texto, int idconsultor)
        {
            try
            {
                /*Instancio*/
                Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                IQueryable<Establecimiento_Educativo> rFiltro = null;

                if (Convert.ToInt32(Session["IDUSUARIO"]) >= 6)
                {
                    rFiltro = from i in db.Establecimiento_Educativos
                              where i.Nombre.Contains(texto) && i.Estado == true && i.Secretaria_Educacion.IdConsultor == idconsultor
                              select i;
                }
                else
                {
                    rFiltro = from i in db.Establecimiento_Educativos
                              where SqlMethods.Like(i.Nombre, String.Format("%{0}%", texto)) && i.Estado == true
                              select i;
                    Session.Add("buscar", "si");
                }

                if (texto.Trim().Length != 0)
                {
                    gvResultados.DataSourceID = null;
                    gvResultados.DataSource = rFiltro;
                }
                else
                {
                    if (Convert.ToInt32(Session["IDUSUARIO"]) >= 6)
                        gvResultados.DataSource = CEE.ObtenerEEs(idconsultor);
                    else
                        gvResultados.DataSource = CEE.ObtenerEEs(idconsultor, false, true);

                }

                gvResultados.DataBind();

                ObtenerTema(gvResultados);
                return true;
            }
            catch (Exception) { return false; }

        }

        protected void ObtenerTema(GridView objGridView)
        {
            if (objGridView.Rows.Count != 0)
            {
                objGridView.HeaderStyle.CssClass = "trheader";

                int color = 0;
                for (int i = 0; i < objGridView.Rows.Count; i++)
                {
                    if (color == 0)
                    {
                        objGridView.Rows[i].CssClass = "trgris";
                        color = 1;
                    }
                    else if (color == 1)
                    {
                        objGridView.Rows[i].CssClass = "trblanca";
                        color = 0;
                    }
                }
            }

        }

        protected void TopEvaluaciones(int idmedicion)
        {
            Session.Add("idmedicion", idmedicion);
            int idie = 0;

            if (Session["idie"] != null)
                idie = Convert.ToInt32(Session["idie"]);

            if (idie != 0 && idmedicion != 0)
            {
                IQueryable objIQueryable = _objevaluacion.ObtenerTopEvaluacion(5, idmedicion, idie);

                gvTopEval.DataSource = objIQueryable;
                gvTopEval.DataBind();
                ObtenerTema(gvTopEval);

            }
        }

        protected void EvaluarActorSeleccionado(string actorSeleccionado, int idactor)
        {
            gvAmb1.Visible = true;
            gvAmb2.Visible = true;
            gvAmb3.Visible = true;
            gvAmb4.Visible = true;
            gvAmb5.Visible = true;
            gvAmb1.Enabled = true;
            gvAmb2.Enabled = true;
            gvAmb3.Enabled = true;
            gvAmb4.Enabled = true;
            gvAmb5.Enabled = true;
            lblerrorAc.Visible = false;
            lbloki.Visible = false;
            informacionuno.Visible = true;

            int idie = Convert.ToInt32(Session["idie"]);
            int idmedicion = Convert.ToInt32(Session["idmedicion"]);

            if (_objevaluacion.ValidarActores(idie, idactor, idmedicion))
            {
                switch (actorSeleccionado)
                {
                    case "No Asignado":
                        gvAmb1.DataSource = null;
                        gvAmb2.DataSource = null;
                        gvAmb3.DataSource = null;
                        gvAmb4.DataSource = null;
                        gvAmb5.DataSource = null;
                        gvAmb1.DataBind();
                        gvAmb2.DataBind();
                        gvAmb3.DataBind();
                        gvAmb4.DataBind();
                        gvAmb5.DataBind();
                        informacionuno.Visible = false;
                        break;

                    case "Estudiante":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        Actorespnl.BackColor = System.Drawing.Color.Black;
                        _objevaluacion.Estudiantes = true;
                        CargarPreguntas(_objevaluacion);
                        break;

                    case "Profesional de Campo":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Profesional = true;
                        CargarPreguntas(_objevaluacion);
                        break;

                    case "Educador":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Docentes = true;
                        CargarPreguntas(_objevaluacion);
                        break;

                    case "Padre de Familia":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Padres = true;
                        CargarPreguntas(_objevaluacion);
                        break;

                    case "Directivos":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Directivos = true;
                        CargarPreguntas(_objevaluacion);
                        break;

                    case "Secretaria de Educacion":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.SecretariaEducacion = true;
                        CargarPreguntas(_objevaluacion);
                        break;
                }


            }
            else
            {
                lblerrorAc.Text = _objevaluacion.Error;
                lblerrorAc.Visible = true;
                informacionuno.Visible = false;
                gvAmb1.Visible = false;
                gvAmb2.Visible = false;
                gvAmb3.Visible = false;
                gvAmb4.Visible = false;
                gvAmb5.Visible = false;
                gvAmb1.DataSource = null;
                gvAmb2.DataSource = null;
                gvAmb3.DataSource = null;
                gvAmb4.DataSource = null;
                gvAmb5.DataSource = null;
                gvAmb1.DataBind();
                gvAmb2.DataBind();
                gvAmb3.DataBind();
                gvAmb4.DataBind();
                gvAmb5.DataBind();
            }

            switch (actorSeleccionado)
            {
                case "Estudiante":

                    Actorespnl.BackColor = System.Drawing.ColorTranslator.FromHtml("#005ea7");
                    break;

                case "Profesional de Campo":
                    Actorespnl.BackColor = System.Drawing.ColorTranslator.FromHtml("#9c0d16");
                    break;

                case "Educador":
                    Actorespnl.BackColor = System.Drawing.ColorTranslator.FromHtml("#3f9c0d");
                    break;

                case "Padre de Familia":
                    Actorespnl.BackColor = System.Drawing.ColorTranslator.FromHtml("#f19300");
                    break;

                case "Directivos":
                    Actorespnl.BackColor = System.Drawing.ColorTranslator.FromHtml("#e0081d");
                    break;

                case "Secretaria de Educacion":
                    Actorespnl.BackColor = System.Drawing.ColorTranslator.FromHtml("#005ea7");
                    break;

            }

        }

        #endregion

        #region Eventos Para Secretaría de Educación
        protected void gvSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objRow = gvSE.SelectedRow;

            _objevaluacion = new EvaluationSettings.CEvaluacion();
            _objevaluacion.SecretariaEducacion = true;

            lblCodIe.Text = objRow.Cells[2].Text;
            lblIE.Text = objRow.Cells[3].Text;
            lblMunicipio.Text = objRow.Cells[4].Text;
            Session.Add("idie", lblCodIe.Text);
            Session.Add("idmedicion", _objevaluacion.CrearMedion());

            adocumentos.Title = "Información ESM";
            adocumentos.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/ModuloDocumentos.aspx?idmedicion=" + Session["idmedicion"].ToString() + "&iframe=true&amp;width=100%&amp;height=100%";

            #region Visualizacion Controles

            ModMediciones.Visible = false;
            ModEvaluacion.Visible = true;
            informacionuno.Visible = true;
            ModDocumentos.Visible = true;
            modSEseleccion.Visible = false;
            #endregion

        }
        #endregion

        protected void objtimer_Tick(object sender, EventArgs e)
        {
            if (cboActores.SelectedItem.Text != "No Asignado" && ModEvaluacion.Visible == true && gvAmb1.Rows.Count != 0 && gvAmb2.Rows.Count != 0 && gvAmb3.Rows.Count != 0 && gvAmb4.Rows.Count != 0 && gvAmb5.Rows.Count != 0)
            {
                bool exist = false;
                if (Session["ideval"] != null)
                {
                    //AlmacenarParcialDefinitiva(false);
                }
                else if (Session["ideval"] == null)
                {
                    string idactor = cboActores.SelectedValue;
                    for (int i = 0; i < gvTopEval.Rows.Count; i++)
                    {
                        if (idactor == gvTopEval.Rows[i].Cells[2].Text)
                            exist = true;
                    }
                    if (!exist)
                    {
                        //AlmacenarInformacion(true);
                        Session.Add("ideval", _objevaluacion.IdEvaluacion);
                    }
                }

            }
        }

        protected void gvSE_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvResultados.PageIndex = e.NewPageIndex;
            CRoles objCRoles = new CRoles();

            int idusuario = Convert.ToInt32(Session["idusuario"]);
            string rol = objCRoles.ObtenerRol(idusuario);

            if (_tipo == 2)
            {
                if (rol == "Administrador")
                {
                    /*Cargo el control gridview con el data source obtenido de instituciones educativas*/
                    gvResultados.DataSourceID = "ldsies";
                }
                else if (rol == "Consultor" || rol == "MEN" || rol == "Revisor")
                {
                    gvResultados.DataSource = CEE.ObtenerEEs(objCRoles.IdConsultor);
                    gvResultados.DataBind();
                }

                ObtenerTema(gvResultados);


            }
        }

        protected void gvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                this.gvResultados.PageIndex = e.NewPageIndex;
                CRoles objCRoles = new CRoles();

                int idusuario = Convert.ToInt32(Session["idusuario"]);
                string rol = objCRoles.ObtenerRol(idusuario);


                if (rol == "Administrador")
                {
                    gvResultados.DataSource = CEE.ObtenerEEs(objCRoles.IdConsultor, false, true);
                    gvResultados.DataBind();
                }
                else if (rol == "Consultor" || rol == "MEN" || rol == "Revisor")
                {
                    gvResultados.DataSource = CEE.ObtenerEEs(objCRoles.IdConsultor);
                    gvResultados.DataBind();
                }

                ObtenerTema(gvResultados);

            }
            catch (Exception) { }

        }


    }
}