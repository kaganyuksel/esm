using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace ESM
{
    public partial class ReporteIndicadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                iframe_reporte.Attributes.Add("src", "reportindicadores.aspx?id=" + Request.QueryString["id"].ToString());
            }
        }

    }
}