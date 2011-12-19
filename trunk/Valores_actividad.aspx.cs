using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class Valores_actividad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvIndicadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objrow = gvIndicadores.SelectedRow;

            Session.Add("indicador_id", objrow.Cells[1].Text);

            txtindicador.Text = objrow.Cells[2].Text;

            gvMediciones_Indicador.DataSource = new Objetos.CActividades().getMed_Indicador(Convert.ToInt32(objrow.Cells[1].Text));
            gvMediciones_Indicador.DataBind();

        }

        protected void lknAlmacenar_Click(object sender, EventArgs e)
        {
            try
            {
                int indicador_id = Convert.ToInt32(Session["indicador_id"]);
                bool almaceno_correctamente = new Objetos.CActividades().AddMeta_Valor(indicador_id, Convert.ToDateTime(txtFecha.Text), Convert.ToInt32(txtmeta.Text), Convert.ToInt32(txtValor.Text));

                txtFecha.Text = "";
                txtValor.Text = "0";
                txtmeta.Text = "0";

                gvMediciones_Indicador.DataSource = new Objetos.CActividades().getMed_Indicador(indicador_id);
                gvMediciones_Indicador.DataBind();
            }
            catch (Exception) { Response.Write("<script>alert('Por favor vuelva a intentarlo y recuerde que los valores fecha, meta y valor son requeridos.')</script>"); }
        }

    }
}