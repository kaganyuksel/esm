using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Linq;
using ESM.Objetos;

namespace ESM
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idusuario"] != null)
            {
                int idusuario = Convert.ToInt32(Session["idusuario"]);

                CRoles objCRoles = new CRoles();

                string rol = objCRoles.ObtenerRol(idusuario);

                if (rol == "Administrador")
                {
                    AdministracionConfiguracion.Visible = true;
                    AdministracionUsuario.Visible = true;
                    MEN.Visible = true;
                    ModEval.Visible = true;
                }
                else if (rol == "Consultor" )
                {
                    var idc = objCRoles.IdConsultor;

                    Session.Add("identiConsultor", idc);
                    citas.HRef = String.Concat("/Citas.aspx?id=", idc);
                    AdministracionConfiguracion.Visible = true;
                    AdministracionUsuario.Visible = true;
                    AdministracionConfiguracion.Visible = false;
                    AdministracionUsuario.Visible = false;
                    MEN.Visible = false;
                    ModEval.Visible = true;
                }
                else if (rol == "MEN")
                {
                    ModEval.Visible = false;
                    AdministracionConfiguracion.Visible = false;
                    AdministracionUsuario.Visible = false;
                    AdministracionConfiguracion.Visible = false;
                    AdministracionUsuario.Visible = false;
                    MEN.Visible = true;
                    ModEval.Visible = true;
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
