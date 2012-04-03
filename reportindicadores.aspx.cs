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
                ReportDataSource rds = new ReportDataSource();
                rptIndicadores.LocalReport.DataSources.Clear();

                rptIndicadores.LocalReport.ReportPath = "Indicadores_Valores_PlanOperativo.rdlc";
                rds.Name = "DataSet1";

                rptIndicadores.LocalReport.SetParameters(new ReportParameter("indicador", "Al 2012/04/11 Construir 989"));

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