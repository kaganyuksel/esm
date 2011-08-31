using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI.HtmlControls;



namespace ESM.Evaluacion
{
    public partial class Evaluacion : System.Web.UI.Page
    {
        #region Propiedades Privadas y Publicas

        protected EvaluationSettings.CEvaluacion _objevaluacion = new EvaluationSettings.CEvaluacion();

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
                    CheckBox objsi = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnSi");
                    CheckBox objno = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNo");
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
                        int contador = 0;
                        object[,] CollectionResultados = null;
                        for (int i = 0; i < gvEvaluacion.Rows.Count; i++)
                        {
                            if (gvEvaluacion.Rows[i].Enabled == true)
                                contador++;
                        }
                        CollectionResultados = new object[contador, 2];


                        for (int e = 0; e < gvEvaluacion.Rows.Count; e++)
                        {
                            if (gvEvaluacion.Rows[e].Enabled == true)
                            {

                                GridViewRow objGridViewRow = gvEvaluacion.Rows[e];
                                Label objIdPregunta = (Label)objGridViewRow.Cells[1].FindControl("lblIdPregunta");
                                CheckBox objsi = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnSi");
                                CheckBox objno = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNo");
                                CheckBox objnoapli = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNoAplica");

                                if (objsi.Checked)
                                {
                                    for (int i = 0; i < CollectionResultados.GetLength(0); i++)
                                    {
                                        CollectionResultados[i, 0] = Convert.ToInt32(objIdPregunta.Text);
                                        CollectionResultados[i, 1] = true;
                                        break;
                                    }

                                }
                                else if (objno.Checked)
                                {
                                    for (int i = 0; i < CollectionResultados.GetLength(0); i++)
                                    {
                                        CollectionResultados[e, 0] = Convert.ToInt32(objIdPregunta.Text);
                                        CollectionResultados[e, 1] = false;
                                        break;
                                    }
                                }
                                else if (objnoapli.Checked)
                                {
                                    for (int i = 0; i < CollectionResultados.GetLength(0); i++)
                                    {
                                        CollectionResultados[e, 0] = Convert.ToInt32(objIdPregunta.Text);
                                        CollectionResultados[e, 1] = null;
                                        break;
                                    }
                                }
                            }
                        }

                        if (_objevaluacion.ActualizarEvaluacion(CollectionResultados, eval, estado))
                        {

                            //ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
                            //var evaluacion = (from e in db.Evaluacion
                            //                  where e.IdEvaluacion == eval
                            //                  select e).Single();

                            //evaluacion.IdEstado = 1;

                            //db.SubmitChanges();

                            //gvEvaluacion.Visible = false;
                            //btnDefinitiva.Visible = false;
                            //btnalmacenarparcial.Visible = false;
                            //lbloki.InnerText = "La evaluacion fue Actualizada correctamente.";


                        }

                        #endregion
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(udpnlFiltro, GetType(), "validevaluation", String.Format("alert('{0}');", "No se puede en estado cerrada, no se contestaron algunas preguntas."), true);
                    }
                }
                if (!estado)
                {
                    #region Almacena Resultados Evaluacion Definitiva

                    _objevaluacion = new EvaluationSettings.CEvaluacion();
                    int contador = 0;
                    object[,] CollectionResultados = null;
                    for (int i = 0; i < gvEvaluacion.Rows.Count; i++)
                    {
                        if (gvEvaluacion.Rows[i].Enabled == true)
                            contador++;
                    }
                    CollectionResultados = new object[contador, 2];


                    for (int e = 0; e < gvEvaluacion.Rows.Count; e++)
                    {
                        if (gvEvaluacion.Rows[e].Enabled == true)
                        {

                            GridViewRow objGridViewRow = gvEvaluacion.Rows[e];
                            Label objIdPregunta = (Label)objGridViewRow.Cells[1].FindControl("lblIdPregunta");
                            CheckBox objsi = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnSi");
                            CheckBox objno = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNo");
                            CheckBox objnoapli = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNoAplica");

                            if (objsi.Checked)
                            {
                                for (int i = 0; i < CollectionResultados.GetLength(0); i++)
                                {
                                    CollectionResultados[i, 0] = Convert.ToInt32(objIdPregunta.Text);
                                    CollectionResultados[i, 1] = true;
                                    break;
                                }

                            }
                            else if (objno.Checked)
                            {
                                for (int i = 0; i < CollectionResultados.GetLength(0); i++)
                                {
                                    CollectionResultados[e, 0] = Convert.ToInt32(objIdPregunta.Text);
                                    CollectionResultados[e, 1] = false;
                                    break;
                                }
                            }
                            else if (objnoapli.Checked)
                            {
                                for (int i = 0; i < CollectionResultados.GetLength(0); i++)
                                {
                                    CollectionResultados[e, 0] = Convert.ToInt32(objIdPregunta.Text);
                                    CollectionResultados[e, 1] = null;
                                    break;
                                }
                            }
                        }
                    }

                    if (_objevaluacion.ActualizarEvaluacion(CollectionResultados, eval, estado))
                    {

                        //ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
                        //var evaluacion = (from e in db.Evaluacion
                        //                  where e.IdEvaluacion == eval
                        //                  select e).Single();

                        //evaluacion.IdEstado = 1;

                        //db.SubmitChanges();

                        //gvEvaluacion.Visible = false;
                        //btnDefinitiva.Visible = false;
                        //btnalmacenarparcial.Visible = false;
                        //lbloki.InnerText = "La evaluacion fue Actualizada correctamente.";


                    }

                    #endregion
                }
                #endregion
            }

            Session.Remove("ideval");
        }

        protected void AlmacenarInformacion(bool estado)
        {

            _objevaluacion = new EvaluationSettings.CEvaluacion();

            object[,] CollectionResultados = new object[gvEvaluacion.Rows.Count, 2];

            for (int i = 0; i < gvEvaluacion.Rows.Count; i++)
            {
                GridViewRow objGridViewRow = gvEvaluacion.Rows[i];
                Label objIdPregunta = (Label)objGridViewRow.Cells[1].FindControl("lblIdPregunta");
                CheckBox objsi = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnSi");
                CheckBox objno = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNo");

                if (objsi.Checked)
                {
                    CollectionResultados[i, 0] = Convert.ToInt32(objIdPregunta.Text);
                    CollectionResultados[i, 1] = true;
                }
                else if (objno.Checked)
                {
                    CollectionResultados[i, 0] = Convert.ToInt32(objIdPregunta.Text);
                    CollectionResultados[i, 1] = false;
                }
            }

            int idie = Convert.ToInt32(Session["idie"]);
            int idmedicion = Convert.ToInt32(Session["idmedicion"]);
            int idactor = Convert.ToInt32(cboActores.SelectedItem.Value);
            int idusuario = Convert.ToInt32(Session["idusuario"]);

            if (_objevaluacion.Almacenar(CollectionResultados, idie, idmedicion, idactor, idusuario, estado))
            {
                gvEvaluacion.Visible = false;
                btnalmacenarparcial.Visible = false;
                string mensaje = "La Evaluación se almaceno correctamente.";
                ScriptManager.RegisterStartupScript(udpnlFiltro, GetType(), "okEvaluacion", String.Format("alert('{0}');", mensaje), true);
            }
        }

        protected void btnalmacenarparcial_Click(object sender, EventArgs e)
        {
            if (Session["ideval"] == null)
                AlmacenarInformacion(true);
            else
                AlmacenarParcialDefinitiva(true);
        }

        protected void btnDefinitiva_Click(object sender, EventArgs e)
        {
            if (Session["ideval"] == null)
                AlmacenarInformacion(false);
            else
                AlmacenarParcialDefinitiva(false);
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
            if (Request.IsAuthenticated)
            {
                /*Prueba de ajax realizada para actualizar un gridview fallida*/
                bool ajax_result = false;
                if (Request.QueryString["text"] != null)
                    ajax_result = Filtro(Request.QueryString["text"]);
                /*En caso de ser postbak realiza validacion para no realizar proceso de recarga al datasource del gridview*/
                if (!Page.IsPostBack && !ajax_result)
                {
                    /*Asigno un valor false a las propiedades del objeto del tipo CEvaluacion para
                     *Evitar que haya error por recivir mas de un tipo de actor al momento de carga de la evaluacion
                     */
                    _objevaluacion.Profesional = false;
                    _objevaluacion.Estudiantes = false;
                    _objevaluacion.SecretariaEducacion = false;
                    _objevaluacion.Padres = false;
                    _objevaluacion.Docentes = false;
                    /*Cargo el control gridview con el data source obtenido de instituciones educativas*/
                    gvResultados.DataSourceID = "ldsies";
                    /*Actualizo el control griview para que el formulario web tome los ultimos cambios realizados*/
                    ObtenerTema(gvResultados);
                    cboActores.DataSourceID = "ldsActores";
                    cboActores.DataBind();

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
            lblCodIe.Text = _objRow.Cells[0].Text;
            lblIE.Text = _objRow.Cells[1].Text;
            lblMunicipio.Text = _objRow.Cells[2].Text;
            btnMedicion.Visible = true;
            var mediciones = _objevaluacion.MedicionesIE(Convert.ToInt32(lblIdIe.Text));

            if (mediciones != null)
            {
                gvMediciones.DataSource = mediciones;
                gvMediciones.DataBind();
                ObtenerTema(gvMediciones);
                gvMediciones.Visible = true;
            }
            else
            {
                string mensaje = "No Existen Mediciones para el establecimiento educativo.";
                ScriptManager.RegisterStartupScript(udpnlFiltro, GetType(), "scriptalert", String.Format("alert('{0}');", mensaje), true);
                gvMediciones.Visible = false;
                btnMedicion.Visible = true;
            }

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
                ScriptManager.RegisterStartupScript(udpnlFiltro, GetType(), "scriptalertactor", "alert('Para la medicion actual ya fue evaluado este actor, proceda a cargar la evaluacion correspondiente en el proceso 2.2');", true);
                gvEvaluacion.Visible = false;
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

        }

        protected void lbtnVolver_Click(object sender, EventArgs e)
        {
            VolverSeleccion();
        }

        protected void btnMedicion_Click(object sender, EventArgs e)
        {
            int idmedicion = _objevaluacion.CrearMedion();
            Session.Add("idmedicion", idmedicion);
            VisualizacionControles();
        }

        protected void gvMediciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objrow = this.gvMediciones.SelectedRow;

            int idmedicion = Convert.ToInt32(objrow.Cells[1].Text);
            TopEvaluaciones(idmedicion);
            ObtenerActoresEvaluar(gvTopEval);
            btnMedicion.Visible = false;

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
                               select new { res.IdPregunta, res.IdResultados, res.Valor, res.NoAplica, pre.Pregunta };

            var listresults = resultbyeval.ToList();

            for (int i = 0; i < gvEvaluacion.Rows.Count; i++)
            {
                for (int e = 0; e < listresults.Count; e++)
                {
                    Label idpregunta = (Label)gvEvaluacion.Rows[i].Cells[1].FindControl("lblIdPregunta");
                    if (idpregunta.Text == listresults[e].IdPregunta.ToString())
                    {

                        if (listresults[e].Valor == true)
                        {
                            RadioButton objsi = (RadioButton)gvEvaluacion.Rows[i].Cells[1].FindControl("rbtnSi");
                            objsi.Checked = true;
                            break;
                        }
                        else if (listresults[e].Valor == false)
                        {
                            RadioButton objno = (RadioButton)gvEvaluacion.Rows[i].Cells[1].FindControl("rbtnNo");
                            objno.Checked = true;
                            break;
                        }
                    }

                }

            }
            cboActores.SelectedValue = idactor.ToString();
            lblActorEvaluado.Text = cboActores.SelectedItem.Text;

            var estadoeval = from e in db.Evaluacion
                             where e.IdEvaluacion == ideval
                             select e;

            var evalsta = estadoeval.ToList();

            if (evalsta[0].IdEstado == 1)
            {
                gvEvaluacion.Visible = true;
                gvEvaluacion.Enabled = false;

                btnalmacenarparcial.Visible = false;
                btnDefinitiva.Visible = false;
            }
            else
            {
                gvEvaluacion.Visible = true;

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
                //Llama al metodo load del objeto CEvaluacion que obtiene el Iqueryable para asignar al datasource del control
                gvEvaluacion.DataSource = objevaluacion.LoadEvaluation();
                //Actualizo el control Gridview en el formulario para que almacene los cambio realizados
                gvEvaluacion.DataBind();
                //Asigna el tema para los griviews
                ObtenerTema(gvEvaluacion);

            }
            /*En caso de presentar excepcion retorno una alerta en javascript que me muestra y me controla el error presentado*/
            catch (Exception) { Response.Write("<script>alert('Ocurrio un error inesperado.');</script>"); }
        }

        /// <summary>
        /// Controla la visualizacion de los controles deacuero a un flujo establecido
        /// </summary>
        protected void VisualizacionControles()
        {
            cboActores.Enabled = true;
            txtFiltro.Enabled = false;
            btnBuscar.Enabled = false;
            gvResultados.Enabled = false;
            titulo3.Visible = true;
            tituloeval.Visible = true;
            lbtnVolver.Visible = true;
            titulo1ie.Visible = false;
            infoEval.Visible = true;
            titulo22.Visible = true;
            gvResultados.Visible = false;
            filtrosp.Visible = false;
            btnalmacenarparcial.Visible = true;
            btnDefinitiva.Visible = true;
            cboActores.SelectedValue = "2";
            gvTopEval.Visible = true;
            infoEval.Visible = true;
            gvEvaluacion.Visible = true;
            informacionuno.Visible = true;
            EvaluarActorSeleccionado("Secretaria de Educacion", 5);


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

            int idie = 0;

            if (Session["idie"] != null)
                idie = Convert.ToInt32(Session["idie"]);

            if (idie != 0 && idmedicion != 0)
            {
                IQueryable objIQueryable = _objevaluacion.ObtenerTopEvaluacion(5, idmedicion, idie);

                gvTopEval.DataSource = objIQueryable;
                gvTopEval.DataBind();
                ObtenerTema(gvTopEval);
                titulo22.Visible = true;
            }
            ///TODO: JCMM: Agregar Alerta para este metodo
        }

        protected void EvaluarActorSeleccionado(string actorSeleccionado, int idactor)
        {
            gvEvaluacion.Enabled = true;
            lblerrorAc.Visible = false;
            lbloki.Visible = false;

            int idie = Convert.ToInt32(Session["idie"]);
            int idmedicion = Convert.ToInt32(Session["idmedicion"]);

            if (_objevaluacion.ValidarActores(idie, idactor, idmedicion))
            {
                switch (actorSeleccionado)
                {
                    case "Estudiante":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        Actorespnl.BackColor = System.Drawing.Color.Black;
                        _objevaluacion.Estudiantes = true;
                        CargarPreguntas(_objevaluacion);
                        lblActorEvaluado.Text = "Estudiante";
                        gvEvaluacion.Visible = true;
                        btnDefinitiva.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Profesional de Campo":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Profesional = true;
                        CargarPreguntas(_objevaluacion);
                        lblActorEvaluado.Text = "Profesional de Campo";
                        gvEvaluacion.Visible = true;
                        btnDefinitiva.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Educador":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Docentes = true;
                        CargarPreguntas(_objevaluacion);
                        lblActorEvaluado.Text = "Educador";
                        gvEvaluacion.Visible = true;
                        btnDefinitiva.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Padre de Familia":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Padres = true;
                        CargarPreguntas(_objevaluacion);
                        lblActorEvaluado.Text = "Padre de Familia";
                        gvEvaluacion.Visible = true;
                        btnDefinitiva.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Directivos":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Directivos = true;
                        CargarPreguntas(_objevaluacion);
                        lblActorEvaluado.Text = "Directivos";
                        gvEvaluacion.Visible = true;
                        btnDefinitiva.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Secretaria de Educacion":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.SecretariaEducacion = true;
                        CargarPreguntas(_objevaluacion);
                        lblActorEvaluado.Text = "Secretaria de Educacion";
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
            txtFiltro.Enabled = true;
            btnBuscar.Enabled = true;
            gvResultados.Enabled = true;
            titulo3.Visible = false;
            tituloeval.Visible = false;
            titulo1ie.Visible = true;
            infoEval.Visible = false;
            lbloki.Visible = false;
            lbtnVolver.Visible = false;
            titulo22.Visible = false;
            gvResultados.Visible = true;
            filtrosp.Visible = true;
            gvTopEval.Visible = false;
            infoEval.Visible = false;
            gvEvaluacion.Visible = false;
            btnalmacenarparcial.Visible = false;
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

    }
}