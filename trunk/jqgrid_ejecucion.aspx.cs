using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class jqgrid_ejecucion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ban_proyecto_id.Value = Request.QueryString["proyecto_id"].ToString();
        }
    }
}