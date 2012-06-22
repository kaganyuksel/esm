using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace ESM
{
    public partial class ReporteGeneralIndicadores : System.Web.UI.Page
    {

        protected int _proyecto_id;

        public int Proyecto_id
        {
            get { return _proyecto_id; }
            set { _proyecto_id = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["proyecto_id"] != null)
                {
                    _proyecto_id = Convert.ToInt32(Request.QueryString["proyecto_id"]);

                    var proyectoInformation = (from p in new ESM.Model.ESMBDDataContext().Proyectos
                                               where p.Id == _proyecto_id
                                               select p).Single();

                    ReportDataSource objReportDataSourceRegistroproyectos = new ReportDataSource();
                    ReportDataSource objReportDataSourceIndicadores = new ReportDataSource();
                    ReportDataSource objReportDataSourceMetas = new ReportDataSource();
                    rptIndicadores.LocalReport.DataSources.Clear();

                    rptIndicadores.LocalReport.ReportPath = "GeneralIndicadores.rdlc";

                    rptIndicadores.LocalReport.SetParameters(new ReportParameter("NombreProyecto", proyectoInformation.Proyecto1.ToUpper()));
                    rptIndicadores.LocalReport.SetParameters(new ReportParameter("Objetivo", proyectoInformation.Proposito.ToUpper()));
                    rptIndicadores.LocalReport.SetParameters(new ReportParameter("FechaProyecto", proyectoInformation.Fecha_Creacion.ToString()));
                    rptIndicadores.LocalReport.SetParameters(new ReportParameter("Fecha", DateTime.Now.ToString()));

                    objReportDataSourceRegistroproyectos.Name = "RegistroProyecto";
                    objReportDataSourceRegistroproyectos.DataSourceId = "ldsRegistroProyecto";

                    objReportDataSourceIndicadores.Name = "Indicadores";
                    objReportDataSourceIndicadores.DataSourceId = "ldsIndicadores";

                    objReportDataSourceMetas.Name = "MetasIntermedias";
                    objReportDataSourceMetas.DataSourceId = "ldsMetas";

                    rptIndicadores.ProcessingMode = ProcessingMode.Local;

                    rptIndicadores.LocalReport.DataSources.Add(objReportDataSourceRegistroproyectos);
                    rptIndicadores.LocalReport.DataSources.Add(objReportDataSourceIndicadores);
                    rptIndicadores.LocalReport.DataSources.Add(objReportDataSourceMetas);

                    rptIndicadores.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

                    rptIndicadores.LocalReport.Refresh();

                }
            }
        }

        void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSet1",ldsmetas));
        }
    }
}