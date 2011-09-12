using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class LecturaContextoEE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                if (!Page.IsPostBack)
                {
                    gvResultados.DataSourceID = "ldsies";
                    gvResultados.Visible = true;
                    gvResultados.DataBind();

                    ObtenerTema(gvResultados);
                    modEESeleccion.Visible = true;
                }
            }
            else
                Response.Write("/Login.aspx");
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            LC.Visible = true;
        }

        protected void gvResultados_SelectedIndexChanged(object sender, EventArgs e)
        {
            modEESeleccion.Visible = false;
            LC.Visible = true;

            Session.Add("codie", gvResultados.SelectedRow.Cells[1].Text);
            string codie = Session["codie"].ToString();

            Label objIDIE = (Label)gvResultados.SelectedRow.FindControl("IDIE");

            int idie = Convert.ToInt32(objIDIE.Text);

            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
            var ie = from ies in db.Establecimiento_Educativo
                     where ies.IdIE == idie
                     select ies;

            foreach (var item in ie)
            {
                txtNombre.Text = item.Nombre;
                txtTelefonos.Text = item.Telefono;
                txtDireccion.Text = item.Direccion;
                txtCodigo.Text = item.CodigoDane;
                txtCorreoElectronico.Text = item.Email;
                txtNombre.Enabled = false;
                txtTelefonos.Enabled = false;
                txtDireccion.Enabled = false;
                txtCorreoElectronico.Enabled = false;
                txtCodigo.Enabled = false;
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

        protected void btnAlmacenar_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('El proceso de almacenamiento finalizo correctamente.');</script>");
            Response.Redirect("LecturaContextoEE.aspx");
        }
    }
}