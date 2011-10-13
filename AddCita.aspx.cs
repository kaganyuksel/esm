using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;

namespace ESM
{
    public partial class AddCita : System.Web.UI.Page
    {
        #region Propiedades Publicas y Privadas

        DateTime fechainicial = DateTime.Now;
        DateTime fechafinal = DateTime.Now;
        int dias = 0;
        int minutos = 0;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["start"] != null && Request.QueryString["end"] != null)
                {
                    fechainicial = Convert.ToDateTime(Request.QueryString["start"]);
                    if (Request.QueryString["end"].ToString() != "\"\"")
                        fechafinal = Convert.ToDateTime(Request.QueryString["end"]);
                    else
                    {
                        dias = Convert.ToInt32(Request.QueryString["dias"]);
                        minutos = Convert.ToInt32(Request.QueryString["minutos"]);
                    }

                    Session.Add("start", fechainicial);
                    Session.Add("end", fechainicial);

                    if (Request.QueryString["edit"] != null && Convert.ToBoolean(Request.QueryString["edit"]) != false)
                    {
                        int idcita = Convert.ToInt32(Request.QueryString["idcita"]);

                        if (dias == 0 && minutos == 0)
                            Actualizar(idcita, fechainicial, fechafinal);
                        else
                            Actualizar(idcita, fechainicial, dias, minutos);
                    }
                }
            }
        }

        protected void Actualizar(int idcita, DateTime fechainicial, int dias, int minutos)
        {
            bool resultado = CCitas.ActualizarCita(idcita, fechainicial, dias, minutos);

            if (resultado)
            {

            }
            else
            {

            }
        }

        protected void Actualizar(int idcita, DateTime fechainicial, DateTime fechafinal)
        {
            bool resultado = CCitas.ActualizarCita(idcita, fechainicial, fechafinal);

            if (resultado)
            {

            }
            else
            {

            }
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

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gvresultadosIE.Visible = false;
            gvresultadosSE.Visible = true;
            lblmensaje.Visible = false;
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gvresultadosIE.Visible = true;
            gvresultadosSE.Visible = false;
            lblmensaje.Visible = false;
        }

        protected void gvresultadosSE_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ObtenerTema(gvresultadosSE);
        }

        protected void gvresultadosIE_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ObtenerTema(gvresultadosIE);
        }

        protected void gvresultadosIE_PageIndexChanged(object sender, EventArgs e)
        {
            ObtenerTema(gvresultadosIE);
        }

        protected void gvresultadosSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objRow = gvresultadosSE.SelectedRow;
            Label lblidse = (Label)objRow.FindControl("lblidse");

            int id = Convert.ToInt32(lblidse.Text);

            AlmacenarCita(id, 0);
        }

        protected void gvresultadosIE_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objRow = gvresultadosIE.SelectedRow;
            Label lblidie = (Label)objRow.FindControl("lblidie");
            int id = Convert.ToInt32(lblidie.Text);

            AlmacenarCita(id, 1);
        }

        protected void AlmacenarCita(int id, int tipo)
        {
            DateTime start = Convert.ToDateTime(Session["start"]);
            DateTime end = Convert.ToDateTime(Session["end"]);

            bool result = CCitas.AsignarCita(id, start, end, tipo);

            if (result)
            {
                lblmensaje.Text = "La cita fue asignada satisfactoriamente.";
                System.Drawing.Color c = System.Drawing.ColorTranslator.FromHtml("#0064B5");
                lblmensaje.ForeColor = c;
            }
            else
            {
                lblmensaje.Text = "La cita no se puede asignar por un error inesperado.";
                System.Drawing.Color c = System.Drawing.ColorTranslator.FromHtml("#992828");
                lblmensaje.ForeColor = c;

            }

            gvresultadosIE.Visible = false;
            gvresultadosSE.Visible = false;

            lblmensaje.Visible = true;
        }
    }
}