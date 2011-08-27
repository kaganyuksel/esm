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
            cboActores.SelectedValue = "5";
            gvTopEval.Visible = true;
            infoEval.Visible = true;
            gvEvaluacion.Visible = true;
            EvaluarActorSeleccionado("Secretaria de Educacion", 5);


        }
        /// <summary>
        /// Realiza el proceso de busqueda para las instituciones educativas
        /// </summary>
        /// <param name="texto">Palabra calve por la que se realizara el proceso de busqueda en la coleccion de instituciones educativas</param>
        /// <returns>valor que me indica si el proceso se realizo satisfactoriamente o no</returns>
        protected bool Filtro(string texto)
        {
            try
            {
                /*Instancio*/
                Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                var rFiltro = from i in db.InstitucionEducativa
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


        protected void ldsIes_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {


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

        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            Filtro(txtFiltro.Text);
        }

        protected void gvResultados_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow _objRow = gvResultados.SelectedRow;
            Label lblIdIe = (Label)_objRow.Cells[5].FindControl("IDIE");
            Session.Add("idie", lblIdIe.Text);
            lblCodIe.Text = _objRow.Cells[0].Text;
            lblIE.Text = _objRow.Cells[1].Text;
            lblMunicipio.Text = _objRow.Cells[2].Text;
            TopEvaluaciones();
            VisualizacionControles();
        }

        protected void btnalmacenarparcial_Click(object sender, EventArgs e)
        {
            AlmacenarInformacion();
        }

        protected void AlmacenarInformacion()
        {

            _objevaluacion = new EvaluationSettings.CEvaluacion();

            object[,] CollectionResultados = new object[gvEvaluacion.Rows.Count, 2];

            bool estado = false;
            for (int i = 0; i < gvEvaluacion.Rows.Count; i++)
            {
                GridViewRow objGridViewRow = gvEvaluacion.Rows[i];
                Label objIdPregunta = (Label)objGridViewRow.Cells[1].FindControl("lblIdPregunta");
                CheckBox objsi = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnSi");
                CheckBox objno = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNo");
                CheckBox objnoapli = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNoAplica");

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
                else if (objnoapli.Checked)
                {
                    CollectionResultados[i, 0] = Convert.ToInt32(objIdPregunta.Text);
                    CollectionResultados[i, 1] = null;
                }
                else
                {
                    estado = true;
                }

            }

            if (_objevaluacion.Almacenar(CollectionResultados, Convert.ToInt32(Session["idie"]), Convert.ToInt32(cboActores.SelectedItem.Value), Convert.ToInt32(Session["idusuario"]), estado))
            {
                gvEvaluacion.Visible = false;
                btnalmacenarparcial.Visible = false;
                lbloki.InnerText = "La evaluacion fue almacenada correctamente.";

            }
        }

        protected void cboActores_SelectedIndexChanged(object sender, EventArgs e)
        {
            EvaluarActorSeleccionado(cboActores.SelectedItem.Text, Convert.ToInt32(cboActores.SelectedItem.Value));
        }

        protected void EvaluarActorSeleccionado(string actorSeleccionado, int idactor)
        {
            gvEvaluacion.Enabled = true;
            lblerrorAc.Visible = false;


            if (_objevaluacion.ValidarActores(Convert.ToInt32(Session["idie"]), idactor))
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
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Profesional de Campo":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Profesional = true;
                        CargarPreguntas(_objevaluacion);
                        lblActorEvaluado.Text = "Profesional de Campo";
                        gvEvaluacion.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Educador":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Docentes = true;
                        CargarPreguntas(_objevaluacion);
                        lblActorEvaluado.Text = "Educador";
                        gvEvaluacion.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Padre de Familia":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Padres = true;
                        CargarPreguntas(_objevaluacion);
                        lblActorEvaluado.Text = "Padre de Familia";
                        gvEvaluacion.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Directivos":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.Directivos = true;
                        CargarPreguntas(_objevaluacion);
                        lblActorEvaluado.Text = "Directivos";
                        gvEvaluacion.Visible = true;
                        btnalmacenarparcial.Visible = true;
                        break;

                    case "Secretaria de Educacion":
                        _objevaluacion = new EvaluationSettings.CEvaluacion();
                        _objevaluacion.SecretariaEducacion = true;
                        CargarPreguntas(_objevaluacion);
                        lblActorEvaluado.Text = "Secretaria de Educacion";
                        gvEvaluacion.Visible = true;
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

                case "Docente":
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

        protected void TopEvaluaciones()
        {
            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

            int idie = Convert.ToInt32(Session["idie"]);

            var Listultmed = from u in db.Evaluacion
                             where u.IdIE == idie
                             select u.IdMedicion;

            var listmed = Listultmed.ToList();

            if (listmed.Count != 0)
            {
                ///TODO: JCMM: Revisar Esta Variable
                var ultmed = (from u in db.Evaluacion
                              where u.IdIE == idie
                              select u.IdMedicion).Max();

                var ulteval = (from e in db.Evaluacion
                               where e.IdIE == idie && e.IdMedicion == ultmed
                               select e).Take(5);

                gvTopEval.DataSource = ulteval;
                gvTopEval.DataBind();
                ObtenerTema(gvTopEval);
            }


        }

        protected void gvTopEval_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvEvaluacion.Enabled = true;
            GridViewRow objrow = gvTopEval.SelectedRow;
            Label idactor = (Label)objrow.Cells[4].FindControl("lblidActor");
            CargarParcial(Convert.ToInt32(objrow.Cells[0].Text), Convert.ToInt32(idactor.Text));

        }

        protected void CargarParcial(int ideval, int idactor)
        {
            gvEvaluacion.Enabled = true;

            Session.Add("idevalpar", ideval);

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
                            gvEvaluacion.Rows[i].Enabled = false;
                            objsi.Checked = true;

                            break;
                        }
                        else if (listresults[e].Valor == false)
                        {
                            RadioButton objno = (RadioButton)gvEvaluacion.Rows[i].Cells[1].FindControl("rbtnNo");
                            gvEvaluacion.Rows[i].Enabled = false;
                            objno.Checked = true;
                            break;
                        }
                        else if (listresults[e].NoAplica == true)
                        {
                            RadioButton objnoapli = (RadioButton)gvEvaluacion.Rows[i].Cells[1].FindControl("rbtnNoAplica");
                            gvEvaluacion.Rows[i].Enabled = false;
                            objnoapli.Checked = true;
                            break;
                        }
                    }

                }

            }
            cboActores.SelectedValue = idactor.ToString();
            lblActorEvaluado.Text = cboActores.SelectedItem.Text;
            cboActores.Enabled = false;

            var estadoeval = from e in db.Evaluacion
                             where e.IdEvaluacion == ideval
                             select e;

            var evalsta = estadoeval.ToList();

            if (evalsta[0].IdEstado == 1)
            {
                gvEvaluacion.Visible = true;
                gvEvaluacion.Enabled = false;

                btnDefinitiva.Visible = false;
                btnalmacenarparcial.Visible = false;
                cboActores.Enabled = true;
            }
            else
            {
                gvEvaluacion.Visible = true;

                btnalmacenarparcial.Visible = false;

                btnDefinitiva.Visible = true;
            }



        }

        protected void btnDefinitiva_Click(object sender, EventArgs e)
        {
            AlmacenarParcialDefinitiva(Convert.ToInt32(Session["idevalpar"]));
        }

        protected void AlmacenarParcialDefinitiva(int eval)
        {
            string estado = "definitiva";
            for (int i = 0; i < gvEvaluacion.Rows.Count; i++)
            {
                GridViewRow objGridViewRow = gvEvaluacion.Rows[i];
                Label objIdPregunta = (Label)objGridViewRow.Cells[1].FindControl("lblIdPregunta");
                CheckBox objsi = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnSi");
                CheckBox objno = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNo");
                CheckBox objnoapli = (CheckBox)objGridViewRow.Cells[1].FindControl("rbtnNoAplica");

                if (objsi.Checked == false && objno.Checked == false && objnoapli.Checked && false)
                    estado = "parcial";
            }

            if (estado == "definitiva")
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

                if (_objevaluacion.InsertarResultadosParcial(CollectionResultados, eval))
                {

                    ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
                    var evaluacion = (from e in db.Evaluacion
                                      where e.IdEvaluacion == eval
                                      select e).Single();

                    evaluacion.IdEstado = 1;

                    db.SubmitChanges();

                    gvEvaluacion.Visible = false;
                    btnDefinitiva.Visible = false;
                    btnalmacenarparcial.Visible = false;
                    lbloki.InnerText = "La evaluacion fue Actualizada correctamente.";


                }

                #endregion
            }
            else
            {
                #region Almacena Resultados Evaluacion Parcial

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

                if (_objevaluacion.InsertarResultadosParcial(CollectionResultados, eval))
                {
                    gvEvaluacion.Visible = false;
                    btnDefinitiva.Visible = false;
                    btnalmacenarparcial.Visible = false;
                    lbloki.InnerText = "La evaluacion fue Actualizada correctamente.";
                }

                #endregion
            }


        }

        protected void lbtnVolver_Click(object sender, EventArgs e)
        {
            VolverSeleccion();
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
    }
}