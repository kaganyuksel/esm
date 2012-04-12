using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class Presupuesto_actividad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var objCactividades = new Objetos.CActividades();

            txtactividad.Value = objCactividades.getActividad_name(Convert.ToInt32(Request.QueryString["idActividad"]));

            gvMediciones_Indicador.DataSource = objCactividades.getMed_presupuesto(Convert.ToInt32(Request.QueryString["idActividad"]));
            gvMediciones_Indicador.DataBind();

            a_Reporte.HRef = "/ReporteActividades.aspx?id=" + Request.QueryString["idActividad"].ToString();
        }

        protected void lknAlmacenar_Click(object sender, EventArgs e)
        {
            try
            {
                int actividad_id = Convert.ToInt32(Request.QueryString["idActividad"]);
                bool almaceno_correctamente = false;
                if (li_valor_meta.Visible != true)
                {
                    almaceno_correctamente = new Objetos.CActividades().AddMeta_presupuesto(actividad_id, Convert.ToDateTime(txtFecha.Text), Convert.ToDecimal(txtmeta_presupuesto.Text));

                    txtFecha.Text = "";
                    txtmeta_presupuesto.Text = "0";

                    gvMediciones_Indicador.DataSource = new Objetos.CActividades().getMed_presupuesto(actividad_id);
                    gvMediciones_Indicador.DataBind();
                }
                else
                {
                    almaceno_correctamente = new Objetos.CActividades().UpdateMeta_presupuesto(idmeta.Value, Convert.ToInt32(actividad_id), Convert.ToDecimal(txtValor.Text), Convert.ToDateTime(txtFecha.Text));

                    txtFecha.Text = "";
                    txtmeta_presupuesto.Text = "0";
                    txtValor.Text = "0";

                    gvMediciones_Indicador.DataSource = new Objetos.CActividades().getMed_presupuesto(actividad_id);
                    gvMediciones_Indicador.DataBind();

                    li_valor_meta.Visible = false;

                    txtmeta_presupuesto.Enabled = true;
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
            txtmeta_presupuesto.Text = objGridViewRow.Cells[3].Text;
            txtmeta_presupuesto.Enabled = false;
            txtValor.Text = objGridViewRow.Cells[4].Text;
            li_valor_meta.Visible = true;

            idmeta.Value = objGridViewRow.Cells[1].Text;
        }
    }
}