using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;
using System.Text;
using System.Web.UI.HtmlControls;

namespace ESM
{


    public partial class ReportMarcoLogico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Request.QueryString["planoperativo"]))
            {
                rbtndetalle.Checked = true;

                gvDetalleActividades.Visible = true;
                gvresultados.Visible = false;
            }
            else
            {
                rbtnresumen.Checked = true;

                gvDetalleActividades.Visible = false;
                gvresultados.Visible = true;
            }

            CEfectos objCEfectos = new CEfectos();

            lblfinalidad.Text = objCEfectos.getEfectos(Convert.ToInt32(Request.QueryString["idproyecto"]));

            GridViewHelper helper = new GridViewHelper(this.gvDetalleActividades);
            helper.RegisterGroup("Resultado", true, true);
            //helper.RegisterSummary("Actividad", SummaryOperation.Sum, "Resultado");
            helper.GroupHeader += new GroupEvent(helper_GroupHeader);
            helper.ApplyGroupSort();
        }

        void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == "Resultado")
            {
                row.Attributes.CssStyle.Add("font-size", "1em");
                row.Attributes.CssStyle.Add("text-align", "center");
                row.Attributes.CssStyle.Add("color", "#005EA7");
                row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
            }
        }

        protected bool Export(HtmlTable table, GridView gvpro_Table, GridView gvresul_Table, GridView gvAct_Table)
        {
            try
            {

                StringBuilder objsb = new StringBuilder();
                System.IO.StringWriter sw = new System.IO.StringWriter(objsb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                System.Web.UI.Page pagina = new System.Web.UI.Page();
                var form = new HtmlForm();
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(table);
                form.Controls.Add(gvpro_Table);
                if (rbtndetalle.Checked)
                    form.Controls.Add(gvAct_Table);
                else if (rbtnresumen.Checked)
                    form.Controls.Add(gvresul_Table);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";

                if (rbtndetalle.Checked)
                    Response.AddHeader("Content-Disposition", "attachment;filename=Plan_Operativo.xls");
                else if (rbtnresumen.Checked)
                    Response.AddHeader("Content-Disposition", "attachment;filename=MarcoLogico.xls");

                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(objsb.ToString());
                Response.End();
                return true;
            }
            catch (Exception) { return false; }

        }

        protected void lknExport_Click(object sender, EventArgs e)
        {
            Export(tbFinalidad, gvproposito, gvresultados, gvDetalleActividades);
        }

        protected void rbtndetalle_CheckedChanged(object sender, EventArgs e)
        {
            gvDetalleActividades.Visible = true;
            gvresultados.Visible = false;
        }

        protected void rbtnresumen_CheckedChanged(object sender, EventArgs e)
        {
            gvDetalleActividades.Visible = false;
            gvresultados.Visible = true;
        }


    }
}