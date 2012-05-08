using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;

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

            Session.Add("indicadores_id", objrow.Cells[1].Text);

            txtindicador.Text = objrow.Cells[2].Text;

            gvMediciones_Indicador.DataSource = new Objetos.CActividades().getMed_Indicador(Convert.ToInt32(objrow.Cells[1].Text));
            gvMediciones_Indicador.DataBind();

            a_Reporte.HRef = "/ReporteIndicadores.aspx?id=" + objrow.Cells[1].Text;
        }

        protected void lknAlmacenar_Click(object sender, EventArgs e)
        {
            try
            {
                int indicador_id = Convert.ToInt32(Session["indicadores_id"]);
                bool almaceno_correctamente = false;
                if (li_valor_meta.Visible != true)
                {
                    //almaceno_correctamente = new Objetos.CActividades().AddMeta_Valor(indicador_id, Convert.ToDateTime(txtFecha.Text), Convert.ToInt32(txtmeta_indicador.Text));

                    txtFecha.Text = "";
                    txtmeta_indicador.Text = "0";

                    gvMediciones_Indicador.DataSource = new Objetos.CActividades().getMed_Indicador(indicador_id);
                    gvMediciones_Indicador.DataBind();
                }
                else
                {
                    almaceno_correctamente = new Objetos.CActividades().UpdateMeta(idmeta.Value, Convert.ToInt32(indicador_id), Convert.ToInt32(txtValor.Text), Convert.ToDateTime(txtFecha.Text));

                    txtFecha.Text = "";
                    txtmeta_indicador.Text = "0";
                    txtValor.Text = "0";

                    gvMediciones_Indicador.DataSource = new Objetos.CActividades().getMed_Indicador(indicador_id);
                    gvMediciones_Indicador.DataBind();

                    li_valor_meta.Visible = false;

                    txtmeta_indicador.Enabled = true;
                    txtFecha.Enabled = true;
                }
            }
            catch (Exception) { Response.Write("<script>alert('Por favor vuelva a intentarlo y recuerde que los valores fecha, meta y valor son requeridos.')</script>"); }
        }

        protected void gvMediciones_Indicador_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objGridViewRow = gvMediciones_Indicador.SelectedRow;

            txtFecha.Text = objGridViewRow.Cells[2].Text;
            txtFecha.Enabled = false;
            txtmeta_indicador.Text = objGridViewRow.Cells[3].Text;
            txtmeta_indicador.Enabled = false;
            txtValor.Text = objGridViewRow.Cells[4].Text;
            li_valor_meta.Visible = true;

            idmeta.Value = objGridViewRow.Cells[1].Text;
        }

    }
}