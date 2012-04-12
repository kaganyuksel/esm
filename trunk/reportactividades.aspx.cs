using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace ESM
{
    public partial class reportactividades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string actividadid = Request.QueryString["id"].ToString();

                ReportDataSource rds = new ReportDataSource();
                rptActividades.LocalReport.DataSources.Clear();

                rptActividades.LocalReport.ReportPath = "Actividades_Valores_PlanOperativo.rdlc";
                rds.Name = "DataSet1";

                var actividad = (from i in new Model.ESMBDDataContext().Actividades
                                 where i.Id == Convert.ToInt32(actividadid)
                                 select i).Single();

                rptActividades.LocalReport.SetParameters(new ReportParameter("indicador", actividad.Actividad));
                rptActividades.LocalReport.SetParameters(new ReportParameter("meta", actividad.Presupuesto.ToString()));

                odsActividadesMetas.FilterExpression = "actividad_id = " + actividadid;

                rds.DataSourceId = "odsActividadesMetas";

                rptActividades.LocalReport.DataSources.Add(rds);
                rptActividades.LocalReport.Refresh();
            }

        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            ReportDataSource rds = new ReportDataSource();
            rptActividades.LocalReport.DataSources.Clear();

            rptActividades.LocalReport.ReportPath = "Indicadores_PlanOperativo.rdlc";
            rds.Name = "DataSet1";
            rds.DataSourceId = "odsIndicadoresMetas_v2";

            rptActividades.LocalReport.SetParameters(new ReportParameter("indicador", "Al 2012/04/04 Extraer 123 GB wefsdfdf"));

            rptActividades.LocalReport.DataSources.Add(rds);
            rptActividades.LocalReport.Refresh();
        }

        protected void btnEjecutado_Click(object sender, EventArgs e)
        {
            ReportDataSource rds = new ReportDataSource();
            rptActividades.LocalReport.DataSources.Clear();

            rptActividades.LocalReport.ReportPath = "Indicadores_Valores_PlanOperativo.rdlc";
            rds.Name = "DataSet1";
            rds.DataSourceId = "odsIndicadoresMetas";

            rptActividades.LocalReport.DataSources.Add(rds);
            rptActividades.LocalReport.Refresh();
        }
    }
}