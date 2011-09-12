using System;
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

namespace ESM.Evaluacion
{
    public partial class Evaluacion : System.Web.UI.Page
    {
        #region Propiedades Privadas y Publicas
        protected EvaluationSettings.CEvaluacion _objevaluacion = new EvaluationSettings.CEvaluacion();
        int _tipo = 0;
        #endregion

        #region Almacenar Informacion de Evaluaciones

        protected void AlmacenarParcialDefinitiva(bool estado)
        {
            if (Session["ideval"] != null)
            {
                int eval = Convert.ToInt32(Session["ideval"]);
                #region Almacena Evaluacion
                bool valid = true;
                for (int i = 0; i < gvEvaluacion.Rows.Count; i++)
                {
                    GridViewRow objGridViewRow = gvEvaluacion.Rows[i];
                    Label objIdPregunta = (Label)objGridViewRow.Cells[1].FindControl("lblIdPregunta");
                    RadioButton objsi = (RadioButton)objGridViewRow.Cells[1].FindControl("rbtnSi");
                    RadioButton objno = (RadioButton)objGridViewRow.Cells[1].FindControl("rbtnNo");
                    CheckBox objnoapli = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNoAplica");

                    if (objsi.Checked == false && objno.Checked == false)
                    {
                        valid = false;
                    }
                }

                if (estado)
                {
                    if (valid)
                    {
                        #region Almacena Resultados Evaluacion Definitiva

                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        object[,] CollectionResultados = null;

                        CollectionResultados = new object[gvEvaluacion.Rows.Count, 4];

                        for (int e = 0; e < gvEvaluacion.Rows.Count; e++)
                        {
                            if (gvEvaluacion.Rows[e].Enabled == true)
                            {

                                GridViewRow objGridViewRow = gvEvaluacion.Rows[e];
                                Label objIdPregunta = (Label)objGridViewRow.Cells[1].FindControl("lblIdPregunta");
                                RadioButton objsi = (RadioButton)objGridViewRow.Cells[1].FindControl("rbtnSi");
                                RadioButton objno = (RadioButton)objGridViewRow.Cells[1].FindControl("rbtnNo");
                                CheckBox objpendiente = (CheckBox)objGridViewRow.Cells[1].FindControl("chxPendiente");
                                CheckBox objnoapli = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNoAplica");
                                TextBox objTextBox = (TextBox)objGridViewRow.FindControl("txtSesion");

                                if (objsi.Checked)
                                {

                                    CollectionResultados[e, 0] = Convert.ToInt32(objIdPregunta.Text);
                                    CollectionResultados[e, 1] = true;

                                }
                                else if (objno.Checked)
                                {
                                    CollectionResultados[e, 0] = Convert.ToInt32(objIdPregunta.Text);
                                    CollectionResultados[e, 1] = false;
                                }
                                else if (!objno.Checked && !objsi.Checked)
                                {
                                    CollectionResultados[e, 0] = Convert.ToInt32(objIdPregunta.Text);
                                    CollectionResultados[e, 1] = null;

                                }
                                CollectionResultados[e, 2] = objTextBox.Text;
                                CollectionResultados[e, 3] = objpendiente.Checked;
                            }
                        }

                        if (_objevaluacion.ActualizarEvaluacion(CollectionResultados, eval, estado))
                        {
                            Alert.Show(udpnlFiltro, "Actualización exitosa");
                            FinalizarProcesoEvaluacionEstado();
                        }
                        else
                        {
                            Alert.Show(udpnlFiltro, "Actualización exitosa.");
                            FinalizarProcesoEvaluacionEstado();
                        }

                        #endregion
                    }
                    else
                    {
                        Alert.Show(udpnlFiltro, "Actualización Falló.");
                        FinalizarProcesoEvaluacionEstado();
                    }
                }
                if (!estado)
                {
                    #region Almacena Resultados Evaluacion Definitiva

                    _objevaluacion = new EvaluationSettings.CEvaluacion();
                    object[,] CollectionResultados = CollectionResultados = new object[gvEvaluacion.Rows.Count, 4];

                    for (int e = 0; e < gvEvaluacion.Rows.Count; e++)
                    {
                        if (gvEvaluacion.Rows[e].Enabled == true)
                        {

                            GridViewRow objGridViewRow = gvEvaluacion.Rows[e];
                            Label objIdPregunta = (Label)objGridViewRow.Cells[1].FindControl("lblIdPregunta");
                            CheckBox objsi = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnSi");
                            CheckBox objno = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNo");
                            CheckBox objpendiente = (CheckBox)objGridViewRow.Cells[1].FindControl("chxPendiente");
                            CheckBox objnoapli = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNoAplica");
                            TextBox objTextBox = (TextBox)objGridViewRow.FindControl("txtSesion");

                            CollectionResultados[e, 0] = Convert.ToInt32(objIdPregunta.Text);
                            if (objsi.Checked)
                                CollectionResultados[e, 1] = true;
                            else if (objno.Checked)
                                CollectionResultados[e, 1] = false;

                            CollectionResultados[e, 2] = objTextBox.Text;
                            CollectionResultados[e, 3] = objpendiente.Checked;

                        }
                    }

                    if (_objevaluacion.ActualizarEvaluacion(CollectionResultados, eval, estado))
                    {
                        Session.Remove("ideval");
                        Alert.Show(udpnlFiltro, "Actualizado Correctamente");
                        FinalizarProcesoEvaluacionEstado();
                    }

                    #endregion
                }
                #endregion
            }
        }

        protected void FinalizarProcesoEvaluacionEstado()
        {
            gvEvaluacion.Visible = false;
            informacionuno.Visible = false;
            btnalmacenarparcial.Visible = false;
            btnDefinitiva.Visible = false;
            gvMediciones.DataBind();
            gvTopEval.DataBind();
            btnVolverEE.Visible = true;
        }

        protected void AlmacenarInformacion(bool estado)
        {
            bool valid = true;
            for (int i = 0; i < gvEvaluacion.Rows.Count; i++)
            {
                GridViewRow objGridViewRow = gvEvaluacion.Rows[i];
                Label objIdPregunta = (Label)objGridViewRow.Cells[1].FindControl("lblIdPregunta");
                RadioButton objsi = (RadioButton)objGridViewRow.Cells[1].FindControl("rbtnSi");
                RadioButton objno = (RadioButton)objGridViewRow.Cells[1].FindControl("rbtnNo");
                CheckBox objnoapli = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNoAplica");

                if (objsi.Checked == false && objno.Checked == false)
                {
                    valid = false;
                }
            }

            if (!estado)
            {
                if (valid)
                {
                    _objevaluacion = new EvaluationSettings.CEvaluacion();

                    object[,] CollectionResultados = new object[gvEvaluacion.Rows.Count, 4];

                    for (int i = 0; i < gvEvaluacion.Rows.Count; i++)
                    {
                        GridViewRow objGridViewRow = gvEvaluacion.Rows[i];
                        Label objIdPregunta = (Label)objGridViewRow.Cells[1].FindControl("lblIdPregunta");
                        RadioButton objsi = (RadioButton)objGridViewRow.Cells[1].FindControl("rbtnSi");
                        RadioButton objno = (RadioButton)objGridViewRow.Cells[1].FindControl("rbtnNo");
                        CheckBox objpendiente = (CheckBox)objGridViewRow.Cells[1].FindControl("chxPendiente");
                        TextBox objTextBox = (TextBox)objGridViewRow.FindControl("txtsesion");

                        CollectionResultados[i, 0] = Convert.ToInt32(objIdPregunta.Text);
                        if (objsi.Checked)
                            CollectionResultados[i, 1] = true;
                        else if (objno.Checked)
                            CollectionResultados[i, 1] = false;

                        CollectionResultados[i, 2] = objTextBox.Text;
                        CollectionResultados[i, 3] = objpendiente.Checked;
                    }

                    int idie = Convert.ToInt32(Session["idie"]);
                    int idmedicion = Convert.ToInt32(Session["idmedicion"]);
                    int idactor = 0;

                    idactor = Convert.ToInt32(cboActores.SelectedItem.Value);

                    int idusuario = Convert.ToInt32(Session["idusuario"]);

                    if (_objevaluacion.Almacenar(CollectionResultados, idie, idmedicion, idactor, idusuario, estado, _tipo))
                    {
                        lbloki.Visible = true;
                        if (estado)
                            lbloki.InnerHtml = "Evaluación Almacenada. \nEstado: Parcial.";
                        else
                            lbloki.InnerHtml = "Evaluación Almacenada. \nEstado: Terminada.";
                        FinalizarProcesoEvaluacionEstado();
                    }
                    else
                    {
                        lbloki.InnerHtml = "Guardado Fallido.";
                        lbloki.Visible = true;
                        FinalizarProcesoEvaluacionEstado();
                    }
                }
                else
                {
                    Alert.Show(udpnlFiltro, "No se pudo completar el proceso de almacenamiento. \n Faltan preguntas por responder.");
                }
            }
            else
            {
                _objevaluacion = new EvaluationSettings.CEvaluacion();

                object[,] CollectionResultados = new object[gvEvaluacion.Rows.Count, 4];

                for (int i = 0; i < gvEvaluacion.Rows.Count; i++)
                {
                    GridViewRow objGridViewRow = gvEvaluacion.Rows[i];
                    Label objIdPregunta = (Label)objGridViewRow.Cells[1].FindControl("lblIdPregunta");
                    RadioButton objsi = (RadioButton)objGridViewRow.Cells[1].FindControl("rbtnSi");
                    RadioButton objno = (RadioButton)objGridViewRow.Cells[1].FindControl("rbtnNo");
                    CheckBox objpendiente = (CheckBox)objGridViewRow.Cells[1].FindControl("chxPendiente");
                    TextBox objTextBox = (TextBox)objGridViewRow.FindControl("txtsesion");

                    CollectionResultados[i, 0] = Convert.ToInt32(objIdPregunta.Text);
                    if (objsi.Checked)
                        CollectionResultados[i, 1] = true;
                    else if (objno.Checked)
                        CollectionResultados[i, 1] = false;

                    CollectionResultados[i, 2] = objTextBox.Text;
                    CollectionResultados[i, 3] = objpendiente.Checked;
                }

                int idie = Convert.ToInt32(Session["idie"]);
                int idmedicion = Convert.ToInt32(Session["idmedicion"]);
                int idactor = 0;

                idactor = Convert.ToInt32(cboActores.SelectedItem.Value);

                int idusuario = Convert.ToInt32(Session["idusuario"]);

                if (_objevaluacion.Almacenar(CollectionResultados, idie, idmedicion, idactor, idusuario, estado, _tipo))
                {
                    lbloki.Visible = true;
                    if (estado)
                        lbloki.InnerHtml = "Evaluación Almacenada. \nEstado: Parcial.";
                    else
                        lbloki.InnerHtml = "Evaluación Almacenada. \nEstado: Terminada.";
                    FinalizarProcesoEvaluacionEstado();
                }
                else
                {
                    lbloki.InnerHtml = "Guardado Fallido.";
                    lbloki.Visible = true;
                    FinalizarProcesoEvaluacionEstado();
                }
            }
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

        protected void btnalmacenarparcial_Click(object sender, EventArgs e)
        {
            bool exist = false;
            if (Session["ideval"] != null)
            {
                string idactor = cboActores.SelectedValue;
                for (int i = 0; i < gvTopEval.Rows.Count; i++)
                {
                    if (idactor == gvTopEval.Rows[i].Cells[2].Text)
                        exist = true;
                }
            }
            if (!exist)
                AlmacenarInformacion(true);
            else
                AlmacenarParcialDefinitiva(false);
        }

        protected void btnDefinitiva_Click(object sender, EventArgs e)
        {
            bool exist = false;
            if (Session["ideval"] != null)
            {
                string idactor = cboActores.SelectedValue;
                for (int i = 0; i < gvTopEval.Rows.Count; i++)
                {
                    if (idactor == gvTopEval.Rows[i].Cells[2].Text)
                        exist = true;
                }
            }
            if (!exist)
                AlmacenarInformacion(false);
            else
                AlmacenarParcialDefinitiva(true);
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
                            gvResultados.DataSourceID = "ldsies";
                        }
                        else if (rol == "Consultor")
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
        protected void gvEvaluacion_RowDataBound(object sender, GridViewRowEventArgs e)
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
                        objTextBox.CssClass = String.Concat("sesion_", lblIdPregunta.Text);
                        objrbtnSi.ToolTip = lblIdPregunta.Text;
                        objrbtnNo.ToolTip = lblIdPregunta.Text;
                        objrbtnSi.CssClass = "radiosi";
                        objrbtnNo.CssClass = "radiono";
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

                for (int i = 0; i < gvMediciones.Rows.Count; i++)
                {
                    if (i == gvMediciones.Rows.Count - 1)
                        gvMediciones.Rows[i].Visible = true;
                    else
                        gvMediciones.Rows[i].Visible = false;
                }
                #endregion


            }
            else
            {
                string mensaje = "No Existen Mediciones para el establecimiento educativo.";
                ScriptManager.RegisterStartupScript(udpnlFiltro, GetType(), "scriptalert", String.Format("alert('{0}');", mensaje), true);

                #region Visualizacion de Controles

                modSEseleccion.Visible = false;
                modEESeleccion.Visible = false;
                ModMediciones.Visible = true;
                btnMedicion.Visible = true;
                gvMediciones.Visible = true;
                modEESeleccion.Visible = false;
                #endregion


            }
            ScriptManager.RegisterStartupScript(udpnlFiltro, udpnlFiltro.GetType(), new Guid().ToString(), "$(document).ready(function () { $.scrollTo('#btnMedicion', 800, { easing: 'elasout' });});", true);
        }

        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            Filtro(txtFiltro.Text);
        }

        protected void ldsIes_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {


        }

        protected void cboActores_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                gvEvaluacion.Visible = true;
            }
            else
            {
                Alert.Show(udpnlFiltro, "El actor seleccionado ya fue evaluado en la medicion actual.");
                gvEvaluacion.Visible = false;
                btnalmacenarparcial.Visible = false;
                btnDefinitiva.Visible = false;
                informacionuno.Visible = false;
            }


        }

        protected void gvTopEval_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvEvaluacion.Enabled = true;
            GridViewRow objrow = gvTopEval.SelectedRow;

            int idevaluacion = Convert.ToInt32(objrow.Cells[1].Text);
            int idactor = Convert.ToInt32(objrow.Cells[2].Text);

            VisualizacionControles();
            CargarParcial(idevaluacion, idactor);
            #region Visualizacion de Controles
            ModDocumentos.Visible = true;
            ModEvaluacion.Visible = true;
            btnVolverEE.Visible = true;
            #endregion

        }

        protected void lbtnVolver_Click(object sender, EventArgs e)
        {
            VolverSeleccion();
        }

        protected void btnMedicion_Click(object sender, EventArgs e)
        {
            int idmedicion = _objevaluacion.CrearMedion();
            Session.Add("idmedicion", idmedicion);
            btnMedicion.Visible = false;
            VisualizacionControles();
            adocumentos.Title = "Información ESM";
            adocumentos.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/ModuloDocumentos.aspx?idmedicion=" + idmedicion.ToString() + "&iframe=true&amp;width=100%&amp;height=100%";
            #region Visualizacion de Controles
            btnMedicion.Visible = false;
            gvTopEval.Visible = false;
            ModMediciones.Visible = true;
            ModDocumentos.Visible = true;
            ModEvaluacion.Visible = true;
            gvEvaluacion.Visible = true;
            gvMediciones.Visible = false;
            cboActores.SelectedIndex = 5;
            btnVolverEE.Visible = false;
            gvEvaluacion.DataSource = null;
            gvEvaluacion.DataBind();
            btnalmacenarparcial.Visible = false;
            btnDefinitiva.Visible = false;
            informacionuno.Visible = false;
            #endregion

        }

        protected void gvMediciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objrow = this.gvMediciones.SelectedRow;

            int idmedicion = Convert.ToInt32(objrow.Cells[1].Text);
            TopEvaluaciones(idmedicion);
            ObtenerActoresEvaluar(gvTopEval);

            #region Visualizacion de Controles
            btnMedicion.Visible = false;
            gvTopEval.Visible = true;
            ModTopEval.Visible = true;
            ModMediciones.Visible = true;
            ModDocumentos.Visible = true;
            ModEvaluacion.Visible = true;
            cboActores.SelectedValue = "7";

            adocumentos.Title = "Información ESM";
            adocumentos.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/ModuloDocumentos.aspx?idmedicion=" + idmedicion.ToString() + "&iframe=true&amp;width=100%&amp;height=100%";
            #endregion

        }
        #endregion

        #region Carga de Evaluaciones Parciales
        protected void CargarParcial(int ideval, int idactor)
        {
            gvEvaluacion.Enabled = true;

            Session.Add("ideval", ideval);

            gvEvaluacion.DataSource = _objevaluacion.LoadEvalParcial(ideval, idactor);
            gvEvaluacion.DataBind();

            ObtenerTema(gvEvaluacion);

            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

            var resultbyeval = from rbe in db.ResultadosByEvaluacion
                               join res in db.Resultados on rbe.IdResultado equals res.IdResultados
                               join pre in db.Preguntas on res.IdPregunta equals pre.IdPregunta
                               where rbe.IdEvaluacion == ideval
                               select new { res.IdPregunta, res.IdResultados, res.Valor, res.Pendiente, pre.Pregunta, res.Sesiones };

            var listresults = resultbyeval.ToList();

            for (int i = 0; i < gvEvaluacion.Rows.Count; i++)
            {
                for (int e = 0; e < listresults.Count; e++)
                {
                    Label lbllp = (Label)gvEvaluacion.Rows[i].Cells[1].FindControl("lblLP");
                    Label idpregunta = (Label)gvEvaluacion.Rows[i].Cells[1].FindControl("lblIdPregunta");
                    TextBox objSesiones = (TextBox)gvEvaluacion.Rows[i].Cells[1].FindControl("txtsesion");
                    CheckBox objprendiente = (CheckBox)gvEvaluacion.Rows[i].Cells[1].FindControl("chxPendiente");
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

                        ESM.Model.Preguntas objPreguntas = _objevaluacion.ObtenerDatosPregunta(Convert.ToInt32(idpregunta.Text));
                        if ((bool)objPreguntas.Sesiones)
                        {
                            objSesiones.Visible = true;
                            objSesiones.Enabled = false;
                            objSesiones.Text = listresults[e].Sesiones.ToString();
                        }
                        if (listresults[e].Valor == true)
                        {
                            RadioButton objsi = (RadioButton)gvEvaluacion.Rows[i].Cells[1].FindControl("rbtnSi");
                            objsi.Checked = true;
                            objSesiones.Enabled = true;
                        }
                        else if (listresults[e].Valor == false)
                        {
                            RadioButton objno = (RadioButton)gvEvaluacion.Rows[i].Cells[1].FindControl("rbtnNo");
                            objno.Checked = true;
                        }




                        break;

                    }

                }

            }
            cboActores.SelectedValue = idactor.ToString();
            //lblActorEvaluado.Text = cboActores.SelectedItem.Text;

            var estadoeval = from e in db.Evaluacion
                             where e.IdEvaluacion == ideval
                             select e;

            var evalsta = estadoeval.ToList();

            if (evalsta[0].IdEstado == 1)
            {
                gvEvaluacion.Visible = true;
                gvEvaluacion.Enabled = false;
                informacionuno.Visible = false;
                btnalmacenarparcial.Visible = false;
                btnDefinitiva.Visible = false;
            }
            else
            {
                gvEvaluacion.Visible = true;
                informacionuno.Visible = true;
                btnalmacenarparcial.Visible = true;
                btnDefinitiva.Visible = true;
            }



        }
        #endregion

        #region Metodos

        /// <summary>
        /// Carga el datasource del gridview evaluacion deacuerdo a al actor seleccionado
        /// </summary>
        /// <param name="objevaluacion"></param>
        protected void CargarPreguntas(EvaluationSettings.CEvaluacion objevaluacion)
        {
            try
            {
                IQueryable<ESM.Model.Preguntas> cpreguntas = objevaluacion.LoadEvaluation();
                //Llama al metodo load del objeto CEvaluacion que obtiene el Iqueryable para asignar al datasource del control
                gvEvaluacion.DataSource = cpreguntas;
                //Actualizo el control Gridview en el formulario para que almacene los cambio realizados
                gvEvaluacion.DataBind();
                //Asigna el tema para los griviews
                ObtenerTema(gvEvaluacion);

                for (int i = 0; i < gvEvaluacion.Rows.Count; i++)
                {
                    Label objlabel = (Label)gvEvaluacion.Rows[i].FindControl("lblIdPregunta");
                    TextBox objtextbox = (TextBox)gvEvaluacion.Rows[i].FindControl("txtsesion");
                    CheckBox objcheckbox = (CheckBox)gvEvaluacion.Rows[i].FindControl("chxPendiente");
                    Label objlabelLP = (Label)gvEvaluacion.Rows[i].FindControl("lblLP");
                    int idPregunta = Convert.ToInt32(objlabel.Text);
                    foreach (var item in cpreguntas)
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
                        }

                    }
                }





            }
            /*En caso de presentar excepcion retorno una alerta en javascript que me muestra y me controla el error presentado*/
            catch (Exception) { Response.Write("<script>alert('Ocurrio un error inesperado.');</script>"); }
        }

        /// <summary>
        /// Controla la visualizacion de los controles deacuero a un flujo establecido
        /// </summary>
        protected void VisualizacionControles()
        {
            //cboActores.Enabled = true;
            //txtFiltro.Enabled = false;
            //btnBuscar.Enabled = false;
            //gvResultados.Enabled = false;
            //titulo3.Visible = true;
            //titulo21.Visible = true;
            //tituloeval.Visible = true;
            //lbtnVolver.Visible = true;
            //titulo1ie.Visible = false;
            //infoEval.Visible = true;
            //titulo22.Visible = true;
            //gvResultados.Visible = false;
            //filtrosp.Visible = false;
            //btnalmacenarparcial.Visible = true;
            //btnDefinitiva.Visible = true;
            //cboActores.SelectedValue = "2";
            //gvTopEval.Visible = true;
            //infoEval.Visible = true;
            //gvEvaluacion.Visible = true;
            //informacionuno.Visible = true;
            //EvaluarActorSeleccionado("Secretaria de Educacion", 5);


        }

        protected bool Filtro(string texto)
        {
            try
            {
                /*Instancio*/
                Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                var rFiltro = from i in db.Establecimiento_Educativo
                              where i.Nombre.Contains(texto)
                              select i;

                gvResultados.DataSourceID = null;
                gvResultados.DataSource = rFiltro;
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
            ///TODO: JCMM: Agregar Alerta para este metodo
        }

        protected void EvaluarActorSeleccionado(string actorSeleccionado, int idactor)
        {
            btnVolverEE.Visible = false;
            gvEvaluacion.Enabled = true;
            gvEvaluacion.Visible = true;
            lblerrorAc.Visible = false;
            lbloki.Visible = false;
            informacionuno.Visible = true;
            btnalmacenarparcial.Visible = true;
            btnDefinitiva.Visible = true;

            int idie = Convert.ToInt32(Session["idie"]);
            int idmedicion = Convert.ToInt32(Session["idmedicion"]);

            if (_objevaluacion.ValidarActores(idie, idactor, idmedicion))
            {
                switch (actorSeleccionado)
                {
                    case "No Asignado":
                        gvEvaluacion.DataSource = null;
                        gvEvaluacion.DataBind();
                        btnalmacenarparcial.Visible = false;
                        btnDefinitiva.Visible = false;
                        informacionuno.Visible = false;
                        break;

                    case "Estudiante":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        Actorespnl.BackColor = System.Drawing.Color.Black;
                        _objevaluacion.Estudiantes = true;
                        CargarPreguntas(_objevaluacion);
                        //lblActorEvaluado.Text = "Estudiante";
                        gvEvaluacion.Visible = true;
                        btnDefinitiva.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Profesional de Campo":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Profesional = true;
                        CargarPreguntas(_objevaluacion);
                        //lblActorEvaluado.Text = "Profesional de Campo";
                        gvEvaluacion.Visible = true;
                        btnDefinitiva.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Educador":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Docentes = true;
                        CargarPreguntas(_objevaluacion);
                        //lblActorEvaluado.Text = "Educador";
                        gvEvaluacion.Visible = true;
                        btnDefinitiva.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Padre de Familia":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Padres = true;
                        CargarPreguntas(_objevaluacion);
                        //lblActorEvaluado.Text = "Padre de Familia";
                        gvEvaluacion.Visible = true;
                        btnDefinitiva.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Directivos":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Directivos = true;
                        CargarPreguntas(_objevaluacion);
                        //lblActorEvaluado.Text = "Directivos";
                        gvEvaluacion.Visible = true;
                        btnDefinitiva.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Secretaria de Educacion":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.SecretariaEducacion = true;
                        CargarPreguntas(_objevaluacion);
                        //lblActorEvaluado.Text = "Secretaria de Educacion";
                        gvEvaluacion.Visible = true;
                        btnDefinitiva.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;
                }


            }
            else
            {
                lblerrorAc.Text = _objevaluacion.Error;
                lblerrorAc.Visible = true;
                gvEvaluacion.Visible = false;
                btnalmacenarparcial.Visible = false;
                btnDefinitiva.Visible = false;
                informacionuno.Visible = false;
                btnVolverEE.Visible = true;
                gvEvaluacion.DataSource = null;
                gvEvaluacion.DataBind();

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

        protected void VolverSeleccion()
        {
            //divcarga.Visible = false;
            //txtFiltro.Enabled = true;
            //btnBuscar.Enabled = true;
            //titulo21.Visible = false;
            //gvResultados.Enabled = true;
            //titulo3.Visible = false;
            //tituloeval.Visible = false;
            //titulo1ie.Visible = true;
            //infoEval.Visible = false;
            //lbloki.Visible = false;
            //lbtnVolver.Visible = false;
            //titulo22.Visible = false;
            //gvResultados.Visible = true;
            //filtrosp.Visible = true;
            //gvTopEval.Visible = false;
            //infoEval.Visible = false;
            //gvEvaluacion.Visible = false;
            //btnalmacenarparcial.Visible = false;
            //btnDefinitiva.Visible = false;
        }

        protected void ObtenerActoresEvaluar(GridView ActoresExist)
        {
            //cboActores.DataBind();

            //for (int i = 0; i < ActoresExist.Rows.Count; i++)
            //{
            //    string idactor = ActoresExist.Rows[i].Cells[2].Text;
            //    for (int a = 0; a < cboActores.Items.Count; a++)
            //    {
            //        if (cboActores.Items[a].Value == idactor)
            //            cboActores.Items.Remove(new ListItem().Value = idactor.ToString());

            //    }
            //    cboActores.Enabled = true;
            //}

        }

        #endregion

        #region Eventos Para Secretaría de Educación
        protected void gvSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objRow = gvSE.SelectedRow;

            _objevaluacion = new EvaluationSettings.CEvaluacion();
            _objevaluacion.SecretariaEducacion = true;
            gvEvaluacion.DataSource = _objevaluacion.LoadEvaluation();
            gvEvaluacion.DataBind();

            //lblActorEvaluado.Text = "Secretaria de Educacion";

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
            btnDefinitiva.Visible = true;
            btnalmacenarparcial.Visible = true;
            informacionuno.Visible = true;
            ModDocumentos.Visible = true;
            modSEseleccion.Visible = false;
            #endregion
            ObtenerTema(gvEvaluacion);

        }
        #endregion
    }
}