using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class jqgrid_causas_efectos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ban_proyecto_id.Value = Request.QueryString["proyecto_id"].ToString();
            string modulo = Request.QueryString["mod"];

            if (modulo == "causas")
                mod_causas_efectos.Visible = true;
            else if (modulo == "objetivos")
                mod_objetivos_beneficios.Visible = true;
        }
    }
}