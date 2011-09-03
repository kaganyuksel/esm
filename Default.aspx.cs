using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Linq;

namespace ESM
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idusuario"] != null)
            {
                int idusuario = Convert.ToInt32(Session["idusuario"]);

                ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
                var us = (from u in db.Usuarios
                          where u.IdUsuario == idusuario
                          select new { u.Roles.Rol }).Single();

                if (us.Rol == "Administrador")
                {
                    AdministracionConfiguracion.Visible = true;
                    AdministracionUsuario.Visible = true;
                }
                else if (us.Rol == "Consultor")
                {
                    AdministracionConfiguracion.Visible = true;
                    AdministracionUsuario.Visible = true;
                }
            }

            if (Request.IsAuthenticated)
            {
                
                System.Collections.IList visibleTables = Global.DefaultModel.VisibleTables;
                if (visibleTables.Count == 0)
                {
                    throw new InvalidOperationException("No hay tablas accesibles. Asegúrese de que hay al menos un modelo de datos registrado en Global.asax y de que está habilitada la técnica scaffolding, o bien implemente páginas personalizadas.");
                }
                Menu1.DataSource = visibleTables;
                Menu1.DataBind();
                
            }
            else
                Response.Redirect("/Login.aspx");

            ObtenerTema(Menu1);

        }

        protected void ObtenerTema(System.Web.UI.WebControls.GridView objGridView)
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

    }
}
