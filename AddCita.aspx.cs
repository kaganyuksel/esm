using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class AddCita : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ObtenerTema(gvresultadosIE);
            ObtenerTema(gvresultadosSE);
        }

        protected void ObtenerTema(GridView objGridView)
        {
            if (objGridView.Rows.Count != 0)
            {
                objGridView.HeaderStyle.CssClass = "trheader";

                int color = 0;
                for (int i = 0; i < objGridView.Rows.Count; i++)
                {
                    if (color == 0)
                    {
                        objGridView.Rows[i].CssClass = "trgris";
                        color = 1;
                    }
                    else if (color == 1)
                    {
                        objGridView.Rows[i].CssClass = "trblanca";
                        color = 0;
                    }
                }
            }

        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gvresultadosIE.Visible = false;
            gvresultadosSE.Visible = true;
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gvresultadosIE.Visible = true;
            gvresultadosSE.Visible = false;
        }

        protected void gvresultadosSE_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ObtenerTema(gvresultadosSE);
        }

        protected void gvresultadosIE_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ObtenerTema(gvresultadosIE);
        }

        protected void gvresultadosIE_PageIndexChanged(object sender, EventArgs e)
        {
            ObtenerTema(gvresultadosIE);
        }
    }
}