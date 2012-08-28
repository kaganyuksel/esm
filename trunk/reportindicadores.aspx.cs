using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace ESM
{
    public partial class reportindicadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string indicadorid = Request.QueryString["id"].ToString();

                ReportDataSource rds = new ReportDataSource();
                rptIndicadores.LocalReport.DataSources.Clear();

                rptIndicadores.LocalReport.ReportPath = "Indicadores_Valores_PlanOperativo.rdlc";
                rds.Name = "DataSet1";

                var indicador = (from i in new Model.ESMBDDataContext().Indicadores
                                 where i.Id == Convert.ToInt32(indicadorid)
                                 select i).Single();

                rptIndicadores.LocalReport.SetParameters(new ReportParameter("proceso", indicador.Actividade.Subproceso.Causas_Efecto.Proceso));
                rptIndicadores.LocalReport.SetParameters(new ReportParameter("subproceso", indicador.Actividade.Subproceso.Subproceso1));
                rptIndicadores.LocalReport.SetParameters(new ReportParameter("actividad", indicador.Actividade.Actividad));
                rptIndicadores.LocalReport.SetParameters(new ReportParameter("indicador", indicador.Indicador));
                rptIndicadores.LocalReport.SetParameters(new ReportParameter("meta", indicador.meta.ToString()));

                rds.DataSourceId = "odsIndicadoresMetas";

                rptIndicadores.LocalReport.DataSources.Add(rds);
                rptIndicadores.LocalReport.Refresh();
            }

        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            ReportDataSource rds = new ReportDataSource();
            rptIndicadores.LocalReport.DataSources.Clear();

            rptIndicadores.LocalReport.ReportPath = "Indicadores_PlanOperativo.rdlc";
            rds.Name = "DataSet1";
            rds.DataSourceId = "odsIndicadoresMetas_v2";

            rptIndicadores.LocalReport.SetParameters(new ReportParameter("indicador", "Al 2012/04/04 Extraer 123 GB wefsdfdf"));

            rptIndicadores.LocalReport.DataSources.Add(rds);
            rptIndicadores.LocalReport.Refresh();
        }

        protected void btnEjecutado_Click(object sender, EventArgs e)
        {
            ReportDataSource rds = new ReportDataSource();
            rptIndicadores.LocalReport.DataSources.Clear();

            rptIndicadores.LocalReport.ReportPath = "Indicadores_Valores_PlanOperativo.rdlc";
            rds.Name = "DataSet1";
            rds.DataSourceId = "odsIndicadoresMetas";

            rptIndicadores.LocalReport.DataSources.Add(rds);
            rptIndicadores.LocalReport.Refresh();
        }
    }
}