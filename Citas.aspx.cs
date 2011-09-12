using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class Citas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                if (Request.QueryString.Get("id") != null)
                {
                    try
                    {
                        idconsultor.Value = Request.QueryString.Get("id").ToString();
                    }
                    catch (Exception) { }
                }
                else
                {
                    idconsultor.Value = "0";
                }
            }
            else
                Response.Redirect("Login.aspx");
        }
    }
}