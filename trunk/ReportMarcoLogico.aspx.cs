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
            CEfectos objCEfectos = new CEfectos();

            lblfinalidad.Text = objCEfectos.getEfectos(Convert.ToInt32(Request.QueryString["idproyecto"]));
        }

        protected bool Export(HtmlTable table, GridView gvproposito, GridView gvresultados)
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
                form.Controls.Add(gvproposito);
                form.Controls.Add(gvresultados);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
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
            Export(tbFinalidad, gvproposito, gvresultados);
        }


    }
}