using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class BancoProyectos : System.Web.UI.Page
    {

        #region Propiedades publicas y privadas

        protected Objetos.Cproyecto _objProyecto = new Objetos.Cproyecto();
        int proyecto_id = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ban_proyecto_id.Value != " ")
            {
                proyecto_id = Convert.ToInt32(ban_proyecto_id.Value);
                ban_proyecto_id.Value = ban_proyecto_id.Value;
            }

            if (!Page.IsPostBack)
            {
                CargarColeccionProyectos();
            }

        }

        protected void btnNuevoProyecto_Click(object sender, EventArgs e)
        {

        }

        protected void btnalmacenarproyecto_Click(object sender, EventArgs e)
        {
            AlmacenarProyetco();
        }

        protected void AlmacenarProyetco()
        {
            try
            {
                string nombre_proyecto = txtnombreproyecto.Value;
                string problema = txtproblema.Value;

                int proyecto_id = _objProyecto.Add(problema, nombre_proyecto);
                ban_proyecto_id.Value = proyecto_id.ToString();
            }
            catch (Exception)
            { Response.Write("alert('Opss... Ocurrio un error inesperado.');"); }
        }

        protected void ActualziarProyecto(int proyecto_id)
        {
            try
            {
                Model.Proyecto objProyecto_Obtenido = _objProyecto.GetProyecto(proyecto_id);
                if (objProyecto_Obtenido != null)
                {
                    txtnombreproyecto.Value = objProyecto_Obtenido.Proyecto1.ToString();
                    txtproblema.Value = objProyecto_Obtenido.Problema.ToString();
                    ban_proyecto_id.Value = objProyecto_Obtenido.Id.ToString();
                }
                else
                    Response.Write("alert('Opss... Ocurrio un error inesperado.');");
            }
            catch (Exception)
            {
                Response.Write("alert('Opss... Ocurrio un error inesperado.');");
            }
        }

        protected void CargarColeccionProyectos()
        {
            cmbproyectos.DataSource = _objProyecto.GetProyectos();
            cmbproyectos.DataTextField = "Proyecto1";
            cmbproyectos.DataValueField = "Id";
            cmbproyectos.DataBind();
        }

        protected void btncargarproyecto_Click(object sender, EventArgs e)
        {
            ActualziarProyecto(Convert.ToInt32(cmbproyectos.SelectedValue));
            Response.Write("$('#magazine').turn('next');");
        }

        protected void btncargar_proyecto_Click(object sender, EventArgs e)
        {

        }

        protected void btnalmacenarregistro_Click(object sender, EventArgs e)
        {
            Almacenar_Registro();
        }

        public void Almacenar_Registro()
        {
            try
            {
                Objetos.CRegistro_Proyectos _objCRegistro_Proyectos = new Objetos.CRegistro_Proyectos();

                _objCRegistro_Proyectos.AddItem(txtcargo.Value, txtdependencia.Value, Convert.ToDateTime(txtfechaelaboracion.Value), txtjustificacion.Value, txtmpp1.Value, txtmpp2.Value, txtmpp3.Value, proyecto_id);

            }
            catch (Exception) { }
        }

    }
}