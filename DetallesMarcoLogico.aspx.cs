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

                titulo_detalles.InnerHtml = "Detalles para Resultados";
            }
            else if (Request.QueryString["idActividad"] != null)
            {
                esActividad = true;
                idactividad = Convert.ToInt32(Request.QueryString["idActividad"]);
                lblFechaFinal.Visible = true;
                txtFechaFinal.Visible = true;
                gvIndicadores_Actividad.Visible = true;
                chxSSP.Visible = true;
                ModResponsables.Visible = true;
                tabs_res.Visible = true;
                pest_responsable.Visible = true;
                titulo_detalles.InnerHtml = "Detalles para Actividades";
            }
            else if (Session["idproyecto"] != null)
            {
                esProyecto = true;
                idproyecto = Convert.ToInt32(Session["idproyecto"]);
                gvproyecto.Visible = true;
                titulo_detalles.InnerHtml = "Detalles para Proyectos";

            }
            if (!Page.IsPostBack)
            {
                sortable1.DataSourceID = "lqMediosVerificacion";
                sortable1.DataBind();

                if (esProyecto)
                {
                    getMediosProyectos(idproyecto);
                    getSupuestosProyectos(idproyecto);
                }
                else if (esResultado)
                {
                    getMediosResultados(idresultado);
                    getSupuestosResultados(idresultado);
                }
                else if (esActividad)
                {
                    getMediosActividad(idactividad);
                    getSupuestosActividad(idactividad);
                    getResponsablesActividad(idactividad);
                }
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
            {
                AlmacenarProyecto();
                gvproyecto.DataBind();
            }
            if (esResultado)
            {
                AlmacenarResultado();
                gvresultados.DataBind();
            }
            if (esActividad && actualizaActividad.Value == "-1")
            {
                AlmacenarActividad();
                gvIndicadores_Actividad.DataBind();
            }
            else if (esActividad && actualizaActividad.Value == "1")
            {
                ActualizarActividad();
                actualizaActividad.Value = "-1";
            }

            txtindicadorg.Text = "";
        }

        private void AlmacenarActividad()
        {
            try
            {
                CActividades objActividades = new CActividades();
                if (txtFechaIndicador.Text.Trim().Length != 0 && txtFechaFinal.Text.Trim().Length != 0 && Meta.Text != "0")
                {
                    int verboid = Convert.ToInt32(cboverbos.SelectedValue);
                    int unidadid = Convert.ToInt32(cboUnidades.SelectedValue);
                    DateTime fecha_inicial = Convert.ToDateTime(txtFechaIndicador.Text);
                    DateTime fecha_final = Convert.ToDateTime(txtFechaFinal.Text);
                    int meta = Convert.ToInt32(Meta.Text);

                    //objActividades.AddIndicador(idactividad, txtindicadorg.Text, verboid, unidadid, fecha_inicial.Date, fecha_final, meta, chxSSP.Checked);
                }
                bool mediosvacios = objActividades.RemoveMedios(idactividad);

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

                bool supuestos = objActividades.RemoveSupuestos(idactividad);

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

                bool responsables = objActividades.RemoveResponsables(idactividad);

                if (responsables)
                {
                    string[] responsables_html = responsablesinput.Value.Trim(',').Split(',');

                    for (int i = 0; i < responsables_html.Length; i++)
                    {
                        int responsableid = new Cresponsables().getResponsable_id(responsables_html[i]);

                        if (responsableid != 0)
                            objActividades.AddResponsables(idactividad, responsableid);
                    }
                }

                getMediosActividad(idactividad);
                getSupuestosActividad(idactividad);
                getResponsablesActividad(idactividad);
            }
            catch (Exception) { }

        }

        private void ActualizarActividad()
        {
            try
            {
                CActividades objActividades = new CActividades();

                int verboid = Convert.ToInt32(cboverbos.SelectedValue);
                int unidadid = Convert.ToInt32(cboUnidades.SelectedValue);
                DateTime fecha_inicial = Convert.ToDateTime(txtFechaIndicador.Text);
                DateTime fecha_final = Convert.ToDateTime(txtFechaFinal.Text);
                int meta = Convert.ToInt32(Meta.Text);
                int indicador_id = Convert.ToInt32(Session["idindicador_actividad"]);
                //objActividades.UpdateIndicador(indicador_id, txtindicadorg.Text, verboid, unidadid, fecha_inicial.Date, fecha_final, meta, chxSSP.Checked);

                Session.Remove("idindicador_actividad");

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

                bool responsables = objActividades.RemoveResponsables(idactividad);

                if (responsables)
                {
                    string[] responsables_html = responsablesinput.Value.Trim(',').Split(',');

                    for (int i = 0; i < responsables_html.Length; i++)
                    {
                        int responsableid = new Csupuestos().getSupuesto_id(responsables_html[i]);

                        if (responsableid != 0)
                            objActividades.AddResponsables(idactividad, responsableid);
                    }
                }

                getMediosActividad(idactividad);
                getSupuestosActividad(idactividad);
                getResponsablesActividad(idactividad);
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

                getMediosProyectos(idproyecto);
                getSupuestosProyectos(idproyecto);
            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        protected void AlmacenarResultado()
        {
            try
            {
                CResultados_proyecto objCresultado = new CResultados_proyecto();

                objCresultado.Update(idresultado, null, txtindicadorg.Text);

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

                getMediosResultados(idresultado);
                getSupuestosResultados(idresultado);
            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        protected void btnAlmacenaSupuesto_Click(object sender, ImageClickEventArgs e)
        {
            Csupuestos objCsupuestos = new Csupuestos();

            bool crea_supuesto = objCsupuestos.AddSupuesto(txtsupuesto.Text);

            if (crea_supuesto)
            {
                int cant_supuestos = sortable3.Items.Count;

                sortable3.DataBind();

                for (int i = 0; i < cant_supuestos; i++)
                {
                    sortable3.Items.Remove(sortable3.Items[i]);
                }
            }

            txtsupuesto.Text = "";

        }

        protected void btnAlmacenaMedio_Click(object sender, ImageClickEventArgs e)
        {
            CMedios objCMedios = new CMedios();

            bool crea_medio = objCMedios.AddMedios(txtmedio.Text);

            if (crea_medio)
            {

                int cant_medios = sortable1.Items.Count;

                sortable1.DataBind();

                for (int i = 0; i < cant_medios; i++)
                {
                    sortable1.Items.Remove(sortable1.Items[i]);
                }
            }

            txtmedio.Text = "";
        }

        protected void gvIndicadores_Actividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objrow = gvIndicadores_Actividad.SelectedRow;
            txtindicadorg.Text = objrow.Cells[3].Text;
            Session.Add("idindicador_actividad", objrow.Cells[1].Text);
            actualizaActividad.Value = "1";
        }

        public void getMediosProyectos(int proyecto_id)
        {
            try
            {
                mediosinput.Value = "";

                for (int r = 0; r < sortable2.Items.Count; r++)
                {
                    if (r != 0)
                        sortable2.Items.Remove(sortable2.Items[r]);
                }

                CMedios objCMedios = new CMedios();

                IQueryable<ESM.Model.Medios_de_verificacion> medios_by_proyecto = objCMedios.getMediosProyecto(proyecto_id);

                foreach (var item in medios_by_proyecto)
                {
                    sortable2.Items.Add(new ListItem(item.Medio_de_verificacion));

                    mediosinput.Value = mediosinput.Value + "," + item.Medio_de_verificacion.ToString();
                }

                mediosinput.Value = mediosinput.Value.Trim(',');

                for (int i = 0; i < sortable2.Items.Count; i++)
                {
                    for (int j = 0; j < sortable1.Items.Count; j++)
                    {
                        if (sortable2.Items[i].Value == sortable1.Items[j].Text)
                        {
                            sortable1.Items.Remove(sortable1.Items[j]);
                            break;
                        }
                    }
                }

            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        public void getSupuestosProyectos(int proyecto_id)
        {
            try
            {
                supuestosinput.Value = "";

                for (int r = 0; r < sortable4.Items.Count; r++)
                {
                    if (r != 0)
                        sortable4.Items.Remove(sortable4.Items[r]);
                }

                Csupuestos objCSupuestos = new Csupuestos();

                IQueryable<ESM.Model.Supuesto> supuestos_by_proyecto = objCSupuestos.getSupuestosProyecto(proyecto_id);

                foreach (var item in supuestos_by_proyecto)
                {
                    sortable4.Items.Add(new ListItem(item.supuesto1));

                    supuestosinput.Value = supuestosinput.Value + "," + item.supuesto1.ToString();
                }

                supuestosinput.Value = supuestosinput.Value.Trim(',');

                for (int i = 0; i < sortable4.Items.Count; i++)
                {
                    for (int j = 0; j < sortable3.Items.Count; j++)
                    {
                        if (sortable4.Items[i].Value == sortable3.Items[j].Text)
                        {
                            sortable3.Items.Remove(sortable3.Items[j]);
                            break;
                        }
                    }
                }

            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        public void getMediosResultados(int resultado_id)
        {
            try
            {
                mediosinput.Value = "";

                for (int r = 0; r < sortable2.Items.Count; r++)
                {
                    if (r != 0)
                        sortable2.Items.Remove(sortable2.Items[r]);
                }

                CMedios objCMedios = new CMedios();

                IQueryable<ESM.Model.Medios_de_verificacion> medios_by_resultado = objCMedios.getMediosResultado(resultado_id);

                foreach (var item in medios_by_resultado)
                {
                    sortable2.Items.Add(new ListItem(item.Medio_de_verificacion));

                    mediosinput.Value = mediosinput.Value + "," + item.Medio_de_verificacion.ToString();
                }

                mediosinput.Value = mediosinput.Value.Trim(',');

                for (int i = 0; i < sortable2.Items.Count; i++)
                {
                    for (int j = 0; j < sortable1.Items.Count; j++)
                    {
                        if (sortable2.Items[i].Value == sortable1.Items[j].Text)
                        {
                            sortable1.Items.Remove(sortable1.Items[j]);
                            break;
                        }
                    }
                }

            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        public void getSupuestosResultados(int resultado_id)
        {
            try
            {
                supuestosinput.Value = "";

                for (int r = 0; r < sortable4.Items.Count; r++)
                {
                    if (r != 0)
                        sortable4.Items.Remove(sortable4.Items[r]);
                }

                Csupuestos objCSupuestos = new Csupuestos();

                IQueryable<ESM.Model.Supuesto> supuestos_by_resultado = objCSupuestos.getSupuestosResultado(resultado_id);

                foreach (var item in supuestos_by_resultado)
                {
                    sortable4.Items.Add(new ListItem(item.supuesto1));

                    supuestosinput.Value = supuestosinput.Value + "," + item.supuesto1.ToString();
                }

                supuestosinput.Value = supuestosinput.Value.Trim(',');

                for (int i = 0; i < sortable4.Items.Count; i++)
                {
                    for (int j = 0; j < sortable3.Items.Count; j++)
                    {
                        if (sortable4.Items[i].Value == sortable3.Items[j].Text)
                        {
                            sortable3.Items.Remove(sortable3.Items[j]);
                            break;
                        }
                    }
                }

            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        public void getMediosActividad(int actividad_id)
        {
            try
            {
                mediosinput.Value = "";

                for (int r = 0; r < sortable2.Items.Count; r++)
                {
                    if (r != 0)
                        sortable2.Items.Remove(sortable2.Items[r]);
                }

                CMedios objCMedios = new CMedios();

                IQueryable<ESM.Model.Medios_de_verificacion> medios_by_actividad = objCMedios.getMediosActividad(actividad_id);

                foreach (var item in medios_by_actividad)
                {
                    sortable2.Items.Add(new ListItem(item.Medio_de_verificacion));

                    mediosinput.Value = mediosinput.Value + "," + item.Medio_de_verificacion.ToString();
                }

                mediosinput.Value = mediosinput.Value.Trim(',');

                for (int i = 0; i < sortable2.Items.Count; i++)
                {
                    for (int j = 0; j < sortable1.Items.Count; j++)
                    {
                        if (sortable2.Items[i].Value == sortable1.Items[j].Text)
                        {
                            sortable1.Items.Remove(sortable1.Items[j]);
                            break;
                        }
                    }
                }

            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        public void getSupuestosActividad(int actividad_id)
        {
            try
            {
                supuestosinput.Value = "";

                for (int r = 0; r < sortable4.Items.Count; r++)
                {
                    if (r != 0)
                        sortable4.Items.Remove(sortable4.Items[r]);
                }

                Csupuestos objCSupuestos = new Csupuestos();

                IQueryable<ESM.Model.Supuesto> supuestos_by_actividad = objCSupuestos.getSupuestosActividad(actividad_id);

                foreach (var item in supuestos_by_actividad)
                {
                    sortable4.Items.Add(new ListItem(item.supuesto1));

                    supuestosinput.Value = supuestosinput.Value + "," + item.supuesto1.ToString();
                }

                supuestosinput.Value = supuestosinput.Value.Trim(',');

                for (int i = 0; i < sortable4.Items.Count; i++)
                {
                    for (int j = 0; j < sortable3.Items.Count; j++)
                    {
                        if (sortable4.Items[i].Value == sortable3.Items[j].Text)
                        {
                            sortable3.Items.Remove(sortable3.Items[j]);
                            break;
                        }
                    }
                }

            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }

        public void getResponsablesActividad(int actividad_id)
        {
            try
            {
                responsablesinput.Value = "";

                for (int r = 0; r < sortable6.Items.Count; r++)
                {
                    if (r != 0)
                        sortable6.Items.Remove(sortable6.Items[r]);
                }

                Cresponsables objCresponsables = new Cresponsables();

                IQueryable<ESM.Model.Usuario> responsables_by_actividad = objCresponsables.getResponsablesActividad(actividad_id);

                foreach (var item in responsables_by_actividad)
                {
                    sortable6.Items.Add(new ListItem(item.Usuario1));

                    responsablesinput.Value = supuestosinput.Value + "," + item.Usuario1.ToString();
                }

                responsablesinput.Value = responsablesinput.Value.Trim(',');

                for (int i = 0; i < sortable6.Items.Count; i++)
                {
                    for (int j = 0; j < sortable5.Items.Count; j++)
                    {
                        if (sortable6.Items[i].Value == sortable4.Items[j].Text)
                        {
                            sortable4.Items.Remove(sortable4.Items[j]);
                            break;
                        }
                    }
                }

            }
            catch (Exception) { /*TODO: JCMM: Controlador Exception*/ }

        }
    }
}