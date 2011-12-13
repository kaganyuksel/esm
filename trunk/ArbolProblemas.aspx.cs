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
                //ObtenerResultados(_idproyecto);
                Bandera.Value = "-1";
            }
            if (!Page.IsPostBack)
            {

            }
        }

        protected void lknAlmacenarP_Click(object sender, EventArgs e)
        {
            #region Insercion de Proyecto

            int idproyecto = objCpoyecto.Add(txtproblema.Text);
            if (idproyecto != 0)
            {
                Session.Add("idproyecto", idproyecto);
                alerthq.Value = "1";
            }
            else
                alerthq.Value = "0";

            #endregion

            //ObtenerResultados(_idproyecto);

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
            #endregion

            //ObtenerResultados(_idproyecto);

        }

        protected void gvProyectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.Remove("idproyecto");

            GridViewRow objRow = gvProyectos.SelectedRow;
            int idproyecto = Convert.ToInt32(objRow.Cells[1].Text);
            Session.Add("idproyecto", idproyecto);
            _idproyecto = idproyecto;
            txtproblema.Text = objRow.Cells[2].Text;
            txtProposito.Text = objRow.Cells[2].Text;
            txtproblema.ReadOnly = true;

            CargarProposito(txtproblema.Text);
            load_procesos();
            getCronograma();
            load_subprocesos();
            Load_resultados();
            lknAlmacenarP.Enabled = false;

            divefectos.Visible = true;
            divproblema.Visible = true;
            lknAlmacenarP.Enabled = true;
            divNuevo.Visible = false;
            divCargado.Visible = true;
            divproyectos.Visible = false;

            lknAlmacenarP.Enabled = false;

            //ObtenerResultados(idproyecto);
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

        protected void ObtenerResultados(int idproyecto)
        {
            try
            {
                IQueryable<ESM.Model.Causas_Efecto> objCausas_Efecto = new CEfectos().getCount(idproyecto);

                int contador = 0;

                int ancho_textbox = 30;

                HtmlGenericControl titulo_resultados = new HtmlGenericControl("h2");
                titulo_resultados.Attributes.CssStyle.Add("float", "left");
                titulo_resultados.Attributes.CssStyle.Add("margin-left", "10px");
                titulo_resultados.InnerHtml = "Estategias";

                HtmlGenericControl br_resultados = new HtmlGenericControl("br");
                HtmlGenericControl br_vita_previa = new HtmlGenericControl("br");

                var visualizar_report = new HtmlAnchor();
                visualizar_report.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/ReportMarcoLogico.aspx?idproyecto=" + Session["idproyecto"].ToString() + "&iframe=true&amp;width=100%&amp;height=100%";
                visualizar_report.Title = "Marco Logico";
                visualizar_report.Attributes.CssStyle.Add("text-decoration", "none");
                visualizar_report.Attributes.CssStyle.Add("font-style", "italic");
                visualizar_report.Attributes.CssStyle.Add("color", "#005EA7");
                visualizar_report.InnerHtml = "<img alt='Detalles' src='/Icons/Search.png' width='24px' /> Visualizar Matriz";
                visualizar_report.Attributes.Add("class", "pretty");

                var visualizar_report_actividades = new HtmlAnchor();
                visualizar_report_actividades.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/ReportMarcoLogico.aspx?idproyecto=" + Session["idproyecto"].ToString() + "&iframe=true&amp;width=100%&amp;height=100%";
                visualizar_report_actividades.Title = "Marco Logico";
                visualizar_report_actividades.Attributes.CssStyle.Add("text-decoration", "none");
                visualizar_report_actividades.Attributes.CssStyle.Add("font-style", "italic");
                visualizar_report_actividades.Attributes.CssStyle.Add("color", "#005EA7");
                visualizar_report_actividades.InnerHtml = "<img alt='Detalles' src='/Icons/Search.png' width='24px' /> Visualizar Matriz";
                visualizar_report_actividades.Attributes.Add("class", "pretty");

                presultados.Controls.Add(br_vita_previa);
                presultados.Controls.Add(titulo_resultados);

                foreach (var item in objCausas_Efecto)
                {
                    HtmlGenericControl span = new HtmlGenericControl("span");

                    HtmlGenericControl br = new HtmlGenericControl("br");

                    span.Attributes.Add("width", "100%");

                    Label objlabel = new Label();
                    objlabel.Text = (contador + 1).ToString() + ". ";

                    Label objNombreResultado = new Label();
                    objNombreResultado.Text = "Resultado";

                    Label objDetalleResultado = new Label();
                    objDetalleResultado.Text = "Detalle";

                    Label objlabelid = new Label();
                    objlabelid.Text = item.Id.ToString();
                    objlabelid.Visible = false;

                    var inputtextcausa = new TextBox();
                    inputtextcausa.ID = "txtcausaR" + (item.Id + 2000).ToString();
                    inputtextcausa.Text = item.Causa.ToString();
                    inputtextcausa.Attributes.Add("class", "txtareacausa");
                    inputtextcausa.Attributes.Add("float", "left");
                    inputtextcausa.Attributes.CssStyle.Add("width", ancho_textbox.ToString() + "%");
                    inputtextcausa.TextMode = TextBoxMode.MultiLine;
                    inputtextcausa.Attributes.CssStyle.Add("border", "solid 2px #ccc");
                    if (item.Color != null && item.Color != "")
                        inputtextcausa.Attributes.CssStyle.Add("background", item.Color);
                    else
                        inputtextcausa.Attributes.CssStyle.Add("background", "#fff");

                    var inputresultado = new TextBox();
                    inputresultado.ID = "txtresultadoplan" + item.Id.ToString();
                    //if (item.Resultado != null)
                    //    inputresultado.Text = item.Resultado.ToString();
                    inputresultado.Attributes.Add("float", "left");
                    //inputresultado.Attributes.Add("class", "txtareacausa");
                    inputresultado.Attributes.Add("placeholder", "Resultado numero" + (contador + 1).ToString());
                    inputresultado.Attributes.CssStyle.Add("width", ancho_textbox.ToString() + "%");
                    inputresultado.TextMode = TextBoxMode.MultiLine;


                    var almacenarresultado = new ImageButton();
                    almacenarresultado.ID = "btnAlmacenar" + contador.ToString();
                    almacenarresultado.Width = 24;
                    almacenarresultado.CausesValidation = false;
                    almacenarresultado.ImageUrl = "/Icons/save-icon.png";
                    almacenarresultado.Attributes.Add("float", "left");
                    almacenarresultado.Attributes.Add("onclick", "AlmacenarResultado('" + item.Id.ToString() + "','" + "ContentPlaceHolder1_" + inputtextcausa.ID + "','" + "ContentPlaceHolder1_" + inputresultado.ID + "');");
                    almacenarresultado.CssClass = item.Id.ToString();

                    var detalles = new HtmlAnchor();
                    detalles.Attributes.Add("class", "pretty");
                    detalles.Attributes.Add("title", "Detalles para Resultados");
                    detalles.Attributes.Add("href", Request.Url.Scheme + "://" + Request.Url.Authority + "/DetallesMarcoLogico.aspx?idResultado=" + item.Id + "&iframe=true&amp;width=100%&amp;height=100%");
                    detalles.InnerHtml = "<img alt='Detalles' src='/Icons/details.png' width='24px' />";

                    var cronograma = new HtmlAnchor();
                    cronograma.Attributes.Add("class", "pretty");
                    cronograma.Attributes.Add("href", Request.Url.Scheme + "://" + Request.Url.Authority + "/DiagramaGant.aspx?idResultado=" + item.Id + "&iframe=true&amp;width=100%&amp;height=100%");
                    cronograma.InnerHtml = "<img alt='Cronograma' src='/Icons/Calender.png' width='24px' />";

                    HtmlGenericControl spanresul = new HtmlGenericControl("span");

                    HtmlGenericControl br_resul = new HtmlGenericControl("br");

                    //spanresul.Controls.Add(br_resul);
                    spanresul.Controls.Add(objlabel);
                    spanresul.Controls.Add(objlabelid);
                    spanresul.Controls.Add(objNombreResultado);
                    spanresul.Controls.Add(inputtextcausa);
                    spanresul.Controls.Add(objDetalleResultado);
                    spanresul.Controls.Add(inputresultado);

                    pnlActividades.Controls.Add(spanresul);

                    HtmlGenericControl spanpo = new HtmlGenericControl("span");

                    HtmlGenericControl br_resul_po = new HtmlGenericControl("br");

                    span.Attributes.Add("width", "100%");
                    span.Attributes.CssStyle.Add("height", "40px");
                    span.Attributes.CssStyle.Add("line-height", "40px");

                    objlabel = new Label();
                    objlabel.Text = (contador + 1).ToString() + ". ";

                    objNombreResultado = new Label();
                    objNombreResultado.Text = "Resultado&nbsp";

                    objDetalleResultado = new Label();
                    objDetalleResultado.Text = "Detalle&nbsp";

                    objlabelid = new Label();
                    objlabelid.Text = item.Id.ToString();
                    objlabelid.Visible = false;

                    inputtextcausa = new TextBox();
                    inputtextcausa.ID = "txtcausaR" + (contador + 8000).ToString();
                    inputtextcausa.Text = item.Causa.ToString();
                    inputtextcausa.Attributes.Add("class", "txtareacausa");
                    inputtextcausa.Attributes.CssStyle.Add("font-size", "15px");
                    inputtextcausa.Attributes.CssStyle.Add("height", "40px");
                    inputtextcausa.Attributes.CssStyle.Add("border", "solid 2px #ccc");
                    if (item.Color != null && item.Color != "")
                        inputtextcausa.Attributes.CssStyle.Add("background", item.Color);
                    else
                        inputtextcausa.Attributes.CssStyle.Add("background", "#fff");
                    inputtextcausa.Attributes.CssStyle.Add("border-radius", "3px 3px 3px 3px");
                    inputtextcausa.Attributes.Add("float", "left");
                    inputtextcausa.Attributes.CssStyle.Add("width", ancho_textbox.ToString() + "%");
                    inputtextcausa.TextMode = TextBoxMode.MultiLine;

                    inputresultado = new TextBox();
                    inputresultado.ID = "txtresultadomarco" + contador.ToString();
                    //if (item.Resultado != null)
                    //    inputresultado.Text = item.Resultado.ToString();
                    inputresultado.Attributes.CssStyle.Add("font-size", "15px");
                    inputresultado.Attributes.CssStyle.Add("height", "40px");
                    inputresultado.Attributes.Add("placeholder", "Resultado numero" + (contador + 1).ToString());
                    inputresultado.Attributes.CssStyle.Add("width", ancho_textbox.ToString() + "%");
                    inputresultado.TextMode = TextBoxMode.MultiLine;

                    almacenarresultado = new ImageButton();
                    almacenarresultado.ID = "btnAlmacenar" + contador.ToString();
                    almacenarresultado.Width = 24;
                    almacenarresultado.CausesValidation = false;
                    almacenarresultado.ImageUrl = "/Icons/save-icon.png";
                    almacenarresultado.Attributes.Add("float", "left");
                    almacenarresultado.Attributes.Add("onclick", "AlmacenarResultado('" + item.Id.ToString() + "','" + "ContentPlaceHolder1_" + inputtextcausa.ID + "','" + "ContentPlaceHolder1_" + inputresultado.ID + "');");
                    almacenarresultado.CssClass = item.Id.ToString();

                    detalles = new HtmlAnchor();
                    detalles.Attributes.Add("class", "pretty");
                    detalles.Attributes.Add("title", "Detalles para Resultados");
                    detalles.Attributes.Add("href", Request.Url.Scheme + "://" + Request.Url.Authority + "/DetallesMarcoLogico.aspx?idResultado=" + item.Id + "&iframe=true&amp;width=100%&amp;height=100%");
                    detalles.InnerHtml = "<img alt='Detalles' src='/Icons/details.png' width='24px' />";

                    cronograma = new HtmlAnchor();
                    cronograma.Attributes.Add("class", "pretty");
                    cronograma.Attributes.Add("href", Request.Url.Scheme + "://" + Request.Url.Authority + "/DiagramaGant.aspx?idResultado=" + item.Id + "&iframe=true&amp;width=100%&amp;height=100%");
                    cronograma.InnerHtml = "<img alt='Cronograma' src='/Icons/Calender.png' width='24px' />";

                    spanpo.Controls.Add(br_resul_po);
                    spanpo.Controls.Add(objlabel);
                    spanpo.Controls.Add(objlabelid);
                    spanpo.Controls.Add(objNombreResultado);
                    spanpo.Controls.Add(inputtextcausa);
                    spanpo.Controls.Add(objDetalleResultado);
                    spanpo.Controls.Add(inputresultado);
                    spanpo.Controls.Add(almacenarresultado);
                    spanpo.Controls.Add(detalles);
                    spanpo.Controls.Add(cronograma);

                    presultados.Controls.Add(spanpo);

                    #region Consultar Actividades

                    getPlanOperativo(item.Id, contador);

                    #endregion

                    contador++;
                }
            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

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

            //ObtenerResultados(_idproyecto);

        }

        protected void CargarFinalidad()
        {
            try
            {
                txtfinalidad.Text = objCEfectos.getEfectos(_idproyecto);
                txtfinalidad_po.Text = txtfinalidad.Text;
            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        protected void lknAlmacenarFinalidad_Click(object sender, EventArgs e)
        {
            objCpoyecto.Update(_idproyecto, null, null, txtfinalidad.Text);
            //ObtenerResultados(_idproyecto);
        }

        protected void getPlanOperativo(int idresultado, int consecutivo)
        {
            try
            {
                var br = new HtmlGenericControl("br");
                #region Consultar Actividades

                var actividades = new HtmlGenericControl("div");
                actividades.Attributes.CssStyle.Add("width", "85%");
                actividades.Attributes.Add("class", "accordion");

                var titulo = new HtmlGenericControl("h3");
                titulo.InnerHtml = "<a href='#'>Actividades</a>";

                var contenido_acordion = new HtmlGenericControl("div");

                var lblnactividad = new Label();
                lblnactividad.Text = "Nombre Actividad";

                var txtactividad = new TextBox();

                txtactividad.Attributes.CssStyle.Add("width", "20%");
                txtactividad.ID = "txtactividadnr" + idresultado.ToString();
                txtactividad.TextMode = TextBoxMode.MultiLine;

                var lblpresupuesto = new Label();
                lblpresupuesto.Text = "Presupuesto";

                var txtpresupuesto = new TextBox();
                txtpresupuesto.ID = "txtpresupuestor" + idresultado.ToString();
                txtpresupuesto.Attributes.Add("class", "numerico");
                txtpresupuesto.Attributes.CssStyle.Add("width", "20%");

                var btnalmacenaractividad = new ImageButton();
                btnalmacenaractividad.ID = "btnAlmacenarActividadr" + idresultado.ToString();
                btnalmacenaractividad.ImageUrl = "/Icons/1314641093_plus_48.png";
                btnalmacenaractividad.Attributes.CssStyle.Add("width", "24px");
                btnalmacenaractividad.Attributes.Add("onclick", "AlmacenarActividad('" + idresultado.ToString() + "','" + "ContentPlaceHolder1_" + txtactividad.ID + "','" + "ContentPlaceHolder1_" + txtpresupuesto.ID + "');");

                br = new HtmlGenericControl("br");

                contenido_acordion.Controls.Add(lblnactividad);
                contenido_acordion.Controls.Add(txtactividad);
                contenido_acordion.Controls.Add(lblpresupuesto);
                contenido_acordion.Controls.Add(txtpresupuesto);
                contenido_acordion.Controls.Add(btnalmacenaractividad);

                IQueryable<ESM.Model.Actividade> ac = null;

                try
                {
                    ac = from a in new ESM.Model.ESMBDDataContext().Actividades
                         where a.Resultado_id == idresultado
                         select a;
                }
                catch (Exception) { }

                if (ac.Count() != 0)
                {
                    var tituloActividades = new HtmlGenericControl("h3");

                    tituloActividades.InnerHtml = "Actividades Existentes";
                    tituloActividades.Attributes.CssStyle.Add("color", "#005EA7");


                    contenido_acordion.Controls.Add(tituloActividades);


                    foreach (var actividadbyresult in ac)
                    {
                        var lblcactividad = new Label();
                        lblcactividad.Text = "Nombre Actividad";
                        var txtcactividad = new TextBox();
                        txtcactividad.Attributes.CssStyle.Add("width", "20%");
                        txtcactividad.ID = "txtactividadcrb" + actividadbyresult.Id.ToString() + consecutivo.ToString();
                        txtcactividad.Text = actividadbyresult.Actividad;
                        txtcactividad.TextMode = TextBoxMode.MultiLine;
                        var lblcpresupuesto = new Label();
                        lblcpresupuesto.Text = "Presupuesto";
                        var txtcpresupuesto = new TextBox();
                        txtcpresupuesto.ID = "txtpresupuestocrb" + actividadbyresult.Id.ToString() + consecutivo.ToString();
                        txtcpresupuesto.Attributes.CssStyle.Add("width", "20%");
                        txtcpresupuesto.Text = actividadbyresult.Presupuesto.ToString();

                        var btnactualizaractividad = new ImageButton();
                        btnactualizaractividad.ImageUrl = "/Icons/save-icon.png";
                        btnactualizaractividad.Attributes.CssStyle.Add("width", "24px");
                        btnactualizaractividad.Attributes.Add("onclick", "ActualizarActividad('" + actividadbyresult.Id.ToString() + "','" + "ContentPlaceHolder1_" + txtcactividad.ID + "','" + "ContentPlaceHolder1_" + txtcpresupuesto.ID + "');");

                        IQueryable<ESM.Model.Indicadore> indicadores = objCActividades.getIndicadores(actividadbyresult.Id);

                        contenido_acordion.Controls.Add(br);
                        contenido_acordion.Controls.Add(lblcactividad);
                        contenido_acordion.Controls.Add(txtcactividad);
                        contenido_acordion.Controls.Add(lblcpresupuesto);
                        contenido_acordion.Controls.Add(txtcpresupuesto);
                        contenido_acordion.Controls.Add(btnactualizaractividad);

                        br = new HtmlGenericControl("br");



                        var detalles_Actividad = new HtmlAnchor();
                        detalles_Actividad.Attributes.Add("class", "pretty");
                        detalles_Actividad.Attributes.Add("title", "Detalles de Actividad");
                        detalles_Actividad.Attributes.Add("href", Request.Url.Scheme + "://" + Request.Url.Authority + "/DetallesMarcoLogico.aspx?idActividad=" + actividadbyresult.Id + "&iframe=true&amp;width=100%&amp;height=100%");
                        detalles_Actividad.InnerHtml = "<img alt='Detalles' src='/Icons/details.png' width='24px' />";

                        contenido_acordion.Controls.Add(detalles_Actividad);

                        int consecutivo_indicador = 0;

                        var br_indicadores = new HtmlGenericControl("br");
                        contenido_acordion.Controls.Add(br_indicadores);

                        var titulo_Indicadores = new HtmlGenericControl("h3");
                        titulo_Indicadores.InnerHtml = "Indicadores";
                        titulo_Indicadores.Attributes.CssStyle.Add("margin-left", "50px");
                        titulo_Indicadores.Attributes.CssStyle.Add("color", "#005EA7");


                        contenido_acordion.Controls.Add(titulo_Indicadores);

                        foreach (var item in indicadores)
                        {
                            Label lblindicador = new Label();
                            lblindicador.ID = "lblindicador" + actividadbyresult.Id.ToString() + "_" + consecutivo_indicador.ToString();
                            lblindicador.Text = (consecutivo_indicador + 1).ToString() + ". " + item.Indicador;
                            lblindicador.Attributes.CssStyle.Add("color", "#000");
                            lblindicador.Attributes.CssStyle.Add("margin-left", "70px");

                            contenido_acordion.Controls.Add(lblindicador);

                            br_indicadores = new HtmlGenericControl("br");
                            contenido_acordion.Controls.Add(br_indicadores);

                            consecutivo_indicador++;

                        }

                        actividades.Controls.Add(titulo);
                        actividades.Controls.Add(contenido_acordion);

                        pnlActividades.Controls.Add(actividades);
                        pnlActividades.Controls.Add(br);
                    }
                }
                else
                {
                    actividades.Controls.Add(titulo);
                    actividades.Controls.Add(contenido_acordion);

                    pnlActividades.Controls.Add(actividades);
                    pnlActividades.Controls.Add(br);
                }
                #endregion

            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        protected void getActividades(int idresultado, int consecutivo)
        {
            try
            {
                var br = new HtmlGenericControl("br");
                #region Consultar Actividades

                var actividades = new HtmlGenericControl("div");
                actividades.Attributes.CssStyle.Add("width", "85%");
                actividades.Attributes.Add("class", "accordion");

                var titulo = new HtmlGenericControl("h3");
                titulo.InnerHtml = "<a href='#'>Actividades</a>";

                var contenido_acordion = new HtmlGenericControl("div");

                var lblnactividad = new Label();
                lblnactividad.Text = "Nombre Actividad";

                var txtactividad = new TextBox();

                txtactividad.Attributes.CssStyle.Add("width", "20%");
                txtactividad.ID = "txtactividadna" + idresultado.ToString() + consecutivo.ToString();

                var lblpresupuesto = new Label();
                lblpresupuesto.Text = "Presupuesto";

                var txtpresupuesto = new TextBox();
                txtpresupuesto.ID = "txtpresupuestoa" + idresultado.ToString() + consecutivo.ToString();
                txtpresupuesto.Attributes.Add("class", "numerico");
                txtpresupuesto.Attributes.CssStyle.Add("width", "20%");

                var btnalmacenaractividad = new ImageButton();
                btnalmacenaractividad.ID = "btnAlmacenarActividad" + idresultado.ToString() + consecutivo.ToString();
                btnalmacenaractividad.ImageUrl = "/Icons/1314641093_plus_48.png";
                btnalmacenaractividad.Attributes.CssStyle.Add("width", "24px");
                btnalmacenaractividad.Attributes.Add("onclick", "AlmacenarActividad('" + idresultado.ToString() + "','" + "ContentPlaceHolder1_" + txtactividad.ID + "','" + "ContentPlaceHolder1_" + txtpresupuesto.ID + "');");

                IQueryable<ESM.Model.Actividade> ac = null;

                try
                {
                    ac = from a in new ESM.Model.ESMBDDataContext().Actividades
                         where a.Resultado_id == idresultado
                         select a;
                }
                catch (Exception) { }

                if (ac.Count() != 0)
                {
                    var tituloActividades = new HtmlGenericControl("h3");

                    tituloActividades.InnerHtml = "Actividades Existentes";
                    tituloActividades.Attributes.CssStyle.Add("color", "#005EA7");


                    contenido_acordion.Controls.Add(tituloActividades);


                    foreach (var actividadbyresult in ac)
                    {
                        var lblcactividad = new Label();
                        lblcactividad.Text = "Nombre Actividad";

                        var txtcactividad = new TextBox();
                        txtcactividad.Attributes.CssStyle.Add("width", "20%");
                        txtcactividad.ID = "txtactividadcac" + actividadbyresult.Id.ToString();
                        txtcactividad.Text = actividadbyresult.Actividad;
                        txtcactividad.ReadOnly = true;

                        var lblcpresupuesto = new Label();
                        lblcpresupuesto.Text = "Presupuesto";

                        var txtcpresupuesto = new TextBox();
                        txtcpresupuesto.ID = "txtpresupuestocac" + actividadbyresult.Id.ToString();
                        txtcpresupuesto.Attributes.CssStyle.Add("width", "20%");
                        txtcpresupuesto.ReadOnly = true;
                        txtcpresupuesto.Text = actividadbyresult.Presupuesto.ToString();

                        var btnactualizaractividad = new ImageButton();
                        btnactualizaractividad.ImageUrl = "/Icons/save-icon.png";
                        btnactualizaractividad.Attributes.CssStyle.Add("width", "24px");
                        btnactualizaractividad.Attributes.Add("onclick", "ActualizarActividad('" + actividadbyresult.Id.ToString() + "','" + "ContentPlaceHolder1_" + txtcactividad.ID + "','" + "ContentPlaceHolder1_" + txtcpresupuesto.ID + "');");

                        br = new HtmlGenericControl("br");
                        contenido_acordion.Controls.Add(br);

                        contenido_acordion.Controls.Add(lblcactividad);
                        contenido_acordion.Controls.Add(txtcactividad);
                        contenido_acordion.Controls.Add(lblcpresupuesto);
                        contenido_acordion.Controls.Add(txtcpresupuesto);

                        //contenido_acordion.Controls.Add(btnactualizaractividad);

                        var detalles_Actividad = new HtmlAnchor();
                        detalles_Actividad.Attributes.Add("class", "pretty");
                        detalles_Actividad.Attributes.Add("title", "Detalles de Actividad");
                        detalles_Actividad.Attributes.Add("href", Request.Url.Scheme + "://" + Request.Url.Authority + "/DetallesMarcoLogico.aspx?idActividad=" + actividadbyresult.Id + "&iframe=true&amp;width=100%&amp;height=100%");
                        detalles_Actividad.InnerHtml = "<img alt='Detalles' src='/Icons/details.png' width='24px' />";

                        //contenido_acordion.Controls.Add(detalles_Actividad);

                        br = new HtmlGenericControl("br");

                        //contenido_acordion.Controls.Add(br);

                        actividades.Controls.Add(titulo);
                        actividades.Controls.Add(contenido_acordion);

                        presultados.Controls.Add(actividades);
                        //presultados.Controls.Add(br);
                    }
                }

                #endregion

            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

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
                    objCell_Causa.InnerHtml = "<label style='color:#000;'>Causa No." + enumeracion.ToString() + ": " + item.Causa + "</label>";

                    objRow_Causa.Cells.Add(objCell_Causa);

                    HtmlTableRow objRow_Proceso = new HtmlTableRow();

                    HtmlTableCell objCell_Proceso = new HtmlTableCell();
                    objCell_Proceso.InnerHtml = "<h3>Proceso</h3><textarea id='txt_area_proceso_id_" + item.Id + "' placeholder='Ingrese el texto para proceso correspondiente.'>" + item.Proceso + "</textarea> ";


                    HtmlInputButton objAlmacenar_proceso = new HtmlInputButton();
                    objAlmacenar_proceso.ID = "btn_proceso_almacenar_id" + item.Id.ToString();
                    objAlmacenar_proceso.Attributes.Add("onclick", String.Format("AlmacenarProceso('{0}','{1}','{2}');", item.Id, item.Causa, "txt_area_proceso_id_" + item.Id));
                    //objAlmacenar_proceso.Src = "/Icons/save-icon.png";
                    objAlmacenar_proceso.Value = "Almacenar proceso";
                    //objAlmacenar_proceso.Attributes.CssStyle.Add("width", "24px");

                    objCell_Proceso.Controls.Add(objAlmacenar_proceso);
                    objRow_Proceso.Cells.Add(objCell_Proceso);

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
                    objCell_Proceso.InnerHtml = "<label style='color:#000;'>Proceso No. " + enumeracion.ToString() + ": " + item.Proceso + "</label>";

                    objRow_Proceso.Cells.Add(objCell_Proceso);

                    HtmlTableRow objRow_SubProceso = new HtmlTableRow();

                    HtmlTableCell objCell_SubProceso = new HtmlTableCell();
                    objCell_SubProceso.InnerHtml = "<h3>Crear Subproceso</h3><textarea id='txt_area_subproceso_id_" + item.Id + "' placeholder='Ingrese el texto para subproceso correspondiente correspondiente.'></textarea> ";

                    HtmlInputButton objAlmacenar_proceso = new HtmlInputButton();
                    objAlmacenar_proceso.ID = "btn_subproceso_almacenar_id" + item.Id.ToString();
                    objAlmacenar_proceso.Attributes.Add("onclick", String.Format("AlmacenarSubProceso('{0}','{1}');", item.Id, "txt_area_subproceso_id_" + item.Id));
                    //objAlmacenar_proceso.Src = "/Icons/save-icon.png";
                    objAlmacenar_proceso.Value = "Almacenar Subproceso";
                    //objAlmacenar_proceso.Attributes.CssStyle.Add("width", "24px");

                    objCell_SubProceso.Controls.Add(objAlmacenar_proceso);
                    objRow_SubProceso.Cells.Add(objCell_SubProceso);

                    objHtmlTable.Rows.Add(objRow_Proceso);
                    objHtmlTable.Rows.Add(objRow_SubProceso);

                    StringBuilder objsb = new StringBuilder();

                    objsb.Append("<div class='accordion'><h3><a href='#'>Subprocesos</a></h3>");
                    objsb.Append("<div>");

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
                    objCell_Proceso.InnerHtml = "<label style='color:#000;'>Proceso No. " + enumeracion.ToString() + ": " + item.Proceso + "</label>";

                    objRow_Proceso.Cells.Add(objCell_Proceso);

                    objHtmlTable.Rows.Add(objRow_Proceso);

                    IQueryable<Model.Subproceso> objSubprocesos = objCSubprocesos.LoadSubprocesos(item.Id);

                    int enumeracion_Subproceso = 1;
                    foreach (var item_subroceso in objSubprocesos)
                    {
                        HtmlTableRow objRow_SubProceso = new HtmlTableRow();

                        HtmlTableCell objCell_SubProceso = new HtmlTableCell();
                        objCell_SubProceso.InnerHtml = "<label style='color:#000;'>Subproceso No. " + enumeracion_Subproceso.ToString() + ": " + item_subroceso.Subproceso1 + "</label>";

                        objRow_SubProceso.Cells.Add(objCell_SubProceso);

                        objHtmlTable.Rows.Add(objRow_SubProceso);

                        HtmlTableRow objRow_Estrategias = new HtmlTableRow();

                        HtmlTableCell objCell_Estrategias = new HtmlTableCell();
                        objCell_Estrategias.InnerHtml = "<h3>Crear Estrategia</h3><textarea id='txt_area_estrategia_id_" + item_subroceso.Id + "' placeholder='Ingrese el texto para estrategia correspondiente.'></textarea> ";

                        HtmlInputButton objAlmacenar_estrategia = new HtmlInputButton();
                        objAlmacenar_estrategia.ID = "btn_estrategia_almacenar_id" + item.Id.ToString();
                        objAlmacenar_estrategia.Attributes.Add("onclick", String.Format("AlmacenarEstrategia('{0}','{1}');", item_subroceso.Id, "txt_area_estrategia_id_" + item_subroceso.Id));
                        objAlmacenar_estrategia.Value = "Almacenar Estrategia";

                        objCell_Estrategias.Controls.Add(objAlmacenar_estrategia);
                        objRow_Estrategias.Cells.Add(objCell_Estrategias);

                        objHtmlTable.Rows.Add(objRow_Estrategias);

                        IQueryable<Model.Resultados_Proyecto> objEstrategias = new CResultados_proyecto().LoadResultados(item_subroceso.Id);

                        StringBuilder objsb = new StringBuilder();

                        objsb.Append("<div class='accordion'><h3><a href='#'>Estrategias</a></h3>");
                        objsb.Append("<div>");

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
                            objsb.Append(" <a title=\"Cronograma Resultado No." + enumeracion_Estrategias.ToString() + "\" class='pretty' href=\""+ Request.Url.Scheme + "://" + Request.Url.Authority + "/DiagramaGant.aspx?idResultado=" + item_estrategias.Id + "&iframe=true&amp;width=100%&amp;height=100%\"\"><img alt='Cronograma' src='/Icons/Calender.png' width='24px' /></a>");
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
    }

}