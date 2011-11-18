using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;
using System.Collections;
using System.Web.UI.HtmlControls;

namespace ESM
{
    public partial class DetallesMarcoLogico : System.Web.UI.Page
    {
        #region Propiedades Publicas y Privadas

        protected int idproyecto = 0;
        protected int idresultado = 0;
        protected int idactividad = 0;
        protected bool esProyecto = false;
        protected bool esResultado = false;
        protected bool esActividad = false;

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["idResultado"] != null)
            {
                esResultado = true;
                idresultado = Convert.ToInt32(Request.QueryString["idResultado"]);
                gvresultados.Visible = true;
            }
            else if (Request.QueryString["idActividad"] != null)
            {
                esActividad = true;
                idactividad = Convert.ToInt32(Request.QueryString["idActividad"]);
                lblFechaFinal.Visible = true;
                txtFechaFinal.Visible = true;
                gvIndicadores_Actividad.Visible = true;

            }
            else if (Session["idproyecto"] != null)
            {
                esProyecto = true;
                idproyecto = Convert.ToInt32(Session["idproyecto"]);
                gvproyecto.Visible = true;
            }
            if (!Page.IsPostBack)
            {
                sortable1.DataSourceID = "lqMediosVerificacion";
                sortable1.DataBind();
            }

        }

        protected void getMediosVerificacion()
        {
            var medios = from m in new ESM.Model.ESMBDDataContext().Medios_de_verificacions
                         select m;

            var list = new System.Web.UI.HtmlControls.HtmlGenericControl("ul");

            int contador = 0;

            foreach (var item in medios)
            {
                var item_list = new System.Web.UI.HtmlControls.HtmlGenericControl("li");

                item_list.ID = "node" + contador;
                item_list.InnerText = item.Medio_de_verificacion;

                contador++;
            }

        }

        protected void lknAgregarIndicador_Click(object sender, EventArgs e)
        {
            if (esProyecto)
                AlmacenarProyecto();
            if (esResultado)
                AlmacenarResultado();
            if (esActividad)
                AlmacenarActividad();
        }

        private void AlmacenarActividad()
        {
            try
            {
                CActividades objActividades = new CActividades();

                int verboid = Convert.ToInt32(cboverbos.SelectedValue);
                int unidadid = Convert.ToInt32(cboUnidades.SelectedValue);
                DateTime fecha_inicial = Convert.ToDateTime(txtFechaIndicador.Text);
                DateTime fecha_final = Convert.ToDateTime(txtFechaFinal.Text);
                int meta = Convert.ToInt32(Meta.Text);

                objActividades.AddIndicador(idactividad, txtindicadorg.Text, verboid, unidadid, fecha_inicial.Date, fecha_final, meta);

                bool mediosvacios = objActividades.RemoveMedios(idresultado);

                if (mediosvacios)
                {
                    string[] medios_html = mediosinput.Value.Trim(',').Split(',');

                    for (int i = 0; i < medios_html.Length; i++)
                    {
                        int medioid = new CMedios().getMedioid(medios_html[i]);

                        if (medioid != 0)
                            objActividades.AddMedios(idactividad, medioid);
                    }
                }

                bool supuestos = objActividades.RemoveSupuestos(idresultado);

                if (supuestos)
                {
                    string[] supuestos_html = supuestosinput.Value.Trim(',').Split(',');

                    for (int i = 0; i < supuestos_html.Length; i++)
                    {
                        int supuestoid = new Csupuestos().getSupuesto_id(supuestos_html[i]);

                        if (supuestoid != 0)
                            objActividades.AddSupuestos(idactividad, supuestoid);
                    }
                }
            }
            catch (Exception) { }

        }

        protected void AlmacenarProyecto()
        {
            try
            {
                Cproyecto objCproyecto = new Cproyecto();

                objCproyecto.Update(idproyecto, txtindicadorg.Text);

                bool mediosvacios = objCproyecto.RemoveMedios(idproyecto);

                if (mediosvacios)
                {
                    string[] medios_html = mediosinput.Value.Trim(',').Split(',');

                    for (int i = 0; i < medios_html.Length; i++)
                    {
                        int medioid = new CMedios().getMedioid(medios_html[i]);

                        if (medioid != 0)
                            objCproyecto.AddMedios(idproyecto, medioid);
                    }
                }

                bool supuestos = objCproyecto.RemoveSupuestos(idproyecto);

                if (supuestos)
                {
                    string[] supuestos_html = supuestosinput.Value.Trim(',').Split(',');

                    for (int i = 0; i < supuestos_html.Length; i++)
                    {
                        int supuestoid = new Csupuestos().getSupuesto_id(supuestos_html[i]);

                        if (supuestoid != 0)
                            objCproyecto.AddSupuestos(idproyecto, supuestoid);
                    }
                }

            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        protected void AlmacenarResultado()
        {
            try
            {
                CEfectos objCresultado = new CEfectos();

                objCresultado.Update(idresultado, null, null, txtindicadorg.Text);

                bool mediosvacios = objCresultado.RemoveMedios(idresultado);

                if (mediosvacios)
                {
                    string[] medios_html = mediosinput.Value.Trim(',').Split(',');

                    for (int i = 0; i < medios_html.Length; i++)
                    {
                        int medioid = new CMedios().getMedioid(medios_html[i]);

                        if (medioid != 0)
                            objCresultado.AddMedios(idresultado, medioid);
                    }
                }

                bool supuestos = objCresultado.RemoveSupuestos(idresultado);

                if (supuestos)
                {
                    string[] supuestos_html = supuestosinput.Value.Trim(',').Split(',');

                    for (int i = 0; i < supuestos_html.Length; i++)
                    {
                        int supuestoid = new Csupuestos().getSupuesto_id(supuestos_html[i]);

                        if (supuestoid != 0)
                            objCresultado.AddSupuestos(idresultado, supuestoid);
                    }
                }

            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        protected void btnAlmacenaSupuesto_Click(object sender, ImageClickEventArgs e)
        {
            Csupuestos objCsupuestos = new Csupuestos();

            bool crea_supuesto = objCsupuestos.AddSupuesto(txtsupuesto.Text);

            if (crea_supuesto)
                sortable3.DataBind();

            txtsupuesto.Text = "";

        }

        protected void btnAlmacenaMedio_Click(object sender, ImageClickEventArgs e)
        {
            CMedios objCMedios = new CMedios();

            bool crea_medio = objCMedios.AddMedios(txtmedio.Text);

            if (crea_medio)
                sortable1.DataBind();

            txtmedio.Text = "";
        }
    }
}