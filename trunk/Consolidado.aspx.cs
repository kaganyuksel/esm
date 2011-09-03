using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ESM.Objetos;

namespace ESM.Consolidado
{
    public partial class Consolidado : System.Web.UI.Page
    {
        ESM.amChartsnet.AmCharts objAmCharts = new amChartsnet.AmCharts();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                #region Carga de Treeview
                var trvambi = from am in new ESM.Model.ESMBDDataContext().Ambientes
                              select am;
                int contador = 0;
                foreach (var item in trvambi)
                {

                    trvConsolidacion.Nodes[0].ChildNodes[0].ChildNodes.Add(new TreeNode(item.Ambiente, item.IdAmbiente.ToString()));

                    var proces = from pro in new ESM.Model.ESMBDDataContext().Procesos
                                 where pro.IdAmbiente == item.IdAmbiente
                                 select pro;

                    foreach (var proc in proces)
                    {
                        trvConsolidacion.Nodes[0].ChildNodes[0].ChildNodes[contador].ChildNodes.Add(new TreeNode(proc.Proceso, proc.IdProceso.ToString()));

                        //var com = from comp in new ESM.Model.ESMBDDataContext().Componentes
                        //          where comp.IdProceso == proc.IdProceso
                        //          select comp;

                        //foreach (var compo in com)
                        //{
                        //    if (trvConsolidacion.Nodes[0].ChildNodes[0].ChildNodes[0].ChildNodes.Count > contador)
                        //        trvConsolidacion.Nodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[contador].ChildNodes.Add(new TreeNode(compo.Componente, compo.IdComponente.ToString()));

                        //}


                    }

                    trvConsolidacion.Nodes[0].ChildNodes[0].ChildNodes[contador].Expanded = false;
                    contador++;

                }
                #endregion

                gvResultados.DataSourceID = "ldsies";
                gvResultados.DataBind();
                ObtenerTema(gvResultados);
            }
        }

        protected void trvConsolidacion_SelectedNodeChanged(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(trvConsolidacion.SelectedNode.Value);
            string nombre = trvConsolidacion.SelectedNode.Text.ToString();
            CConsolidacion objConsolidacion = new CConsolidacion();
            string xmldata = objConsolidacion.ValidarNodos(id.ToString(), nombre, Convert.ToInt32(Session["idmedi"]));
            if (xmldata != null)
                objAmCharts.genera_chart_column_Flash("amcharts", xmldata, "Nivel General para Ambientes", Page, 900, 600);

        }

        protected void gvResultados_SelectedIndexChanged(object sender, EventArgs e)
        {
            EvaluationSettings.CEvaluacion objevaluacion = new EvaluationSettings.CEvaluacion();
            GridViewRow _objRow = gvResultados.SelectedRow;
            Label lblIdIe = (Label)_objRow.Cells[5].FindControl("IDIE");

            var mediciones = objevaluacion.MedicionesIE(Convert.ToInt32(lblIdIe.Text));

            gvMediciones.DataSource = mediciones;
            gvMediciones.DataBind();
            ObtenerTema(gvMediciones);
        }

        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            Filtro(txtFiltro.Text);
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

                var rFiltro = from i in db.Establecimiento_Educativo
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

        protected void gvMediciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            CConsolidacion objCConsolidacion = new CConsolidacion();
            GridViewRow objRow = gvMediciones.SelectedRow;

            int idMedicion = Convert.ToInt32(objRow.Cells[1].Text);
            Session.Add("idmedi", idMedicion);
            if (objCConsolidacion.Exist(idMedicion))
            {
                trvConsolidacion.Visible = true;
                string xmldata = null;

                xmldata = objCConsolidacion.ObtenerConslAmbiente(idMedicion);
                objAmCharts.genera_chart_column_Flash("amcharts", xmldata, "Nivel General para Ambientes", Page, 900, 600);

            }
            else
            {
                EvaluationSettings.CEvaluacion objCEvaluacion = new EvaluationSettings.CEvaluacion();
                objCEvaluacion.Consolidar(idMedicion);

                trvConsolidacion.Visible = true;
                string xmldata = null;

                xmldata = objCConsolidacion.ObtenerConslAmbiente(idMedicion);
                objAmCharts.genera_chart_column_Flash("amcharts", xmldata, "Nivel General para Ambientes", Page, 900, 600);

            }
        }

    }
}