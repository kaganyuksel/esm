using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;
using System.Web.UI.HtmlControls;

namespace ESM
{
    public partial class ArbolProblemas : System.Web.UI.Page
    {
        #region Propiedades Publicas y Privadas

        protected Cproyecto objCpoyecto = new Cproyecto();
        protected CEfectos objCEfectos = new CEfectos();
        int _idproyecto = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["idproyecto"] != null)
            {
                hidproyecto.Value = Session["idproyecto"].ToString();
                _idproyecto = Convert.ToInt32(Session["idproyecto"]);
            }
            if (Bandera.Value == "1")
            {
                ObtenerResultados(_idproyecto);
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

        }

        protected void lknAlmacenarE_Click(object sender, EventArgs e)
        {
            #region Insercion de Efecto

            int idproyecto = Convert.ToInt32(Session["idproyecto"]);
            bool agroefecto = objCEfectos.Add(txtEfecto1.Text, txtCausa1.Text, idproyecto);

            if (agroefecto)
                alerthq.Value = "1";
            else
                alerthq.Value = "0";

            gvEfectos.DataBind();
            #endregion

        }

        //protected void lknAlmacenarC_Click(object sender, EventArgs e)
        //{
        //    #region Insercion Causa
        //    int idefecto = Convert.ToInt32(cboefectos.SelectedItem.Value);
        //    bool actualizo_causa = objCEfectos.Update(idefecto, txtCausa1.Text);

        //    if (actualizo_causa)
        //        alerthq.Value = "1";
        //    else
        //        alerthq.Value = "0";

        //    gvCausas.DataBind();
        //    #endregion

        //}

        protected void gvProyectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.Remove("idproyecto");

            GridViewRow objRow = gvProyectos.SelectedRow;
            int idproyecto = Convert.ToInt32(objRow.Cells[1].Text);
            Session.Add("idproyecto", idproyecto);
            txtproblema.Text = objRow.Cells[2].Text;
            txtProposito.Text = objRow.Cells[2].Text;
            txtproblema.ReadOnly = true;

            CargarProposito(txtproblema.Text);

            lknAlmacenarP.Enabled = false;

            divefectos.Visible = true;
            divproblema.Visible = true;
            lknAlmacenarP.Enabled = true;
            divNuevo.Visible = false;
            divCargado.Visible = true;
            divproyectos.Visible = false;

            lknAlmacenarP.Enabled = false;

            ObtenerResultados(idproyecto);
        }

        protected void CargarProposito(string problema)
        {
            try
            {
                string proposito = Cproyecto.CargarProposito(_idproyecto);

                if (proposito != null)
                    txtProposito.Text = proposito;
                else
                    txtProposito.Text = problema;
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
                foreach (var item in objCausas_Efecto)
                {
                    HtmlGenericControl span = new HtmlGenericControl("span");

                    HtmlGenericControl br = new HtmlGenericControl("br");

                    span.Attributes.Add("width", "100%");

                    Label objlabel = new Label();
                    objlabel.Text = (contador + 1).ToString();

                    Label objlabelid = new Label();
                    objlabelid.Text = item.Id.ToString();
                    objlabelid.Visible = false;

                    var inputtextcausa = new TextBox();
                    inputtextcausa.ID = "txtcausaR" + contador.ToString();
                    inputtextcausa.Text = item.Causa.ToString();
                    inputtextcausa.Attributes.Add("float", "left");
                    inputtextcausa.Attributes.CssStyle.Add("width", ancho_textbox.ToString() + "%");

                    var inputresultado = new TextBox();
                    inputresultado.ID = "txtresultado" + contador.ToString();
                    if (item.Resultado != null)
                        inputresultado.Text = item.Resultado.ToString();
                    inputresultado.Attributes.Add("float", "left");
                    inputresultado.Attributes.Add("placeholder", "Resultado numero" + (contador + 1).ToString());
                    inputresultado.Attributes.CssStyle.Add("width", ancho_textbox.ToString() + "%");

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
                    detalles.Attributes.Add("href", Request.Url.Scheme + "://" + Request.Url.Authority + "/DetallesMarcoLogico.aspx?idResultado=" + item.Id + "&iframe=true&amp;width=100%&amp;height=100%");
                    detalles.InnerHtml = "<img alt='Detalles' src='/Icons/details.png' width='24px' />";

                    var cronograma = new HtmlAnchor();
                    cronograma.Attributes.Add("class", "pretty");
                    cronograma.Attributes.Add("href", Request.Url.Scheme + "://" + Request.Url.Authority + "/DiagramaGant.aspx?idResultado=" + item.Id + "&iframe=true&amp;width=100%&amp;height=100%");
                    cronograma.InnerHtml = "<img alt='Cronograma' src='/Icons/Calender.png' width='24px' />";

                    span.Controls.Add(objlabel);
                    span.Controls.Add(objlabelid);
                    span.Controls.Add(inputtextcausa);
                    span.Controls.Add(inputresultado);
                    span.Controls.Add(almacenarresultado);
                    span.Controls.Add(detalles);
                    span.Controls.Add(cronograma);

                    presultados.Controls.Add(span);

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
                    txtactividad.ID = "txtactividadn" + (contador + 1).ToString();
                    var lblpresupuesto = new Label();
                    lblpresupuesto.Text = "Presupuesto";
                    var txtpresupuesto = new TextBox();
                    txtpresupuesto.ID = "txtpresupuesto" + (contador + 1).ToString();
                    txtpresupuesto.Attributes.CssStyle.Add("width", "20%");

                    var btnalmacenaractividad = new ImageButton();
                    btnalmacenaractividad.ImageUrl = "/Icons/1314641093_plus_48.png";
                    btnalmacenaractividad.Attributes.CssStyle.Add("width", "24px");
                    btnalmacenaractividad.Attributes.Add("onclick", "AlmacenarActividad('" + item.Id.ToString() + "','" + "ContentPlaceHolder1_" + txtactividad.ID + "','" + "ContentPlaceHolder1_" + txtpresupuesto.ID + "');");


                    contenido_acordion.Controls.Add(lblnactividad);
                    contenido_acordion.Controls.Add(txtactividad);
                    contenido_acordion.Controls.Add(br);
                    contenido_acordion.Controls.Add(lblpresupuesto);
                    contenido_acordion.Controls.Add(txtpresupuesto);
                    contenido_acordion.Controls.Add(btnalmacenaractividad);

                    #region Consultar Actividades

                    IQueryable<ESM.Model.Actividade> ac = null;

                    try
                    {
                        ac = from a in new ESM.Model.ESMBDDataContext().Actividades
                             where a.Resultado_id == item.Id
                             select a;
                    }
                    catch (Exception) { }

                    if (ac.Count() != 0)
                    {
                        var tituloActividades = new HtmlGenericControl("h3");

                        tituloActividades.InnerHtml = "Actividades Existentes";
                        tituloActividades.Attributes.CssStyle.Add("color", "#005EA7");

                        contenido_acordion.Controls.Add(br);
                        contenido_acordion.Controls.Add(br);
                        contenido_acordion.Controls.Add(tituloActividades);
                        contenido_acordion.Controls.Add(br);
                        contenido_acordion.Controls.Add(br);

                        foreach (var actividadbyresult in ac)
                        {
                            var lblcactividad = new Label();
                            lblcactividad.Text = "Nombre Actividad";
                            var txtcactividad = new TextBox();
                            txtcactividad.Attributes.CssStyle.Add("width", "20%");
                            txtcactividad.ID = "txtactividadc" + actividadbyresult.Id.ToString();
                            txtcactividad.Text = actividadbyresult.Actividad;
                            var lblcpresupuesto = new Label();
                            lblpresupuesto.Text = "Presupuesto";
                            var txtcpresupuesto = new TextBox();
                            txtcpresupuesto.ID = "txtpresupuestoc" + actividadbyresult.Id.ToString();
                            txtcpresupuesto.Attributes.CssStyle.Add("width", "20%");
                            txtcpresupuesto.Text = actividadbyresult.Presupuesto.ToString();

                            var btnactualizaractividad = new ImageButton();
                            btnactualizaractividad.ImageUrl = "/Icons/save-icon.png";
                            btnactualizaractividad.Attributes.CssStyle.Add("width", "24px");
                            btnactualizaractividad.Attributes.Add("onclick", "ActualizarActividad('" + actividadbyresult.Id.ToString() + "','" + "ContentPlaceHolder1_" + txtcactividad.ID + "','" + "ContentPlaceHolder1_" + txtcpresupuesto.ID + "');");

                            contenido_acordion.Controls.Add(lblcactividad);
                            contenido_acordion.Controls.Add(txtcactividad);
                            contenido_acordion.Controls.Add(lblcpresupuesto);
                            contenido_acordion.Controls.Add(txtcpresupuesto);
                            contenido_acordion.Controls.Add(btnactualizaractividad);

                            var detalles_Actividad = new HtmlAnchor();
                            detalles_Actividad.Attributes.Add("class", "pretty");
                            detalles_Actividad.Attributes.Add("href", Request.Url.Scheme + "://" + Request.Url.Authority + "/DetallesMarcoLogico.aspx?idActividad=" + actividadbyresult.Id + "&iframe=true&amp;width=100%&amp;height=100%");
                            detalles_Actividad.InnerHtml = "<img alt='Detalles' src='/Icons/details.png' width='24px' />";

                            contenido_acordion.Controls.Add(detalles_Actividad);

                            br = new HtmlGenericControl("br");

                            contenido_acordion.Controls.Add(br);


                        }
                    }

                    #endregion

                    actividades.Controls.Add(titulo);
                    actividades.Controls.Add(contenido_acordion);

                    presultados.Controls.Add(actividades);
                    presultados.Controls.Add(br);


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

        }



    }
}