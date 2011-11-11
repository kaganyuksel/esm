using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace ESM
{
    public partial class DiagramaGant : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["idResultado"] != null)
                {
                    int idresultado = Convert.ToInt32(Request.QueryString["idResultado"]);
                    JsGantt Gantt = new JsGantt();
                    Gantt.genera_gantt("GanttChartDIV", idresultado, Page);
                }
            }
        }
    }

    public class JsGantt
    {
        public string MsgError;

        private string GetRandomColor()
        {
            System.Threading.Thread.Sleep(50);
            Random random = new Random();
            string color = String.Format("{0:X6}", random.Next(100000));
            return color;
        }

        public bool genera_gantt(string DivId, int Resultado_id, Page pagina)
        {
            try
            {
                var cstype = pagina.GetType();
                ClientScriptManager cs = pagina.ClientScript;
                StringBuilder sb = new StringBuilder();
                int Band = 0;
                int i = 1;
                int j = 0;
                string ColorTemp;
                ESM.Model.ESMBDDataContext db = new ESM.Model.ESMBDDataContext();
                var results = from c in db.gantts
                              where c.Resultado_id == Resultado_id
                              orderby c.Resultado_id
                              select c;

                sb.AppendLine("var g = new JSGantt.GanttChart('g', document.getElementById('" + DivId + "'), 'day');");
                sb.AppendLine("g.setShowRes(0); // Show/Hide Responsible (0/1)");
                sb.AppendLine("g.setShowDur(0); // Show/Hide Duration (0/1);");
                sb.AppendLine("g.setShowComp(0); // Show/Hide % Complete(0/1)");
                sb.AppendLine("g.setShowStartDate(1); // Show/Hide Start Date(0/1)");
                sb.AppendLine("g.setShowEndDate(1); // Show/Hide End Date(0/1)");
                sb.AppendLine(@"g.setFormatArr( ""day"", ""week"", ""month"",""quarter""); // Set format options (up to 4 : ""minute"",""hour"",""day"",""week"",""month"",""quarter"")");
                sb.AppendLine("g.setDateInputFormat('dd/mm/yyyy'); // Set format of input dates ('mm/dd/yyyy', 'dd/mm/yyyy', 'yyyy-mm-dd')");
                sb.AppendLine("g.setDateDisplayFormat('dd/mm/yyyy'); // Set format to display dates ('mm/dd/yyyy', 'dd/mm/yyyy', 'yyyy-mm-dd')");
                sb.AppendLine("if (g) {");

                foreach (var x in results)
                {
                    if (Band != x.parent)
                    {
                        ColorTemp = GetRandomColor();
                        sb.AppendLine("g.AddTaskItem(new JSGantt.TaskItem(" + i + ", '" + x.actividad + "', '', '', '" + ColorTemp + "', '', 0, '', 100, 1, 0, 1,'','',0));");
                        j = i;
                        i = i + 1;
                    }
                    ColorTemp = GetRandomColor();
                    sb.AppendLine("g.AddTaskItem(new JSGantt.TaskItem(" + i + ", '" + x.Indicador + "', '" + x.fecha_inicial + "', '" + x.fecha_final + "', '" + ColorTemp + "', '', 0, '', 100, 0, " + j + ", 0,'',''," + x.Id + "));");
                    Band = x.parent;
                    i = i + 1;
                }
                //Parameters(pID, 'pName', 'pStart', 'pEnd', 'pColor', 'pLink', pMile ,'pRes', pComp, pGroup, pParent, pOpen, pDepend, pCaption, ID)
                sb.AppendLine("g.Draw();");
                sb.AppendLine("g.DrawDependencies();");
                sb.AppendLine(@" }else { alert(""no definida""); }");
                if ((cs.IsStartupScriptRegistered(DivId) == false) && results.Count() != 0)
                {
                    cs.RegisterStartupScript(cstype, DivId, sb.ToString(), true);
                }

                if (results.Count() != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string MsgError = ex.Message;
                return false;
            }
        }

    }
}

