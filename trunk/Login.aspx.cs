using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace ESM
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                StringBuilder objStringBuilder = new StringBuilder();

                objStringBuilder.Append("<div id='dialog-message' title='Error al Inicio Sesión'> \n ");
                objStringBuilder.Append("<p> \n ");
                objStringBuilder.Append("<span class='ui-icon ui-icon-circle-check' style='float: left; margin: 0 7px 50px 0;'> \n ");
                objStringBuilder.Append("</span>Lo sentimos puede que el nombre de usuario o la contraseña sean incorrectos, \n ");
                objStringBuilder.Append("verifique la información ingresada. \n ");
                objStringBuilder.Append("</p> \n ");
                objStringBuilder.Append("</div> \n ");

                formulario.InnerHtml = objStringBuilder.ToString();
            }

        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            IniciarSesion();
        }

        protected void IniciarSesion()
        {
            Session.Add("idusuario", SecuritySesion.AutenticacionUsuario(txtUsuario.Text, txtContrasena.Text));
            if (Session["idusuario"] != null)
                Response.Redirect("/BancoProyectos.aspx");
        }
    }
}